using CLHBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLHBankApp.Managers.Interfaces
{
    public interface IStaffManager
    {
        public void AddNewStaff(Staff staff);
        public void CreateAccountType(Staff staff);
        public void UpdateAccountType(Staff staff);
        public Staff Login();
    }
}
