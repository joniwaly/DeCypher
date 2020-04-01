using System;
using System.Threading;

namespace AI_Alg
{
    class Program
    {
        public static int[] GuessGen(int[] guess)
        {
            Random randGen = new Random();
            for(int i=0; i < 4; i++)
            {
                int randVal = randGen.Next(0, 10);
                guess[i] = randVal;
            }
            return guess;
        }
        static void Main(string[] args)
        {
            int count = 0;
            bool win = false;
            while (count <= 5 && !win)
            {
                int[] guessAI = new int[4];
                GuessGen(guessAI);
                int[] guessP = new int[4] { 1, 2, 3, 4 };
                bool[] guessResults = new bool[4];
                for (int i = 0; i < 4; i++)
                {
                    if (guessP[i] == guessAI[i])
                        guessResults[i] = true;
                    else
                        guessResults[i] = false;
                }
                for(int i=0; i<guessResults.Length; i++)
                {
                    if (i == 1)
                    {
                        if (guessResults[i])
                        {
                            win = true;
                        }
                        else
                        {
                            win = false;
                        }
                    }
                    else
                    {
                        if (guessResults[i] && win)
                        {
                            win = true;
                        }
                        else
                        {
                            win = false;
                        }
                    }
                }
                if (!win)
                {
                    foreach (bool Element in guessResults)
                    {
                        Console.Write(Element);
                    }
                    Thread.Sleep(1000);
                    Console.Clear();

                }
                else
                {
                    Console.WriteLine("win");
                }
                count++;
            }
        }
    }
}
