
using System;
using System.Collections.Generic;


public enum Flag
{
    // ReSharper disable InconsistentNaming
    defaultFlag, //flag put on all dialogue
    monologueFlag1,
    monologueFlag2,
    weirdNoise,
    benTalk,
    edScare,
    janHelp,
    janCard,
    alexLine,
    dylanTarot,
    earlCandy,
    nateHelp,
    oldMan,
    cletCard,
    cletGhost,
    firstTony,
    denTony,
    tonyNote,
    beanKrak,
    benPrank,
    benNah,
    stephTalk,
    drewTalk,
    helpDrew,
    weedMan,
    weedwoMan,
    noteVan,
    vannessaFine,
    denGhost,
    denReceipt,
    denFin,
    COACH,
    itemBatteries,
    itemMedKey, // in game
    itemSleepPill, // in game
    itemFoolCard,
    itemBlankBit, // in game
    itemJellyBeans,
    itemReceiptOne,
    itemReceiptTwo,
    itemCandy,
    itemTNote,
    dylanSoWhat,
    cletNotSaying,
    None,
    
};

public enum Name
{
    InnerMonologue = -1,
    Timothy,
    Ed,
    Janet,
    Alex,
    Dylan,
    Earl,
    Nate,
    Charles,
    Gen,
    Mallory,
    Cletus,
    Dawn,
    Tony,
    Dennis,
    Benny,
    Steph,
    Drew,
    Kyle,
    Kylie,
    Vanessa,
    Coach,
    April,
    Kai,
    Kraken,
    Death
};

public struct Convo
{
    public string[] Dialogue;
    public Flag[] RequiredFlags;
    public Flag[] GrantedFlags;
    public InventoryItem.ItemType[] GrantedItems;

    
    public Convo(string[] strings, Flag[] requiredFlags, Flag[] grantedFlags, InventoryItem.ItemType[] grantedItems)
    {
        Dialogue = strings;
        RequiredFlags = requiredFlags;
        GrantedFlags = grantedFlags;
        GrantedItems = grantedItems;
    }
    
    public Convo(string[] strings, Flag[] requiredFlags, Flag[] grantedFlags)
    {
        Dialogue = strings;
        RequiredFlags = requiredFlags;
        GrantedFlags = grantedFlags;
        GrantedItems = new[] {InventoryItem.ItemType.None};
    }
        
    public Convo(string[] strings, Flag[] requiredFlags, InventoryItem.ItemType[] grantedItems)
    {
        Dialogue = strings;
        RequiredFlags = requiredFlags[..^1]; //all items but the last one (^1)
        GrantedFlags = new []{requiredFlags[^1]}; //just the last one
        GrantedItems = grantedItems;
    }
    
    public Convo(string[] strings, Flag[] requiredFlags)
    {
        Dialogue = strings;
        RequiredFlags = requiredFlags[..^1]; //all items but the last one (^1)
        GrantedFlags = new []{requiredFlags[^1]}; //just the last one
        GrantedItems = new[] {InventoryItem.ItemType.None};
    }
}

// Represents a list of dialogue and the required Flags to trigger it
[Serializable]
public struct ConvoMetadata
{
    public Name npcName;
    public int dialogueIndex;
    public ConvoMetadata(Name npcName, int dialogueIndex)
    {
        this.npcName = npcName;
        this.dialogueIndex = dialogueIndex;
    }
}

