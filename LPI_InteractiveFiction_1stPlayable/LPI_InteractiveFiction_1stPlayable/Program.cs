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

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Small Note: I ended up using my story the last time I did this project because I ran out of time. \nI never did submit this assignment last year so I hope that using this story is alright.\n");
            Console.WriteLine("> Press Any Key to Start... \n");
            Console.ReadKey(true);

            Story.printStory();

            Console.ReadKey(true);
        }
    }

    public class PageNode
    {
        public string plotText;
        public string choiceTextA;
        public string choiceTextB;

        public bool isTitle;
        public bool isEnd;

        public PageNode choiceA;
        public PageNode choiceB;
    }

    public class Story
    {
        //using this old story as a placeholder
        public static string[] story = new string[]
        {
                //Title Page | page:0
                "---------------------------------- An interesting Dinner ----------------------------------."+ 
                ";" +
                ";" +
                ";1" +
                ";1",
                //-----------------------------------------------------------------------------------------------
                //1st | page:1
                " *Your trying to decide to eat Stake or Sushi tonight, both seem appealing but you just cant decide!" +
                ";I'll go with the stake, its not expensive and its good." +
                ";Sushi time! I've been craving it for a while and I deserve to treat myself every once in a while." +
                ";2" +
                ";3", 
                //A: stake | page:2
                "Eh I Think I'll go with the Stake, basic and good. \nThough Should I eat out? or buy what I need and make it myself?" +
                ";I'll eat out. I was thinking about trying stake from this new place anyway." +
                ";I think I'll make the Stake, kinda wanted to try out this new recipe for a while." +
                ";4" +
                ";5", 
                //-----------------------------------------------------------------------------------------------
                //B: sushi | page:3
                "I'll choose Sushi! I'm feeling experimental today. \nThere's a nearby sushi place that's pretty close, or should I ask an old friend I know and try out his Sushi?" +
                ";I'll go to that Sushi place, The sweet old lady there always likes to give me extra mints before I leave." +
                ";Eh. I'll give an old freind a visit. Maybe catch up and finally try his Sushi recipe." +
                ";6" +
                ";7", 
                //-----------------------------------------------------------------------------------------------
                // stake / A: out | page:4
                "I'm gonna eat out, too lazy to cook it myself. \nI was thinking about hanging out with the boys, haven't talked to them much lately. \nBut I'm not sure." +
                ";Eh, they can be a bit noisy, I might just head to the stake house on my own." +
                ";Aaah Ill invite em all, we could have a Stake party! " +
                ";8" +
                ";9", 
                // stake / B: cook | page:5
                "It wouldn't hurt to try my hand at cooking! I just gotta get the ingredients. \nshould I eat alone, or invite my friends over?" +
                ";Nah, I think I'm gonna eat alone, kinda feel like having time to myself." +
                ";yeah I dont mind having some company, and crack open a cold one with the boys after lunch." +
                ";10" +
                ";11",
                //-----------------------------------------------------------------------------------------------
                // sushi / A: out | page:6
                "The Sushi place sounds like a good idea, been a while since I last been there. \nNow that I think about it, I could go alone, or I could invite a few friends." +
                ";Nah I'll go alone, saves me some money anyway." +
                ";Yeah I'll hang out with the boys, See what they've been up to lately." +
                ";12" +
                ";13",
                // sushi / B: oldfriend | page:7
                "I Think I'll visit my old friend, see if he's had any luck getting into Psychology School or whatever he calls it. \nWe could catch up on old times while he makes Sushi, or I could invite some friends to finally meet him." +
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

        static Dictionary<int, PageNode> pages = new Dictionary<int, PageNode>(); //if I want to have mutiple stories in the same project, then nothing should be static

        public static PageNode ParseStory(string[] storyData)
        {
            //gets the plot text and choices
            for (int i = 0; i < storyData.Length; i++)
            {
                string pageSource = storyData[i];
                PageNode pageNode = new PageNode();
                string[] storySplit = pageSource.Split(';');

                if (i == 0)
                    pageNode.isTitle = true;

                else
                    pageNode.isTitle = false;

                pageNode.plotText = storySplit[0];
                pageNode.choiceTextA = storySplit[1];
                pageNode.choiceTextB = storySplit[2];

                pages.Add(i, pageNode);
            }

            //gets the choice numbers / destination pages. links all pages togeather in the tree
            for (int j = 0; j < storyData.Length; j++)
            {
                string pageSource = storyData[j];
                string[] storySplit = pageSource.Split(';');
                PageNode pageNode = pages[j];

                //these ignore if there isnt any numbers in the string[] to parse
                try 
                {
                    int A = int.Parse(storySplit[3]);
                    pageNode.choiceA = pages[A];
                }
                catch (FormatException) 
                {
                    pageNode.choiceA = null;
                    pageNode.isEnd = true;
                    //putting a Console.WriteLine() here results in something very intresting at the start of the program
                }

                try
                {
                    int B = int.Parse(storySplit[4]);
                    pageNode.choiceB = pages[B];
                }
                catch (FormatException) 
                {
                    pageNode.choiceB = null;
                    pageNode.isEnd = true;
                    //putting a Console.WriteLine() here results in something very intresting at the start of the program
                }
            }

            return pages[0];
        }

        public static void printStory()
        {
            ParseStory(story);

            ConsoleKeyInfo keyPress;

            for (int i = 0; i < pages.Count;)
            {
                Console.WriteLine(pages[i].plotText);
                Console.WriteLine();

                if (pages[i].isTitle == true) { i++; continue; }

                if (pages[i].isEnd == true) { break; }

                if (pages[i].choiceTextA != null)
                    Console.WriteLine("A:" + pages[i].choiceTextA);

                if (pages[i].choiceTextB != null)
                    Console.WriteLine("B:" + pages[i].choiceTextB);

                Console.WriteLine("\n > Awaiting Choice... \n");

                keyPress = Console.ReadKey(true);

                    if (keyPress.Key == ConsoleKey.A)
                    {

                        for (int j = 0; j < pages.Count; j++)
                        {
                            if (pages[j] == pages[i].choiceA)
                            {
                                i = j;
                                break;
                            }
                        }
                    }

                    if (keyPress.Key == ConsoleKey.B)
                    {
                        for (int j = 0; j < pages.Count; j++)
                        {
                            if (pages[j] == pages[i].choiceB)
                            {
                                i = j;
                                break;
                            }
                        }
                    }

                Console.WriteLine("-------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
