using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Administrator
    {
        private string username;
        private string password;
        private string name;
        private string surname;
        private DateTime birthdate;

        public Administrator() { }

        public Administrator(string username, string password, string name, string surname, DateTime date)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.birthdate = date;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime BirthDate { get => birthdate; set => birthdate = value; }
    }
}