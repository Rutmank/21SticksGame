using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace _21SticksGame
{
    class Program
    {
        const int FIRST_STICK = 1;
        const int SECOND_STICK = 2;
        const int COUNT_OF_STICKS = 21;

        public static void Main()
        {
            int sticks = COUNT_OF_STICKS;

            bool isUserTurn = IsTheGameStarting();

            Console.WriteLine(PlayTheGame(isUserTurn, sticks));
        }

        static bool IsTheGameStarting()
        {
            string userAnswer;
            do
            {
                Console.Write("Do you want to start 21 stick game (y/n): ");
                userAnswer = Console.ReadLine();

            } while (!Regex.IsMatch(userAnswer, @"^(y|Y|n|N)\b+$"));

            return AbleToDefineARole(userAnswer);
        }

        static bool AbleToDefineARole(string userAnswer)
        {
            bool isUserTurn = userAnswer.ToUpperInvariant() == "Y";

            return isUserTurn;
        }

        static string PlayTheGame(bool isUserTurn, int sticks)
        {
            while (sticks > 1)
            {
                sticks = isUserTurn ? PlayUser(sticks) : PlayComputer(sticks);
                isUserTurn = !isUserTurn;
            }

            string result = !isUserTurn && sticks == 1 ? "You won!" : "Computer won!";
            return result;
        }

        static int PlayUser(int sticks)
        {
            int userPick = GetUserTurn();
            sticks -= userPick;
            CheckStick(sticks);
            return sticks;
        }

        static void CheckStick(int sticks)
        {
            if (sticks >= 1)
            {
                Console.WriteLine($"{sticks} sticks left");
            }
        }

        static int GetUserTurn()
        {
            Console.WriteLine("How many sticks will you take:");
            int userPick = CheckUserTurn();

            return userPick;
        }

        static int PlayComputer(int sticks)
        {
            int computerPick = GenerateComputerTurn(sticks);
            sticks -= computerPick;
            CheckStick(sticks);
            return sticks;
        }

        static int GenerateComputerTurn(int sticks)
        {
            int computerNumber;

            computerNumber = (sticks - 2) % 3 == 0 || sticks - 2 == 0 ? 1 : 2;

            Console.WriteLine($"Copmuter took {computerNumber} sticks.");

            return computerNumber;
        }

        static int CheckUserTurn()
        {
            int userPick;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out userPick) || !Enumerable.Range(FIRST_STICK, SECOND_STICK).Contains(userPick))
                {
                    Console.WriteLine("Please take 1 or 2 stick");
                }
            } while (!Enumerable.Range(FIRST_STICK, SECOND_STICK).Contains(userPick));

            return userPick;
        }
    }
}
