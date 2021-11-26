using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPI_InteractiveFiction_1stPlayable
{
    /* Project - C#: Interactive Fiction - First Playable:
     
     * Develop a first playable build of an interactive fiction game. *

        + Technical Requirements:
            - string[] story -- stores an interactive story:
                Includes title and pages.
                Each page contains:
                    plot text
                    two story options
                    two destination page numbers
            - Pages that end the story will have no story options and no destination page numbers.
            - lets user play through the story.
            - a mini game engine
                shows story.
                reads user input.
                changes page.
                ends games.
            - Encapsulation (make use of methods when appropriate).
            - Comment your code.
            - Follow coding naming conventions.
            - Decouple design from code (do not store design of page output within the story array).
            - create your own story:
                title
                unique
                creative
                10 pages or longer
                multiple endings

        + Extra Mile Suggestions:
            - color
            - audio
            - click sound
            - music
            - proper text wrapping
            - no wall-of-text
            - slow text output
            - click to speed up
            - unique, creative, interesting story
            - story that challenges limited technical specifications
            - ascii art
            - splash screen
            - inventory
            - file access (required in FINAL project)
            - read story from file (required in FINAL project)
            - save game (required in FINAL project)
            - main menu (required in FINAL project)
     */

    // resorces: https://stackoverflow.com/questions/8946808/can-console-clear-be-used-to-only-clear-a-line-instead-of-whole-console

    class Program
    {
        static void Main()
        {
            Story.printStory();

            Console.ReadKey(true);

        }
    }

    public class PageNode
    {
        public string plotText;
        public string choiceTextA;
        public string choiceTextB;

        public PageNode choiceA;
        public PageNode choiceB;
    }

    public class Story
    {
        //using this old story as a placeholder
        public static string[] story = new string[]
        {
                //Title Page | page:0
                "----------------------------------.\n"+
                "An interesting Dinner \n" +
                "----------------------------------.\n"+ 
                ";" +
                ";" +
                ";1" + //goes to page 1
                ";",
                //-----------------------------------------------------------------------------------------------
                //1st | page:1
                " *Your trying to decide to eat Stake or Sushi tonight, both seem appealing but you just cant decide!" +
                ";I'll go with the stake, its not expensive and its good." +
                ";Sushi time! I've been craving it for a while and I deserve to treat myself every once in a while." +
                ";2" +
                ";3", 
                //A: stake | page:2
                "Eh I Think I'll go with the Stake, basic and good. Though Should I eat out? or buy what I need and make it myself?" +
                ";I'll eat out. I was thinking about trying stake from this new place anyway." +
                ";I think I'll make the Stake, kinda wanted to try out this new recipe for a while." +
                ";4" +
                ";5", 
                //-----------------------------------------------------------------------------------------------
                //B: sushi | page:3
                "I'll choose Sushi! I'm feeling experimental today. There's a nearby sushi place that's pretty close, or should I ask an old friend I know and try out his Sushi?" +
                ";I'll go to that Sushi place, The sweet old lady there always likes to give me extra mints before I leave." +
                ";Eh. I'll give an old freind a visit. Maybe catch up and finally try his Sushi recipe." +
                ";6" +
                ";7", 
                //-----------------------------------------------------------------------------------------------
                // stake / A: out | page:4
                "I'm gonna eat out, too lazy to cook it myself. I was thinking about hanging out with the boys, haven't talked to them much lately. But I'm not sure." +
                ";Eh, they can be a bit noisy, I might just head to the stake house on my own." +
                ";Aaah Ill invite em all, we could have a Stake party! " +
                ";8" +
                ";9", 
                // stake / B: cook | page:5
                "It wouldn't hurt to try my hand at cooking! I just gotta get the ingredients. should I eat alone, or invite my friends over?" +
                ";Nah, I think I'm gonna eat alone, kinda feel like having time to myself." +
                ";yeah I dont mind having some company, and crack open a cold one with the boys after lunch." +
                ";10" +
                ";11",
                //-----------------------------------------------------------------------------------------------
                // sushi / A: out | page:6
                "The Sushi place sounds like a good idea, been a while since I last been there. Now that I think about it, I could go alone, or I could invite a few friends." +
                ";Nah I'll go alone, saves me some money anyway." +
                ";Yeah I'll hang out with the boys, See what they've been up to lately." +
                ";12" +
                ";13",
                // sushi / B: oldfriend | page:7
                "I Think I'll visit my old friend, see if he's had any luck getting into Psychology School or whatever he calls it. We could catch up on old times while he makes Sushi, or I could invite some friends to finally meet him." +
                ";I dont want to bother him with a big crowed, It'll just be me and him, with some delicious sushi" +
                ";Eh, ill bring a few friends along, I'm sure he wont mind." +
                ";14" +
                ";15",
                //-----------------------------------------------------------------------------------------------
                // stake / out / A: alone | page:8
                " *You decided to head to the Stake House alone." +
                ";" +
                ";" +
                ";" +
                ";",
                // stake / out / B: friends | page:9
                " *You decided to head to the Stake House with da bois." +
                ";" +
                ";" +
                ";" +
                ";",
                // stake / cook / A: alone | page:10
                " *You decided to cook stake and have lunch alone." +
                ";" +
                ";" +
                ";" +
                ";",
                // stake / cook / B: friends | page:11
                " *You decided to cook stake and party with da bois." +
                ";" +
                ";" +
                ";" +
                ";",
                //-----------------------------------------------------------------------------------------------
                // sushi / out / A: alone | page:12
                " *You decided to head to the Sushi restaurant alone." +
                ";" +
                ";" +
                ";" +
                ";",
                // sushi / out / B: buddies | page:13
                " *You decided to head to the Sushi restaurant with da bois." +
                ";" +
                ";" +
                ";" +
                ";",
                // sushi / oldfriend / A: alone | page:14
                " *You decided to visit your old friend for Sushi alone." +
                ";" +
                ";" +
                ";" +
                ";",
                // sushi / oldfriend / B: buddies | page:15
                " *You decided to visit your old friend for Sushi with da bois." +
                ";" +
                ";" +
                ";" +
                ";",
        };

        static Dictionary<int, PageNode> pages = new Dictionary<int, PageNode>(); //if I want to have mutiple stories in the same project, then nothing should be static.

        public static PageNode ParseStory(string[] storyData)
        {
            //gets the plot text and choices
            for (int i = 0; i < storyData.Length; i++)
            {
                string pageSource = storyData[i];
                PageNode pageNode = new PageNode();
                string[] storySplit = pageSource.Split(';');

                pageNode.plotText = storySplit[0];
                pageNode.choiceTextA = storySplit[1];
                pageNode.choiceTextB = storySplit[2];

                pages.Add(i, pageNode);
            }

            //gets the choice numbers / destination pages
            for (int j = 0; j < storyData.Length; j++)
            {
                string pageSource = storyData[j];
                string[] storySplit = pageSource.Split(';');
                PageNode pageNode = pages[j];

                //these ignore if there isnt any numbers in the string[] to parse
                try {int A = int.Parse(storySplit[3]);
                     pageNode.choiceA = pages[A];}
                catch (FormatException) {pageNode.choiceA = null;}

                try{int B = int.Parse(storySplit[4]);
                    pageNode.choiceB = pages[B];}
                catch (FormatException) {pageNode.choiceB = null;}
            }

            return pages[0];
        }

        public static void printStory() 
        {
            ParseStory(story);

            ConsoleKey keyPress;

            foreach (var page in pages)
            {
                Console.WriteLine(page.Value.plotText);
                Console.WriteLine();

                //this ignores choice in page 0 (title) entirely. 
                if (page.Key == 0) {continue;}

                if (page.Value.choiceA != null)
                    Console.WriteLine("A:" + page.Value.choiceTextA);
                if (page.Value.choiceB != null)
                    Console.WriteLine("B:" + page.Value.choiceTextB);

                Console.WriteLine("-------------------------------------------------------");

                keyPress = Console.ReadKey(true).Key;

                Console.WriteLine("> Awaiting Choice...");

                //for this to work, id need to make a copy of the dictionary? because the way iom doing it keeps tryng to modify the key whenever i wanna turn the page. 

                int pageA = page.key.choiceA; //reeee i wanna make a new one aaaaaaaa

                if (keyPress == ConsoleKey.A)
                {
                    page.key.choiceA
                }

                if (keyPress == ConsoleKey.B)
                {
                    page.key.choiceB
                }

                //then await input
            }
        }

        //public static ConsoleKey input()
        //{

        //}
    }

    /* i wanna die
    using System.Text.RegularExpressions; //Regex https://regex101.com/ should I use this??
    public class StoryB
    {
        static string[,] nostory = new string[,]
            {
                //The "Idk what im doing" Diagram:

                //story (class)
                // > room (method)
                //  > page (array)
                //   > paragraph (variable)

                // wait I think this is wrong ^^^ REEEEEEEEEEEEEEE

                {   //page 0 - this techically isnt a page
                    "This is a title screen at page 0",
                    ";",
                    ";",
                    ";",
                    ";"
                },
                {   //page 1
                    "Your mom is ded", //paragraph 0
                    ";oh no", //paragraph 1
                    ";eh whatever", //paragraph 2
                    ";2", //paragraph 3
                    ";3" //paragraph 4
                },
                {   //page 2
                    "thats sad",
                    ";",
                    ";",
                    ";",
                    ";"
                },
                {   //page 3
                    "lol XD",
                    ";",
                    ";",
                    ";",
                    ";"
                },
            };

        public static void anothertest(int pageNum, int paragraphNum) // y, x
        {
            List<string> sortedStory = new List<string>(nostory[pageNum, paragraphNum].Split(';')); //wait... this works?!

            foreach (string paragraph in sortedStory)
            {
                Console.WriteLine(paragraph);
            }
            Console.ReadKey(true);
        }

        public static void test()
        {
            //STORY ARRAY

            string[,] teststory = new string[,]
            {
                {   //page 0
                    "insert title here", //paragraph 0
                    ";", //paragraph 1
                    ";", //paragraph 2
                    ";", //paragraph 3
                    ";" //paragraph 4
                },
                {   //page 1
                    "Plot",
                    ";ChoiceA",
                    ";ChoiceB",
                    ";2",
                    ";3"
                },
                {   //page 2
                    "Plot",
                    ";ChoiceA",
                    ";ChoiceB",
                    ";4",
                    ";5"
                },
                {   //page 3
                    "Plot",
                    ";ChoiceA",
                    ";ChoiceB",
                    ";6",
                    ";7"
                },
                {   //page 4
                    "end4",
                    ";",
                    ";",
                    ";",
                    ";"
                },
                {   //page 5
                    "end5",
                    ";",
                    ";",
                    ";",
                    ";"
                },
                {   //page 6
                    "end6",
                    ";",
                    ";",
                    ";",
                    ";"
                },
                {   //page 7
                    "end7",
                    ";",
                    ";",
                    ";",
                    ";"
                },
            };


            Console.WriteLine(teststory[6, 0]);
            Console.ReadKey(true);

            //PRINTING STORY  


            while (true) //in a loop (not this one in particular but you get the point
            {
                //since its a multidimentional array cant I just tell it to point to a dimention in the array to get a page?
                //like page 0 is dimention 0 in array

                //string parcing

                int page = 0; //the value will represent the pointer on the Y axis??? (fuck if I know what im talking about)
                string[] TextSplit = teststory[page, 0].Split(';'); //dont use the 0 here

                //now there is split text 0-4

                //string Paragraph = TextSplit[0];
                //string ChoiceA = TextSplit[1];
                //string ChoiceB = TextSplit[2];
                //string PageA = int.Parse(TextSplit[3]);
                //string PageB = int.Parse(TextSplit[4]);


                //PLAYER INPUT (does it have to be in a loop??)

                //getting key pressed vs getting key press info. ConsoleKey vs ConsoleKeyInfo
                ConsoleKey Keypressed = Console.ReadKey(true).Key;

            }


        }
    }

    //forget about this Clusterfuck rn
    struct StoryA //should I make this a class???
    {
        //MAKE ROOM METHODS STUPID lel

        //here's my idea:

        //instead of page 1 page 2
        //have it so its room 1 room 2. (or page inside of room
        //room objects? each with its own array of strings. that contain the story that happens in that room. and lists of items that can be intracted with to progress through the story[]
        //and each room contains metaphorical items you could intract with. and the text discribes it.
        //you can ignore certain items, or choose all of it.


        // The begining of the story wont be in a "room" and wont have any choices
        class RoomExample
        {
            string[,] Pages = new string[,]
            {

            };

            List<string> Items = new List<string>()
            {

            };
        }

        class Room1 //ex: this room will contain pages x to pages z
        {
            //will have string[] for story, pages, choices, etc
            //will have list<> of "items". Ties into string[]
        }

        class Room2
        {
            //will have string[] for story, pages, choices, etc
            //will have list<> of "items". Ties into string[]
        }

        class Room3
        {
            //will have string[] for story, pages, choices, etc
            //will have list<> of "items". Ties into string[]
        }

        // this wont be used in the final game, but is there for proof of consept
        static void CreateNewRoom()
        {
            //create a fuction that can create new rooms with Lists and string[]
            //that means ill need to have an original to create a new copy from
        }

        // idea for a menu
         //* //Main Menu
         //* "Title"
         //* 
         //* > New Game
         //*      "select a new story"
         //*      > story1
         //*      > story2
         //*      > story3
         //*      > + Add story from file
         //*      > < back
         //*      
         //* > Load
         //*      "Select a prevous save"
         //*      > story1
         //*      > story2
         //*      > story3
         //*      > - Delete a Save
         //*      > < back
         //*      
         //* > Quit
         //*      "Are you sure"
         //*      > Y/N
         //* 
         
    }
    */
}
