using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LPI__InteractiveFiction_Final
{
    /* Project - C#: Interactive Fiction - Final
     
     * Develop a first playable build of an interactive fiction game. *

        + Technical Requirements:

        - A Story:
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
        - Main menu with choices:
            new game
            load game
            quit game
        - New game show first "page" in story
        - Load story from a file called "story.txt"
        - In game options:
            An additional save game option
                saves game to "savegame.txt" -- called persistence in the industry: it persists across sessions
                Only one save slot required
                UI is important and messages should appear when user saves game
                user can continue to play game after saving game
            An additional quit game option
        - at game end, the main menu will appear again
        - error checking:
            user input must be error checked
            file must be error checked (existing, corruption, hacked, etc.)
            give informative messages when there are errors
            give user another chance to enter correctly

        + Extra Mile Suggestions:
            - Data encryption.
            - Data integrity (anti-hack) -- use hash function
            - Have multiple save spots.
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
    */

    class Program
    {
        static void Main()
        {
            MainMenu.TestMainMenu();

            Console.ReadKey(true);
            //how would I make it so the user cant exit the program just by accedentally pressing a buttton, but also not have the program quit without the need of a ReadKey(true)?
        }
    }

    public class MainMenu
    {
        static string[] mainMenu = File.ReadAllLines(@"MainMenu.txt");

        static int cursor(char cursorChar = '<') //this does nothing right now, but it could be something cool
        {
            return cursorChar;
        }

        public static void TestMainMenu()
        {
            ConsoleKeyInfo keyPress;

            for (int i = 0; i < mainMenu.Length; i++)
            {
                Console.WriteLine(mainMenu[i]);
            }

            keyPress = Console.ReadKey(true);

            switch (keyPress.Key)
            {
                case ConsoleKey.A:
                    Console.Clear();
                    Story.printStory();
                    break;

                case ConsoleKey.B:
                    Console.WriteLine("This does nothing yet");
                    break;

                case ConsoleKey.C:
                    Console.WriteLine();
                    //This should automatically exit the program when C is pressed. Stull doesnt do that yet
                    break;

                default:
                    //this dosnt do what it should do yet. doing nothing when any other key is pressed
                    break;

            }
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
        public static string[] story = File.ReadAllLines(@"StoryData.txt");

        static Dictionary<int, PageNode> pages = new Dictionary<int, PageNode>();

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

                switch (keyPress.Key)
                {
                    case ConsoleKey.A:
                        for (int j = 0; j < pages.Count; j++)
                        {
                            if (pages[j] == pages[i].choiceA)
                            {
                                i = j;
                                break;
                            }
                        }
                        break;

                    case ConsoleKey.B:
                        for (int j = 0; j < pages.Count; j++)
                        {
                            if (pages[j] == pages[i].choiceB)
                            {
                                i = j;
                                break;
                            }
                        }
                        break;

                    default:
                        //this still does nothing to stop the last page from looping and printing when a differnt key is pressed. 
                        break;
                }

                Console.WriteLine("-------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
