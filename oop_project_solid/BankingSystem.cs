using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_project_solid
{
    public class BankingSystem
    {
        private List<Account> accounts;
        private IUserAuthenticationService authenticationService;
        private ITransactionService transactionService;
        private Account currentUser;

        public BankingSystem(List<Account> accounts, IUserAuthenticationService authService, ITransactionService transService)
        {
            this.accounts = accounts;
            this.authenticationService = authService;
            this.transactionService = transService;
        }

        public void Run()
        {
            while (true)
            {
                if (currentUser == null)
                {
                    Console.WriteLine("1. Create Account");
                    //Console.WriteLine("2. Login");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                }
                else
                {
                    Console.WriteLine("1. Perform Transaction");;
                    Console.WriteLine("2. Logout");
                    Console.WriteLine("3. Check Balance")
                    Console.WriteLine("4. Exit");
                }

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if (currentUser == null)
                        {
                            CreateAccount();
                        }
                        else
                        {
                            PerformTransaction();
                        }
                        break;

                    case 3:
                        if (currentUser == null)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine($"Current balance: {currentUser.Balance}");
                        }
                        break;

                    case 2:
                        if (currentUser == null)
                        {
                            AdminLogin();
                        }
                        else
                        {
                            Logout();
                        }
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter initial balance:");
            double balance = Convert.ToDouble(Console.ReadLine());

            accounts.Add(new Account(name, balance, password));

            Console.WriteLine("Account created successfully!");
        }

        private void AdminLogin()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            if (username == "istiaq" && password == "123")
            {
                Console.WriteLine("login successful!");
                currentUser = new Account("Admin", 0, "");
            }
            else
            {
                Console.WriteLine("Admin login failed. Incorrect credentials.");
            }
        }

        private void Logout()
        {
            Console.WriteLine($"Logged out from {currentUser.Name}");
            currentUser = null;
        }

        private void PerformTransaction()
        {
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");

            int choice = Convert.ToInt32(Console.ReadLine());

            transactionService.PerformTransaction(currentUser, choice);
        }
    }
}
