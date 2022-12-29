using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Entities
{
    public abstract class Person
    {
        public string Name { get; private set; }
        public bool Active { get; protected set; }

        public Person(string name)
        {
            Name = name;
            Active = true;
        }
        public Person(string name, bool active)
        {
            Name = name;
            Active = active;
        }

        public void Disable()
        {
            Active = false;
        }
    }
}