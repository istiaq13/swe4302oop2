using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_project_solid
{
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
}
