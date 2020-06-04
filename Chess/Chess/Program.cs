﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Lifetime;
using System.Dynamic;
using System.Diagnostics;

namespace Chess
{

    class Program
    {
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        
        
        
        bool color_chess = false;
        string[] figur = new string[64]; 

        
        void menu()
        {
            Console.SetWindowSize(60, 30);
            Console.CursorVisible = false;
            string str;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"
                                       ___     ___  
            //\\  ||   || ||------|   // \\   // \\
           //  \\ ||   || ||         //  _\\ //  _\\
          //      ||---|| ||------|  \\ \\   \\ \\
          \\      ||---|| ||------|  _\\ \\  _\\ \\
           \\  // ||   || ||         \\   \\ \\   \\
            \\//  ||   || ||------|   \\__//  \\__//
                   ");
            Console.CursorLeft = 10;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ХОЧЕШЬ ПРОВЕРИТЬ СВОИ НАВЫКИ ИГРЫ В ШАХМАТЫ?");
            Console.CursorLeft = 22;
            Console.CursorTop = 11;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("ТОГДА ТЫ ПО АДРЕСУ...");

            str = "[НАЧАТЬ]";
            while (keypress.Key != ConsoleKey.Enter)
            {

                Console.ResetColor();
                Console.SetCursorPosition(25, 14);
                Console.WriteLine("Нажмите Enter");
                Console.SetCursorPosition(25, 14);
                keypress = Console.ReadKey();


            }
            Console.SetCursorPosition(24, 14);
            Console.Write("              ");

            Console.ResetColor();
            Console.SetCursorPosition(24, 14);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("|    BEGIN    |");
            Console.ResetColor();


            Console.SetCursorPosition(24, 17);
            Console.Write("|    ВЫЙТИ    |");

            
            bool select = false;
            while (select == false)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.DownArrow)
                {
                    str = "[ВЫЙТИ ИЗ ИГРЫ]";
                    Console.SetCursorPosition(24, 17);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("|    ВЫЙТИ    |");
                    Console.SetCursorPosition(24, 14);
                    Console.ResetColor();
                    Console.Write("|    BEGIN    |");
                }
                else if (keypress.Key == ConsoleKey.UpArrow)
                {
                    str = "[НАЧАТЬ]";
                    Console.SetCursorPosition(24, 14);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("|    BEGIN    |");
                    Console.SetCursorPosition(24, 17);
                    Console.ResetColor();
                    Console.Write("|    ВЫЙТИ    |");
                }
                if (str == "[ВЫЙТИ ИЗ ИГРЫ]")
                {
                    if (keypress.Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                }
                if (str == "[НАЧАТЬ]")
                {
                    if (keypress.Key == ConsoleKey.Enter)
                    {
                        select = true;
                    }
                }
            }
            Console.SetCursorPosition(10, 9);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("ХОЧЕШЬ ПРОВЕРИТЬ СВОИ НАВЫКИ ИГРЫ В ШАХМАТЫ?");
            Console.SetCursorPosition(21, 11);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  ВЫБЕРИ ЦВЕТ ФИГУР    ");

