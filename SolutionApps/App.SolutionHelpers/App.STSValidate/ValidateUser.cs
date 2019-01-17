using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.STSValidate
{
    public class ValidateUser
    {
        public bool IsUSerValid(string pUserName, string pPassword, string pSource)
        {
            bool _isValid = false;
            if (pUserName == "tarun" && pPassword == "kapoor")
                _isValid = true;
            return _isValid;
        }
    }
}
