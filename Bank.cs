using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChrisAmatuWk5
{
    public class Bank
    {
        private List<User> users = new List<User>();
        private string pathName = @"C:\Users\Decagon\source\repos\ChrisAmatuWk5\userDetails.json";
        public void Run()
        {
            Console.WriteLine("Welcome to Christox Bank\n");

            while (true)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SignIn();
                        break;

                    case "2":
                        SignUp();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }


        private void SignUp()
        {
            User newUser = new User();

            var collect = newUser.CreateUser();

            if (collect != null)
            {
                users.Add(collect);
                Console.WriteLine("Sign up successful! Your account number is: " + collect.Accounts.First().AccountNumber);
            }

            SaveUserToJson();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private void SignIn()
        {
            Console.Write("Enter your username: ");
            string userName = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            User user = users.FirstOrDefault(u => u.UserName == userName && u.ValidatePassword(password));

            if (user != null)
            {
                Console.WriteLine("Sign in successful!");
                user.ShowAccountMenu(users);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
        public void SaveUserToJson()
        {
            try
            {
                string jsonUserData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(pathName, jsonUserData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving Customer Details: {ex.Message}");
            }
        }
    }
}

