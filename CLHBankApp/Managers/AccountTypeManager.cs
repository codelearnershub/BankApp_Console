using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers
{
    public class AccountTypeManager
    {
        public static int NoOfAccountType = 0;
        public List<AccountType> accountTypes = new List<AccountType>();



        public AccountType GetAccountType(string name)
        {
            foreach(var accountType in accountTypes)
            {
                if(accountType.Name == name)
                {
                    return accountType;
                }
            }
            return null;
        }
    }
}
