using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_project_solid
{
    public interface IUserAuthenticationService
    {
        Account AuthenticateUser(string username, string password);
    }

    public interface ITransactionService
    {
        void PerformTransaction(Account account, int choice);
    }

    public class UserAuthenticationService : IUserAuthenticationService
    {
        private List<Account> accounts;

        public UserAuthenticationService(List<Account> accounts)
        {
            this.accounts = accounts;
        }

        public Account AuthenticateUser(string username, string password)
        {
            foreach (var acc in accounts)
            {
                if (acc.Name == username && acc.Password == password)
                {
                    return acc;
                }
            }
            return null;
        }
    }

    public class TransactionService : ITransactionService
    {
        public void PerformTransaction(Account account, int choice)
        {
            switch (choice)
            {
                case 1:
                    Withdraw(account);
                    break;

                case 2:
                    Deposit(account);
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        private void Withdraw(Account account)
        {
            Console.WriteLine("Enter amount to withdraw:");
            double amountToWithdraw = Convert.ToDouble(Console.ReadLine());

            if (amountToWithdraw > account.Balance)
            {
                Console.WriteLine("Insufficient balance. Withdrawal failed.");
            }
            else
            {
                account.Balance -= amountToWithdraw;
                Console.WriteLine($"Withdrawal successful. Updated balance: {account.Balance}");
            }
        }

        private void Deposit(Account account)
        {
            Console.WriteLine("Enter amount to deposit:");
            double amountToDeposit = Convert.ToDouble(Console.ReadLine());
            account.Balance += amountToDeposit;
            Console.WriteLine($"Deposit successful. Updated balance: {account.Balance}");
        }
    }

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
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Admin Login");
                    Console.WriteLine("4. Exit");
                }
                else
                {
                    Console.WriteLine("1. Perform Transaction");
                    Console.WriteLine("2. Check Balance");
                    Console.WriteLine("3. Logout");
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

                    case 2:
                        if (currentUser == null)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine($"Current balance: {currentUser.Balance}");
                        }
                        break;

                    case 3:
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
            Console.WriteLine("Enter admin username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter admin password:");
            string password = Console.ReadLine();

            if (username == "admin" && password == "123")
            {
                Console.WriteLine("Admin login successful!");
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

    public class Account
    {
        public string Name;
        public double Balance;
        public string Password;

        public Account(string name, double balance, string password)
        {
            Name = name;
            Balance = balance;
            Password = password;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Account> accounts = new List<Account>();
            IUserAuthenticationService authService = new UserAuthenticationService(accounts);
            ITransactionService transService = new TransactionService();

            BankingSystem bankingSystem = new BankingSystem(accounts, authService, transService);
            bankingSystem.Run();
        }
    }

}
