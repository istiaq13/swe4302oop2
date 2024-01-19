using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_project_solid
{
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