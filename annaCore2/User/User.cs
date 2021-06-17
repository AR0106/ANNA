using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.UserInfo
{
    public class User
    {
        public string FirstName
        {
            get;
        }

        public User(string firstName)
        {
            FirstName = firstName;
        }
    }
}
