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


    //here's my idea:

    //instead of page 1 page 2
    //have it so its room 1 room 2. (or page inside of room
    //room objects? each with its own array of strings. that contain the story that happens in that room. and lists of items that can be intracted with to progress through the story[]
    //and each room contains metaphorical items you could intract with. and the text discribes it.
    //you can ignore certain items, or choose all of it.

    //story

    class Program
    {
        static void Main()
        {
            //test

            //STORY ARRAY
            string[,] story = new string[,]
            {
                //title is at point 0 in the array
                //wait, so it needs to be the same length throughout??  
                {"insert title here"," "," "," "," "},  //page 0
                {"Plot","ChoiceA","ChoiceB","2","3"},   //page 1
                {"Plot","ChoiceA","ChoiceB","4","5"},   //page 2
            };

            //PLAYER INPUT

        }
    }

    struct Story //should I make this a class???
    {
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
    }
    
}
