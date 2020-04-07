using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FifteenPuzzle_1._0._6
{
    class Program
    {


        static void Main(string[] args)
        {
            int[] scores = new int[5];

        startGame:

            int key = 0, mc = 0, rightSec = 0;
            bool condition = true, hit = false, mute = false, helpOpened = false;

            string wm = "\n\tWeclome in my fifteen puzzle game!\n\tFor help, feel free to press H!\n\tTo mute the sound press M!\n\tIf you are too weak for that, ESC is for you.";
            string pg = "\tPlease, enjoy the game, mate!";
            int[,] mainMas = new int[4, 4];
            int[,] helpMas = new int[4, 4];
            int[] lineMas = new int[17];
            int[] source = new int[17] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            Console.SetWindowSize(80, 40);
            generatePlayField(lineMas, source, mainMas, helpMas);
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesMadeDisplay(scores);

            // W=119, A=97, S=100, D=100,
            //****************Core playing loop****************//
            while (condition == true)
            {

                controlKeys(out key);
                // Moving keys
                if (hit == false)
                {
                    mc += 1;
                }
                if (key == 115)
                {
                    helpOpened = false;
                    movingUp(mainMas, wm, pg, mc, out hit, mute, rightSec, helpMas, helpOpened, scores);
                }
                else if (key == 97)
                {
                    helpOpened = false;
                    movingRght(mainMas, wm, pg, mc, out hit, mute, rightSec, helpMas, helpOpened, scores);
                }
                else if (key == 119)
                {
                    helpOpened = false;
                    movingDwn(mainMas, wm, pg, mc, out hit, mute, rightSec, helpMas, helpOpened, scores);
                }
                else if (key == 100)
                {
                    helpOpened = false;
                    movingLft(mainMas, wm, pg, mc, out hit, mute, rightSec, helpMas, helpOpened, scores);
                }
                // Help section
                else if (key == 104)
                {
                    helpOpened = true;
                    help(helpMas, rightSec, helpOpened);
                    hit = true;
                }
                // mute button
                else if (key == 109)
                {
                    MuteButton(hit, ref mute, mainMas, wm, rightSec, helpMas, helpOpened, mc, pg, scores);
                }
                // ESC button
                else if (key == 27)
                {
                    break;
                }
                // Wrong button pressed
                else if (key != 97 && key != 100 && key != 115 && key != 119 && key != 104)
                {
                    Console.WriteLine(wm);
                    helpOpened = false;
                    printMas(mainMas, rightSec, helpMas, helpOpened);
                    Console.WriteLine("\t Oi! Carefully with buttons mate, only W, A, S, D is in use,\n\t keep it in mind\n");
                    hit = true;
                }

                //****************Condition of Winning****************//

                if (mainMas[0, 0] == 1 && mainMas[0, 1] == 2 && mainMas[0, 2] == 3 && mainMas[0, 3] == 4
                && mainMas[1, 0] == 5 && mainMas[1, 1] == 6 && mainMas[1, 2] == 7 && mainMas[1, 3] == 8
                && mainMas[2, 0] == 9 && mainMas[2, 1] == 10 && mainMas[2, 2] == 11 && mainMas[2, 3] == 12
                && mainMas[3, 0] == 13 && mainMas[3, 1] == 14 && mainMas[3, 2] == 15 && mainMas[3, 3] == 0)
                {

                    condition = false;

                    yourScore(mc, condition, ref scores);
                    Console.Clear();
                    Console.WriteLine("Congratulations, you have won the bloody game!");
                    pieIsNotALie();
                    Console.WriteLine("Another one? (y/n)");
                    controlKeys(out key);

                    while (condition == false)
                    {
                        if (key != 121 && key != 110 && key != 27)
                        {
                            Console.Clear();
                            Console.WriteLine("Congratulations, you have won the bloody game!");
                            pieIsNotALie();
                            Console.WriteLine("Excuse me, it was Y or N?");
                            controlKeys(out key);
                        }
                        else if (key == 121)
                        {
                            goto startGame;
                        }
                        else if (key == 110 || key == 27)
                        {
                            break;
                        }

                    }

                }

            }

        }
        //****************METHODES****************METHODES****************METHODES****************METHODES****************METHODES****************METHODES****************//

        //Moving methods
        static void movingUp(int[,] mainMas, string wm, string pg, int mc, out bool hit, bool mute, int rightSec, int[,] helpMas, bool helpOpened, int[] scores)
        {
            hit = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (mainMas[i, j] == 0)
                    {
                        if (mainMas[i, j] != mainMas[0, j])
                        {
                            mainMas[i, j] = mainMas[i - 1, j];
                            mainMas[i - 1, j] = 0;
                        }
                        else
                        {
                            if (mute != true)
                            {
                                Console.Beep(1000, 100);
                            }
                            hit = true;
                        }
                        break;
                    }
                }

            }
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesCurMade(mc);
            MovesMadeDisplay(scores);

        }
        static void movingDwn(int[,] mainMas, string wm, string pg, int mc, out bool hit, bool mute, int rightSec, int[,] helpMas, bool helpOpened, int[] scores)
        {

            hit = false;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (mainMas[i, j] == 0)
                    {
                        if (mainMas[i, j] != mainMas[3, j])
                        {
                            mainMas[i, j] = mainMas[i + 1, j];
                            mainMas[i + 1, j] = 0;
                        }
                        else
                        {
                            if (mute != true)
                            {
                                Console.Beep(1000, 100);
                            }
                            hit = true;
                        }
                        break;


                    }
                }

            }
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesCurMade(mc);
            MovesMadeDisplay(scores);

        }
        static void movingRght(int[,] mainMas, string wm, string pg, int mc, out bool hit, bool mute, int rightSec, int[,] helpMas, bool helpOpened, int[] scores)
        {

            hit = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (mainMas[i, j] == 0)
                    {
                        if (mainMas[i, j] != mainMas[i, 3])
                        {
                            mainMas[i, j] = mainMas[i, j + 1];
                            mainMas[i, j + 1] = 0;
                        }
                        else
                        {
                            if (mute != true)
                            {
                                Console.Beep(1000, 100);
                            }
                            hit = true;
                        }
                        break;
                    }
                }

            }
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesCurMade(mc);
            MovesMadeDisplay(scores);

        }
        static void movingLft(int[,] mainMas, string wm, string pg, int mc, out bool hit, bool mute, int rightSec, int[,] helpMas, bool helpOpened, int[] scores)
        {

            hit = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (mainMas[i, j] == 0)
                    {
                        if (mainMas[i, j] != mainMas[i, 0])
                        {
                            mainMas[i, j] = mainMas[i, j - 1];
                            mainMas[i, j - 1] = 0;
                        }
                        else
                        {
                            if (mute != true)
                            {
                                Console.Beep(1000, 100);
                            }
                            hit = true;
                        }
                        break;

                    }
                }
            }
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesCurMade(mc);
            MovesMadeDisplay(scores);

        }
        //Core methods
        static void generatePlayField(int[] lineMas, int[] source, int[,] mainMas, int[,] helpMas)
        {
            bool letsplay;
        genAg:
            // Here we shuffle numer sequence in line array
            lineMas[0] = source[0];    // Using Fisher–Yates shuffle alghoritm
            Random gen = new Random();
            for (int i = 1; i < source.Length - 1; i++)
            {
                int j = gen.Next(0, i);
                lineMas[i] = lineMas[j];
                lineMas[j] = source[i];
            }
            // Asking, if this secquence has a non-resolvable cases
            isTheSequencePlayable(lineMas, out letsplay);
            while (letsplay == false)
            {
                goto genAg;
            }

            int k = 0, g = 0;
            // And here we putting line array in square
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mainMas[i, j] = lineMas[k];
                    k++;
                }

            }
            //But here we will generate array for showing it in help
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    helpMas[i, j] = source[g + 1];
                    g++;
                }

            }

        }
        static void printMas(int[,] mainMas, int rightSec, int[,] helpMas, bool helpOpened)
        {
            doneAlready(mainMas, helpMas, out rightSec);
            Console.WriteLine("\n\t---------------------------------");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("\t|       |       |       |       |");
                for (int j = 0; j < 4; j++)
                {
                    if (mainMas[i, j] == 0 || mainMas[i, j] == 16)
                    {
                        Console.Write("\t|");
                    }
                    else if (helpOpened == false)
                    {
                        if (rightSec >= i * 4 + j + 1)
                        {
                            Console.Write("\t|");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("   {0}", mainMas[i, j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("\t|   {0}", mainMas[i, j]);
                        }
                    }
                    else if (helpOpened == true)
                    {
                        if (mainMas[i, j] == 0 || mainMas[i, j] == 16)
                        {
                            Console.Write("\t|", mainMas[i, j]);
                        }
                        else
                        {
                            Console.Write("\t|   {0}", mainMas[i, j]);
                        }
                    }
                }
                Console.WriteLine("\t|");
                Console.WriteLine("\t|       |       |       |       |");
                Console.WriteLine("\t---------------------------------");
            }
            Console.WriteLine();
        }
        static void controlKeys(out int key)
        {
            char keyp = Console.ReadKey(true).KeyChar;
            key = (int)keyp;
            Console.Clear();
        }
        static void isTheSequencePlayable(int[] lineMas, out bool letsplay)
        {
            letsplay = true;
            int ni = 0, e = 0;

            for (int i = 0; i <= 15; i++)
            {
                if (lineMas[i] == 0)
                {
                    e = (i + 4) / 4;
                }
                for (int p = i + 1; p <= 15; p++)
                {
                    if (lineMas[i] > lineMas[p] && lineMas[p] != 0)
                    {
                        ni++;
                    }
                }
            }
            int N = ni + e;
            if (N % 2 != 0)
            {
                letsplay = false;
            }
        }
        //Other methodes
        static void pieIsNotALie()
        {
            string pie = @"
                          #,
                         ###
                        ## ##
                       ##  ##
                        ####
                          :
                         #####
                        ######
                        ##  ##
                        ##  ##
                        ##  ##
                        ##  ##########
                        ##  #############
                   #######  ###############
               #############################
         .###################################
        #####################################;
        ##                                 ##.
        ##                                 ##
        #####################################
        ##                                 ##
        ##                                 ##
        ##                                 ###
     #####                                 #####a
    ### ##################################### ###
   ###  ##                                 ##  ###
   ##   ## ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, ##   ##
    ##  #####################################  ##
     ##                                       ##
      ####                                 ####
        ######                         ######
           ###############################";
            Console.WriteLine("{0}\n",pie);
        }
        static void help(int[,] helpMas, int rightSec, bool helpOpened)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.WriteLine("\n\t***HELP***");
            Console.WriteLine("\n\tThe game called \"Fifteen puzzle\" is a sliding puzzle game.\n\tGoal of the game is to put all tiles in ascending order.\n\tBy pressing W, A, S, D buttons on keyboard,\n\tuser can respectively slide tiles.\n\t(W-up, S-down, A-right, D-left)\n\tSo in the end puzzle should looks like that:");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            printMas(helpMas, rightSec, helpMas, helpOpened);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\tAs you can see mate, it's pretty simple, good luck!\n\tPress any key to return to the game... except h, for obvious reason...");
            Console.WriteLine("\n\n\n\n\n\n\tSevastjanovs Aleksandrs, Pardubice 2012.");
            Console.ResetColor();
        }
        static void doneAlready(int[,] mainMas, int[,] helpMas, out int rightSec)
        {
            rightSec = 0;
            if (mainMas[0, 0] == helpMas[0, 0])
            {
                rightSec++;
                if (mainMas[0, 1] == helpMas[0, 1])
                {
                    rightSec++;
                    if (mainMas[0, 2] == helpMas[0, 2])
                    {
                        rightSec++;
                        if (mainMas[0, 3] == helpMas[0, 3])
                        {
                            rightSec++;
                            if (mainMas[1, 0] == helpMas[1, 0])
                            {
                                rightSec++;
                                if (mainMas[1, 1] == helpMas[1, 1])
                                {
                                    rightSec++;
                                    if (mainMas[1, 2] == helpMas[1, 2])
                                    {
                                        rightSec++;
                                        if (mainMas[1, 3] == helpMas[1, 3])
                                        {
                                            rightSec++;
                                            if (mainMas[2, 0] == helpMas[2, 0])
                                            {
                                                rightSec++;
                                                if (mainMas[2, 1] == helpMas[2, 1])
                                                {
                                                    rightSec++;
                                                    if (mainMas[2, 2] == helpMas[2, 2])
                                                    {
                                                        rightSec++;
                                                        if (mainMas[2, 3] == helpMas[2, 3])
                                                        {
                                                            rightSec++;
                                                            if (mainMas[3, 0] == helpMas[3, 0])
                                                            {
                                                                rightSec++;
                                                                if (mainMas[3, 1] == helpMas[3, 1])
                                                                {
                                                                    rightSec++;
                                                                    if (mainMas[3, 2] == helpMas[3, 2])
                                                                    {
                                                                        rightSec++;
                                                                        if (mainMas[3, 3] == helpMas[3, 3])
                                                                        {
                                                                            rightSec++;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static void MuteButton(bool hit, ref bool mute, int[,] mainMas, string wm, int rightSec, int[,] helpMas, bool helpOpened, int mc, string pg, int[] scores)
        {
            hit = true;
            if (mute == false)
            {
                mute = true;
                Console.WriteLine("\n\tYou turned sound off mate!");
                hit = true;
            }
            else
            {
                mute = false;
                Console.WriteLine("\n\tYou turned sound on mate!");
                hit = true;
            }
            Console.WriteLine(wm);
            printMas(mainMas, rightSec, helpMas, helpOpened);
            Console.WriteLine(pg);
            MovesCurMade(mc);
            MovesMadeDisplay(scores);
        }
        static void MovesCurMade(int mc)
        {
            if (mc % 10 == 1 && mc != 11)
            {
                Console.WriteLine("\tYou made {0} move", mc);
            }
            else
            {
                Console.WriteLine("\tYou made {0} moves", mc);
            }
        }
        //Moves table displaying methodes
        static void yourScore(int mc, bool condition, ref int[] scores)
        {

            if (condition == false)
            {
                if (scores[0] == 0)
                {
                    scores[0] = mc;
                }
                else
                {
                    if (mc < scores[4])
                    {
                        scores[4] = mc;
                    }
                }
            }
            ScoreSort(scores);

        }
        static void ScoreSort(int[] scores)
        {                                   //SelectionSort taken from 11 lecture =)
            int min, temp;
            for (int i = 0; i < scores.Length; i++)
            {
                min = i; // the minimum value position
                // find minimum
                for (int j = i + 1; j < scores.Length; j++)
                {
                    if (scores[j] < scores[min])
                    {
                        min = j;
                    }
                }
                // swap
                temp = scores[i];
                scores[i] = scores[min];
                scores[min] = temp;
            }
        }
        static void MovesMadeDisplay(int[] scores)
        {
            if (scores[0] != 0 || scores[1] != 0 || scores[2] != 0 || scores[3] != 0 || scores[4] != 0)
            {
                Console.WriteLine("\n\tBest previous results:\n");
            }
            for (int i = 0; i < 5; i++)
            {
                if (scores[i] != 0)
                {

                    Console.WriteLine("\t{0}", scores[i]);
                }
            }
        }
    }
}