            str = "BLACK";
            Console.SetCursorPosition(24, 17);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|    BlACK    |");
            Console.SetCursorPosition(24, 14);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("|    WHITE    |");
            while (select == true)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.DownArrow)
                {
                    str = "WHITE";
                    Console.SetCursorPosition(24, 17);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("|    BlACK    |");
                    Console.SetCursorPosition(24, 14);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|    WHITE    |");
                }
                else if (keypress.Key == ConsoleKey.UpArrow)
                {
                    str = "BLACK";
                    Console.SetCursorPosition(24, 17);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|    BlACK    |");
                    Console.SetCursorPosition(24, 14);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("|    WHITE    |");
                }
                else if (str == "BLACK")
                {
                    if (keypress.Key == ConsoleKey.Enter)
                    {
                        Console.ResetColor();
                        Console.Clear();
                        str = "BLACK";
                        select = false;
                        color_chess = false;
                    }
                }
                else if (str == "WHITE")
                {
                    if (keypress.Key == ConsoleKey.Enter)
                    {
                        Console.ResetColor();
                        Console.Clear();
                        str = "WHITE";
                        select = false;
                        color_chess = true;
                    }
                }
            }
            
        }
        void Portrayal()
        {
            Console.SetWindowSize(100, 44);

            int top = 6, left = 20, Point = 0, line = 0, Row = 0, SquareLine = 0, quantity = 0;
            Console.SetCursorPosition(left, top);
            bool color = false;

            while (SquareLine < 8) // Прорисовка шахматной доски
            {
                while (Row < 4)
                {
                    while (line < 8)
                    {
                        Console.SetCursorPosition(left, top);
                        if (color == false)
                        {
                            color = true;
                            do
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("-");
                                Point++;
                                left++;
                            } while (Point < 8);
                            Point = 0;
                        }
                        else
                        {
                            color = false;
                            do
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("-");
                                left++;
                                Point++;
                            } while (Point < 8);
                            Point = 0;
                        }
                        line++;
                    }
                    top++;
                    left = 20;
                    line = 0;
                    Row++;
                }
                Row = 0;
                if (color == true) { color = false; } else { color = true; }
                SquareLine++;
            }

            top = 5;
            left = 18;
            while (quantity < 68) // Рамка: Прорисовка левого края доски
            {
                if (quantity > 33)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(left + 1, top - 1);
                    top--;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(left, top);
                    top++;
                }

                quantity++;
                Console.Write("-");
            }
            quantity = 0;
            left = 84;
            top = 5;
            while (quantity < 68)// Рамка: Прорисовка правого края доски
            {
                if (quantity > 33)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(left + 1, top - 1);
                    top--;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(left, top);
                    top++;
                }

                quantity++;
                Console.Write("-");
            }
            quantity = 0;
            left = 20;
            top = 38;
            while (quantity < 64)// Рамка: Прорисовка нижнего края доски
            {

                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(left, top);
                Console.Write("-");
                left++;
                quantity++;
            }

            quantity = 0;
            left = 20;
            top = 5;
            while (quantity < 64)// Рамка: Прорисовка верхнего края доски
            {

                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(left, top);
                Console.Write("-");
                left++;
                quantity++;
            }

            quantity = 0;
            left = 86;
            top = 5;
            while (quantity < 34) // Закраска окружения 
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(left, top);
                Console.Write("-");
                quantity++; top++;

            }
            quantity = 0;
            left = 20;
            top = 39;
            while (quantity < 32) // Закраска окружения 
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(left, top);
                Console.Write("-");
                quantity++; left++;
            }
            Console.ResetColor();
            Console.CursorVisible = false;

            Figure(color_chess);
        }

        void Figure(bool step)
        {
            int left = 21, top = 6;
            if (step != true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.White;
            }
            // Темная Левая Башня
            Console.SetCursorPosition(left, top);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 1, top + 2);
            Console.Write("____");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("|____|");
            // Темный Левый Конь
            Console.SetCursorPosition(left + 8, top);
            Console.Write("-----");
            Console.SetCursorPosition(left + 10, top + 1);
            Console.Write("---");
            Console.SetCursorPosition(left + 10, top + 2);
            Console.Write("---");
            Console.SetCursorPosition(left + 8, top + 3);
            Console.Write("------");
            // Темный Правый Конь
            Console.SetCursorPosition(left + 48, top);
            Console.Write("-----");
            Console.SetCursorPosition(left + 50, top + 1);
            Console.Write("---");
            Console.SetCursorPosition(left + 50, top + 2);
            Console.Write("---");
            Console.SetCursorPosition(left + 48, top + 3);
            Console.Write("------");

            // Темная Правая Башня
            Console.SetCursorPosition(left + 56, top);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 57, top + 1);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 57, top + 2);
            Console.Write("____");
            Console.SetCursorPosition(left + 56, top + 3);
            Console.Write("------");
            // Темный левый слон
            Console.SetCursorPosition(left + 16, top);
            Console.Write("------");
            Console.SetCursorPosition(left + 16, top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 18, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 16, top + 3);
            Console.Write("------");
            // Темный правый слон
            Console.SetCursorPosition(left + 40, top);
            Console.Write("------");
            Console.SetCursorPosition(left + 40, top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 42, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 40, top + 3);
            Console.Write("------");
            // Темный ферзь
            Console.SetCursorPosition(left + 26, top);
            Console.Write("--");
            Console.SetCursorPosition(left + 25, top + 1);
            Console.Write("----");
            Console.SetCursorPosition(left + 26, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 24, top + 3);
            Console.Write("------");
            // Темный король
            Console.SetCursorPosition(left + 32, top);
            Console.Write("-");
            Console.SetCursorPosition(left + 34, top);
            Console.Write("--");
            Console.SetCursorPosition(left + 37, top);
            Console.Write("-");
            Console.SetCursorPosition(left + 32, top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 33, top + 2);
            Console.Write("----");
            Console.SetCursorPosition(left + 32, top + 3);
            Console.Write("------");
             // Темный пешки
            for (int i = 0; i < 64; i += 8)
            {
                Console.SetCursorPosition(left + 1 + i, top + 5);
                Console.Write("----");
                Console.SetCursorPosition(left + 2 + i, top + 6);
                Console.Write("--");
                Console.SetCursorPosition(left + 1 + i, top + 7);
                Console.Write("----");
            }



            if (step == true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.White;
            }
            // Светлая Левая Башня
            Console.SetCursorPosition(left, top + 28);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 1, top + 29);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 1, top + 30);
            Console.Write("____");
            Console.SetCursorPosition(left, top + 31);
            Console.Write("|____|");
            // Светлая Правая Башня
            Console.SetCursorPosition(left + 56, top + 28);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 57, top + 29);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 57, top + 30);
            Console.Write("____");
            Console.SetCursorPosition(left + 56, top + 31);
            Console.Write("|____/");
            // Светлый Правый Конь
            Console.SetCursorPosition(left + 8, top + 28);
            Console.Write("-----");
            Console.SetCursorPosition(left + 10, top + 29);
            Console.Write("---");
            Console.SetCursorPosition(left + 10, top + 30);
            Console.Write("---");
            Console.SetCursorPosition(left + 8, top + 31);
            Console.Write("------");
            // Светлый Левый Конь
            Console.SetCursorPosition(left + 48, top + 28);
            Console.Write("-----");
            Console.SetCursorPosition(left + 50, top + 29);
            Console.Write("---");
            Console.SetCursorPosition(left + 50, top + 30);
            Console.Write("---");
            Console.SetCursorPosition(left + 48, top + 31);
            Console.Write("------");
            // Светлый левый слон
            Console.SetCursorPosition(left + 16, top + 28);
            Console.Write("------");
            Console.SetCursorPosition(left + 16, top + 29);
            Console.Write("------");
            Console.SetCursorPosition(left + 18, top + 30);
            Console.Write("--");
            Console.SetCursorPosition(left + 16, top + 31);
            Console.Write("------");
            // Светлый правый слон
            Console.SetCursorPosition(left + 40, top + 28);
            Console.Write("------");
            Console.SetCursorPosition(left + 40, top + 29);
            Console.Write("------");
            Console.SetCursorPosition(left + 42, top + 30);
            Console.Write("--");
            Console.SetCursorPosition(left + 40, top + 31);
            Console.Write("------");
            // Светлый ферзь
            Console.SetCursorPosition(left + 34, top + 28);
            Console.Write("--");
            Console.SetCursorPosition(left + 33, top + 29);
            Console.Write("----");
            Console.SetCursorPosition(left + 34, top + 30);
            Console.Write("--");
            Console.SetCursorPosition(left + 32, top + 31);
            Console.Write("------");
            // Светлый король
            Console.SetCursorPosition(left + 24, top + 28);
            Console.Write("-");
            Console.SetCursorPosition(left + 26, top + 28);
            Console.Write("--");
            Console.SetCursorPosition(left + 29, top + 28);
            Console.Write("-");
            Console.SetCursorPosition(left + 24, top + 29);
            Console.Write("------");
            Console.SetCursorPosition(left + 25, top + 30);
            Console.Write("----");
            Console.SetCursorPosition(left + 24, top + 31);
            Console.Write("------");

           
            // Светлые пешки
            for (int i = 0; i < 64; i += 8)
            {
                Console.SetCursorPosition(left + 1 + i, top + 25);
                Console.Write("----");
                Console.SetCursorPosition(left + 2 + i, top + 26);
                Console.Write("--");
                Console.SetCursorPosition(left + 1 + i, top + 27);
                Console.Write("----");
            }
        }



        int left = 20, top = 34;
        int dis = 3, loc = 56;
        bool one = false;
        void SelectFigure()
        {
            while (one == false)
            {
                figur[0] = "LUTOWER";
                figur[1] = "LUHORSE";
                figur[2] = "LUELEPHANT";
                figur[3] = "DF";
                figur[4] = "DKING";
                figur[5] = "RUELEPHANT";
                figur[6] = "RUHORSE";
                figur[7] = "RUTOWER";
                figur[8] = "DP1";
                figur[9] = "DP2";
                figur[10] = "DP3";
                figur[11] = "DP4";
                figur[12] = "DP5";
                figur[13] = "DP6";
                figur[14] = "DP7";
                figur[15] = "DP8";

                figur[48] = "UP1";
                figur[49] = "UP2";
                figur[50] = "UP3";
                figur[51] = "UP4";
                figur[52] = "UP5";
                figur[53] = "UP6";
                figur[54] = "UP7";
                figur[55] = "UP8";
                figur[56] = "LDTOWER";
                figur[57] = "LDHORSE";
                figur[58] = "LDELEPHANT";
                figur[59] = "WKING";
                figur[60] = "WF";
                figur[61] = "RDELEPHANT";
                figur[62] = "RDHORSE";
                figur[63] = "RDTOWER";
                for (int i = 0; i < 64; i++)
                {
                
                    if (((figur[i] != "DP1" && figur[i] != "DP2" && figur[i] != "DP3" && figur[i] != "DP4" && figur[i] != "DP5" &&
                          figur[i] != "DP6" && figur[i] != "DP7" && figur[i] != "DP8") && (figur[i] != "UP1" && figur[i] != "UP2" &&
                          figur[i] != "UP3" && figur[i] != "UP4" && figur[i] != "UP5" && figur[i] != "UP6" && figur[i] != "UP7" && figur[i] != "UP8")) && 
                          figur[i] != "WF" && figur[i] != "DF" && figur[i] != "DKING" && figur[i] != "WKING" &&
                          figur[i] != "RDELEPHANT" && figur[i] != "LDELEPHANT" && figur[i] != "LUELEPHANT" &&
                          figur[i] != "RUELEPHANT" && figur[i] != "RDHORSE" && figur[i] != "LDHORSE" &&
                          figur[i] != "RUHORSE" && figur[i] != "LUHORSE" && figur[i] != "RDTOWER" &&
                          figur[i] != "LDTOWER" && figur[i] != "RUTOWER" && figur[i] != "LUTOWER")
                    {
                        figur[i] = "Null";
                    }
                }
                one = true;
            }

            int buff_loc = 0;
            Console.CursorVisible = false;

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < 4; ++i)
            {
                Console.SetCursorPosition(left + 7, top + i);
                Console.Write("-");
                Console.SetCursorPosition(left, top + i);
                Console.Write("-");
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            bool select = false;
            while (select == false)
            {
                  keypress = Console.ReadKey();
                if (keypress.Key == ConsoleKey.RightArrow && left + 8 < 77)
                {

                    for (int i = 0; i < 4; ++i)
                    {
                        if (dis % 2 != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        left += 7;

                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 1;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left -= 15;
                    }
                    left += 8;
                    dis++;
                    loc += 1;
                }
                else if (keypress.Key == ConsoleKey.LeftArrow && left - 8 > 19)
                {
                    for (int i = 0; i < 4; ++i)
                    {

                        if (dis % 2 != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");

                        left -= 8;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left -= 7;

                        Console.SetCursorPosition(left, top + i);

                        Console.Write("-");

                        left += 8;

                    }
                    left -= 8;
                    dis++;
                    loc -= 1;
                }
                else if (keypress.Key == ConsoleKey.DownArrow && top + 4 < 38)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        top += 4;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 8;
                        left -= 15;


                        top -= 4;
                        if (dis % 2 != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");

                        left -= 7;
                    }
                    top += 4;
                    dis++;
                    loc += 8;
                }
                else if (keypress.Key == ConsoleKey.UpArrow && top - 4 > 5)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        top -= 4;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
                        left += 8;
                        left -= 15;

                        top += 4;
                        if (dis % 2 != 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(left, top + i);
                            Console.Write("-");
                        }
                        left += 7;
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");

                        left -= 7;
                    }
                    top -= 4;
                    dis++;
                    loc -= 8;
                }
                else if (keypress.Key == ConsoleKey.Enter)
                {
                    int count = 0;
                    bool wh = false;
                    while (wh == false)
                    {
                        if ((figur[loc] == "DP1" || figur[loc] == "DP2" || figur[loc] == "DP3" || figur[loc] == "DP4" || figur[loc] == "DP5"
                            || figur[loc] == "DP6" || figur[loc] == "DP7" || figur[loc] == "DP8") || (figur[loc] == "UP1" || figur[loc] == "UP2" ||
                            figur[loc] == "UP3" || figur[loc] == "UP4" || figur[loc] == "UP5" || figur[loc] == "UP6" || figur[loc] == "UP7" || figur[loc] == "UP8"))
                            
                        {
                            
                            while (keypress.Key != ConsoleKey.Escape)
                            {

                                if (figur[loc - 8] == "Null" && top - 4 > 5)
                                {

                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                        left -= 23;
                                    }
                                    top += 4;
                                }
                                if (figur[loc + 1] == "Null" && left + 8 < 77)
                                {
                                    left += 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left -= 8;
                                }
                                if (figur[loc - 1] == "Null" && left - 8 > 19)
                                {
                                    left -= 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left += 8;
                                }




                                Console.SetCursorPosition(0, 0);
                                keypress = Console.ReadKey();
                                if ((figur[loc - 8] == "Null" || (figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5"
                                       || figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8")) &&  top - 4 > 5 && keypress.Key == ConsoleKey.A && count == 0)
                                {
                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                        left -= 23;
                                    }
                                    top += 4;
                                }
                                if ((figur[loc + 1] == "Null" || (figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5"
                                       || figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8"))  && left + 8 < 77 && keypress.Key == ConsoleKey.A && count == 1)
                                {
                                    left += 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left -= 8;
                                }
                                if ((figur[loc - 1] == "Null" || (figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5"
                                       || figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8")) && left - 8 > 19 && keypress.Key == ConsoleKey.A && count == 2)
                                {
                                    left -= 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left += 8;
                                }
                                
                                Console.ResetColor();
                                Console.SetCursorPosition(0, 0);
                                


                                if (count == 2)
                                { count = 0; }
                                else { count++; }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                            }
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                        }
                        
                        if (figur[loc] == "RDHORSE" || figur[loc] == "LDHORSE" || figur[loc] == "RUHORSE" || figur[loc] == "LUHORSE")
                        {
                            try
                            {
                                if (figur[loc - 15] == "Null")
                                {
                                    top -= 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top += 8;
                                }
                            } catch { }
                            try
                            {
                                if (figur[loc - 17] == "Null")
                                {

                                    top -= 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }
                                    top += 8;
                                }
                            } catch { }
                            try
                            {
                                if (figur[loc + 15] == "Null")
                                {
                                    top += 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }
                                    top -= 8;
                                }
                            } catch { }
                            try
                            {
                                if (figur[loc + 17] == "Null")
                                {
                                    top += 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top -= 8;
                                }
                            } catch { }

                            try
                            {
                                if (figur[loc - 6] == "Null")
                                {
                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 16;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 23;
                                    }
                                    top += 4;
                                }


                            } catch { }
                            try
                            {
                                if (figur[loc + 10] == "Null" && left + 16 < 84)
                                {
                                    top += 4;
                                    for (int i = 3; i >= 0; i--)
                                    {
                                        left += 16;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 23;
                                    }
                                    top -= 4;
                                }


                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 6] == "Null")
                                {
                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 9;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                    }
                                    top -= 4;
                                }


                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 10] == "Null")
                                {
                                    top -= 4;
                                    for (int i = 0; i > 4; i++)
                                    {
                                        left -= 9;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                    }
                                    top += 4;
                                }


                            }
                            catch { }



                        }
                        if (figur[loc] == "RDELEPHANT" || figur[loc] == "RUELEPHANT" || figur[loc] == "LDELEPHANT" || figur[loc] == "LUELEPHANT")
                        {
                            int buff_left = left;
                            int buff_top = top;
                            try
                            {
                                if (figur[loc - 9] == "Null")
                                {

                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 7] == "Null")
                                {
                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 9] == "Null")
                                {

                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 7] == "Null")
                                {
                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;

                                }
                            }
                            catch { }
                        }

                        if (figur[loc] == "RDTOWER" || figur[loc] == "LDTOWER" || figur[loc] == "RUTOWER" || figur[loc] == "LUTOWER")
                        {



                            int buff_left = left;
                            int buff_top = top;
                            try
                            {
                                if (figur[loc + 1] == "Null")
                                {
                                    while (left > 19 && left < 76)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 1] == "Null")
                                {
                                    while (left > 20 && left < 77)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 8] == "Null")
                                {
                                    while (top > 5 && top < 34)
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                        }
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 8] == "Null")
                                {
                                    while (top > 6 && top < 35)
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                        }
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }

                        }
                        if (figur[loc] == "DF" || figur[loc] == "WF")
                        {
                            int buff_left = left;
                            int buff_top = top;
                            try
                            {
                                if (figur[loc + 1] == "Null")
                                {
                                    while (left > 19 && left < 76)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 1] == "Null")
                                {
                                    while (left > 20 && left < 77)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 8] == "Null")
                                {
                                    while (top > 5 && top < 34)
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                        }
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 8] == "Null")
                                {
                                    while (top > 6 && top < 35)
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                        }
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 9] == "Null")
                                {

                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 7] == "Null")
                                {
                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 9] == "Null")
                                {

                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        left += 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 7] == "Null")
                                {
                                    while ((left > 20 && left < 76) && (top > 5 && top < 38))
                                    {
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        left -= 8;
                                    }
                                    left = buff_left;
                                    top = buff_top;

                                }
                            }
                            catch { }

                        }



                        if (figur[loc] == "DKING" || figur[loc] == "WKING")
                        {

                            try
                            {
                                if (figur[loc + 8] == "Null")
                                {
                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                        left -= 23;
                                    }
                                    top -= 4;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 8] == "Null")
                                {
                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                        left -= 23;
                                    }
                                    top += 4;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 1] == "Null" && left + 8 < 77)
                                {
                                    left += 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left -= 8;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 1] == "Null" && left - 8 > 20)
                                {
                                    left -= 8;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left += 8;
                                }
                            }
                            catch { }

                            try
                            {
                                if (figur[loc - 9] == "Null")
                                {


                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }

                                    top += 4;

                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc - 7] == "Null")
                                {

                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top += 4;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 9] == "Null")
                                {

                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top -= 4;
                                }
                            }
                            catch { }
                            try
                            {
                                if (figur[loc + 7] == "Null")
                                {

                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }
                                    top -= 4;
                                }
                            }
                            catch { }
                        }

                        if ((figur[loc] == "DP1" || figur[loc] == "DP2" || figur[loc] == "DP3" || figur[loc] == "DP4" || figur[loc] == "DP5"
                            || figur[loc] == "DP6" || figur[loc] == "DP7" || figur[loc] == "DP8") || (figur[loc] == "UP1" || figur[loc] == "UP2" ||
                            figur[loc] == "UP3" || figur[loc] == "UP4" || figur[loc] == "UP5" || figur[loc] == "UP6" || figur[loc] == "UP7" || figur[loc] == "UP8"))
                        {

                            if (dis % 2 == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.BackgroundColor = ConsoleColor.Yellow;
                            }
                            if (left - 8 > 19)
                            {
                                left -= 8;

                                for (int i = 0; i < 4; i++)
                                {
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left += 7;
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left -= 7;
                                }
                                left += 8;
                            }

                            if (left + 8 < 77)
                            {
                                left += 8;
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left += 7;
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left -= 7;
                                }
                                left -= 8;
                            }
                            if (top - 4 > 5)
                            {
                                top -= 4;
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left += 7;
                                    Console.SetCursorPosition(left, top + i);
                                    Console.Write("-");
                                    left += 16;
                                    left -= 23;
                                }

                                top += 4;
                            }
                            if (count == 2)
                            { count = 0; }
                            else { count++; }
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            Console.SetCursorPosition(0, 0);
                        keypress = Console.ReadKey();
                            if (keypress.Key == ConsoleKey.Enter)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (dis % 2 != 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                    }
                                }
                                for (int i = 0; i < 4; i++)
                                {
                                    if (dis % 2 != 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        Console.SetCursorPosition(left + 7, top + i);
                                        Console.Write("-");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.BackgroundColor = ConsoleColor.Yellow;
                                        Console.SetCursorPosition(left + 7, top + i);
                                        Console.Write("-");
                                    }
                                }



                                if (count == 2)
                                {
                                    if (figur[loc - 1] == "Null" || (figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5"
                                   || figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") && left - 8 > 19)
                                    {
                                        empty(left - 1, top);
                                        if (figur[loc] == "UP1")
                                        {
                                            figur[loc - 1] = "UP1";
                                        }
                                        else if (figur[loc] == "UP2")
                                        {
                                            figur[loc - 1] = "UP2";
                                        }
                                        else if (figur[loc] == "UP3")
                                        {
                                            figur[loc - 1] = "UP3";
                                        }
                                        else if (figur[loc] == "UP4")
                                        {
                                            figur[loc - 1] = "UP4";
                                        }
                                        else if (figur[loc] == "UP5")
                                        {
                                            figur[loc - 1] = "UP5";
                                        }
                                        else if (figur[loc] == "UP6")
                                        {
                                            figur[loc - 1] = "UP6";
                                        }
                                        else if (figur[loc] == "UP7")
                                        {
                                            figur[loc - 1] = "UP7";
                                        }
                                        else if (figur[loc] == "UP8")
                                        {
                                            figur[loc - 1] = "UP8";
                                        }
                                        Pawn(left - 8, top);
                                        figur[loc] = "Null";

                                        if ((figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5"
                                   || figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") && left - 8 > 19)
                                        {
                                            if (figur[loc] == "UP1")
                                            {
                                                figur[loc - 1] = "UP1";
                                            }
                                            else if (figur[loc] == "UP2")
                                            {
                                                figur[loc - 1] = "UP2";
                                            }
                                            else if (figur[loc] == "UP3")
                                            {
                                                figur[loc - 1] = "UP3";
                                            }
                                            else if (figur[loc] == "UP4")
                                            {
                                                figur[loc - 1] = "UP4";
                                            }
                                            else if (figur[loc] == "UP5")
                                            {
                                                figur[loc - 1] = "UP5";
                                            }
                                            else if (figur[loc] == "UP6")
                                            {
                                                figur[loc - 1] = "UP6";
                                            }
                                            else if (figur[loc] == "UP7")
                                            {
                                                figur[loc - 1] = "UP7";
                                            }
                                            else if (figur[loc] == "UP8")
                                            {
                                                figur[loc - 1] = "UP8";
                                            }
                                            Pawn(left - 8, top);
                                        }
                                    }
                                }
                                else if (count == 1)
                                {
                                    if (figur[loc + 1] == "Null" || (figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5"
                                   || figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") && left + 8 < 77)
                                    {
                                        empty(left, top);
                                        if (figur[loc] == "UP1")
                                        {
                                            figur[loc + 1] = "UP1";
                                        }
                                        else if (figur[loc] == "UP2")
                                        {
                                            figur[loc + 1] = "UP2";
                                        }
                                        else if (figur[loc] == "UP3")
                                        {
                                            figur[loc + 1] = "UP3";
                                        }
                                        else if (figur[loc] == "UP4")
                                        {
                                            figur[loc + 1] = "UP4";
                                        }
                                        else if (figur[loc] == "UP5")
                                        {
                                            figur[loc + 1] = "UP5";
                                        }
                                        else if (figur[loc] == "UP6")
                                        {
                                            figur[loc + 1] = "UP6";
                                        }
                                        else if (figur[loc] == "UP7")
                                        {
                                            figur[loc + 1] = "UP7";
                                        }
                                        else if (figur[loc] == "UP8")
                                        {
                                            figur[loc + 1] = "UP8";
                                        }
                                        Pawn(left + 8, top);
                                        figur[loc] = "Null";

                                        if ((figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5"
                                   || figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") && left + 8 < 77)
                                        {
                                            if (figur[loc] == "UP1")
                                            {
                                                figur[loc + 1] = "UP1";
                                            }
                                            else if (figur[loc] == "UP2")
                                            {
                                                figur[loc + 1] = "UP2";
                                            }
                                            else if (figur[loc] == "UP3")
                                            {
                                                figur[loc + 1] = "UP3";
                                            }
                                            else if (figur[loc] == "UP4")
                                            {
                                                figur[loc + 1] = "UP4";
                                            }
                                            else if (figur[loc] == "UP5")
                                            {
                                                figur[loc + 1] = "UP5";
                                            }
                                            else if (figur[loc] == "UP6")
                                            {
                                                figur[loc + 1] = "UP6";
                                            }
                                            else if (figur[loc] == "UP7")
                                            {
                                                figur[loc + 1] = "UP7";
                                            }
                                            else if (figur[loc] == "UP8")
                                            {
                                                figur[loc + 1] = "UP8";
                                            }
                                            Pawn(left + 8, top);
                                        }
                                    }
                                }
                                else if (count == 0)
                                {
                                    if ((figur[loc - 8] == "Null" && top - 4 > 5) || (figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5"
                                   || figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8"))
                                    {
                                        empty(left, top);
                                        if (figur[loc] == "UP1")
                                        {
                                            figur[loc - 8] = "UP1";
                                        }
                                        else if (figur[loc] == "UP2")
                                        {
                                            figur[loc - 8] = "UP2";
                                        }
                                        else if (figur[loc] == "UP3")
                                        {
                                            figur[loc - 8] = "UP3";
                                        }
                                        else if (figur[loc] == "UP4")
                                        {
                                            figur[loc - 8] = "UP4";
                                        }
                                        else if (figur[loc] == "UP5")
                                        {
                                            figur[loc - 8] = "UP5";
                                        }
                                        else if (figur[loc] == "UP6")
                                        {
                                            figur[loc - 8] = "UP6";
                                        }
                                        else if (figur[loc] == "UP7")
                                        {
                                            figur[loc - 8] = "UP7";
                                        }
                                        else if (figur[loc] == "UP8")
                                        {
                                            figur[loc - 8] = "UP8";
                                        }
                                        Pawn(left, top - 4);
                                        figur[loc] = "Null";

                                        if ((figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5"
                                   || figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") && top - 4 < 5)
                                        {
                                            if (figur[loc] == "UP1")
                                            {
                                                figur[loc - 8] = "UP1";
                                            }
                                            else if (figur[loc] == "UP2")
                                            {
                                                figur[loc - 8] = "UP2";
                                            }
                                            else if (figur[loc] == "UP3")
                                            {
                                                figur[loc - 8] = "UP3";
                                            }
                                            else if (figur[loc] == "UP4")
                                            {
                                                figur[loc - 8] = "UP4";
                                            }
                                            else if (figur[loc] == "UP5")
                                            {
                                                figur[loc - 8] = "UP5";
                                            }
                                            else if (figur[loc] == "UP6")
                                            {
                                                figur[loc - 8] = "UP6";
                                            }
                                            else if (figur[loc] == "UP7")
                                            {
                                                figur[loc - 8] = "UP7";
                                            }
                                            else if (figur[loc] == "UP8")
                                            {
                                                figur[loc - 8] = "UP8";
                                            }
                                            Pawn(left, top - 4);
                                        }
                                    }
                                }

                            }

                            else if ((figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5"
                               || figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") && top - 4 < 5)
                            {
                                if (figur[loc] == "UP1")
                                {
                                    figur[loc - 8] = "UP1";
                                }
                                else if (figur[loc] == "UP2")
                                {
                                    figur[loc - 8] = "UP2";
                                }
                                else if (figur[loc] == "UP3")
                                {
                                    figur[loc - 8] = "UP3";
                                }
                                else if (figur[loc] == "UP4")
                                {
                                    figur[loc - 8] = "UP4";
                                }
                                else if (figur[loc] == "UP5")
                                {
                                    figur[loc - 8] = "UP5";
                                }
                                else if (figur[loc] == "UP6")
                                {
                                    figur[loc - 8] = "UP6";
                                }
                                else if (figur[loc] == "UP7")
                                {
                                    figur[loc - 8] = "UP7";
                                }
                                else if (figur[loc] == "UP8")
                                {
                                    figur[loc - 8] = "UP8";
                                }
                                Pawn(left, top - 4);
                            }
                            Console.ResetColor();

                        }


                        Console.SetCursorPosition(0, 0);

                        

                        wh = true;
                    }
                }
                select = true;
                Console.SetCursorPosition(0, 0);
                Console.ResetColor();
                Console.CursorVisible = false;
               

            }



        }
        static void Main()
        {
            Console.Title = "Chess";
            Program program = new Program();
            program.menu();
            program.Portrayal();
            while (true)
            {
                program.SelectFigure();
            }

        }
       
        void Elephant(int left, int top)
        {
            Console.SetCursorPosition(left  + 1, top);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("------");
            Console.SetCursorPosition(left + 1 , top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 3, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 1 , top + 3);
            Console.Write("------");
        }
        void Horse(int left, int top)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(left, top);
            Console.Write("-----");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("---");
            Console.SetCursorPosition(left + 2, top + 2);
            Console.Write("---");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("------");
        }
        void Tower(int left, int top)
        {
            Console.SetCursorPosition(left + 1, top);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 2, top + 2);
            Console.Write("____");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("|____|");
        }
        void Queen(int left, int top)
        {
            Console.SetCursorPosition(left + 3, top);
            Console.Write("--");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("----");
            Console.SetCursorPosition(left + 3, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("------");
        }
        void King(int left, int top)
        {
            Console.SetCursorPosition(left + 1, top);
            Console.Write("-");
            Console.SetCursorPosition(left + 3, top);
            Console.Write("--");
            Console.SetCursorPosition(left + 6, top);
            Console.Write("-");
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 2, top + 2);
            Console.Write("----");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("------");
        }
        void Pawn(int left, int top)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(left + 2 , top + 1);
            Console.Write("----");
            Console.SetCursorPosition(left + 3 , top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 2 , top + 3);
            Console.Write("----");
        }
        void empty(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 1);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 2);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("--------");

        }
    }
}
