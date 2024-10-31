using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Item : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    private InventoryItem item = new InventoryItem();

    [SerializeField] private InventoryItem.ItemType type = InventoryItem.ItemType.None;
    [SerializeField] private Player.flags flag;
    [SerializeField] private GameObject self;
    private bool on = false; // Variable to check if highlight trigger is on


    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();

        if (playerScript.dialogueFlags.Contains(flag))
        {
            Destroy(self);
        }

        item.itemType = type;

        // Initialize LineRenderer and PolygonCollider for boundary highlighting
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Configure LineRenderer
        lineRenderer.positionCount = polygonCollider.points.Length;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.z - transform.position.z, 2));
        float height = Mathf.Abs(player.transform.position.y - transform.position.y);
        // I changed the datatype so that it can allign better with Math and Vector3

        // Highlight around it, even if you are not pressing it
        if (dist <= 25 && height <= 20) 
        {
            if (Input.GetButtonDown("E"))
            {
                on = true;
            }
            highlight(on);
        }

            playerScript.dialogueFlags.Add(flag);
            playerScript.AddToInventory(item);
            Destroy(self);
    }

    void highlight(bool on)
    {
        if (on)
        {
            // Draw boundary outline
            Vector3[] points = new Vector3[polygonCollider.points.Length];
            for (int i = 0; i < polygonCollider.points.Length; i++)
            {
                Vector2 localPoint = polygonCollider.points[i];
                points[i] = transform.TransformPoint(localPoint);
            }
            lineRenderer.SetPositions(points);
            lineRenderer.enabled = true;
        }
        else
        {
            // Disable the boundary outline when not highlighted
            lineRenderer.enabled = false;
        }
    }

}
