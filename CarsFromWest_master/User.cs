using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFromWest_master
{
    class User
    {
        public int id { get; set; }

        private string login, pass, status;

        public string Login { get { return login; } set { login = value; } }

        public string Pass { get { return pass; } set { pass = value; } }

        public string Status { get { return status; } set { status = value; } }


        public User() { }

        public User(/*int id,*/ string login, string pass, string status)
        {
            //this.id = id;
            this.login = login;
            this.pass = pass;
            this.status = status;
        }
    }
}
