using System;
using System.Threading;
using System.Windows;

namespace AI_Alg
{

    public class AI_Guesser
    {
        /*Checks which numbers were false and removes them from the guess pool\
         it accepts three inputs - The guess the computer made(AIGuess), The feedback(guessRes)
         and the guess pool from which the guess is made*/ 
        public static void MemEdit(int[] AIGuess, bool[] guessRes, int[,] nums) 
        {
            //for loop runs for the length of the integer array length.
            for(int i=0; i<nums.GetLength(0); i++)
            {
                //checks if the guess was correct
                if (guessRes[i])
                {
                    //for loop runs on integer array width.
                    for (int j = 0; j < nums.GetLength(1); j++)
                    {
                        //if the numbers aren't equal, the number in the memory is replaced with -1, because the AI's guess is correct
                        if (AIGuess[i] != nums[i, j])
                        {
                            
                            nums[i, j] = AIGuess[i];
                        }
                    }
                }
                //checks if the guess was wrong
                else
                {
                    //for loop runs on integer array width.
                    for (int j = 0; j < nums.GetLength(1); j++)
                    {
                        //if the numbers are equal, the number in the memory is replaced with -1, because the AI's guess is wrong
                        if (AIGuess[i] == nums[i, j])
                        {

                            nums[i, j] = -1;
                        }
                    }
                }
            }
        }

        /*Unlike the other guess generator, this one considers the feedback it gets. otherwise - same as "GuessGenDumb".
         it is different in that it changes the possible guess pool according to your code/ doesn't vheat, though*/
        public static void GuessGenSmart(int[] guess, int[,] guessPool)
        {
            Random randGen = new Random();
            for (int i = 0; i < 4; i++)
            {
                //creates a random value between 0 and 9
                int randVal = randGen.Next(10);
                //uses the random value to assign a guess
                int guessSingle = guessPool[i, randVal];
                //if the value is unavaliable at this spot(turned to -1), it reselects it
                while (guessSingle == -1)
                {
                    randVal = randGen.Next(10);
                    guessSingle = guessPool[i, randVal];
                }
                //inserts the generated guess into the guess array
                guess[i] = guessSingle;

                
            }
        }

        /*a dumber guess generator. not dissimilair to the smart one,
         exept it doesn't take into account whether the number has been proven wrong or not*/
        public static void GuessGenDumb(int[] guess)
        {
            //creates a random value between 0 and 10 with no regard to anything.
            Random randGen = new Random();
            for(int i=0; i < 4; i++)
            {
                int randVal = randGen.Next(0, 10);
                guess[i] = randVal;
            }
        }

        /*Gets input, dumps it into an array and returns the actual number*/
        static int InputSeperator(int input, int[] Array)
        {
            //takes your guess
            input = int.Parse(Console.ReadLine());
            //pops it into a placeholder so that it isn't wrecked
            int ph = input;
            //dumps it into an array that doesn't need to be returned because arrays
            for(int i=0; i < Array.Length; i++) 
            {
                Array[i] = ph % 10;
                ph /= 10;
            }
            //returns the input
            return input;
        }

