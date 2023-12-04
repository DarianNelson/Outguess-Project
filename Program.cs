using System;
using System.Diagnostics;

namespace Outguess_Project {
    internal class Program {
        static void Main(string[] args) {
            //VARIABLES
            double round = 1;
            double number = 0;
            double wagerTries = 0;
            double randomNumber = 0;
            decimal cash = 0;
            decimal wager = 0;
            decimal winnings = 0;
            double wins = 0;
            double winPercent = 0;
            bool playAgain = true;
            string answer = "";
            string input = "";

            //WELCOME MESSSAGE
            Console.WriteLine("Welcome to Outguess!");
            Console.WriteLine("---------------------------------");

            //GET CASH AMOUNT
            cash = TryInputDecimal("Before we begin - how much cash are you bringing to the table? ");

            //INPUT VALIDATION LOOP
            while (cash <= 0) {
                Console.WriteLine("Please enter a positive amount.");
                cash = InputDecimal("Enter cash :");
            }//end loop

            //LOOP
            while (playAgain && cash > 0) {
                //GET WAGER
                wager = TryInputDecimal("How much would you like to wager for this round? ");

                //INPUT VALIDATION LOOP
                while (wager > cash) {
                    Console.WriteLine("You can't wager more than you bring to the table. Try again.");
                    wager = TryInputDecimal("Enter wager: ");
                }//end loop
                while (wager <= 0) {
                    Console.WriteLine("Wagers must be a positive amount. Try again.");
                    wager = TryInputDecimal("Enter wager: ");
                }//end loop

                //GET TRIES PER WAGER
                wagerTries = TryInputDouble("How many guesses - up to 10 - do you wish to use? ");

                //INPUT VALIDATION LOOP
                while (wagerTries > 10) {
                    Console.WriteLine("You can't exceed 10 guesses. Try again. ");
                    wagerTries = TryInputDouble("Enter number of guesses :");
                }//end loop
                while (wagerTries <= 0) {
                    Console.WriteLine("Please enter a positive number of guesses.");
                    wagerTries = TryInputDouble("Enter number of guesses :");
                }//end loop 

                //GENERATE RANDOM NUMBER
                Random randomNum = new Random();
                randomNumber = randomNum.Next(101);

                while (wagerTries > 0) {
                    //GET FIRST NUMBER INPUT
                    Console.WriteLine("---------------------------------");
                    number = TryInputDouble("Guess the secret number! ");
                    while (number < 0 || number > 100) {
                        Console.WriteLine("Please enter a positive number between 0 - 100.");
                        number = TryInputDouble("Guess the secret number! ");
                    }//end loop

                        //IF 
                        if (number < randomNumber) {
                        wagerTries--;
                        Console.WriteLine($"Sorry too low! You have {wagerTries} tries remaining for this round.");
                        if (number != randomNumber && wagerTries == 0) {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine($"You Lose! Correct number was {randomNumber}");
                            cash = cash - wager;
                            Console.WriteLine($"Current cash amount : ${cash}");
                            if (cash <= 0) {
                                Console.WriteLine("---------------------------------");
                                Console.WriteLine("You're out of cash! Game over!");
                                wagerTries = 0;
                            }//nested if
                        }//end nested if
                    } else if (number > randomNumber) {
                        wagerTries--;
                        Console.WriteLine($"Sorry too high! You have {wagerTries} tries remaining for this round.");
                        if (number != randomNumber && wagerTries == 0) {
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine($"You Lose! Correct number was {randomNumber}");
                            cash = cash - wager;
                            Console.WriteLine($"Current cash amount : ${cash}");
                            if (cash <= 0) {
                                Console.WriteLine("---------------------------------");
                                Console.WriteLine("You're out of cash! Game over!");
                                wagerTries = 0;
                            }//nested if
                        }//end nested if
                    } else if (number == randomNumber) {
                        Console.WriteLine($"Congratulations! {randomNumber} was the correct number!");
                        //CALL WINNINGS FUNCTION AND DISPLAY ROUND WINNINGS
                        winnings = WinningsMultiplier(wagerTries, randomNumber, number, winnings, wager);
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine($"Total winnings for the round : ${winnings}");
                        //CALCULATE CASH
                        cash = cash + winnings;
                        Console.WriteLine($"Total cash : ${cash}");
                        wins = wins + 1;
                        wagerTries = 0;
                    }//end if
                }//end loop 

                if (cash > 0) {
                    //ARE WE PLAYING AGAIN?
                    Console.WriteLine("---------------------------------");
                    input = Input("Would you like to play again? ");
                    answer = input.ToLower();
                    //INPUT VALIDATION LOOP
                    while (answer != "yes" && answer != "no") {
                        Console.WriteLine("Please answer with yes or no. ");
                        input = Input("Play again? ");
                        answer = input.ToLower();
                    }//end loop
                    playAgain = answer == "yes";
                }//end if 

                winPercent = (100 * wins) / round;

                if (playAgain) {
                    round++;
                }//end if 
            }//end loop

            //OUTPUT
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Win Percentage : {winPercent}%");
            Console.WriteLine($"Total cash : ${cash}");
            Console.WriteLine("Thanks for playing!");

        }//end main

        static decimal WinningsMultiplier(double wagerTries, double randomNumber, double number, decimal winnings, decimal wager) {
            //IF- WAGER WINNINGS
            if (wagerTries == 1 && number == randomNumber) {
                winnings = wager * 10;
            } else if (wagerTries == 2 && number == randomNumber) {
                winnings = wager * 9;
            } else if (wagerTries == 3 && number == randomNumber) {
                winnings = wager * 8;
            } else if (wagerTries == 4 && number == randomNumber) {
                winnings = wager * 7;
            } else if (wagerTries == 5 && number == randomNumber) {
                winnings = wager * 6;
            } else if (wagerTries == 6 && number == randomNumber) {
                winnings = wager * 5;
            } else if (wagerTries == 7 && number == randomNumber) {
                winnings = wager * 4;
            } else if (wagerTries == 8 && number == randomNumber) {
                winnings = wager * 3;
            } else if (wagerTries == 9 && number == randomNumber) {
                winnings = wager * 2;
            } else if (wagerTries == 10 && number == randomNumber) {
                winnings = wager * 1;
            }//end if

            return winnings;
        }//end function

        #region HELPER FUNCTIONS
        static string Input(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }//end function

        static decimal InputDecimal(string message) {
            string value = Input(message);
            return decimal.Parse(value);
        }//end function

        static decimal TryInputDecimal(string message) {
            //VARIABLES
            decimal parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DECIMAL HAS BEEN SUBMITTED
            do {
                gotParsed = decimal.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static double InputDouble(string message) {
            string value = Input(message);
            return double.Parse(value);
        }//end function

        static double TryInputDouble(string message) {
            //VARIABLES
            double parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DOUBLE HAS BEEN SUBMITTED
            do {
                gotParsed = double.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static int InputInt(string message) {
            string value = Input(message);
            return int.Parse(value);
        }//end function

        static int TryInputInt(string message) {
            //VARIABLES
            int parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID INT HAS BEEN SUBMITTED
            do {
                gotParsed = int.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static void Print(string message) {
            Console.WriteLine(message);
        }//end function

        static void PrintColor(string message, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }//end function
        #endregion

    }//end class
}//end namespace