using System;
using System.Globalization;

namespace AdaCredit.Entities
{
    public sealed class Employee : Person
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public DateTime LastLoggedIn { get; private set; }
        public bool FirstLogin { get; private set; }

        public Employee(string name, string username) : base(name)
        {
            Salt = BC.GenerateSalt();
            PasswordHash = BC.HashPassword("pass", Salt);
            Username = username;
            LastLoggedIn = DateTime.MinValue;
            FirstLogin = true;
        }

        public Employee(string name, string username, string salt, string passwordHash, string lastLoggedIn, bool active, bool firstLogin) : base(name, active)
        {
            Username = username;
            Salt = salt;
            PasswordHash = passwordHash;
            LastLoggedIn = DateTime.Parse(lastLoggedIn, CultureInfo.InvariantCulture);
            Active = active;
            FirstLogin = firstLogin;
        }

        public void UpdatePassword(string password)
        {
            Salt = BC.GenerateSalt();
            PasswordHash = BC.HashPassword(password, Salt);
        }

        public void UpdateLastLoggedIn()
        {
            LastLoggedIn = DateTime.Now;
        }

        public void UpdateFirstLogin()
        {
            FirstLogin = false;
        }
    }
}