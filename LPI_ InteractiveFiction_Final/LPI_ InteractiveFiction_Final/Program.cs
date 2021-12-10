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

            //NOTE TO SELF. a friend's critiqe: https://discord.com/channels/@me/845910623870779402/918353923713159168
    */

    class Program
    {
        static void Main()
        {
            MainMenu.PrintMainMenu();

            Colour.SetColour("Blue");
            Console.WriteLine(" >> Program Has Ended, Press Any Key To Exit");
            Colour.SetColour("Gray");
            Console.ReadKey(true);
        }
    }

    public class Colour
    {
        public static void SetColour(string n)
        {
            switch (n)
            {
                case "Gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

            }
        }
    }

    public class MainMenu
    {
        static string[] mainMenu = File.ReadAllLines(@"MainMenu.txt");

        public static void PrintMainMenu()
        {
            ConsoleKeyInfo keyPress;

            for (int i = 0; i < mainMenu.Length; i++)
            {
                Colour.SetColour("Yellow");
                Console.WriteLine(mainMenu[i]);
                Colour.SetColour("Gray");
            }

            bool isRightKey = false;

            while (isRightKey == false)
            {
                keyPress = Console.ReadKey(true);

                switch (keyPress.Key)
                {
                    case ConsoleKey.A:
                        isRightKey = true;
                        Console.Clear();
                        Story.LoadStory(false); // THIS LOADS A NEW GAME
                        break;

                    case ConsoleKey.B:
                        isRightKey = true;
                        Console.Clear();
                        Story.LoadStory(true); // THIS LOADS A SAVE 
                        break;

                    case ConsoleKey.C:
                        isRightKey = true;
                        Colour.SetColour("Yellow");
                        Console.WriteLine("\n> Game Exit.");
                        Colour.SetColour("Gray");
                        break;

                    default:
                        isRightKey = false;
                        break;
                }
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
        static string[] story = File.ReadAllLines(@"StoryData.txt");

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
                {
                    pageNode.isTitle = true;
                }
                else
                {
                    pageNode.isTitle = false;
                }

                if (i == storyData.Length - 1)
                {
                    pageNode.isEnd = true;
                }

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

        public static void LoadStory(bool isASave)
        {
            ParseStory(story);

            //the save data should capture the last page the player was on.
            //therefor when the story is loaded again that number will be assigned to page node, signalling where the story should continue

            int pageNum = 0;

            switch (isASave)
            {
                case true:
                    try
                    {
                        pageNum = int.Parse(File.ReadAllText(@"StorySave.txt"));
                        PrintStory(pageNum);
                    }
                    catch (FileNotFoundException)
                    {
                        Colour.SetColour("Blue");
                        Console.WriteLine(">> ERROR File Not Found.");
                        Colour.SetColour("Gray");
                    }
                    break;

                case false:
                    PrintStory(pageNum);
                    break;
            }
        }

        public static void PrintStory(int pageNum)
        {
            ConsoleKeyInfo keyPress;

            for (int i = pageNum; i < pages.Count;)
            {
                //Console.WriteLine("page " + i);
                Console.WriteLine(pages[i].plotText);
                Console.WriteLine();

                if (pages[i].isTitle == true) 
                { 
                    i++; 
                    continue; 
                }

                if (pages[i].isEnd == true) 
                { 
                    break;
                }

                if (pages[i].choiceTextA != null)
                {
                    Colour.SetColour("Cyan");
                    Console.WriteLine("A:" + pages[i].choiceTextA);
                    Colour.SetColour("Gray");
                }

                if (pages[i].choiceTextB != null)
                {
                    Colour.SetColour("Cyan");
                    Console.WriteLine("B:" + pages[i].choiceTextB);
                    Colour.SetColour("Gray");
                }

                Colour.SetColour("Magenta");
                Console.WriteLine("\n > Awaiting Choice... \n");
                Colour.SetColour("Gray");

                bool isRightKey = false;
                string currentPage = i.ToString();

                while (isRightKey == false)
                {
                    keyPress = Console.ReadKey(true);

                    switch (keyPress.Key)
                    {
                        case ConsoleKey.A:
                            isRightKey = true;
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
                            isRightKey = true;
                            for (int j = 0; j < pages.Count; j++)
                            {
                                if (pages[j] == pages[i].choiceB) 
                                { 
                                    i = j; 
                                    break; 
                                }
                            }
                            break;

                        case ConsoleKey.S:
                            Colour.SetColour("Magenta");
                            Console.WriteLine("Saved");
                            Colour.SetColour("Gray");
                            File.WriteAllText(@"StorySave.txt", currentPage);
                            break;

                        case ConsoleKey.Escape:
                            isRightKey = true;
                            Console.WriteLine("Game End");
                            pages[i].isEnd = true;
                            break;

                        default:
                            Console.WriteLine("Wrong Key Press... Try again.");
                            break;
                    }
                }

                Console.WriteLine("¤ ═══════════════════════════════════════════════════════════════════════════════════════ ¤\n");
                //i get a windows error sound when • is printed to console. what?
            }
        }
    }
}
