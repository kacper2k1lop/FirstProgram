using System;
using System.Collections.Generic;

namespace KolkoKrzyzyk
{
    class Program
    {
        static string[] tablica = new string[9];
        static string[] symbols = new string[2];
        static string player1 = "";
        static string player2 = "";

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Oto gra w kółko i krzyżyk.");
            player1 = ProvideName(0);
            player2 = ProvideName(1);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Imiona graczy to: {0} i {1}.", player1, player2);
            //Game(names);
            Game();
            Console.ReadKey();
        }
        static string ProvideName(int number)
        {
            string name = "";
            bool test = false;
            while (test == false)
            {
                if (number == 0) Console.WriteLine("Podaj imię pierwszego gracza: ");
                if (number == 1) Console.WriteLine("Podaj imię drugiego gracza: ");
                try
                {
                    name = Console.ReadLine();
                    test = true;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Podałeś nieprawidłową wartość. Złapałeś wyjatek: " + e.Message);
                    Console.WriteLine();
                    //trow;
                }
            }
            return name;
        }
        static void CreateMatrix()
        {
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                tablica[i] = " ";
            }
        }
        static void Show()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("{0} {1} {2}", HasValue(3 * i), HasValue(3 * i + 1), HasValue(3 * i + 2));
            }
        }
        static string HasValue(int number)
        {
            if (tablica[number] == " ")
                return (number+1).ToString();
            else return tablica[number];
        }
        static bool ValidateWinner()
        {
            bool score = false;
            int[] score0 = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] scoreX = { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                if (i % 3 == 0)
                {
                    if (tablica[i] == symbols[0]) { scoreX[0] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[0] += 1; }
                    //0,3,6 lewa kolumna
                }
                if ((i + 1) % 3 == 0)
                {
                    if (tablica[i] == symbols[0]) { scoreX[1] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[1] += 1; }
                    //srodkowa kolumna
                }
                if ((i + 2) % 3 == 0)
                {
                    if (tablica[i] == symbols[0]) { scoreX[2] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[2] += 1; }
                    //prawa kolumna
                }
                if (i % 4 == 0)//przekatna 0 4 8
                {
                    if (tablica[i] == symbols[0]) { scoreX[6] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[6] += 1; }
                    //przekatna 0 4 8
                }
                if (i == 2 || i == 4 | i == 6)
                {
                    if (tablica[i] == symbols[0]) { scoreX[7] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[7] += 1; }
                    //druga przekatna
                }
            }
            for (int i = 0; i < 3; i++) //wiersze
            {
                if (tablica[i] != " ")
                {
                    if (tablica[i] == symbols[0]) { scoreX[3] += 1; }
                    else if (tablica[i] == symbols[1]) { score0[3] += 1; }
                    //gorny wiersz
                }
                if (tablica[i + 3] != " ")
                {
                    if (tablica[i + 3] == symbols[0]) { scoreX[4] += 1; }
                    else if (tablica[i + 3] == symbols[1]) { score0[4] += 1; }
                    //srodkowy wiersz
                }
                if (tablica[i + 6] != " ")
                {
                    if (tablica[i + 6] == symbols[0]) { scoreX[5] += 1; }
                    else if (tablica[i + 6] == symbols[1]) { score0[5] += 1; }
                    //dolny wiersz
                }
            }
            for (int i = 0; i < score0.Length; i++)
            {
                if (score0[i] == 3)
                {
                    Console.WriteLine("Wygrał gracz {0}.", player2);
                    score = true;
                }
            }
            for (int i = 0; i < scoreX.Length; i++)
            {
                if (scoreX[i] == 3)
                {
                    Console.WriteLine("Wygrał gracz {0}.", player1);
                    score = true;
                }
            }
            return score;
        }
        static bool Continue()
        {
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                if (tablica[i] == " ")
                {
                    return true;
                }
            }
            return false;
        }
        static bool CheckScore()
        {
            // Wygrana, jezeli wygrany zwraca true, jezeli nikt nie wygral zwraca false
            if (ValidateWinner())
            {
                return true;
            }
            //walidacja, jezeli kontynuowac; sprawdza, czy jest puste " " pole
            if (Continue())
            {
                Console.WriteLine("Jeszcze nikt nie wygrał. Gra zostanie zakończona, gdy jeden z graczy wygra, badź wszystkie pola zostaną wypełnione.");
                return false;
            }
            //walidacja czy case z brakiem wygranego
            Console.WriteLine("Nikt nie wygrał. Impas. Wszystko pola zapełnione.");
            return true;
        }
        static void GetValue(string sign, string name)
        {
            List<int> table = new List<int>();
            for (int i = 0; i < tablica.Length; i++)
            {
                if (tablica[i] == " ")
                {
                    table.Add(i+1);
                }
            }

            bool success = false;
            int x = 0;

            string tab = "{";
            foreach (var item in table)
            {
                tab += " " + item + ",";
            }
            tab += "}";

            Console.Clear();
            while (success == false)
            {
                Show();
                Console.WriteLine("Możesz podać liczby z przedziału: " + tab);
                Console.WriteLine("Pamiętaj twój symbol to: "+sign);
                Console.Write(name+ " proszę podaj liczbę: ");
                if (Int32.TryParse(Console.ReadLine(), out x))
                {
                    Console.WriteLine("Podałeś liczbę.");
                }
                else
                {
                    Console.WriteLine("Nie Podałeś liczby. Popraw się.");
                    continue;
                }
                for (int i = 0; i < table.Count; i++)
                {
                    if (Int32.Equals(x, table[i]))
                    {
                        success = true;
                        Console.Clear();
                        Console.WriteLine("Podana liczba należy do przedziału: " + tab);
                        break;
                    }
                    else if (i == table.Count - 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Podales liczbe {0}, nie nalezy ona do przedziału: {1}",x, tab);
                        Console.WriteLine("Spróbuj jeszcze raz.");
                        break;
                    }
                }
                Console.WriteLine();
            }
            tablica[x-1] = sign;
        }
        static void Game()//kontroler
        {
            CreateMatrix();
            int player = 0;
            symbols[0] = "0";
            symbols[1] = "X";
            while (CheckScore() == false)
            {
                Console.WriteLine();
                if (player == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    GetValue(symbols[0], player1);
                    player = 1;
                    continue;
                }
                else if (player == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    GetValue(symbols[1], player2);
                    player = 0;
                }
            }
            Show();
        }
    }
}