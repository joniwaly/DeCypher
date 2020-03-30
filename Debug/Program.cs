using System;
using System.Threading;

namespace Debug
{
public class Program
        {
            public static void Main(string[] args)
            {
                int[] code = new int[4] { 1, 2, 3, 4 };
                int c;
                int i;
                int[,] codeTable = new int[6, 4];
                bool win = false;
                int codeDmp = 0;
                for(int j=0; j < code.Length; j++)
                {
                codeDmp = (codeDmp * 10) + code[j];
                }

            for (i = 0; i < codeTable.GetLength(0) && !win; i++)
            {

                for (c = 0; c < codeTable.GetLength(1); c++)
                {
                    Console.WriteLine($"ENTER THE {c + 1} NUMBER");
                    codeTable[i, c] = int.Parse(Console.ReadLine());
                }
                Console.Clear();
                int guessDmp = 0;
                for(int j=0; j < codeTable.GetLength(1); j++)
                {
                    guessDmp = (guessDmp * 10) + codeTable[i, j];
                }
                if (guessDmp==codeDmp)
                {
                    Console.WriteLine("Congratulations, You won!");
                    win = true;
                }
                else
                {
                    if (codeTable[i, c - 4] == code[0])
                        Console.WriteLine("cool! the number {0} is corect!", code[0]);

                    if (codeTable[i, c - 3] == code[1])
                        Console.WriteLine("cool! the number {0} is corect!", code[1]);

                    if (codeTable[i, c - 2] == code[2])
                        Console.WriteLine("cool! the number {0} is corect!", code[2]);

                    if (codeTable[i, c - 1] == code[3])
                        Console.WriteLine("cool! the number {0} atis corect!", code[3]);
                    }
                Thread.Sleep(1000);
                Console.Clear();
                }
            }
        }


    }