        //main program body
        static void Main(string[] args)
        {
            //The computer's guess
            int[] AIGuess = new int[4];
            //the computer's code
            int[] AICode = new int[4];
            //the computer's code as an integer, for display purposes
            int codeAI = 0;

            //your guess
            int[] playerGuess = new int[4] { 0, 0, 0, 0 };
            //your guess as an integer - it just looks better, okay?
            int guessP = 0;
            //your code
            int[] playerCode = new int[4];
            //your code as an integer, to simplify input and dispaly
            int codeP = 0;
            //string that makes sure that the only thing that can screw this up entered enough bloody digits
            String codePS="AAAA";

            //boolean if the computer won
            bool winAI = false;
            //boolean if you won
            bool winP = false;
            //boolean array to narrow down guess pool
            bool[] guessResAI = new bool[4];
            //your guess results
            bool[] guessResP = new bool[4];
            //ConsoleKeyInfo to exit loop
            ConsoleKeyInfo cki;
            //do-while loop that runs so long as you don't press Esc
            do
            {
                //turn Counter
                int count = 0;
                //checks if you purposefuly opened the program and gives you a way out
                Console.WriteLine("Press any key to continue, press Escape to end program");
                cki = Console.ReadKey(false);
                if (cki.Key == ConsoleKey.Escape)
                    Environment.Exit(1);
                Console.Clear();
                
                //difficulty selector
                Console.WriteLine("for hard difficulty Press '1'. for easy, press '2'.");
                cki = Console.ReadKey(false);

                //hard, for intelectuals
                if (cki.Key == ConsoleKey.D1 || cki.Key == ConsoleKey.NumPad1)
                {
                    //possible guess pool
                     int[,] numStore = new int[4, 10] { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } };
                    //clears console for a nicer experience
                    Console.Clear();

                    //tells you which difficulty you selected, asks if you want to see the rules
                    Console.WriteLine("You selected easy difficulty!");
                    Console.WriteLine(" press 'H' to see the rules (recomended for first use), or any other key to continue.");
                    cki = Console.ReadKey(false);
                    //prints the rules if you pressed h, continues otherwise
                    if (cki.Key == ConsoleKey.H)
                    {
                        Console.Clear();
                        Console.WriteLine("The rules are simple. You enter a code, and the computer does too.");
                        Console.WriteLine("your goal is to decypher the computer's code before it decyphers yours.");
                        Console.Write("You are given 10 attempts. if you guess the code first - you win. ");
                        Console.WriteLine("If the computer guesses it first - it wins. if neither succeed, you both lose");
                        Console.WriteLine("the code consists of four digits between 1 and 9.");
                        Console.WriteLine("Good luck! press any key to continue");
                        Console.Read();
                    }
                    Console.Clear();

                    //Explenations
                    Console.WriteLine("Well then, let's begin. Enter your code. enter it as one number(for example, 1787.)");
                    Console.WriteLine("Note that if you entered a number over 4 digits' only the first four will be used. ");
                    Console.WriteLine("for example,if you entered 14325, 4325 will be used");

                    //Takes your code as an integer, makes sure you didn't mess up
                    do
                    {
                        if (codePS.Length < 4)
                            Console.WriteLine("Hey, your code is too short, please try again");

                        //takes your code, dumps into the array
                        codeP = InputSeperator(codeP, playerCode);
                        //dumps it into the testing string
                        codePS = codeP.ToString();
                    } while (codePS.Length < 4);
                    Console.Clear();

                    //generates computer's code
                    GuessGenDumb(AICode);
                    //dumps AI code into an integer
                    foreach (int num in AICode)
                        codeAI = codeAI * 10 + num;
                    //Writes your code, tells you how to proceed
                    Console.WriteLine($"Well done! your code is {codeP}. you will go first. Press any key to continue.");
                    Console.Read();
                    Console.Clear();

                    while (!winAI && !winP && count < 10)
                    {

                        /*this piece of code is a bit weird, but let me explain. the program had a weird error where
                         it was unable to parse the first integer of the loop. so I just made a TryParse clause.
                         it will always fail due to this stupid error, but it means the rest of the code will run properly.
                         you can try removing this part if you wish, I wouldn't recommend.*/


                        string inp = Console.ReadLine();
                        int b;
                        int a;
                        if (int.TryParse(inp, out b))
                            a = b;
                        Console.Clear();

                        //lets you enter your guess
                        Console.WriteLine("Enter your guess(as one number). ");
                        //actually recieves your code
                        do
                        {
                            if (codePS.Length < 4)
                                Console.WriteLine("Hey, your guess is too short, please try again");

                            //takes your code, dumps into the array
                            guessP = InputSeperator(guessP, playerCode);
                            //dumps it into the testing string
                            codePS = guessP.ToString();
                        } while (codePS.Length < 4);
                        Console.Clear();

                        //explenation, feedback on your guess.
                        Console.WriteLine("F means you got the number wrong. T means you got the number right.");
                        Console.WriteLine("The position matters, as it's the same as numbers in your guess.");
                        for (int i = 0; i < playerGuess.Length; i++)
                        {
                            //checks whether or not your guess was correct
                            if (playerGuess[i] == AICode[i])
                            {
                                //outputs feedback and changes answer array accordingly
                                Console.Write("T");
                                guessResP[i] = true;
                            }
                            else
                            {
                                //outputs feedback and changes answer array accordingly
                                Console.Write("F");
                                guessResP[i] = false;
                            }
                        }

                        //checks whether the player won or not.
                        if (guessResP[0] && guessResP[1] && guessResP[2] && guessResP[3])
                        {
                            winP = true;
                        }
                        Console.WriteLine();

                        Console.WriteLine("Press any key to continue.");
                        Console.Read();
                        Console.Clear();

                        //The computers turn to guess!
                        GuessGenSmart(AIGuess, numStore);
                        Console.WriteLine("Computer's guess results: ");
                        for (int i = 0; i < playerGuess.Length; i++)
                        {
                            //checks whether the computer got its guess right or not
                            if (AIGuess[i] == playerCode[i])
                            {
                                //outputs feedback, changes answer array accordingly
                                Console.Write("T");
                                guessResAI[i] = true;
                            }
                            else
                            {
                                //outputs feedback, changes answer array accordingly
                                Console.Write("F");

                                guessResAI[i] = false;
                            }

                        }
                        //changes the possible guess-pool
                        MemEdit(AIGuess, guessResAI, numStore);

                        //checks whether the computer won or not.
                        if (guessResAI[0] && guessResAI[1] && guessResAI[2] && guessResAI[3])
                        {
                            winAI = true;
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        count++;
                    }
                    if (winP)
                    {
                        Console.WriteLine("You won! Congratulations! (Press any key to continues)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                    else if(winAI)
                    {
                        Console.WriteLine("You lost. (Press any key to continue)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                    else
                    {
                        Console.WriteLine("no one won. (Press any key to continue)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                }
                
                //easy difficulty for you losers.
                if (cki.Key == ConsoleKey.D2 || cki.Key == ConsoleKey.NumPad2)
                {

                    //clears console for a nicer experience
                    Console.Clear();

                    //tells you which difficulty you selected, asks if you want to see the rules
                    Console.WriteLine("You selected easy difficulty!");
                    Console.WriteLine(" press 'H' to see the rules (recomended for first use), or any other key to continue.");
                    cki = Console.ReadKey(false);
                    //prints the rules if you pressed h, continues otherwise
                    if (cki.Key == ConsoleKey.H)
                    {
                        Console.Clear();
                        Console.WriteLine("The rules are simple. You enter a code, and the computer does too.");
                        Console.WriteLine("your goal is to decypher the computer's code before it decyphers yours.");
                        Console.Write("You are given 10 attempts. if you guess the code first - you win. ");
                        Console.WriteLine("If the computer guesses it first - it wins. if neither succeed, you both lose");
                        Console.WriteLine("the code consists of four digits between 1 and 9.");
                        Console.WriteLine("Good luck! press any key to continue.");
                        Console.Read();
                    }
                    Console.Clear();

                   //Explenations
                   Console.WriteLine("Well then, let's begin. Enter your code. enter it as one number(for example, 1787.)");
                   Console.WriteLine("Note that if you entered a number over 4 digits' only the first four will be used. ");
                   Console.WriteLine("for example,if you entered 14325, 4325 will be used");
                    //Takes your code as an integer, makes sure you didn't mess up
                    do
                    {
                        if (codePS.Length < 4)
                            Console.WriteLine("Hey, your code is too short, please try again");

                        //takes your code, dumps into the array
                        codeP = InputSeperator(codeP, playerCode);
                        //dumps it into the testing string
                        codePS = codeP.ToString();
                    } while (codePS.Length < 4);
                    Console.Clear();
    
                    //generates computer's code
                    GuessGenDumb(AICode);
                    //dumps AI code into an integer
                    foreach (int num in AICode)
                    codeAI = codeAI * 10 + num;
                    //Writes your code, tells you how to proceed
                    Console.WriteLine($"Well done! your code is {codeP}. you will go first. Press any key to continue.");
                    Console.Read();
                    Console.Clear();
                            
                    while (!winAI && !winP && count < 10) 
                    {

                        /*this piece of code is a bit weird, but let me explain. the program had a weird error where
                         it was unable to parse the first integer of the loop. so I just made a TryParse clause.
                         it will always fail due to this stupid error, but it means the rest of the code will run properly.
                         you can try removing this part if you wish, I wouldn't recommend.*/

                        
                        string inp = Console.ReadLine();
                        int b;
                        int a;
                        if (int.TryParse(inp, out b))
                            a = b;
                        Console.Clear();
                            
                        //lets you enter your guess
                        Console.WriteLine("Enter your guess (as one number). ");
                        do
                        {
                            if (codePS.Length < 4)
                                Console.WriteLine("Hey, your guess is too short, please try again");
                            //takes your code, dumps into the array
                            guessP = InputSeperator(guessP, playerGuess);
                            //dumps it into the testing string
                            codePS = guessP.ToString();
                        } while (codePS.Length < 4);

                        //explenation, feedback on your guess.
                        Console.WriteLine("F means you got the number wrong. T means you got the number right.");
                        Console.WriteLine("The position matters, as it's the same as numbers in your guess.");
                        for (int i = 0; i < playerGuess.Length; i++)
                        {
                            //checks whether or not your guess was correct
                            if (playerGuess[i] == AICode[i])
                            {
                                //outputs feedback and changes answer array accordingly
                                Console.Write("T");
                                guessResP[i] = true;
                            }
                            else
                            {   
                                //outputs feedback and changes answer array accordingly
                                Console.Write("F");
                                guessResP[i] = false;
                            }
                        }
                            
                        //checks whether the player won or not.
                        if (guessResP[0] && guessResP[1] && guessResP[2] && guessResP[3])
                        {
                            winP = true;
                        }
                        Console.WriteLine();

                        Console.WriteLine("Press any key to continue.");
                        Console.Read();
                        Console.Clear();
                            
                        //The computers turn to guess!
                        GuessGenDumb(AIGuess);
                        Console.WriteLine("Computer's guess results: ");
                        for (int i = 0; i < playerGuess.Length; i++)
                        {
                            //checks whether the computer got its guess right or not
                            if (AIGuess[i] == playerCode[i])
                            {
                                //outputs feedback, changes answer array accordingly
                                Console.Write("T");
                                guessResAI[i] = true;
                            }
                            else
                            {
                                //outputs feedback, changes answer array accordingly
                                Console.Write("F");
                
                                guessResAI[i] = false;
                            }
                        }
    
                        //checks whether the computer won or not.
                        if (guessResAI[0]&& guessResAI[1]&& guessResAI[2]&& guessResAI[3])
                        {
                            winAI = true;
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        count++;
                    }
                    if (winP)
                    {
                        Console.WriteLine("You won! Congratulations! (Press any key to continues)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                    else if(winAI)
                    {
                        Console.WriteLine("You lost. (Press any key to continue)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                    else
                    {
                        Console.WriteLine("no one won. (Press any key to continue)");
                        Console.Read();
                        Console.Clear();
                        Console.Write("Do you want to continue playing? if so, ");
                    }
                }   
            } while (true);
            
        }
    }
}
