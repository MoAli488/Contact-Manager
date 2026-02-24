using System;
using System.Collections.Generic;
using System.Text;

namespace Contact_Manager
{
    public class Contact
    {
        private static int idCounter = 0;

        private int id = 0;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CurrentDateTime { get; set; }
        public static void setIdCounter(int value)
        {
            idCounter = value;
        }
        public static int getIdCounter()
        {
            return idCounter;
        }
        public int getID()
        {
            return id;
        }
        public Contact(string name, string phoneNumber, string email)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.id = ++idCounter;
            this.CurrentDateTime = DateTime.Now.ToString();
        }

        public override string ToString()
        {
            return $"ID: {id},\nName: {Name},\nPhone: {PhoneNumber},\nEmail: {Email},\nCreated At: {CurrentDateTime}\n";
        }
    }
}
