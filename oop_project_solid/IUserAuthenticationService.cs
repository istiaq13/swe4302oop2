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
}