public class DialogueData
{
    
public static readonly SortedDictionary<Name, Convo[]> AllDialogues = new ()
    {
        {Name.InnerMonologue, new Convo[] {
            new (
                new []{ "test dialogue", "test", "test2"},
                new []{ Flag.monologueFlag1}),
            new (
                new []{  "yo baby. I have 7 cds", "And three jam boxes"},
                new []{ Flag.monologueFlag2}),
        }},
        {Name.Timothy, new Convo[] { 
            new (
                new []{ "Ed...", "what was that noise?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{  "I always hear weird noises coming from these woods, but Ed takes me here to get over my fears.",
                    "I'm not exactly a fan of the dark either.","Ghosts though?","I'm chill with them",
                    "I wouldn't be surprised if benny had something to do with it.","He's always causing trouble." },
                new []{ Flag.defaultFlag, Flag.weirdNoise}),
            new (
                new []{  "Oh, Benny's in the clear?", "That's a relief,",
                    "but I hope he's done pulling pranks on the rest of the camp,",
                    "they always keep me on edge."},
                new []{ Flag.defaultFlag, Flag.weirdNoise, Flag.benNah, Flag.benTalk}),
        }},
        {Name.Ed, new Convo[] {
            new (
                new []{ "Come on, Tim, it's just a forest.","Chill out."},
                new []{ Flag.defaultFlag }),
            new (
                new []{"AHHHHHHHHHHHHHHHHHHHHHHHHH!!!!!??!!!?", "I mean....",
                    "whatever......","At least you don't have to feel anything anymore.",
                    "So...you're trying to find out who killed you?",
                    "I feel like it's whoever you'd least expect, people surprise you like that."},
                new []{ Flag.defaultFlag, Flag.edScare}),
        }},
        {Name.Janet, new Convo[] {
            new (
                new []{ "Hope the tarot card helps you out!"},
                new []{ Flag.defaultFlag }),
            new (
                new []{  "Oh my gosh! Are you ok?","Why are you a ghost?"},
                new []{ Flag.defaultFlag, Flag.janHelp}),
            new (
                new []{  "Hey!","I didn't have anything to do with that!",
                    "I was getting a tarot reading... ",
                    "Though I did hear some commotion when we flipped the first card, but I really wanted to find out why I got the three of swords.",
                    "The last card seemed really important though..."},
                new []{ Flag.defaultFlag, Flag.janHelp, Flag.janCard},
                new [] { InventoryItem.ItemType.foolCard }),
        }},
        {Name.Alex, new Convo[] {
            new (
                new []{ "Next!"},
                new []{ Flag.defaultFlag}),
            new (
                new []{ "Sorry, Dylan was in line before you","...Oh, it's you!",
                    "I see you're a ghost now, well, I think it's pretty obvious I'm not the one responsible for that.",
                    "I've been doing tarot readings all night."},
                new []{ Flag.defaultFlag,Flag.alexLine}),
        }},
        {Name.Dylan, new Convo[] {
            new (
                new []{ "I don't know you like that"},
                new []{ Flag.defaultFlag}),
            new (
                new []{  "So what I'm getting a tarot reading? Alex got me into it ok?",
                    "Oh...that's not what it's about?", "...", "Why would i be the one to kill you?"},
                new []{ Flag.defaultFlag, Flag.dylanTarot}),
            new (
                new []{  "So what I bought a bag of jelly beans? Is that a crime?"},
                new []{ Flag.defaultFlag, Flag.dylanTarot, Flag.itemReceiptOne, Flag.dylanSoWhat}),
        }},
         {Name.Earl, new Convo[] {
            new (
                new []{ "I don't talk to strangers"},
                new []{ Flag.defaultFlag}),
            new (
                new []{  "Go to the lake"},
                new []{ Flag.defaultFlag, Flag.earlCandy}),
            new (
                new []{  "Caramel, my favorite!","...so, you want to know about the kraken?",
                    "Well, those camp stories you heard? They're true.",
                    "Call me crazy all you want, but you'll know the truth if you use these.",
                    "Hold these near the lake and you'll find the answers you've been looking for."},
                new []{ Flag.defaultFlag, Flag.itemCandy, Flag.earlCandy},
                new []{InventoryItem.ItemType.jellyBeans}),
        }},
         {Name.Nate, new Convo[] {
            new (
                new []{ "I do talk to strangers"},
                new []{ Flag.defaultFlag}),
            new (
                new []{  "So you wanna talk to Old Man Earl, but he's not letting up.",
                    "Well... as your chill camp counselor, I'll help you out. Give him this."},
                new []{ Flag.defaultFlag},
                new []{Flag.nateHelp}
               ),
            new (
                new []{  "He's wise, right?"},
                new []{ Flag.defaultFlag, Flag.nateHelp},
                new []{Flag.oldMan}),
        }},
         {Name.Charles, new Convo[] {
            new (
                new []{ "I can't wait to get back to my private chef.",
                    " Um will pay for subjecting me to these meals"},
                new []{ Flag.defaultFlag}),
        }},
         {Name.Gen, new Convo[] {
            new (
                new []{ "Charles is the only company worth having at camp.",
                    "He understands how one must properly behave themselves."},
                new []{ Flag.defaultFlag}),
        }},
        {Name.Mallory, new Convo[] {
            new (
                new []{ "Just cause you're a ghost doesn't mean I'll feel bad for you."},
                new []{ Flag.defaultFlag}),
        }},
        {Name.Cletus, new Convo[] {
            new (
                new []{ "Bug off."},
                new []{ Flag.defaultFlag}),
            new (
                new []{ "Why are you a ghost? Is it some costume? You're not impressing anyone",
                    "...actually never mind. It's a killer costume.","Oh, you ARE a ghost?????","SHIT"},
                new []{ Flag.defaultFlag,Flag.cletGhost}),
            new (
                new []{  "I told you I'm not saying anything else."},
                new []{Flag.defaultFlag, Flag.itemFoolCard, Flag.cletCard}),
            new (
                new []{"Reckless??? Me????",
                    "...yeah, okay, maybe, but not about this.",
                    "Why would I want to kill you?",
                    "Sure, I know some people that have a problem with you, but me? No.",
                    "...oh shit did I say too much?",
                    "I just know some people have said some things about you getting in the way",
                    "I'm not saying anything else."},
                new []{Flag.defaultFlag, Flag.itemFoolCard, Flag.cletCard, Flag.cletNotSaying}),
        }},
        {Name.Dawn, new Convo[] {
            new (
                new []{ "Some of the best bugs come out at night."},
                new []{ Flag.defaultFlag}),
        }},
        {Name.Tony, new Convo[] { 
            new (
                new []{"You want something from me?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"You want something from me?",
                    "I've got nothing to say about that ghost situation of yours.",
                    "Come over to me when you actually have something to say." },
                new []{ Flag.defaultFlag, Flag.firstTony}),
            new (
                new []{"You heard correctly from Dennis, I won't be here next summer.",
                    "My family decided it was time for me to focus on other matters.",
                    "That's a relief,", "but I hope he's done pulling pranks on the rest of the camp,",
                    "they always keep me on edge."},
                new []{ Flag.defaultFlag, Flag.firstTony, Flag.denGhost, Flag.denTony}),
            new (
                new []{"I didn't write that note. You shouldn't believe everything you see."},
                new []{ Flag.defaultFlag, Flag.itemTNote},
                new []{Flag.tonyNote}),
        }},
        {Name.Benny, new Convo[] { 
            new (
                new []{ "wonder what prank I'm gonna pull next..."},
                new []{ Flag.defaultFlag }),
            new (
                new []{  "sick prank bro!", "the ghost getup? it's impressive...almost too impressive", 
                    "...","oh, you're dead now? At least I got to play one last prank on you." },
                new []{ Flag.defaultFlag, Flag.benPrank}),
            new (
                new []{  "That last prank I was talking about was not killing you! That's not a prank, that's just messed up.",
                    "I just tied your shoelaces together, but I guess you don't have shoes anymore.",
                    "Lame prank, I know...but it's the end of the summer, I've been running out of ideas.",
                    "I guess I'll help you figure out who actually did this though.",
                    "I saw some weird stuff when I checked out the med cabinet earlier. Or I guess a lack of stuff."
                    ,"Figured it'd be prime prank reality, with the nurse gone a day early and all.",
                    "Guess someone else had the same idea.",
                    "I hid the key near the camp counselor cabin to cause some trouble for next year.",
                    "Why don't you go check it out for yourself?"},
                new []{ Flag.defaultFlag, Flag.benPrank , Flag.weirdNoise, Flag.benNah}),
        }},
        {Name.Steph, new Convo[] { 
            new (
                new []{ "shoo shoo, you're embarrassing me!"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"Hey roomie! Why are you all see-through now?", "...","oh ... you're dead?? How tragic!",
                    "Hey!!! Just cause I wasn't in our cabin doesn't mean I had anything to do with this.",
                    " Every other night I've snuck out, and now you notice?",
                    "What have I been doing? Just getting an extra look at Kyle. Isn't he dreamy???",
                    "He pretends not to notice me, but I know it takes everything in him to look away!",
                    " Good thing they don't have curtains here. Normally he's asleep by now, I wonder why he's awake tonight...",
                    "Anyways, get out of here! I have a cute boy to stare at." },
                new []{ Flag.defaultFlag, Flag.stephTalk}),
        }},
         {Name.Drew, new Convo[] { 
            new (
                new []{ "Wait you're back... did you find any batteries?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{ "...help me...",
                    "I haven't been able to leave all night cause she's just there. Watching.",
                    "Normally I just listen to music to drown out the creepiness, but my Walkman is out of batteries.",
                    "Guess I'm gonna be up all night...",
                    "If you happen to find any batteries, please bring them to me. It'd mean a lot." },
                new []{ Flag.defaultFlag},
                new []{Flag.drewTalk}
                ),
            new (
                new []{  "Thanks","...Now that I think about it, I know where you might find answers about your death.",
                    "One of the other counselors, Nate, visits this old cabin near camp.",
                    "He says that \"Old Man Earl knows all.\"",
                    "His cabin is hidden behind some tree to the left of the boat house."},
                new []{ Flag.defaultFlag, Flag.drewTalk, Flag.itemBatteries, Flag.helpDrew},
                new []{ Flag.None}),
                new (
                    new []{ "Thanks again"},
                    new []{ Flag.defaultFlag, Flag.itemBatteries},
                    new []{ Flag.None}),
        }},
        {Name.Kyle, new Convo[] { 
            new (
                new []{ "Come back to smoke?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"SUPPPPP!! I see you're transparent now, that's new.",
                    "Is that what was happening over in the lake?",
                    "That was crazy. That giant monster thing just ate the hell out of you.",
                    "Then the fairies came to take you away, but they weren't really fairies. They were killer sharks.",
                    " One of them was actually a dolphin disguised as a shark. Or two of them, no clue, man.",
                    "Either way, wanna smoke?","...","No? Your loss, man." },
                new []{ Flag.defaultFlag, Flag.weedMan}),
        }},
        {Name.Kylie, new Convo[] { 
            new (
                new []{ "See ya!"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"I just got here a few minutes ago.",
                    " Have you heard Kyle's story? He's so out of it.",
                    "Maybe he should be a writer, I'd act in something of his.",
                    "Oh, you're wondering if I killed you??","It wasn't me, I was in bed till a few minutes ago.",
                    "Couldn't sleep so I came here. Kyle is nice company." },
                new []{ Flag.defaultFlag, Flag.weedwoMan}),
        }},
        {Name.Vanessa, new Convo[] { 
            new (
                new []{ "Can't believe you died"},
                new []{ Flag.defaultFlag }),
            new (
                new []{  "Where did that note come from? I genuinely have no idea how you got this.",
                    "I don't know what else you want me to say, I haven't seen that before." },
                new []{ Flag.defaultFlag, Flag.itemTNote}),
            new (
                new []{ "Fine! It was me, I didn't mean to kill you, it was just supposed to be a fun prank.",
                    " We wanted to initiate you into our cabin. Mallory, Cletus, and I thought you'd be a good fit.",
                    "As much as we might not show it, we like you.",
                    "To carry your mattress, we needed an extra hand, so we had Dennis tag along.",
                    "He's been saying yes to everything we've asked lately.",
                    "After we put you in the water, he said he'd make sure you got out okay.",
                    "A kraken? What, are you crazy??? The kraken is just a myth.","Please forgive us!"},
                new []{ Flag.defaultFlag, Flag.itemTNote, Flag.itemReceiptTwo, Flag.vannessaFine}),
        }},
        {Name.Dennis, new Convo[] { 
            new (
                new []{ "OHMYGODOHMYGODOHMYGODOHMYGODOHMYGODOHMYGODOHMYGODOHMYGOD"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"OHMYGODYOUREAGHOST!","WHAT DO YOU WANT?","You want to get inside the cabin? Sorry I'm on guard duty.",
                    "Tony isn't coming back next summer so there's an extra spot in the Evergreen cabin",
                    ". It's perfect, in the center of it all, with the most important people in camp;",
                    "Vanessa, Cletus, Mallory, and hopefully, me.",
                    "Vanessa said that I might have a chance for the spot if I watch the door for them at night.",
                    "Normally I shouldn't be letting people in, but I'll let you be an exception." },
                new []{ Flag.defaultFlag, Flag.denGhost}),
            new (
                new []{"IT WASN'T ME, VANESSA MADE ME DO IT!", "She wanted to be able to point the finger at someone else!",
                    "She knew about the kraken, and that putting your mattress in the lake would kill you.",
                    "She was worried you were becoming too popular for her to compete!"},
                new []{ Flag.defaultFlag, Flag.denGhost, Flag.itemReceiptTwo, Flag.denReceipt}),
            new (
                new []{"Fine. You caught me. I knew about it all, the kraken, the jellybeans, everything.",
                    "They wanted you for the last Evergreen cabin slot and I wanted you out of the way.",
                    "They've always considered me insignificant, but now I'll finally get the recognition I deserve."},
                new []{ Flag.defaultFlag,Flag.denGhost,Flag.denReceipt,Flag.denFin }),
        }},
        {Name.Coach, new Convo[] { 
            new (
                new []{ "Drop and give me 20!","...just kidding.","...unless?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{"Here for a late night workout?","You are looking a little shapeless...",
                    "I heard some commotion over by the lake, but had to get the rest of my reps in.",
                    "Lookin this good is a full time job." },
                new []{ Flag.defaultFlag, Flag.COACH}),
        }},
        {Name.April, new Convo[] { 
            new (
                new []{ "Hm...maybe I should add some more blue over there..."},
                new []{ Flag.defaultFlag }),
        }},
        {Name.Kai, new Convo[] { 
            new (
                new []{ "Where the heck is Stephanie? And why is your bed gone?",
                    "AND WHY ARE YOU A GHOST NOW???","God I'm a heavy sleeper."},
                new []{ Flag.defaultFlag }),
        }},
        {Name.Kraken, new Convo[] { 
            new (
                new []{ "DO YOU HAVE ANY MORE JELLY BEANS?"},
                new []{ Flag.defaultFlag }),
            new (
                new []{ "AHDHOSIDHGVUOHDFSOVGHWPDHPVAQDGV","Ahem...","Are those jelly beans?",
                    "You want answers? Hand ‘em over and I'll talk","So...I ate you, but it wasn't my fault.",
                    " I couldn't help it.",
                    "I was trying to help you at first, but that red blanket of yours made you look an awful lot like a jelly bean.",
                    "Lemme tell you, you don't taste like one.",
                    "Some campers pushed you in on a mattress.",
                    " And I have a feeling they knew about my jelly bean addiction, cause there were some on the bed to lure me out.",
                    "You should talk with JANET.",
                    "She's in charge of keeping track of the camp shop purchases, that way you can find out who bought the jelly beans."},
                new []{ Flag.defaultFlag,Flag.beanKrak, }),
        }},
        

    };
}
