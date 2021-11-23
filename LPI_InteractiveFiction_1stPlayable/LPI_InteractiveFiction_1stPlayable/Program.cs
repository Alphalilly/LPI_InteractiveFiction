using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions; //Regex https://regex101.com/ should I use this??

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
            //Story.anothertest();
        }
    }

    public class Story
    {
        static string[,] story = new string[,]
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
            List<string> sortedStory = new List<string>(story[pageNum, paragraphNum].Split(';')); //wait... this works?!

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
        /* //Main Menu
         * "Title"
         * 
         * > New Game
         *      "select a new story"
         *      > story1
         *      > story2
         *      > story3
         *      > + Add story from file
         *      > < back
         *      
         * > Load
         *      "Select a prevous save"
         *      > story1
         *      > story2
         *      > story3
         *      > - Delete a Save
         *      > < back
         *      
         * > Quit
         *      "Are you sure"
         *      > Y/N
         * 
         */
    }
}
