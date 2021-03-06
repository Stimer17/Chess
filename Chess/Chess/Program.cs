using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chess
{
    class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

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
            //Верхние фигуры доски

            Tower(left - 1,top, !step);         // Левая Башня
            Horse(left + 8, top, !step);        // Левый Конь
            Horse(left + 48, top, !step);       // Правый Конь
            Tower(left + 55, top, !step);       // Правая Башня      
            Elephant(left + 15, top, !step);    // левый слон
            Elephant(left + 39, top, !step);    // правый слон
            Queen(left + 23, top,!step);        // ферзь
            King(left + 31, top, !step);        // король
            for (int i = 0; i < 64; i += 8)
            {
                Pawn(left + i - 1, top + 4,!step);  //  пешки
            }
            // Нижние фигруы

            Tower(left - 1, top + 28, step);         // Левая Башня
            Tower(left + 55, top + 28, step);        // Правая Башня
            Horse(left + 8, top + 28, step);         // Правый Конь
            Horse(left + 48, top + 28, step);        // Левый Конь
            Elephant(left + 15, top + 28, step);     // левый слон
            Elephant(left + 39, top + 28, step);     // правый слон
            Queen(left + 31, top + 28, step);        // ферзь
            King(left + 23, top + 28, step);         // король
            
            for (int i = 0; i < 64; i += 8)
            {
                Pawn(left - 1 + i, top + 24, step);  // пешки
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
                        Frame();
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
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
                        Frame();
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
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
                        Frame();
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
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
                        Frame();
                        Console.SetCursorPosition(left, top + i);
                        Console.Write("-");
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
                    int buff_left;
                    int buff_top;
                    bool wh = false;
                    while (wh == false)
                    {
                        if ((figur[loc] == "UP1" || figur[loc] == "UP2" || figur[loc] == "UP3" || figur[loc] == "UP4" || figur[loc] == "UP5" || figur[loc] == "UP6" || figur[loc] == "UP7" || figur[loc] == "UP8"))
                        {
                            if (figur[loc - 8] == "UP1" || figur[loc - 8] == "UP2" || figur[loc - 8] == "UP3" || figur[loc - 8] == "UP4" ||
                                   figur[loc - 8] == "UP5" || figur[loc - 8] == "UP6" || figur[loc - 8] == "UP7" || figur[loc - 8] == "UP8" ||
                                   figur[loc - 8] == "LDTOWER" || figur[loc - 8] == "RDTOWER" || figur[loc - 8] == "LDHORSE" || figur[loc - 8] == "WF" ||
                                   figur[loc - 8] == "LDELEPHANT" || figur[loc - 8] == "RDELEPHANT" || figur[loc - 8] == "RDHORSE")
                            {
                                break;
                            }
                            int count = 10;
                            bool tru = true;
                            bool f = false;
                            while (tru == true)
                            {
                                try
                                {
                                    if ((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                        figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8" || figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                        figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER") && top - 4 > 5)
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
                                if (((figur[loc] == "UP1" && loc == 48) || (figur[loc] == "UP2" && loc == 49) || (figur[loc] == "UP3" && loc == 50) ||
                                    (figur[loc] == "UP4" && loc == 51) || (figur[loc] == "UP5" && loc == 52) || (figur[loc] == "UP6" && loc == 53) ||
                                    (figur[loc] == "UP7" && loc == 54) || (figur[loc] == "UP8" && loc == 55)) && figur[loc - 16] != "LDTOWER" && 
                                    figur[loc - 16] != "RDTOWER" && figur[loc - 16] != "LDHORSE" && figur[loc - 16] != "RDHORSE" && figur[loc - 16] != "LDELEPHANT" &&
                                    figur[loc - 16] != "RDELEPHANT" && figur[loc - 16] != "WF")
                                {
                                    top -= 8;
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
                                    top += 8;
                                }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();

                                try
                                {
                                    if ((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                        figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8" || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                        figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER" )) && top - 4 > 5)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        else if (keypress.Key == ConsoleKey.A)
                                        {
                                            if (figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                                figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8" || figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                                figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                Console.BackgroundColor = ConsoleColor.Cyan;
                                            }
                                            else 
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                            }
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
                                            count = 0;
                                        }
                                    }
                                }
                                catch { }
                                Console.ResetColor();
                                Console.SetCursorPosition(0, 0);
                                if (((figur[loc] == "UP1" && loc == 48) || (figur[loc] == "UP2" && loc == 49) || (figur[loc] == "UP3" && loc == 50) ||
                                    (figur[loc] == "UP4" && loc == 51) || (figur[loc] == "UP5" && loc == 52) || (figur[loc] == "UP6" && loc == 53) ||
                                    (figur[loc] == "UP7" && loc == 54) || (figur[loc] == "UP8" && loc == 55)) && figur[loc - 16] != "LDTOWER" &&
                                    figur[loc - 16] != "RDTOWER" && figur[loc - 16] != "LDHORSE" && figur[loc - 16] != "RDHORSE" && figur[loc - 16] != "LDELEPHANT" &&
                                    figur[loc - 16] != "RDELEPHANT" && figur[loc - 16] != "WF")
                                {
                                    keypress = Console.ReadKey();
                                    if (keypress.Key == ConsoleKey.Enter)
                                    {
                                        goto m;
                                    }
                                    if (keypress.Key == ConsoleKey.Escape)
                                    {
                                        tru = false;
                                        goto n;
                                    }
                                    else if (keypress.Key == ConsoleKey.A)
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
                                        top += 8;
                                        count = 1;
                                    }
                                }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                keypress = Console.ReadKey();
                                m:
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                if (keypress.Key == ConsoleKey.Enter)
                                {
                                    if (count == 0)
                                    {
                                        try
                                        {
                                            if ((figur[loc - 8] == "Null" && top - 4 > 5) || (figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5"
                                           || figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8" || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                        figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top - 1 > 5 && loc != 0 && loc != 1 && loc != 2 && loc != 3 && loc != 4 && loc != 5 && loc != 6 && loc != 7)
                                            {
                                                Empty(left, top);
                                                dis++;
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
                                                Pawn(left, top - 4, color_chess);
                                                figur[loc] = "Null";
                                                dis--;
                                                tru = false;
                                                f = true;
                                            }
                                        }
                                        catch { }
                                    }
                                    if (count == 1)
                                    {
                                        Empty(left, top);
                                        dis++;
                                        if (figur[loc] == "UP1")
                                        {
                                            figur[loc - 16] = "UP1";
                                        }
                                        else if (figur[loc] == "UP2")
                                        {
                                            figur[loc - 16] = "UP2";
                                        }
                                        else if (figur[loc] == "UP3")
                                        {
                                            figur[loc - 16] = "UP3";
                                        }
                                        else if (figur[loc] == "UP4")
                                        {
                                            figur[loc - 16] = "UP4";
                                        }
                                        else if (figur[loc] == "UP5")
                                        {
                                            figur[loc - 16] = "UP5";
                                        }
                                        else if (figur[loc] == "UP6")
                                        {
                                            figur[loc - 16] = "UP6";
                                        }
                                        else if (figur[loc] == "UP7")
                                        {
                                            figur[loc - 16] = "UP7";
                                        }
                                        else if (figur[loc] == "UP8")
                                        {
                                            figur[loc - 16] = "UP8";
                                        }
                                        Pawn(left, top - 8, color_chess);
                                        figur[loc] = "Null";
                                        dis--;
                                        tru = false;
                                        f = true;
                                    }
                                    Console.SetCursorPosition(0, 0);
                                    Console.ResetColor();
                                    
                                }
                                if ((loc - 8 == 0 || loc - 8 == 1 || loc - 8 == 2 || loc - 8 == 3 || loc - 8 == 4 || loc - 8 == 5 || loc - 8 == 6 || loc - 8 == 7) && f == true)
                                {
                                    Empty(left, top);
                                    figur[loc - 8] = "WF";
                                    dis++;
                                    top -= 4;
                                    loc -= 8;
                                    Empty(left, top);
                                    Queen(left, top, color_chess);
                                }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                n:
                                dis++;
                                Frame();
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
                                dis--;
                                Frame();
                                if (top - 8 > 5)
                                {
                                    top -= 8;
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
                                    top += 8;
                                }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                            }
                        }





                        if (figur[loc] == "RDHORSE" || figur[loc] == "LDHORSE" || figur[loc] == "RUHORSE" || figur[loc] == "LUHORSE")
                        {
                            int count = 10;
                            bool tru = true;
                            while (tru == true)
                            {

                                try
                                {
                                    if (((figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                            figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8") || (figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                            figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER")) && left + 8 < 84 && top - 8 > 5)
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
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (((figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                            figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8") || (figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                            figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER")) && left - 8 > 19 && top - 8 > 5)
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
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                            figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8" || figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                            figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER") && left - 8 > 19 && top + 8 < 35)
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
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                            figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                            figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER") && left + 8 < 84 && top + 8 < 35)
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
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (((figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                            figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8") || (figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                            figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER")) && left + 23 < 84 && top - 4 > 5)
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
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 10] == "Null" || figur[loc + 10] == "DP1" || figur[loc + 10] == "DP2" || figur[loc + 10] == "DP3" || figur[loc + 10] == "DP4" || figur[loc + 10] == "DP5" ||
                                            figur[loc + 10] == "DP6" || figur[loc + 10] == "DP7" || figur[loc + 10] == "DP8" || figur[loc + 10] == "LUTOWER" || figur[loc + 10] == "LUHORSE" || figur[loc + 10] == "LUELEPHANT" ||
                                            figur[loc + 10] == "DF" || figur[loc + 10] == "RUELEPHANT" || figur[loc + 10] == "RUHORSE" || figur[loc + 10] == "RUTOWER") && left + 23 < 84 && top + 4 < 35)
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
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 6] == "Null" || figur[loc + 6] == "DP1" || figur[loc + 6] == "DP2" || figur[loc + 6] == "DP3" || figur[loc + 6] == "DP4" || figur[loc + 6] == "DP5" ||
                                         figur[loc + 6] == "DP6" || figur[loc + 6] == "DP7" || figur[loc + 6] == "DP8" || figur[loc + 6] == "LUTOWER" || figur[loc + 6] == "LUHORSE" || figur[loc + 6] == "LUELEPHANT" ||
                                         figur[loc + 6] == "DF" || figur[loc + 6] == "RUELEPHANT" || figur[loc + 6] == "RUHORSE" || figur[loc + 6] == "RUTOWER") && left - 16 > 19 && top + 4 < 35)
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
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (((figur[loc - 10] == "Null" || figur[loc - 10] == "DP1" || figur[loc - 10] == "DP2" || figur[loc - 10] == "DP3" || figur[loc - 10] == "DP4" || figur[loc - 10] == "DP5" ||
                                            figur[loc - 10] == "DP6" || figur[loc - 10] == "DP7" || figur[loc - 10] == "DP8") || (figur[loc - 10] == "LUTOWER" || figur[loc - 10] == "LUHORSE" || figur[loc - 10] == "LUELEPHANT" ||
                                            figur[loc - 10] == "DF" || figur[loc - 10] == "RUELEPHANT" || figur[loc - 10] == "RUHORSE" || figur[loc - 10] == "RUTOWER")) && left - 16 > 19 && top - 4 > 5)
                                    {
                                        top -= 4;
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
                                        top += 4;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();


                                try
                                {
                                    if ((figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                            figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                            figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER") && left + 15 < 84 && top - 8 > 5)
                                    {

                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            top -= 8;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left += 8;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 15;
                                            }
                                            top += 8;
                                            count = 0;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                            figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                            figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER") && left - 8 > 19 && top - 8 > 5)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                 figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                 figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
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
                                            }
                                            catch { }
                                            top -= 8;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 1;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 8;
                                            }
                                            top += 8;
                                            count = 1;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                            figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8" || figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                            figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER") && left - 8 > 19 && top + 8 < 35)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            top += 8;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 1;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 8;
                                            }
                                            top -= 8;
                                            count = 2;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                            figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                            figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER") && left + 15 < 84 && top + 8 < 35)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top + 8 < 35 && (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                                figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8") || (figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                                figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            top += 8;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left += 8;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 15;
                                            }
                                            top -= 8;
                                            count = 3;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                            figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8" || figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                            figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER") && left + 23 < 84 && top - 4 > 5)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top + 8 < 35 && (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                                figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8") || (figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                                figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 15 < 84 && top + 8 < 35 && (figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                                figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                                figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            top -= 4;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left += 16;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 23;
                                            }
                                            top += 4;
                                            count = 4;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 10] == "Null" || figur[loc + 10] == "DP1" || figur[loc + 10] == "DP2" || figur[loc + 10] == "DP3" || figur[loc + 10] == "DP4" || figur[loc + 10] == "DP5" ||
                                            figur[loc + 10] == "DP6" || figur[loc + 10] == "DP7" || figur[loc + 10] == "DP8" || figur[loc + 10] == "LUTOWER" || figur[loc + 10] == "LUHORSE" || figur[loc + 10] == "LUELEPHANT" ||
                                            figur[loc + 10] == "DF" || figur[loc + 10] == "RUELEPHANT" || figur[loc + 10] == "RUHORSE" || figur[loc + 10] == "RUTOWER") && left + 23 < 84 && top + 4 < 35)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top + 8 < 35 && (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                                figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8") || (figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                                figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 15 < 84 && top + 8 < 35 && (figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                                figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                                figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 23 < 84 && top - 4 > 5 && (figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                                figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8" || figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                                figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER"))
                                                {
                                                    top -= 4;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 16;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 23;
                                                    }
                                                    top += 4;
                                                }
                                            }
                                            catch { }
                                            top += 4;
                                            for (int i = 3; i >= 0; i--)
                                            {
                                                left += 16;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 23;
                                            }
                                            top -= 4;
                                            count = 5;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if ((figur[loc + 6] == "Null" || figur[loc + 6] == "DP1" || figur[loc + 6] == "DP2" || figur[loc + 6] == "DP3" || figur[loc + 6] == "DP4" || figur[loc + 6] == "DP5" ||
                                         figur[loc + 6] == "DP6" || figur[loc + 6] == "DP7" || figur[loc + 6] == "DP8" || figur[loc + 6] == "LUTOWER" || figur[loc + 6] == "LUHORSE" || figur[loc + 6] == "LUELEPHANT" ||
                                         figur[loc + 6] == "DF" || figur[loc + 6] == "RUELEPHANT" || figur[loc + 6] == "RUHORSE" || figur[loc + 6] == "RUTOWER") && left - 16 > 19 && top + 4 < 35)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        else if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top + 8 < 35 && (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                                figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8") || (figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                                figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 15 < 84 && top + 8 < 35 && (figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                                figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                                figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 23 < 84 && top - 4 > 5 && (figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                                figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8" || figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                                figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER"))
                                                {
                                                    top -= 4;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 16;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 23;
                                                    }
                                                    top += 4;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 23 < 84 && top + 4 < 35 && (figur[loc + 10] == "Null" || figur[loc + 10] == "DP1" || figur[loc + 10] == "DP2" || figur[loc + 10] == "DP3" || figur[loc + 10] == "DP4" || figur[loc + 10] == "DP5" ||
                                                figur[loc + 10] == "DP6" || figur[loc + 10] == "DP7" || figur[loc + 10] == "DP8" || figur[loc + 10] == "LUTOWER" || figur[loc + 10] == "LUHORSE" || figur[loc + 10] == "LUELEPHANT" ||
                                                figur[loc + 10] == "DF" || figur[loc + 10] == "RUELEPHANT" || figur[loc + 10] == "RUHORSE" || figur[loc + 10] == "RUTOWER"))
                                                {
                                                    top += 4;
                                                    for (int i = 3; i >= 0; i--)
                                                    {
                                                        left += 16;
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
                                            top += 4;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 9;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 16;
                                            }
                                            top -= 4;
                                            count = 6;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (((figur[loc - 10] == "Null" || figur[loc - 10] == "DP1" || figur[loc - 10] == "DP2" || figur[loc - 10] == "DP3" || figur[loc - 10] == "DP4" || figur[loc - 10] == "DP5" ||
                                            figur[loc - 10] == "DP6" || figur[loc - 10] == "DP7" || figur[loc - 10] == "DP8") || (figur[loc - 10] == "LUTOWER" || figur[loc - 10] == "LUHORSE" || figur[loc - 10] == "LUELEPHANT" ||
                                            figur[loc - 10] == "DF" || figur[loc - 10] == "RUELEPHANT" || figur[loc - 10] == "RUHORSE" || figur[loc - 10] == "RUTOWER")) && left - 16 > 19 && top - 4 > 5)
                                    {
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter)
                                        {
                                            goto m;
                                        }
                                        if (keypress.Key == ConsoleKey.Escape)
                                        {
                                            tru = false;
                                            goto n;
                                        }
                                        if (keypress.Key == ConsoleKey.A)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                                            try
                                            {
                                                if (left + 15 < 84 && top - 8 > 5 && (figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                                figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8" || figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                                figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER"))
                                                {

                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top - 8 > 5 && (figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                                figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8" || figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                                figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER"))
                                                {
                                                    top -= 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top += 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left - 8 > 19 && top + 8 < 35 && (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                                figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8") || (figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                                figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 1;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 8;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 15 < 77 && top + 8 < 35 && (figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                                figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                                figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER"))
                                                {
                                                    top += 8;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 8;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 15;
                                                    }
                                                    top -= 8;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 23 < 77 && top - 4 > 5 && (figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                                figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8" || figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                                figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER"))
                                                {
                                                    top -= 4;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left += 16;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 23;
                                                    }
                                                    top += 4;
                                                }
                                            }
                                            catch { }
                                            try
                                            {
                                                if (left + 23 < 77 && top + 4 < 35 && (figur[loc + 10] == "Null" || figur[loc + 10] == "DP1" || figur[loc + 10] == "DP2" || figur[loc + 10] == "DP3" || figur[loc + 10] == "DP4" || figur[loc + 10] == "DP5" ||
                                                figur[loc + 10] == "DP6" || figur[loc + 10] == "DP7" || figur[loc + 10] == "DP8" || figur[loc + 10] == "LUTOWER" || figur[loc + 10] == "LUHORSE" || figur[loc + 10] == "LUELEPHANT" ||
                                                figur[loc + 10] == "DF" || figur[loc + 10] == "RUELEPHANT" || figur[loc + 10] == "RUHORSE" || figur[loc + 10] == "RUTOWER"))
                                                {
                                                    top += 4;
                                                    for (int i = 3; i >= 0; i--)
                                                    {
                                                        left += 16;
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
                                                if (left - 16 > 19 && top + 4 < 35 && (figur[loc + 6] == "Null" || figur[loc + 6] == "DP1" || figur[loc + 6] == "DP2" || figur[loc + 6] == "DP3" || figur[loc + 6] == "DP4" || figur[loc + 6] == "DP5" ||
                                             figur[loc + 6] == "DP6" || figur[loc + 6] == "DP7" || figur[loc + 6] == "DP8" || figur[loc + 6] == "LUTOWER" || figur[loc + 6] == "LUHORSE" || figur[loc + 6] == "LUELEPHANT" ||
                                             figur[loc + 6] == "DF" || figur[loc + 6] == "RUELEPHANT" || figur[loc + 6] == "RUHORSE" || figur[loc + 6] == "RUTOWER"))
                                                {
                                                    top += 4;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        left -= 9;
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
                                            top -= 4;
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 9;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 16;
                                            }
                                            top += 4;
                                            count = 7;
                                        }
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                keypress = Console.ReadKey();
                            m:
                                

                                if (keypress.Key == ConsoleKey.Enter)
                                {
                                    if (count == 0)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 15] == "Null" || figur[loc - 15] == "DP1" || figur[loc - 15] == "DP2" || figur[loc - 15] == "DP3" || figur[loc - 15] == "DP4" || figur[loc - 15] == "DP5" ||
                                            figur[loc - 15] == "DP6" || figur[loc - 15] == "DP7" || figur[loc - 15] == "DP8") || (figur[loc - 15] == "LUTOWER" || figur[loc - 15] == "LUHORSE" || figur[loc - 15] == "LUELEPHANT" ||
                                            figur[loc - 15] == "DF" || figur[loc - 15] == "RUELEPHANT" || figur[loc - 15] == "RUHORSE" || figur[loc - 15] == "RUTOWER")) && left + 15 < 84 && top - 8 > 5)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc - 15] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc - 15] = "RDHORSE"; }
                                                Empty(left + 8, top - 8);
                                                Horse(left + 9, top - 8, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 1)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 17] == "Null" || figur[loc - 17] == "DP1" || figur[loc - 17] == "DP2" || figur[loc - 17] == "DP3" || figur[loc - 17] == "DP4" || figur[loc - 17] == "DP5" ||
                                            figur[loc - 17] == "DP6" || figur[loc - 17] == "DP7" || figur[loc - 17] == "DP8") || (figur[loc - 17] == "LUTOWER" || figur[loc - 17] == "LUHORSE" || figur[loc - 17] == "LUELEPHANT" ||
                                            figur[loc - 17] == "DF" || figur[loc - 17] == "RUELEPHANT" || figur[loc - 17] == "RUHORSE" || figur[loc - 17] == "RUTOWER")) && left - 8 > 19 && top - 8 > 5)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc - 17] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc - 17] = "RDHORSE"; }
                                                Empty(left - 8, top - 8);
                                                Horse(left - 7, top - 8, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 2)
                                    {
                                        try
                                        {
                                            if (figur[loc + 15] == "Null" || figur[loc + 15] == "DP1" || figur[loc + 15] == "DP2" || figur[loc + 15] == "DP3" || figur[loc + 15] == "DP4" || figur[loc + 15] == "DP5" ||
                                            figur[loc + 15] == "DP6" || figur[loc + 15] == "DP7" || figur[loc + 15] == "DP8" || figur[loc + 15] == "LUTOWER" || figur[loc + 15] == "LUHORSE" || figur[loc + 15] == "LUELEPHANT" ||
                                            figur[loc + 15] == "DF" || figur[loc + 15] == "RUELEPHANT" || figur[loc + 15] == "RUHORSE" || figur[loc + 15] == "RUTOWER" && left - 8 > 19 && top + 8 < 35)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc + 15] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc + 15] = "RDHORSE"; }
                                                Empty(left - 8, top + 8);
                                                Horse(left - 7, top + 8, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 3)
                                    {
                                        try
                                        {
                                            if ((figur[loc + 17] == "Null" || figur[loc + 17] == "DP1" || figur[loc + 17] == "DP2" || figur[loc + 17] == "DP3" || figur[loc + 17] == "DP4" || figur[loc + 17] == "DP5" ||
                                            figur[loc + 17] == "DP6" || figur[loc + 17] == "DP7" || figur[loc + 17] == "DP8") || (figur[loc + 17] == "LUTOWER" || figur[loc + 17] == "LUHORSE" || figur[loc + 17] == "LUELEPHANT" ||
                                            figur[loc + 17] == "DF" || figur[loc + 17] == "RUELEPHANT" || figur[loc + 17] == "RUHORSE" || figur[loc + 17] == "RUTOWER") && left + 15 < 84 && top + 8 < 35)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc + 17] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc + 17] = "RDHORSE"; }
                                                Empty(left + 8, top + 8);
                                                Horse(left + 9, top + 8, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 4)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                            figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8") || (figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                            figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER")) && left + 23 < 84 && top - 4 > 5)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc - 6] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc - 6] = "RDHORSE"; }
                                                Empty(left + 16, top - 4);
                                                Horse(left + 17, top - 4, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 5)
                                    {
                                        try
                                        {
                                            if ((figur[loc + 10] == "Null" || figur[loc + 10] == "DP1" || figur[loc + 10] == "DP2" || figur[loc + 10] == "DP3" || figur[loc + 10] == "DP4" || figur[loc + 10] == "DP5" ||
                                            figur[loc + 10] == "DP6" || figur[loc + 10] == "DP7" || figur[loc + 10] == "DP8" || figur[loc + 10] == "LUTOWER" || figur[loc + 10] == "LUHORSE" || figur[loc + 10] == "LUELEPHANT" ||
                                            figur[loc + 10] == "DF" || figur[loc + 10] == "RUELEPHANT" || figur[loc + 10] == "RUHORSE" || figur[loc + 10] == "RUTOWER") && left + 23 < 84 && top + 4 < 35)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc + 10] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc + 10] = "RDHORSE"; }
                                                Empty(left + 16, top + 4);
                                                Horse(left + 17, top + 4, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 6)
                                    {
                                        try
                                        {
                                            if ((figur[loc + 6] == "Null" || figur[loc + 6] == "DP1" || figur[loc + 6] == "DP2" || figur[loc + 6] == "DP3" || figur[loc + 6] == "DP4" || figur[loc + 6] == "DP5" ||
                                         figur[loc + 6] == "DP6" || figur[loc + 6] == "DP7" || figur[loc + 6] == "DP8" || figur[loc + 6] == "LUTOWER" || figur[loc + 6] == "LUHORSE" || figur[loc + 6] == "LUELEPHANT" ||
                                         figur[loc + 6] == "DF" || figur[loc + 6] == "RUELEPHANT" || figur[loc + 6] == "RUHORSE" || figur[loc + 6] == "RUTOWER") && left - 16 > 19 && top + 4 < 35)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc + 6] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc + 6] = "RDHORSE"; }
                                                Empty(left - 16, top + 4);
                                                Horse(left - 15, top + 4, color_chess);
                                                figur[loc] = "Null";
                                                dis--; tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 7)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 10] == "Null" || figur[loc - 10] == "DP1" || figur[loc - 10] == "DP2" || figur[loc - 10] == "DP3" || figur[loc - 10] == "DP4" || figur[loc - 10] == "DP5" ||
                                            figur[loc - 10] == "DP6" || figur[loc - 10] == "DP7" || figur[loc - 10] == "DP8") || (figur[loc - 10] == "LUTOWER" || figur[loc - 10] == "LUHORSE" || figur[loc - 10] == "LUELEPHANT" ||
                                            figur[loc - 10] == "DF" || figur[loc - 10] == "RUELEPHANT" || figur[loc - 10] == "RUHORSE" || figur[loc - 10] == "RUTOWER")) && left - 16 > 19 && top - 4 > 5)
                                            {
                                                Empty(left, top);
                                                dis++;
                                                if (figur[loc] == "LDHORSE") { figur[loc - 10] = "LDHORSE"; }
                                                if (figur[loc] == "RDHORSE") { figur[loc - 10] = "RDHORSE"; }
                                                Empty(left - 16, top - 4);
                                                Horse(left - 15, top - 4, color_chess);
                                                figur[loc] = "Null";
                                                dis--;tru = false;
                                            }
                                        }
                                        catch { }
                                    }

                                }
                            n:
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();

                                if (left + 15 < 84 && top - 8 > 5)
                                {
                                    top -= 8; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top += 8; dis--;
                                }
                                if (left - 8 > 19 && top - 8 > 5)
                                {

                                    top -= 8; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }
                                    top += 8; dis--;
                                }

                                if (left - 8 > 19 && top + 8 < 35)
                                {
                                    top += 8; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 8;
                                    }
                                    top -= 8; dis--;
                                }
                                if (left + 15 < 84 && top + 8 < 35)
                                {
                                    top += 8; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 15;
                                    }
                                    top -= 8; dis--;
                                }
                                if (left + 23 < 84 && top - 4 > 5)
                                {
                                    top -= 4; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 16;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 23;
                                    }
                                    top += 4; dis--;
                                }
                                if (left + 23 < 84 && top + 4 < 35)
                                {
                                    top += 4; dis++;
                                    for (int i = 3; i >= 0; i--)
                                    {
                                        left += 16;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 23;
                                    }
                                    top -= 4; dis--;
                                }
                                if (left - 16 > 19 && top + 4 < 35)
                                {
                                    top += 4; left -= 16; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                    }
                                    left += 16; ; top -= 4; dis--;
                                }
                                if (left - 16 > 19 && top - 4 > 5)
                                {
                                    top -= 4; dis++;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 9;
                                        Frame();
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left -= 7;
                                        Console.SetCursorPosition(left, top + i);
                                        Console.Write("-");
                                        left += 16;
                                    }
                                    top += 4; dis--;
                                }
                            }
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                        }




                        if (figur[loc] == "RDELEPHANT" || figur[loc] == "RUELEPHANT" || figur[loc] == "LDELEPHANT" || figur[loc] == "LUELEPHANT")
                        {
                            buff_left = left;
                            buff_top = top;
                            int buff_loc = loc;
                            int count = 29;
                            bool tru = true;
                            while (tru == true)
                            {
                                try
                                {
                                    if (figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                            figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8" || figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                            figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER")
                                    {

                                        while ((left - 1 > 20 && left < 77) && (top - 4 > 5 && top < 35) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                            figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                            figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                            figur[loc - 9] != "UP8")
                                        {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                loc -= 9;
                                                left -= 8;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                            figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8" || figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                            figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER")
                                    {
                                        while ((left > 19 && left < 76) && (top - 4 > 5 && top < 35) && figur[loc - 7] != "LDHORSE" && figur[loc - 7] != "RDHORSE" &&
                                            figur[loc - 7] != "LDELEPHANT" && figur[loc - 7] != "RDELEPHANT" && figur[loc - 7] != "LDTOWER" && figur[loc - 7] != "RDTOWER" && figur[loc - 7] != "UP1" &&
                                            figur[loc - 7] != "UP2" && figur[loc - 7] != "UP3" && figur[loc - 7] != "UP4" && figur[loc - 7] != "UP5" && figur[loc - 7] != "UP6" && figur[loc - 7] != "UP7" &&
                                            figur[loc - 7] != "UP8")
                                        {
                                            
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc -= 7;
                                                left += 8;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                            figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8" || figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                            figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER")
                                    {

                                        while ((left > 19 && left < 76) && (top > 5 && top + 4 < 35) && figur[loc + 9] != "LDHORSE" && figur[loc + 9] != "RDHORSE" &&
                                            figur[loc + 9] != "LDELEPHANT" && figur[loc + 9] != "RDELEPHANT" && figur[loc + 9] != "LDTOWER" && figur[loc + 9] != "RDTOWER" && figur[loc + 9] != "UP1" &&
                                            figur[loc + 9] != "UP2" && figur[loc + 9] != "UP3" && figur[loc + 9] != "UP4" && figur[loc + 9] != "UP5" && figur[loc + 9] != "UP6" && figur[loc + 9] != "UP7" &&
                                            figur[loc + 9] != "UP8")
                                        {
                                            
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc += 9;
                                                left += 8;

                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                            figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8" || figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                            figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER")
                                    {
                                        while ((left > 20 && left < 77) && (top > 5 && top + 4 < 35) && figur[loc + 7] != "LDHORSE" && figur[loc + 7] != "RDHORSE" &&
                                            figur[loc + 7] != "LDELEPHANT" && figur[loc + 7] != "RDELEPHANT" && figur[loc + 7] != "LDTOWER" && figur[loc + 7] != "RDTOWER" && figur[loc + 7] != "UP1" &&
                                            figur[loc + 7] != "UP2" && figur[loc + 7] != "UP3" && figur[loc + 7] != "UP4" && figur[loc + 7] != "UP5" && figur[loc + 7] != "UP6" && figur[loc + 7] != "UP7" &&
                                            figur[loc + 7] != "UP8")
                                        {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                loc += 7;
                                                left -= 8;
                                            }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();



                                try
                                {
                                    if (figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                                figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8" || figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                                figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER" && (left > 20 && left < 77) && (top > 5 && top < 35))
                                    {
                                        count = 0;
                                        while ((left - 1 > 20 && left < 77) && (top > 6 && top < 35) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                            figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                            figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                            figur[loc - 9] != "UP8")
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 1 || count == 2 || count == 3 || count == 4 || count == 5 || count == 6 || count == 7)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }
                                            if (keypress.Key == ConsoleKey.Enter){goto m;}
                                            else if (keypress.Key == ConsoleKey.Escape){tru = false;goto n;}
                                            if (keypress.Key == ConsoleKey.A)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                loc -= 9;
                                                left -= 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                                count++;
                                            }
                                        }

                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                    }
                                }
                                catch {  }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                                figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8" || figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                                figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER" && (left > 20 && left < 77) && (top > 5 && top < 35))
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        keypress = Console.ReadKey();
                                        if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                        else if (keypress.Key == ConsoleKey.Escape) { goto n; }
                                        Console.ResetColor();
                                        try
                                        {
                                            while ((left - 1 > 20 && left < 77) && (top > 5 && top < 38) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                                figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                                figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                                figur[loc - 9] != "UP8")
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
                                                loc -= 9;
                                                left -= 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;

                                        count = 7;
                                        while ((left > 19 && left < 76) && (top  > 7 && top < 35) && figur[loc - 7] != "LDHORSE" && figur[loc - 7] != "RDHORSE" &&
                                        figur[loc - 7] != "LDELEPHANT" && figur[loc - 7] != "RDELEPHANT" && figur[loc - 7] != "LDTOWER" && figur[loc - 7] != "RDTOWER" && figur[loc - 7] != "UP1" &&
                                        figur[loc - 7] != "UP2" && figur[loc - 7] != "UP3" && figur[loc - 7] != "UP4" && figur[loc - 7] != "UP5" && figur[loc - 7] != "UP6" && figur[loc - 7] != "UP7" &&
                                        figur[loc - 7] != "UP8")
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 8 || count == 9 || count == 10 || count == 11 || count == 12 || count == 13 || count == 14)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }
                                            if (keypress.Key == ConsoleKey.Enter)
                                            {
                                                goto m;
                                            }
                                            else if (keypress.Key == ConsoleKey.Escape)
                                            {
                                                tru = false;
                                                goto n;
                                            }
                                            else if (keypress.Key == ConsoleKey.A)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc -= 7;
                                                left += 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                                count++;
                                            }
                                        }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                    }
                                
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                            figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8" || figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                            figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER" && (left > 20 && left < 76) && (top > 5 && top < 35))
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        keypress = Console.ReadKey();
                                        Console.ResetColor();
                                        if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                        if (keypress.Key == ConsoleKey.Escape) { goto n; }
                                        try
                                        {
                                            while ((left - 1 > 20 && left < 76) && (top > 5 && top < 35) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                                figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                                figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                                figur[loc - 9] != "UP8")
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
                                                loc -= 9;
                                                left -= 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                        try
                                        {
                                            while ((left > 19 && left < 76) && (top > 5 && top < 38) && figur[loc - 7] != "LDHORSE" && figur[loc - 7] != "RDHORSE" &&
                                            figur[loc - 7] != "LDELEPHANT" && figur[loc - 7] != "RDELEPHANT" && figur[loc - 7] != "LDTOWER" && figur[loc - 7] != "RDTOWER" && figur[loc - 7] != "UP1" &&
                                            figur[loc - 7] != "UP2" && figur[loc - 7] != "UP3" && figur[loc - 7] != "UP4" && figur[loc - 7] != "UP5" && figur[loc - 7] != "UP6" && figur[loc - 7] != "UP7" &&
                                            figur[loc - 7] != "UP8")
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc -= 7;
                                                left += 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;




                                        count = 14;
                                        while ((left > 19 && left < 76) && (top > 5 && top < 35) && figur[loc + 9] != "LDHORSE" && figur[loc + 9] != "RDHORSE" &&
                                            figur[loc + 9] != "LDELEPHANT" && figur[loc + 9] != "RDELEPHANT" && figur[loc + 9] != "LDTOWER" && figur[loc + 9] != "RDTOWER" && figur[loc + 9] != "UP1" &&
                                            figur[loc + 9] != "UP2" && figur[loc + 9] != "UP3" && figur[loc + 9] != "UP4" && figur[loc + 9] != "UP5" && figur[loc + 9] != "UP6" && figur[loc + 9] != "UP7" &&
                                            figur[loc + 9] != "UP8")
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 15 || count == 16 || count == 17 || count == 18 || count == 19 || count == 20 || count == 21)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }

                                            if (keypress.Key == ConsoleKey.Enter)
                                            {
                                                goto m;
                                            }
                                            else if (keypress.Key == ConsoleKey.Escape)
                                            {
                                                tru = false;
                                                goto n;
                                            }
                                            if (keypress.Key == ConsoleKey.A)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc += 9;
                                                left += 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                                count++;
                                            }
                                        }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                    }
                                }
                                catch { }
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                            figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8" || figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                            figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER" && (left > 20 && left < 77) && (top > 5 && top < 35))
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        keypress = Console.ReadKey();
                                        Console.ResetColor();
                                        if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                        if (keypress.Key == ConsoleKey.Escape) { goto n; }
                                        try
                                        {
                                            while ((left - 1 > 20 && left < 77) && (top  > 6 && top < 35) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                                figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                                figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                                figur[loc - 9] != "UP8")
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
                                                loc -= 9;
                                                left -= 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                        try
                                        {
                                            while ((left > 19 && left < 76) && (top > 5 && top < 35) && figur[loc - 7] != "LDHORSE" && figur[loc - 7] != "RDHORSE" &&
                                            figur[loc - 7] != "LDELEPHANT" && figur[loc - 7] != "RDELEPHANT" && figur[loc - 7] != "LDTOWER" && figur[loc - 7] != "RDTOWER" && figur[loc - 7] != "UP1" &&
                                            figur[loc - 7] != "UP2" && figur[loc - 7] != "UP3" && figur[loc - 7] != "UP4" && figur[loc - 7] != "UP5" && figur[loc - 7] != "UP6" && figur[loc - 7] != "UP7" &&
                                            figur[loc - 7] != "UP8")
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc -= 7;
                                                left += 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                        try
                                        {
                                            while ((left  - 1 > 20 && left < 76) && (top > 5 && top < 35) && figur[loc + 9] != "LDHORSE" && figur[loc + 9] != "RDHORSE" &&
                                               figur[loc + 9] != "LDELEPHANT" && figur[loc + 9] != "RDELEPHANT" && figur[loc + 9] != "LDTOWER" && figur[loc + 9] != "RDTOWER" && figur[loc + 9] != "UP1" &&
                                               figur[loc + 9] != "UP2" && figur[loc + 9] != "UP3" && figur[loc + 9] != "UP4" && figur[loc + 9] != "UP5" && figur[loc + 9] != "UP6" && figur[loc + 9] != "UP7" &&
                                               figur[loc + 9] != "UP8")
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                loc += 9;
                                                left += 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                            }
                                        }
                                        catch { }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;


                                        count = 21;
                                        while ((left  - 1 > 20 && left < 77) && (top > 5 && top < 35) && figur[loc + 7] != "LDHORSE" && figur[loc + 7] != "RDHORSE" &&
                                            figur[loc + 7] != "LDELEPHANT" && figur[loc + 7] != "RDELEPHANT" && figur[loc + 7] != "LDTOWER" && figur[loc + 7] != "RDTOWER" && figur[loc + 7] != "UP1" &&
                                            figur[loc + 7] != "UP2" && figur[loc + 7] != "UP3" && figur[loc + 7] != "UP4" && figur[loc + 7] != "UP5" && figur[loc + 7] != "UP6" && figur[loc + 7] != "UP7" &&
                                            figur[loc + 7] != "UP8")
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 22 || count == 23 || count == 24 || count == 25 || count == 26 || count == 27 || count == 28)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }
                                            if (keypress.Key == ConsoleKey.Enter)
                                            {
                                                goto m;
                                            }
                                            else if (keypress.Key == ConsoleKey.Escape)
                                            {
                                                tru = false;
                                                goto n;
                                            }
                                            if (keypress.Key == ConsoleKey.A)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                loc += 7;
                                                left -= 8;
                                                Console.SetCursorPosition(0, 0);
                                                Console.ResetColor();
                                                count++;
                                            }
                                        }
                                        left = buff_left;
                                        top = buff_top;
                                        loc = buff_loc;
                                    }

                                }
                                catch { }



                                Console.SetCursorPosition(0, 0);
                                keypress = Console.ReadKey();
                                m:
                                left = buff_left;
                                top = buff_top;
                                loc = buff_loc;
                                try
                                {
                                    while (((left - 1 > 20 && left < 77) && (top - 4 > 5 && top < 35) && figur[loc - 9] != "LDHORSE" && figur[loc - 9] != "RDHORSE" &&
                                            figur[loc - 9] != "LDELEPHANT" && figur[loc - 9] != "RDELEPHANT" && figur[loc - 9] != "LDTOWER" && figur[loc - 9] != "RDTOWER" && figur[loc - 9] != "UP1" &&
                                            figur[loc - 9] != "UP2" && figur[loc - 9] != "UP3" && figur[loc - 9] != "UP4" && figur[loc - 9] != "UP5" && figur[loc - 9] != "UP6" && figur[loc - 9] != "UP7" &&
                                            figur[loc - 9] != "UP8") && (figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                            figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8" || figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                            figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER"))
                                    {
                                        Frame();
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;

                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        loc -= 9;
                                        left -= 8;
                                    }
                                    
                                }
                                catch { }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                try
                                {
                                    while (((figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                            figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8" || figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                            figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER") &&
                                            ((left > 19 && left < 76) && (top - 4 > 5 && top < 35) && figur[loc - 7] != "LDHORSE" && figur[loc - 7] != "RDHORSE" &&
                                            figur[loc - 7] != "LDELEPHANT" && figur[loc - 7] != "RDELEPHANT" && figur[loc - 7] != "LDTOWER" && figur[loc - 7] != "RDTOWER" && figur[loc - 7] != "UP1" &&
                                            figur[loc - 7] != "UP2" && figur[loc - 7] != "UP3" && figur[loc - 7] != "UP4" && figur[loc - 7] != "UP5" && figur[loc - 7] != "UP6" && figur[loc - 7] != "UP7" &&
                                            figur[loc - 7] != "UP8")))

                                    {
                                        Frame();
                                        top -= 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;

                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        loc -= 7;
                                        left += 8;
                                    }
                                }
                                catch {  }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                try
                                {
                                    while (((figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                            figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8" || figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                            figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER") &&
                                        ((left > 19 && left < 76) && (top > 5 && top < 35) && figur[loc + 9] != "LDHORSE" && figur[loc + 9] != "RDHORSE" &&
                                        figur[loc + 9] != "LDELEPHANT" && figur[loc + 9] != "RDELEPHANT" && figur[loc + 9] != "LDTOWER" && figur[loc + 9] != "RDTOWER" && figur[loc + 9] != "UP1" &&
                                        figur[loc + 9] != "UP2" && figur[loc + 9] != "UP3" && figur[loc + 9] != "UP4" && figur[loc + 9] != "UP5" && figur[loc + 9] != "UP6" && figur[loc + 9] != "UP7" &&
                                        figur[loc + 9] != "UP8")))
                                    {
                                        Frame();
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left += 8;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 15;
                                        }
                                        loc += 9;
                                        left += 8;
                                    }
                                }
                                catch { }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                try
                                {
                                    while (((figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                            figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8" || figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                            figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER") &&
                                        ((left > 20 && left < 77) && (top > 5 && top + 4 < 35) && figur[loc + 7] != "LDHORSE" && figur[loc + 7] != "RDHORSE" &&
                                        figur[loc + 7] != "LDELEPHANT" && figur[loc + 7] != "RDELEPHANT" && figur[loc + 7] != "LDTOWER" && figur[loc + 7] != "RDTOWER" && figur[loc + 7] != "UP1" &&
                                        figur[loc + 7] != "UP2" && figur[loc + 7] != "UP3" && figur[loc + 7] != "UP4" && figur[loc + 7] != "UP5" && figur[loc + 7] != "UP6" && figur[loc + 7] != "UP7" &&
                                        figur[loc + 7] != "UP8")))
                                    {
                                        Frame();
                                        top += 4;
                                        for (int i = 0; i < 4; i++)
                                        {
                                            left -= 1;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left -= 7;
                                            Console.SetCursorPosition(left, top + i);
                                            Console.Write("-");
                                            left += 8;
                                        }
                                        loc += 7;
                                        left -= 8;
                                    }
                                }
                                catch { }
                                left = buff_left;
                                top = buff_top;
                                loc = buff_loc;
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();

                                if (keypress.Key == ConsoleKey.Enter)
                                {
                                    if (count == 1)
                                    {
                                        try
                                        {
                                            if (figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                                figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8" || figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                                figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 9] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 9] = "RDELEPHANT"; }
                                                Empty(left - 8, top - 4);
                                                Elephant(left - 8, top - 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 2)
                                    {

                                        try
                                        {
                                            if (figur[loc - 18] == "Null" || figur[loc - 18] == "DP1" || figur[loc - 18] == "DP2" || figur[loc - 18] == "DP3" || figur[loc - 18] == "DP4" || figur[loc - 18] == "DP5" ||
                                                figur[loc - 18] == "DP6" || figur[loc - 18] == "DP7" || figur[loc - 18] == "DP8" || figur[loc - 18] == "LUTOWER" || figur[loc - 18] == "LUHORSE" || figur[loc - 18] == "LUELEPHANT" ||
                                                figur[loc - 18] == "DF" || figur[loc - 18] == "RUELEPHANT" || figur[loc - 18] == "RUHORSE" || figur[loc - 18] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 18] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 18] = "RDELEPHANT"; }
                                                Empty(left - 16, top - 8);
                                                Elephant(left - 16, top - 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 3)
                                    {
                                        try
                                        {
                                            if (figur[loc - 27] == "Null" || figur[loc - 27] == "DP1" || figur[loc - 27] == "DP2" || figur[loc - 27] == "DP3" || figur[loc - 27] == "DP4" || figur[loc - 27] == "DP5" ||
                                                figur[loc - 27] == "DP6" || figur[loc - 27] == "DP7" || figur[loc - 27] == "DP8" || figur[loc - 27] == "LUTOWER" || figur[loc - 27] == "LUHORSE" || figur[loc - 27] == "LUELEPHANT" ||
                                                figur[loc - 27] == "DF" || figur[loc - 27] == "RUELEPHANT" || figur[loc - 27] == "RUHORSE" || figur[loc - 27] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 27] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 27] = "RDELEPHANT"; }
                                                Empty(left - 24, top - 12);
                                                Elephant(left - 24, top - 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 4)
                                    {
                                        try
                                        {
                                            if (figur[loc - 36] == "Null" || figur[loc - 36] == "DP1" || figur[loc - 36] == "DP2" || figur[loc - 36] == "DP3" || figur[loc - 36] == "DP4" || figur[loc - 36] == "DP5" ||
                                                figur[loc - 36] == "DP6" || figur[loc - 36] == "DP7" || figur[loc - 36] == "DP8" || figur[loc - 36] == "LUTOWER" || figur[loc - 36] == "LUHORSE" || figur[loc - 36] == "LUELEPHANT" ||
                                                figur[loc - 36] == "DF" || figur[loc - 36] == "RUELEPHANT" || figur[loc - 36] == "RUHORSE" || figur[loc - 36] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 36] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 36] = "RDELEPHANT"; }
                                                Empty(left - 32, top - 16);
                                                Elephant(left - 32, top - 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 5)
                                    {
                                        try
                                        {
                                            if (figur[loc - 45] == "Null" || figur[loc - 45] == "DP1" || figur[loc - 45] == "DP2" || figur[loc - 45] == "DP3" || figur[loc - 45] == "DP4" || figur[loc - 45] == "DP5" ||
                                                figur[loc - 45] == "DP6" || figur[loc - 45] == "DP7" || figur[loc - 45] == "DP8" || figur[loc - 45] == "LUTOWER" || figur[loc - 45] == "LUHORSE" || figur[loc - 45] == "LUELEPHANT" ||
                                                figur[loc - 45] == "DF" || figur[loc - 45] == "RUELEPHANT" || figur[loc - 45] == "RUHORSE" || figur[loc - 45] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 45] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 45] = "RDELEPHANT"; }
                                                Empty(left - 40, top - 20);
                                                Elephant(left - 40, top - 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 6)
                                    {
                                        try
                                        {
                                            if (figur[loc - 54] == "Null" || figur[loc - 54] == "DP1" || figur[loc - 54] == "DP2" || figur[loc - 54] == "DP3" || figur[loc - 54] == "DP4" || figur[loc - 54] == "DP5" ||
                                                figur[loc - 54] == "DP6" || figur[loc - 54] == "DP7" || figur[loc - 54] == "DP8" || figur[loc - 54] == "LUTOWER" || figur[loc - 54] == "LUHORSE" || figur[loc - 54] == "LUELEPHANT" ||
                                                figur[loc - 54] == "DF" || figur[loc - 54] == "RUELEPHANT" || figur[loc - 54] == "RUHORSE" || figur[loc - 54] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 54] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 54] = "RDELEPHANT"; }
                                                Empty(left - 48, top - 24);
                                                Elephant(left - 48, top - 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 7)
                                    {
                                        try
                                        {
                                            if (figur[loc - 63] == "Null" || figur[loc - 63] == "DP1" || figur[loc - 63] == "DP2" || figur[loc - 63] == "DP3" || figur[loc - 63] == "DP4" || figur[loc - 63] == "DP5" ||
                                                figur[loc - 63] == "DP6" || figur[loc - 63] == "DP7" || figur[loc - 63] == "DP8" || figur[loc - 63] == "LUTOWER" || figur[loc - 63] == "LUHORSE" || figur[loc - 63] == "LUELEPHANT" ||
                                                figur[loc - 63] == "DF" || figur[loc - 63] == "RUELEPHANT" || figur[loc - 63] == "RUHORSE" || figur[loc - 63] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 63] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 63] = "RDELEPHANT"; }
                                                Empty(left - 56, top - 28);
                                                Elephant(left - 56, top - 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 8)
                                    {
                                        try
                                        {
                                            if (figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                                figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8" || figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                                figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 7] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 7] = "RDELEPHANT"; }
                                                Empty(left + 8, top - 4);
                                                Elephant(left + 8, top - 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 9)
                                    {

                                        try
                                        {
                                            if (figur[loc - 14] == "Null" || figur[loc - 14] == "DP1" || figur[loc - 14] == "DP2" || figur[loc - 14] == "DP3" || figur[loc - 14] == "DP4" || figur[loc - 14] == "DP5" ||
                                                figur[loc - 14] == "DP6" || figur[loc - 14] == "DP7" || figur[loc - 14] == "DP8" || figur[loc - 14] == "LUTOWER" || figur[loc - 14] == "LUHORSE" || figur[loc - 14] == "LUELEPHANT" ||
                                                figur[loc - 14] == "DF" || figur[loc - 14] == "RUELEPHANT" || figur[loc - 14] == "RUHORSE" || figur[loc - 14] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 14] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 14] = "RDELEPHANT"; }
                                                Empty(left + 16, top - 8);
                                                Elephant(left + 16, top - 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 10)
                                    {
                                        try
                                        {
                                            if (figur[loc - 21] == "Null" || figur[loc - 21] == "DP1" || figur[loc - 21] == "DP2" || figur[loc - 21] == "DP3" || figur[loc - 21] == "DP4" || figur[loc - 21] == "DP5" ||
                                                figur[loc - 21] == "DP6" || figur[loc - 21] == "DP7" || figur[loc - 21] == "DP8" || figur[loc - 21] == "LUTOWER" || figur[loc - 21] == "LUHORSE" || figur[loc - 21] == "LUELEPHANT" ||
                                                figur[loc - 21] == "DF" || figur[loc - 21] == "RUELEPHANT" || figur[loc - 21] == "RUHORSE" || figur[loc - 21] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 21] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 21] = "RDELEPHANT"; }
                                                Empty(left + 24, top - 12);
                                                Elephant(left + 24, top - 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 11)
                                    {
                                        try
                                        {
                                            if (figur[loc - 28] == "Null" || figur[loc - 28] == "DP1" || figur[loc - 28] == "DP2" || figur[loc - 28] == "DP3" || figur[loc - 28] == "DP4" || figur[loc - 28] == "DP5" ||
                                                figur[loc - 28] == "DP6" || figur[loc - 28] == "DP7" || figur[loc - 28] == "DP8" || figur[loc - 28] == "LUTOWER" || figur[loc - 28] == "LUHORSE" || figur[loc - 28] == "LUELEPHANT" ||
                                                figur[loc - 28] == "DF" || figur[loc - 28] == "RUELEPHANT" || figur[loc - 28] == "RUHORSE" || figur[loc - 28] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 28] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 28] = "RDELEPHANT"; }
                                                Empty(left + 32, top - 16);
                                                Elephant(left + 32, top - 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 12)
                                    {
                                        try
                                        {
                                            if (figur[loc - 35] == "Null" || figur[loc - 35] == "DP1" || figur[loc - 35] == "DP2" || figur[loc - 35] == "DP3" || figur[loc - 35] == "DP4" || figur[loc - 35] == "DP5" ||
                                                figur[loc - 35] == "DP6" || figur[loc - 35] == "DP7" || figur[loc - 35] == "DP8" || figur[loc - 35] == "LUTOWER" || figur[loc - 35] == "LUHORSE" || figur[loc - 35] == "LUELEPHANT" ||
                                                figur[loc - 35] == "DF" || figur[loc - 35] == "RUELEPHANT" || figur[loc - 35] == "RUHORSE" || figur[loc - 35] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 35] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 35] = "RDELEPHANT"; }
                                                Empty(left + 40, top - 20);
                                                Elephant(left + 40, top - 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 13)
                                    {
                                        try
                                        {
                                            if (figur[loc - 42] == "Null" || figur[loc - 42] == "DP1" || figur[loc - 42] == "DP2" || figur[loc - 42] == "DP3" || figur[loc - 42] == "DP4" || figur[loc - 42] == "DP5" ||
                                                figur[loc - 42] == "DP6" || figur[loc - 42] == "DP7" || figur[loc - 42] == "DP8" || figur[loc - 42] == "LUTOWER" || figur[loc - 42] == "LUHORSE" || figur[loc - 42] == "LUELEPHANT" ||
                                                figur[loc - 42] == "DF" || figur[loc - 42] == "RUELEPHANT" || figur[loc - 42] == "RUHORSE" || figur[loc - 42] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 42] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 42] = "RDELEPHANT"; }
                                                Empty(left + 48, top - 24);
                                                Elephant(left + 48, top - 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 14)
                                    {
                                        try
                                        {
                                            if (figur[loc - 49] == "Null" || figur[loc - 49] == "DP1" || figur[loc - 49] == "DP2" || figur[loc - 49] == "DP3" || figur[loc - 49] == "DP4" || figur[loc - 49] == "DP5" ||
                                                figur[loc - 49] == "DP6" || figur[loc - 49] == "DP7" || figur[loc - 49] == "DP8" || figur[loc - 49] == "LUTOWER" || figur[loc - 49] == "LUHORSE" || figur[loc - 49] == "LUELEPHANT" ||
                                                figur[loc - 49] == "DF" || figur[loc - 49] == "RUELEPHANT" || figur[loc - 49] == "RUHORSE" || figur[loc - 49] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc - 49] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc - 49] = "RDELEPHANT"; }
                                                Empty(left + 56, top - 28);
                                                Elephant(left + 56, top - 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 15)
                                    {
                                        try
                                        {
                                            if (figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                                figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8" || figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                                figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 9] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 9] = "RDELEPHANT"; }
                                                Empty(left + 8, top + 4);
                                                Elephant(left + 8, top + 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 16)
                                    {

                                        try
                                        {
                                            if (figur[loc + 18] == "Null" || figur[loc + 18] == "DP1" || figur[loc + 18] == "DP2" || figur[loc + 18] == "DP3" || figur[loc + 18] == "DP4" || figur[loc + 18] == "DP5" ||
                                                figur[loc + 18] == "DP6" || figur[loc + 18] == "DP7" || figur[loc + 18] == "DP8" || figur[loc + 18] == "LUTOWER" || figur[loc + 18] == "LUHORSE" || figur[loc + 18] == "LUELEPHANT" ||
                                                figur[loc + 18] == "DF" || figur[loc + 18] == "RUELEPHANT" || figur[loc + 18] == "RUHORSE" || figur[loc + 18] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 18] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 18] = "RDELEPHANT"; }
                                                Empty(left + 16, top + 8);
                                                Elephant(left + 16, top + 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 17)
                                    {
                                        try
                                        {
                                            if (figur[loc + 27] == "Null" || figur[loc + 27] == "DP1" || figur[loc + 27] == "DP2" || figur[loc + 27] == "DP3" || figur[loc + 27] == "DP4" || figur[loc + 27] == "DP5" ||
                                                figur[loc + 27] == "DP6" || figur[loc + 27] == "DP7" || figur[loc + 27] == "DP8" || figur[loc + 27] == "LUTOWER" || figur[loc + 27] == "LUHORSE" || figur[loc + 27] == "LUELEPHANT" ||
                                                figur[loc + 27] == "DF" || figur[loc + 27] == "RUELEPHANT" || figur[loc + 27] == "RUHORSE" || figur[loc + 27] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 27] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 27] = "RDELEPHANT"; }
                                                Empty(left + 24, top + 12);
                                                Elephant(left + 24, top + 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 18)
                                    {
                                        try
                                        {
                                            if (figur[loc + 36] == "Null" || figur[loc + 36] == "DP1" || figur[loc + 36] == "DP2" || figur[loc + 36] == "DP3" || figur[loc + 36] == "DP4" || figur[loc + 36] == "DP5" ||
                                                figur[loc + 36] == "DP6" || figur[loc + 36] == "DP7" || figur[loc + 36] == "DP8" || figur[loc + 36] == "LUTOWER" || figur[loc + 36] == "LUHORSE" || figur[loc + 36] == "LUELEPHANT" ||
                                                figur[loc + 36] == "DF" || figur[loc + 36] == "RUELEPHANT" || figur[loc + 36] == "RUHORSE" || figur[loc + 36] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 36] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 36] = "RDELEPHANT"; }
                                                Empty(left + 32, top + 16);
                                                Elephant(left + 32, top + 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 19)
                                    {
                                        try
                                        {
                                            if (figur[loc + 45] == "Null" || figur[loc + 45] == "DP1" || figur[loc + 45] == "DP2" || figur[loc + 45] == "DP3" || figur[loc + 45] == "DP4" || figur[loc + 45] == "DP5" ||
                                                figur[loc + 45] == "DP6" || figur[loc + 45] == "DP7" || figur[loc + 45] == "DP8" || figur[loc + 45] == "LUTOWER" || figur[loc + 45] == "LUHORSE" || figur[loc + 45] == "LUELEPHANT" ||
                                                figur[loc + 45] == "DF" || figur[loc + 45] == "RUELEPHANT" || figur[loc + 45] == "RUHORSE" || figur[loc + 45] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 45] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 45] = "RDELEPHANT"; }
                                                Empty(left + 40, top + 20);
                                                Elephant(left + 40, top + 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 20)
                                    {
                                        try
                                        {
                                            if (figur[loc + 54] == "Null" || figur[loc + 54] == "DP1" || figur[loc + 54] == "DP2" || figur[loc + 54] == "DP3" || figur[loc + 54] == "DP4" || figur[loc + 54] == "DP5" ||
                                                figur[loc + 54] == "DP6" || figur[loc + 54] == "DP7" || figur[loc + 54] == "DP8" || figur[loc + 54] == "LUTOWER" || figur[loc + 54] == "LUHORSE" || figur[loc + 54] == "LUELEPHANT" ||
                                                figur[loc + 54] == "DF" || figur[loc + 54] == "RUELEPHANT" || figur[loc + 54] == "RUHORSE" || figur[loc + 54] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 54] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 54] = "RDELEPHANT"; }
                                                Empty(left + 48, top + 24);
                                                Elephant(left + 48, top + 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 21)
                                    {
                                        try
                                        {
                                            if (figur[loc + 63] == "Null" || figur[loc + 63] == "DP1" || figur[loc + 63] == "DP2" || figur[loc + 63] == "DP3" || figur[loc + 63] == "DP4" || figur[loc + 63] == "DP5" ||
                                                figur[loc + 63] == "DP6" || figur[loc + 63] == "DP7" || figur[loc + 63] == "DP8" || figur[loc + 63] == "LUTOWER" || figur[loc + 63] == "LUHORSE" || figur[loc + 63] == "LUELEPHANT" ||
                                                figur[loc + 63] == "DF" || figur[loc + 63] == "RUELEPHANT" || figur[loc + 63] == "RUHORSE" || figur[loc + 63] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 63] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 63] = "RDELEPHANT"; }
                                                Empty(left + 56, top + 28);
                                                Elephant(left + 56, top + 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 22)
                                    {
                                        try
                                        {
                                            if (figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                                figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8" || figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                                figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 7] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 7] = "RDELEPHANT"; }
                                                Empty(left - 8, top + 4);
                                                Elephant(left - 8, top + 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 23)
                                    {

                                        try
                                        {
                                            if (figur[loc + 14] == "Null" || figur[loc + 14] == "DP1" || figur[loc + 14] == "DP2" || figur[loc + 14] == "DP3" || figur[loc + 14] == "DP4" || figur[loc + 14] == "DP5" ||
                                                figur[loc + 14] == "DP6" || figur[loc + 14] == "DP7" || figur[loc + 14] == "DP8" || figur[loc + 14] == "LUTOWER" || figur[loc + 14] == "LUHORSE" || figur[loc + 14] == "LUELEPHANT" ||
                                                figur[loc + 14] == "DF" || figur[loc + 14] == "RUELEPHANT" || figur[loc + 14] == "RUHORSE" || figur[loc + 14] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 14] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 14] = "RDELEPHANT"; }
                                                Empty(left - 16, top + 8);
                                                Elephant(left - 16, top + 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 24)
                                    {
                                        try
                                        {
                                            if (figur[loc + 21] == "Null" || figur[loc + 21] == "DP1" || figur[loc + 21] == "DP2" || figur[loc + 21] == "DP3" || figur[loc + 21] == "DP4" || figur[loc + 21] == "DP5" ||
                                                figur[loc + 21] == "DP6" || figur[loc + 21] == "DP7" || figur[loc + 21] == "DP8" || figur[loc + 21] == "LUTOWER" || figur[loc + 21] == "LUHORSE" || figur[loc + 21] == "LUELEPHANT" ||
                                                figur[loc + 21] == "DF" || figur[loc + 21] == "RUELEPHANT" || figur[loc + 21] == "RUHORSE" || figur[loc + 21] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 21] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 21] = "RDELEPHANT"; }
                                                Empty(left - 24, top + 12);
                                                Elephant(left - 24, top + 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 25)
                                    {
                                        try
                                        {
                                            if (figur[loc + 28] == "Null" || figur[loc + 28] == "DP1" || figur[loc + 28] == "DP2" || figur[loc + 28] == "DP3" || figur[loc + 28] == "DP4" || figur[loc + 28] == "DP5" ||
                                                figur[loc + 28] == "DP6" || figur[loc + 28] == "DP7" || figur[loc + 28] == "DP8" || figur[loc + 28] == "LUTOWER" || figur[loc + 28] == "LUHORSE" || figur[loc + 28] == "LUELEPHANT" ||
                                                figur[loc + 28] == "DF" || figur[loc + 28] == "RUELEPHANT" || figur[loc + 28] == "RUHORSE" || figur[loc + 28] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 28] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 28] = "RDELEPHANT"; }
                                                Empty(left - 32, top + 16);
                                                Elephant(left - 32, top + 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 26)
                                    {
                                        try
                                        {
                                            if (figur[loc + 35] == "Null" || figur[loc + 35] == "DP1" || figur[loc + 35] == "DP2" || figur[loc + 35] == "DP3" || figur[loc + 35] == "DP4" || figur[loc + 35] == "DP5" ||
                                                figur[loc + 35] == "DP6" || figur[loc + 35] == "DP7" || figur[loc + 35] == "DP8" || figur[loc + 35] == "LUTOWER" || figur[loc + 35] == "LUHORSE" || figur[loc + 35] == "LUELEPHANT" ||
                                                figur[loc + 35] == "DF" || figur[loc + 35] == "RUELEPHANT" || figur[loc + 35] == "RUHORSE" || figur[loc + 35] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 35] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 35] = "RDELEPHANT"; }
                                                Empty(left - 40, top + 20);
                                                Elephant(left - 40, top + 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 27)
                                    {
                                        try
                                        {
                                            if (figur[loc + 42] == "Null" || figur[loc + 42] == "DP1" || figur[loc + 42] == "DP2" || figur[loc + 42] == "DP3" || figur[loc + 42] == "DP4" || figur[loc + 42] == "DP5" ||
                                                figur[loc + 42] == "DP6" || figur[loc + 42] == "DP7" || figur[loc + 42] == "DP8" || figur[loc + 42] == "LUTOWER" || figur[loc + 42] == "LUHORSE" || figur[loc + 42] == "LUELEPHANT" ||
                                                figur[loc + 42] == "DF" || figur[loc + 42] == "RUELEPHANT" || figur[loc + 42] == "RUHORSE" || figur[loc + 42] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 42] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 42] = "RDELEPHANT"; }
                                                Empty(left - 48, top + 24);
                                                Elephant(left - 48, top + 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 28)
                                    {
                                        try
                                        {
                                            if (figur[loc + 49] == "Null" || figur[loc + 49] == "DP1" || figur[loc + 49] == "DP2" || figur[loc + 49] == "DP3" || figur[loc + 49] == "DP4" || figur[loc + 49] == "DP5" ||
                                                figur[loc + 49] == "DP6" || figur[loc + 49] == "DP7" || figur[loc + 49] == "DP8" || figur[loc + 49] == "LUTOWER" || figur[loc + 49] == "LUHORSE" || figur[loc + 49] == "LUELEPHANT" ||
                                                figur[loc + 49] == "DF" || figur[loc + 49] == "RUELEPHANT" || figur[loc + 49] == "RUHORSE" || figur[loc + 49] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDELEPHANT") { figur[loc + 49] = "LDELEPHANT"; }
                                                if (figur[loc] == "RDELEPHANT") { figur[loc + 49] = "RDELEPHANT"; }
                                                Empty(left - 56, top + 28);
                                                Elephant(left - 56, top + 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                n:
                                left = buff_left;
                                top = buff_top;
                                loc = buff_loc;
                                

                            }

                        }

                        if (figur[loc] == "RDTOWER" || figur[loc] == "LDTOWER")
                        {
                            int buff_dis = dis;
                            int buff_loc = loc;
                            buff_left = left;
                            buff_top = top;
                            int count = 0;
                            bool tru = true;
                            while (tru == true)
                            {
                                try
                                {
                                    if (((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                            figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                            figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER")) && left < 76)
                                    {
                                        while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                            figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                            figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
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
                                            loc += 1;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                            figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                            figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")) && left > 19)
                                    {
                                        while (left > 20 && left < 77 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                            figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                            figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER") && figur[loc - 1] != "LDHORSE" && figur[loc - 1] != "RDHORSE" &&
                                            figur[loc - 1] != "LDELEPHANT" && figur[loc - 1] != "RDELEPHANT" && figur[loc - 1] != "UP1" &&
                                            figur[loc - 1] != "UP2" && figur[loc - 1] != "UP3" && figur[loc - 1] != "UP4" && figur[loc - 1] != "UP5" && figur[loc - 1] != "UP6" && figur[loc - 1] != "UP7" &&
                                            figur[loc - 1] != "UP8")
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
                                            loc--;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                            figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                            figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")) && top < 35)
                                    {
                                        while (top > 5 && top < 34 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                            figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                            figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER") && figur[loc + 8] != "LDHORSE" && figur[loc + 8] != "RDHORSE" &&
                                            figur[loc + 8] != "LDELEPHANT" && figur[loc + 8] != "RDELEPHANT" && figur[loc + 8] != "UP1" &&
                                            figur[loc + 8] != "UP2" && figur[loc + 8] != "UP3" && figur[loc + 8] != "UP4" && figur[loc + 8] != "UP5" && figur[loc + 8] != "UP6" && figur[loc + 8] != "UP7" &&
                                            figur[loc + 8] != "UP8" && figur[loc + 8] != "WKING")
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
                                            loc += 8;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                            figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                            figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top > 5)
                                    {
                                        while (top > 6 && top < 35 && (figur[loc - 8] != "DP1" && figur[loc - 8] != "DP2" && figur[loc - 8] != "DP3" && figur[loc - 8] != "DP4" && figur[loc - 8] != "DP5" &&
                                            figur[loc - 8] != "DP6" && figur[loc - 8] != "DP7" && figur[loc - 8] != "DP8" && figur[loc - 8] != "LUTOWER" && figur[loc - 8] != "LUHORSE" && figur[loc - 8] != "LUELEPHANT" &&
                                            figur[loc - 8] != "DF" && figur[loc - 8] != "RUELEPHANT" && figur[loc - 8] != "RUHORSE" && figur[loc - 8] != "RUTOWER") && figur[loc - 8] != "LDHORSE" && figur[loc - 8] != "RDHORSE" &&
                                            figur[loc - 8] != "LDELEPHANT" && figur[loc - 8] != "RDELEPHANT" && figur[loc - 8] != "UP1" &&
                                            figur[loc - 8] != "UP2" && figur[loc - 8] != "UP3" && figur[loc - 8] != "UP4" && figur[loc - 8] != "UP5" && figur[loc - 8] != "UP6" && figur[loc - 8] != "UP7" &&
                                            figur[loc - 8] != "UP8" && figur[loc - 8] != "WKING")
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
                                            loc -= 8;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }





                                try
                                {
                                    if (((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                            figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                            figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER")) && left < 76)
                                    {
                                        count = 28;
                                        while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                            figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                            figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 29 || count == 30 || count == 31 || count == 32 || count == 33 || count == 34 || count == 35)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }
                                            if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                            else if (keypress.Key == ConsoleKey.Escape) { tru = false; goto n; }
                                            else if (keypress.Key == ConsoleKey.A)
                                            {
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                left += 8;
                                                loc += 1;
                                                count++;
                                            }
                                        }

                                    }
                                }
                                catch { }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();
                                try
                                {
                                    if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                            figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                            figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")) && left > 19)
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        Console.ResetColor();
                                        keypress = Console.ReadKey();
                                        while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                            figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                            figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
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
                                            loc += 1;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;

                                        count = 35;
                                        while (left > 20 && left < 77 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                        figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                        figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER"))
                                        {
                                            keypress = Console.ReadKey();
                                            if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                            else if (keypress.Key == ConsoleKey.Escape) { tru = false; goto n; }
                                            else if (keypress.Key == ConsoleKey.A)
                                            {
                                                keypress = Console.ReadKey();
                                                if (count == 36 || count == 37 || count == 38 || count == 39 || count == 40 || count == 41 || count == 42)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                    for (int i = 0; i < 4; i++)
                                                    {
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left += 7;
                                                        Console.SetCursorPosition(left, top + i);
                                                        Console.Write("-");
                                                        left -= 7;
                                                    }
                                                }



                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                left -= 8;
                                                loc--;
                                                count++;
                                            }
                                        }

                                    }
                                }
                                catch { }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                try
                                {
                                    if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                            figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                            figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")) && top < 35)
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        Console.ResetColor();
                                        keypress = Console.ReadKey();
                                        while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                            figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                            figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
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
                                            loc += 1;

                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                        while (left > 20 && left < 77 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                       figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                       figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER"))
                                        {
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 1;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 8;
                                            }
                                            left -= 8;
                                            loc--;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;

                                        count = 42;
                                        while (top > 5 && top < 34 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                            figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                            figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER"))
                                        {
                                            keypress = Console.ReadKey();
                                            if (count == 43 || count == 44 || count == 45 || count == 46 || count == 47 || count == 48 || count == 49)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }

                                            top += 4;
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
                                            loc += 8;
                                        }

                                    }
                                }
                                catch { }
                                loc = buff_loc;
                                left = buff_left;
                                top = buff_top;
                                try
                                {
                                    if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                            figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                            figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top > 5)
                                    {
                                        Console.SetCursorPosition(0, 0);
                                        Console.ResetColor();
                                        keypress = Console.ReadKey();
                                        while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                            figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                            figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
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
                                            loc += 1;

                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                        while (left > 20 && left < 77 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                       figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                       figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER"))
                                        {
                                            for (int i = 0; i < 4; i++)
                                            {
                                                left -= 1;
                                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left -= 7;
                                                Console.SetCursorPosition(left, top + i);
                                                Console.Write("-");
                                                left += 8;
                                            }
                                            left -= 8;
                                            loc--;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                        while (top > 5 && top < 34 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                            figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                            figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER"))
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
                                            loc += 8;
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;



                                        count = 49;
                                        while (top > 6 && top < 35 && (figur[loc - 8] != "DP1" && figur[loc - 8] != "DP2" && figur[loc - 8] != "DP3" && figur[loc - 8] != "DP4" && figur[loc - 8] != "DP5" &&
                                            figur[loc - 8] != "DP6" && figur[loc - 8] != "DP7" && figur[loc - 8] != "DP8" && figur[loc - 8] != "LUTOWER" && figur[loc - 8] != "LUHORSE" && figur[loc - 8] != "LUELEPHANT" &&
                                            figur[loc - 8] != "DF" && figur[loc - 8] != "RUELEPHANT" && figur[loc - 8] != "RUHORSE" && figur[loc - 8] != "RUTOWER"))
                                        {

                                            keypress = Console.ReadKey();
                                            if (count == 50 || count == 51 || count == 52 || count == 53 || count == 54 || count == 55 || count == 56)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                            }
                                            if (keypress.Key == ConsoleKey.Enter) { goto m; }
                                            else if (keypress.Key == ConsoleKey.Escape) { tru = false; goto n; }
                                            else if (keypress.Key == ConsoleKey.A)
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
                                                    left -= 7;
                                                }
                                                loc -= 8;
                                                count++;
                                            }
                                        }
                                        loc = buff_loc;
                                        left = buff_left;
                                        top = buff_top;
                                    }
                                }
                                catch { }





                            m:
                                Console.SetCursorPosition(0, 0);
                                keypress = Console.ReadKey();
                                Console.ResetColor();
                            n:
                                Console.SetCursorPosition(0, 0);
                                Console.ResetColor();



                                if (keypress.Key == ConsoleKey.Enter)
                                {
                                    if (count == 29)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                            figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                            figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 1] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 1] = "LDTOWER"; }
                                                Empty(left - 8, top);
                                                Tower(left - 8, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 30)
                                    {

                                        try
                                        {
                                            if (((figur[loc - 2] == "Null" || figur[loc - 2] == "DP1" || figur[loc - 2] == "DP2" || figur[loc - 2] == "DP3" || figur[loc - 2] == "DP4" || figur[loc - 2] == "DP5" ||
                                                  figur[loc - 2] == "DP6" || figur[loc - 2] == "DP7" || figur[loc - 2] == "DP8") || (figur[loc - 2] == "LUTOWER" || figur[loc - 2] == "LUHORSE" || figur[loc - 2] == "LUELEPHANT" ||
                                                  figur[loc - 2] == "DF" || figur[loc - 2] == "RUELEPHANT" || figur[loc - 2] == "RUHORSE" || figur[loc - 2] == "RUTOWER")) && left > 19)
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 2] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 2] = "LDTOWER"; }
                                                Empty(left - 16, top );
                                                Tower(left - 16, top , color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 31)
                                    {
                                        try
                                        {
                                            if ((figur[loc - 3] == "Null" || figur[loc - 3] == "DP1" || figur[loc - 3] == "DP2" || figur[loc - 3] == "DP3" || figur[loc - 3] == "DP4" || figur[loc - 3] == "DP5" ||
                                             figur[loc - 3] == "DP6" || figur[loc - 3] == "DP7" || figur[loc - 3] == "DP8" || figur[loc - 3] == "LUTOWER" || figur[loc - 3] == "LUHORSE" || figur[loc - 3] == "LUELEPHANT" ||
                                             figur[loc - 3] == "DF" || figur[loc - 3] == "RUELEPHANT" || figur[loc - 3] == "RUHORSE" || figur[loc - 3] == "RUTOWER"))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 3] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 3] = "LDTOWER"; }
                                                Empty(left - 24, top);
                                                Tower(left - 24, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 32)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 4] == "Null" || figur[loc - 4] == "DP1" || figur[loc - 4] == "DP2" || figur[loc - 4] == "DP3" || figur[loc - 4] == "DP4" || figur[loc - 4] == "DP5" ||
                                            figur[loc - 4] == "DP6" || figur[loc - 4] == "DP7" || figur[loc - 4] == "DP8") || (figur[loc - 4] == "LUTOWER" || figur[loc - 4] == "LUHORSE" || figur[loc - 4] == "LUELEPHANT" ||
                                            figur[loc - 4] == "DF" || figur[loc - 4] == "RUELEPHANT" || figur[loc - 4] == "RUHORSE" || figur[loc - 4] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 4] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 4] = "LDTOWER"; }
                                                Empty(left - 32, top );
                                                Tower(left - 32, top , color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 33)
                                    {
                                        try
                                        {
                                            if (figur[loc - 5] == "Null" || figur[loc - 5] == "DP1" || figur[loc - 5] == "DP2" || figur[loc - 5] == "DP3" || figur[loc - 5] == "DP4" || figur[loc - 5] == "DP5" ||
                                                figur[loc - 5] == "DP6" || figur[loc - 5] == "DP7" || figur[loc - 5] == "DP8" || figur[loc - 5] == "LUTOWER" || figur[loc - 5] == "LUHORSE" || figur[loc - 5] == "LUELEPHANT" ||
                                                figur[loc - 5] == "DF" || figur[loc - 5] == "RUELEPHANT" || figur[loc - 5] == "RUHORSE" || figur[loc - 5] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 5] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 5] = "LDTOWER"; }
                                                Empty(left - 40, top);
                                                Tower(left - 40, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 34)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 6] == "Null" || figur[loc - 6] == "DP1" || figur[loc - 6] == "DP2" || figur[loc - 6] == "DP3" || figur[loc - 6] == "DP4" || figur[loc - 6] == "DP5" ||
                                            figur[loc - 6] == "DP6" || figur[loc - 6] == "DP7" || figur[loc - 6] == "DP8") || (figur[loc - 6] == "LUTOWER" || figur[loc - 6] == "LUHORSE" || figur[loc - 6] == "LUELEPHANT" ||
                                            figur[loc - 6] == "DF" || figur[loc - 6] == "RUELEPHANT" || figur[loc - 6] == "RUHORSE" || figur[loc - 6] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 6] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 6] = "LDTOWER"; }
                                                Empty(left - 48, top);
                                                Tower(left - 48, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 35)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                            figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8") || (figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                            figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc - 7] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc - 7] = "LDTOWER"; }
                                                Empty(left - 56, top );
                                                Tower(left - 56, top,  color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 36)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                            figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                            figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "RDTOWER") { figur[loc + 1] = "RDTOWER"; }
                                                else if (figur[loc] == "LDTOWER") { figur[loc + 1] = "LDTOWER"; }
                                                Empty(left + 8, top );
                                                Tower(left + 8, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 37)
                                    {

                                        try
                                        {
                                            if (((figur[loc + 2] == "Null" || figur[loc + 2] == "DP1" || figur[loc + 2] == "DP2" || figur[loc + 2] == "DP3" || figur[loc + 2] == "DP4" || figur[loc + 2] == "DP5" ||
                                            figur[loc + 2] == "DP6" || figur[loc + 2] == "DP7" || figur[loc + 2] == "DP8") || (figur[loc + 2] == "LUTOWER" || figur[loc + 2] == "LUHORSE" || figur[loc + 2] == "LUELEPHANT" ||
                                            figur[loc + 2] == "DF" || figur[loc + 2] == "RUELEPHANT" || figur[loc + 2] == "RUHORSE" || figur[loc + 2] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 2] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 2] = "RDTOWER"; }
                                                Empty(left + 16, top);
                                                Tower(left + 16, top , color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 38)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 3] == "Null" || figur[loc + 3] == "DP1" || figur[loc + 3] == "DP2" || figur[loc + 3] == "DP3" || figur[loc + 3] == "DP4" || figur[loc + 3] == "DP5" ||
                                            figur[loc + 3] == "DP6" || figur[loc + 3] == "DP7" || figur[loc + 3] == "DP8") || (figur[loc + 3] == "LUTOWER" || figur[loc + 3] == "LUHORSE" || figur[loc + 3] == "LUELEPHANT" ||
                                            figur[loc + 3] == "DF" || figur[loc + 3] == "RUELEPHANT" || figur[loc + 3] == "RUHORSE" || figur[loc + 3] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 3] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 3] = "RDTOWER"; }
                                                Empty(left + 24, top);
                                                Tower(left + 24, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 39)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 4] == "Null" || figur[loc + 4] == "DP1" || figur[loc + 4] == "DP2" || figur[loc + 4] == "DP3" || figur[loc + 4] == "DP4" || figur[loc + 4] == "DP5" ||
                                            figur[loc + 4] == "DP6" || figur[loc + 4] == "DP7" || figur[loc + 4] == "DP8") || (figur[loc + 4] == "LUTOWER" || figur[loc + 4] == "LUHORSE" || figur[loc + 4] == "LUELEPHANT" ||
                                            figur[loc + 4] == "DF" || figur[loc + 4] == "RUELEPHANT" || figur[loc + 4] == "RUHORSE" || figur[loc + 4] == "RUTOWER")) )
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 28] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 28] = "RDTOWER"; }
                                                Empty(left + 32, top );
                                                Tower(left + 32, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 40)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 5] == "Null" || figur[loc + 5] == "DP1" || figur[loc + 5] == "DP2" || figur[loc + 5] == "DP3" || figur[loc + 5] == "DP4" || figur[loc + 5] == "DP5" ||
                                            figur[loc + 5] == "DP6" || figur[loc + 5] == "DP7" || figur[loc + 5] == "DP8") || (figur[loc + 5] == "LUTOWER" || figur[loc + 5] == "LUHORSE" || figur[loc + 5] == "LUELEPHANT" ||
                                            figur[loc + 5] == "DF" || figur[loc + 5] == "RUELEPHANT" || figur[loc + 5] == "RUHORSE" || figur[loc + 5] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 5] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 5] = "RDTOWER"; }
                                                Empty(left + 40, top);
                                                Tower(left + 40, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 41)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 6] == "Null" || figur[loc + 6] == "DP1" || figur[loc + 6] == "DP2" || figur[loc + 6] == "DP3" || figur[loc + 6] == "DP4" || figur[loc + 6] == "DP5" ||
                                            figur[loc + 6] == "DP6" || figur[loc + 6] == "DP7" || figur[loc + 6] == "DP8") || (figur[loc + 6] == "LUTOWER" || figur[loc + 6] == "LUHORSE" || figur[loc + 6] == "LUELEPHANT" ||
                                            figur[loc + 6] == "DF" || figur[loc + 6] == "RUELEPHANT" || figur[loc + 6] == "RUHORSE" || figur[loc + 6] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 6] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 6] = "RDTOWER"; }
                                                Empty(left + 48, top);
                                                Tower(left + 48, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 42)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                            figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8") || (figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                            figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 7] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 7] = "RDTOWER"; }
                                                Empty(left + 56, top);
                                                Tower(left + 56, top, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 43)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                            figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                            figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 9] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 9] = "RDTOWER"; }
                                                Empty(left , top + 4);
                                                Tower(left, top + 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 44)
                                    {

                                        try
                                        {
                                            if (((figur[loc + 16] == "Null" || figur[loc + 16] == "DP1" || figur[loc + 16] == "DP2" || figur[loc + 16] == "DP3" || figur[loc + 16] == "DP4" || figur[loc + 16] == "DP5" ||
                                            figur[loc + 16] == "DP6" || figur[loc + 16] == "DP7" || figur[loc + 16] == "DP8") || (figur[loc + 16] == "LUTOWER" || figur[loc + 16] == "LUHORSE" || figur[loc + 16] == "LUELEPHANT" ||
                                            figur[loc + 16] == "DF" || figur[loc + 16] == "RUELEPHANT" || figur[loc + 16] == "RUHORSE" || figur[loc + 16] == "RUTOWER")) )
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 16] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 16] = "RDTOWER"; }
                                                Empty(left, top + 8);
                                                Tower(left, top + 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 45)
                                    {
                                        try
                                        {
                                            if (((figur[loc + 24] == "Null" || figur[loc + 24] == "DP1" || figur[loc + 24] == "DP2" || figur[loc + 24] == "DP3" || figur[loc + 24] == "DP4" || figur[loc + 24] == "DP5" ||
                                            figur[loc + 24] == "DP6" || figur[loc + 24] == "DP7" || figur[loc + 24] == "DP8") || (figur[loc + 24] == "LUTOWER" || figur[loc + 24] == "LUHORSE" || figur[loc + 24] == "LUELEPHANT" ||
                                            figur[loc + 24] == "DF" || figur[loc + 24] == "RUELEPHANT" || figur[loc + 24] == "RUHORSE" || figur[loc + 24] == "RUTOWER")) )
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 24] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 24] = "RDTOWER"; }
                                                Empty(left, top + 12);
                                                Tower(left, top + 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 46)
                                    {
                                        try
                                        {
                                            if (figur[loc + 32] == "Null" || figur[loc + 32] == "DP1" || figur[loc + 32] == "DP2" || figur[loc + 32] == "DP3" || figur[loc + 32] == "DP4" || figur[loc + 32] == "DP5" ||
                                                figur[loc + 32] == "DP6" || figur[loc + 32] == "DP7" || figur[loc + 32] == "DP8" || figur[loc + 32] == "LUTOWER" || figur[loc + 32] == "LUHORSE" || figur[loc + 32] == "LUELEPHANT" ||
                                                figur[loc + 32] == "DF" || figur[loc + 32] == "RUELEPHANT" || figur[loc + 32] == "RUHORSE" || figur[loc + 32] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 32] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 32] = "RDTOWER"; }
                                                Empty(left, top + 16);
                                                Tower(left, top + 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 47)
                                    {
                                        try
                                        {
                                            if (figur[loc + 40] == "Null" || figur[loc + 40] == "DP1" || figur[loc + 40] == "DP2" || figur[loc + 40] == "DP3" || figur[loc + 40] == "DP4" || figur[loc + 40] == "DP5" ||
                                                figur[loc + 40] == "DP6" || figur[loc + 40] == "DP7" || figur[loc + 40] == "DP8" || figur[loc + 40] == "LUTOWER" || figur[loc + 40] == "LUHORSE" || figur[loc + 40] == "LUELEPHANT" ||
                                                figur[loc + 40] == "DF" || figur[loc + 40] == "RUELEPHANT" || figur[loc + 40] == "RUHORSE" || figur[loc + 40] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 40] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 40] = "RDTOWER"; }
                                                Empty(left , top + 20);
                                                Tower(left , top + 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 48)
                                    {
                                        try
                                        {
                                            if (figur[loc + 48] == "Null" || figur[loc + 48] == "DP1" || figur[loc + 48] == "DP2" || figur[loc + 48] == "DP3" || figur[loc + 48] == "DP4" || figur[loc + 48] == "DP5" ||
                                                figur[loc + 48] == "DP6" || figur[loc + 48] == "DP7" || figur[loc + 48] == "DP8" || figur[loc + 48] == "LUTOWER" || figur[loc + 48] == "LUHORSE" || figur[loc + 48] == "LUELEPHANT" ||
                                                figur[loc + 48] == "DF" || figur[loc + 48] == "RUELEPHANT" || figur[loc + 48] == "RUHORSE" || figur[loc + 48] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 48] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 48] = "RDTOWER"; }
                                                Empty(left , top + 24);
                                                Tower(left , top + 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 49)
                                    {
                                        try
                                        {
                                            if (figur[loc + 56] == "Null" || figur[loc + 56] == "DP1" || figur[loc + 56] == "DP2" || figur[loc + 56] == "DP3" || figur[loc + 56] == "DP4" || figur[loc + 56] == "DP5" ||
                                                figur[loc + 56] == "DP6" || figur[loc + 56] == "DP7" || figur[loc + 56] == "DP8" || figur[loc + 56] == "LUTOWER" || figur[loc + 56] == "LUHORSE" || figur[loc + 56] == "LUELEPHANT" ||
                                                figur[loc + 56] == "DF" || figur[loc + 56] == "RUELEPHANT" || figur[loc + 56] == "RUHORSE" || figur[loc + 56] == "RUTOWER")
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 56] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 56] = "RDTOWER"; }
                                                Empty(left , top + 28);
                                                Tower(left , top + 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 50)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                            figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                            figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 8] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 8] = "RDTOWER"; }
                                                Empty(left , top - 4);
                                                Tower(left , top - 4, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 51)
                                    {

                                        try
                                        {
                                            if (((figur[loc - 16] == "Null" || figur[loc - 16] == "DP1" || figur[loc - 16] == "DP2" || figur[loc - 16] == "DP3" || figur[loc - 16] == "DP4" || figur[loc - 16] == "DP5" ||
                                            figur[loc - 16] == "DP6" || figur[loc - 16] == "DP7" || figur[loc - 16] == "DP8") || (figur[loc - 16] == "LUTOWER" || figur[loc - 16] == "LUHORSE" || figur[loc - 16] == "LUELEPHANT" ||
                                            figur[loc - 16] == "DF" || figur[loc - 16] == "RUELEPHANT" || figur[loc - 16] == "RUHORSE" || figur[loc - 16] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 16] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 16] = "RDTOWER"; }
                                                Empty(left , top - 8);
                                                Tower(left , top - 8, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 52)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 24] == "Null" || figur[loc - 24] == "DP1" || figur[loc - 24] == "DP2" || figur[loc - 24] == "DP3" || figur[loc - 24] == "DP4" || figur[loc - 24] == "DP5" ||
                                            figur[loc - 24] == "DP6" || figur[loc - 24] == "DP7" || figur[loc - 24] == "DP8") || (figur[loc - 24] == "LUTOWER" || figur[loc - 24] == "LUHORSE" || figur[loc - 24] == "LUELEPHANT" ||
                                            figur[loc - 24] == "DF" || figur[loc - 24] == "RUELEPHANT" || figur[loc - 24] == "RUHORSE" || figur[loc - 24] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 24] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 24] = "RDTOWER"; }
                                                Empty(left, top - 12);
                                                Tower(left, top - 12, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }

                                    }
                                    else if (count == 53)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 32] == "Null" || figur[loc - 32] == "DP1" || figur[loc - 32] == "DP2" || figur[loc - 32] == "DP3" || figur[loc - 32] == "DP4" || figur[loc - 32] == "DP5" ||
                                            figur[loc - 32] == "DP6" || figur[loc - 32] == "DP7" || figur[loc - 32] == "DP8") || (figur[loc - 32] == "LUTOWER" || figur[loc - 32] == "LUHORSE" || figur[loc - 32] == "LUELEPHANT" ||
                                            figur[loc - 32] == "DF" || figur[loc - 32] == "RUELEPHANT" || figur[loc - 32] == "RUHORSE" || figur[loc - 32] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 32] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 32] = "RDTOWER"; }
                                                Empty(left , top - 16);
                                                Tower(left , top - 16, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 54)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 40] == "Null" || figur[loc - 40] == "DP1" || figur[loc - 40] == "DP2" || figur[loc - 40] == "DP3" || figur[loc - 40] == "DP4" || figur[loc - 40] == "DP5" ||
                                            figur[loc - 40] == "DP6" || figur[loc - 40] == "DP7" || figur[loc - 40] == "DP8") || (figur[loc - 40] == "LUTOWER" || figur[loc - 40] == "LUHORSE" || figur[loc - 40] == "LUELEPHANT" ||
                                            figur[loc - 40] == "DF" || figur[loc - 40] == "RUELEPHANT" || figur[loc - 40] == "RUHORSE" || figur[loc - 40] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc + 35] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc + 35] = "RDTOWER"; }
                                                Empty(left, top - 20);
                                                Tower(left , top - 20, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 55)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 48] == "Null" || figur[loc - 48] == "DP1" || figur[loc - 48] == "DP2" || figur[loc - 48] == "DP3" || figur[loc - 48] == "DP4" || figur[loc - 48] == "DP5" ||
                                            figur[loc - 48] == "DP6" || figur[loc - 48] == "DP7" || figur[loc - 48] == "DP8") || (figur[loc - 48] == "LUTOWER" || figur[loc - 48] == "LUHORSE" || figur[loc - 48] == "LUELEPHANT" ||
                                            figur[loc - 48] == "DF" || figur[loc - 48] == "RUELEPHANT" || figur[loc - 48] == "RUHORSE" || figur[loc - 48] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 48] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 48] = "RDTOWER"; }
                                                Empty(left, top - 24);
                                                Tower(left, top - 24, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }
                                    else if (count == 56)
                                    {
                                        try
                                        {
                                            if (((figur[loc - 56] == "Null" || figur[loc - 56] == "DP1" || figur[loc - 56] == "DP2" || figur[loc - 56] == "DP3" || figur[loc - 56] == "DP4" || figur[loc - 56] == "DP5" ||
                                            figur[loc - 56] == "DP6" || figur[loc - 56] == "DP7" || figur[loc - 56] == "DP8") || (figur[loc - 56] == "LUTOWER" || figur[loc - 56] == "LUHORSE" || figur[loc - 56] == "LUELEPHANT" ||
                                            figur[loc - 56] == "DF" || figur[loc - 56] == "RUELEPHANT" || figur[loc - 56] == "RUHORSE" || figur[loc - 56] == "RUTOWER")))
                                            {
                                                Empty(left, top);
                                                if (figur[loc] == "LDTOWER") { figur[loc - 56] = "LDTOWER"; }
                                                else if (figur[loc] == "RDTOWER") { figur[loc - 56] = "RDTOWER"; }
                                                Empty(left , top - 28);
                                                Tower(left , top - 28, color_chess);
                                                figur[loc] = "Null";
                                                tru = false;
                                            }
                                        }
                                        catch { }
                                    }









                                    try
                                    {
                                        if (((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                                figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                                figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER")) && left < 76)
                                        {
                                            while (left > 19 && left < 76 && (figur[loc] != "DP1" || figur[loc] != "DP2" || figur[loc] != "DP3" || figur[loc] != "DP4" || figur[loc] != "DP5" ||
                                                figur[loc] != "DP6" || figur[loc] != "DP7" || figur[loc] != "DP8" || figur[loc] != "LUTOWER" || figur[loc] != "LUHORSE" || figur[loc] != "LUELEPHANT" ||
                                                figur[loc] != "DF" || figur[loc] != "RUELEPHANT" || figur[loc] != "RUHORSE" || figur[loc] != "RUTOWER") && figur[loc + 1] != "LDHORSE" && figur[loc + 1] != "RDHORSE" &&
                                                figur[loc + 1] != "LDELEPHANT" && figur[loc + 1] != "RDELEPHANT" && figur[loc + 1] != "LDTOWER" && figur[loc + 1] != "RDTOWER" && figur[loc + 1] != "UP1" &&
                                                figur[loc + 1] != "UP2" && figur[loc + 1] != "UP3" && figur[loc + 1] != "UP4" && figur[loc + 1] != "UP5" && figur[loc + 1] != "UP6" && figur[loc + 1] != "UP7" &&
                                                figur[loc + 1] != "UP8" && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
                                            {
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left += 8;
                                                    Frame();
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 15;
                                                }
                                                dis++;
                                                left += 8;
                                                loc += 1;
                                            }
                                            dis = buff_dis;
                                            loc = buff_loc;
                                            left = buff_left;
                                            top = buff_top;
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                                figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                                figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")) && left > 19)
                                        {
                                            while (left > 20 && left < 77 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                                figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                                figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
                                            {
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    left -= 1;
                                                    Frame();
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 8;
                                                }
                                                dis++;
                                                left -= 8;
                                                loc--;
                                            }
                                            dis = buff_dis;
                                            loc = buff_loc;
                                            left = buff_left;
                                            top = buff_top;
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                                figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                                figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")) && top < 35)
                                        {
                                            while (top > 5 && top < 34 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                                figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                                figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
                                            {
                                                top += 4;
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    Frame();
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                                dis++;
                                                loc += 8;

                                            }
                                            dis = buff_dis;
                                            loc = buff_loc;
                                            left = buff_left;
                                            top = buff_top;
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                                figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                                figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top > 5)
                                        {
                                            while (top > 6 && top < 35 && (figur[loc] != "DP1" && figur[loc] != "DP2" && figur[loc] != "DP3" && figur[loc] != "DP4" && figur[loc] != "DP5" &&
                                                figur[loc] != "DP6" && figur[loc] != "DP7" && figur[loc] != "DP8" && figur[loc] != "LUTOWER" && figur[loc] != "LUHORSE" && figur[loc] != "LUELEPHANT" &&
                                                figur[loc] != "DF" && figur[loc] != "RUELEPHANT" && figur[loc] != "RUHORSE" && figur[loc] != "RUTOWER") && figur[loc + 8] != "WKING" && figur[loc] != "WKING")
                                            {
                                                top -= 4;
                                                for (int i = 0; i < 4; i++)
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
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left += 7;
                                                    Console.SetCursorPosition(left, top + i);
                                                    Console.Write("-");
                                                    left -= 7;
                                                }
                                                dis++;
                                                loc -= 8;
                                            }
                                            dis = buff_dis;
                                            loc = buff_loc;
                                            left = buff_left;
                                            top = buff_top;
                                        }
                                    }
                                    catch { }



                                    Console.SetCursorPosition(0, 0);
                                    Console.ResetColor();


                                }
                            }
                        }

                        if (figur[loc] == "WF")
                        {
                            







                           


                        }
                        if (figur[loc] == "DKING" || figur[loc] == "WKING")
                        {

                            try
                            {
                                if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                        figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                        figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")) && top < 35)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                        figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                        figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top > 5)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if ((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                        figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                        figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER") && left < 76)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                        figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                        figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")) && left > 20)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if (((figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                        figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8") || (figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                        figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER")) && top > 5)
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
                                if (((figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                        figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8") || (figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                        figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER")) && top < 35)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if (((figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                        figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8") || (figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                        figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER")) && top < 35)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();
                            try
                            {
                                if (((figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                        figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8") || (figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                        figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER") && top < 35)
)
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
                            Console.SetCursorPosition(0, 0);
                            Console.ResetColor();

                            Console.SetCursorPosition(0, 0);
                            Console.ReadKey();
                            Console.ResetColor();





                            try
                            {
                                if (((figur[loc + 8] == "Null" || figur[loc + 8] == "DP1" || figur[loc + 8] == "DP2" || figur[loc + 8] == "DP3" || figur[loc + 8] == "DP4" || figur[loc + 8] == "DP5" ||
                                        figur[loc + 8] == "DP6" || figur[loc + 8] == "DP7" || figur[loc + 8] == "DP8") || (figur[loc + 8] == "LUTOWER" || figur[loc + 8] == "LUHORSE" || figur[loc + 8] == "LUELEPHANT" ||
                                        figur[loc + 8] == "DF" || figur[loc + 8] == "RUELEPHANT" || figur[loc + 8] == "RUHORSE" || figur[loc + 8] == "RUTOWER")) && top < 35)
                                {
                                    top += 4;
                                    for (int i = 0; i < 4; i++)
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
                                if (((figur[loc - 8] == "Null" || figur[loc - 8] == "DP1" || figur[loc - 8] == "DP2" || figur[loc - 8] == "DP3" || figur[loc - 8] == "DP4" || figur[loc - 8] == "DP5" ||
                                        figur[loc - 8] == "DP6" || figur[loc - 8] == "DP7" || figur[loc - 8] == "DP8") || (figur[loc - 8] == "LUTOWER" || figur[loc - 8] == "LUHORSE" || figur[loc - 8] == "LUELEPHANT" ||
                                        figur[loc - 8] == "DF" || figur[loc - 8] == "RUELEPHANT" || figur[loc - 8] == "RUHORSE" || figur[loc - 8] == "RUTOWER")) && top > 5)
                                {
                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
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
                                if ((figur[loc + 1] == "Null" || figur[loc + 1] == "DP1" || figur[loc + 1] == "DP2" || figur[loc + 1] == "DP3" || figur[loc + 1] == "DP4" || figur[loc + 1] == "DP5" ||
                                        figur[loc + 1] == "DP6" || figur[loc + 1] == "DP7" || figur[loc + 1] == "DP8") || (figur[loc + 1] == "LUTOWER" || figur[loc + 1] == "LUHORSE" || figur[loc + 1] == "LUELEPHANT" ||
                                        figur[loc + 1] == "DF" || figur[loc + 1] == "RUELEPHANT" || figur[loc + 1] == "RUHORSE" || figur[loc + 1] == "RUTOWER") && left < 76)
                                {
                                    left += 8;
                                    for (int i = 0; i < 4; i++)
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
                                if (((figur[loc - 1] == "Null" || figur[loc - 1] == "DP1" || figur[loc - 1] == "DP2" || figur[loc - 1] == "DP3" || figur[loc - 1] == "DP4" || figur[loc - 1] == "DP5" ||
                                        figur[loc - 1] == "DP6" || figur[loc - 1] == "DP7" || figur[loc - 1] == "DP8") || (figur[loc - 1] == "LUTOWER" || figur[loc - 1] == "LUHORSE" || figur[loc - 1] == "LUELEPHANT" ||
                                        figur[loc - 1] == "DF" || figur[loc - 1] == "RUELEPHANT" || figur[loc - 1] == "RUHORSE" || figur[loc - 1] == "RUTOWER")) && left > 20)
                                {
                                    left -= 8;
                                    for (int i = 0; i < 4; i++)
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
                                if (((figur[loc - 9] == "Null" || figur[loc - 9] == "DP1" || figur[loc - 9] == "DP2" || figur[loc - 9] == "DP3" || figur[loc - 9] == "DP4" || figur[loc - 9] == "DP5" ||
                                        figur[loc - 9] == "DP6" || figur[loc - 9] == "DP7" || figur[loc - 9] == "DP8") || (figur[loc - 9] == "LUTOWER" || figur[loc - 9] == "LUHORSE" || figur[loc - 9] == "LUELEPHANT" ||
                                        figur[loc - 9] == "DF" || figur[loc - 9] == "RUELEPHANT" || figur[loc - 9] == "RUHORSE" || figur[loc - 9] == "RUTOWER")) && top > 5)
                                {


                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        if (dis % 2 != 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                        }
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
                                if (((figur[loc - 7] == "Null" || figur[loc - 7] == "DP1" || figur[loc - 7] == "DP2" || figur[loc - 7] == "DP3" || figur[loc - 7] == "DP4" || figur[loc - 7] == "DP5" ||
                                        figur[loc - 7] == "DP6" || figur[loc - 7] == "DP7" || figur[loc - 7] == "DP8") || (figur[loc - 7] == "LUTOWER" || figur[loc - 7] == "LUHORSE" || figur[loc - 7] == "LUELEPHANT" ||
                                        figur[loc - 7] == "DF" || figur[loc - 7] == "RUELEPHANT" || figur[loc - 7] == "RUHORSE" || figur[loc - 7] == "RUTOWER")) && top > 5)
                                {

                                    top -= 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        if (dis % 2 != 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                        }
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
                                if (((figur[loc + 9] == "Null" || figur[loc + 9] == "DP1" || figur[loc + 9] == "DP2" || figur[loc + 9] == "DP3" || figur[loc + 9] == "DP4" || figur[loc + 9] == "DP5" ||
                                        figur[loc + 9] == "DP6" || figur[loc + 9] == "DP7" || figur[loc + 9] == "DP8") || (figur[loc + 9] == "LUTOWER" || figur[loc + 9] == "LUHORSE" || figur[loc + 9] == "LUELEPHANT" ||
                                        figur[loc + 9] == "DF" || figur[loc + 9] == "RUELEPHANT" || figur[loc + 9] == "RUHORSE" || figur[loc + 9] == "RUTOWER")) && top < 35)
                                {

                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left += 8;
                                        if (dis % 2 != 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                        }
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
                                if (((figur[loc + 7] == "Null" || figur[loc + 7] == "DP1" || figur[loc + 7] == "DP2" || figur[loc + 7] == "DP3" || figur[loc + 7] == "DP4" || figur[loc + 7] == "DP5" ||
                                        figur[loc + 7] == "DP6" || figur[loc + 7] == "DP7" || figur[loc + 7] == "DP8") || (figur[loc + 7] == "LUTOWER" || figur[loc + 7] == "LUHORSE" || figur[loc + 7] == "LUELEPHANT" ||
                                        figur[loc + 7] == "DF" || figur[loc + 7] == "RUELEPHANT" || figur[loc + 7] == "RUHORSE" || figur[loc + 7] == "RUTOWER") && top < 35)
)
                                {

                                    top += 4;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        left -= 1;
                                        if (dis % 2 != 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.BackgroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.BackgroundColor = ConsoleColor.Yellow;
                                        }
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
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);


            Console.Title = "Chess";
            Program program = new Program();
            program.menu();
            program.Portrayal();
            while (true)
            {
                program.SelectFigure();
            }

        }

        void Elephant(int left, int top, bool step)
        {
            Color(step);
            Console.SetCursorPosition(left + 1, top);
            Console.Write("------");
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write("------");
            Console.SetCursorPosition(left + 3, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("------");
        }
        void Horse(int left, int top, bool step)
        {
            Color(step);
            Console.SetCursorPosition(left, top);
            Console.Write("-----");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("---");
            Console.SetCursorPosition(left + 2, top + 2);
            Console.Write("---");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("------");
        }
        void Tower(int left, int top, bool step)
        {
            Color(step);
            Console.SetCursorPosition(left + 1, top);
            Console.Write("|_||_|");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("_||_");
            Console.SetCursorPosition(left + 2, top + 2);
            Console.Write("____");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("|____|");
        }
        void Queen(int left, int top, bool step)
        {
            Color(step);
            Console.SetCursorPosition(left + 3, top);
            Console.Write("--");
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("----");
            Console.SetCursorPosition(left + 3, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 1, top + 3);
            Console.Write("------");

        }
        void King(int left, int top, bool step)
        {
            Color(step);
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
        void Pawn(int left, int top, bool step)
        {
            Color(step);
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write("----");
            Console.SetCursorPosition(left + 3, top + 2);
            Console.Write("--");
            Console.SetCursorPosition(left + 2, top + 3);
            Console.Write("----");
        }
        void Empty(int left, int top)
        {
            Frame();
            Console.SetCursorPosition(left, top);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 1);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 2);
            Console.Write("--------");
            Console.SetCursorPosition(left, top + 3);
            Console.Write("--------");

        }

        void Color(bool step)
        {
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
        }

        void Frame()
        {
            if (dis % 2 != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
        }

    }
}
