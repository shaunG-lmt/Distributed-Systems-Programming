﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkLab
{
    class Person
    {
        public int PersonID { get; set; } // Class name followed by ID will be picked up as a key for storing object in people table.
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public Address Address { get; set; }
        public Person() { }
    }
}
