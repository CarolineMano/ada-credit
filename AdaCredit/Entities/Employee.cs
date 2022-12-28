using System;

namespace AdaCredit.Entities
{
    public sealed class Employee : Person
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public DateTime LastLoggedIn { get; set; }

        public Employee(string name, string username) : base(name)
        {
            Salt = BC.GenerateSalt();
            PasswordHash = BC.HashPassword("pass", Salt);
            Username = username;
            LastLoggedIn = DateTime.MinValue;
        }

        public Employee(string name, string username, string salt, string passwordHash, string lastLoggedIn, bool active) : base(name)
        {
            Username = username;
            Salt = salt;
            PasswordHash = passwordHash;
            LastLoggedIn = DateTime.Parse(lastLoggedIn);
            Active = active;
        }
    }
}