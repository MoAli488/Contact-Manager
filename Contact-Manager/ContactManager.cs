using Contact_Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Contact_Manager
{
    class ContactManager
    {
        private static Dictionary<int, Contact> contactList = new Dictionary<int, Contact>();
        static void Main(String[] args)
        {

            Contact[] contacts = JsonContactRepository.Load().ToArray();
            Contact.setIdCounter(contacts.Length > 0 ? contacts.Max(c => c.getID()) : 1);
            foreach (Contact contact in contacts)
            {
                ContactManager.contactList.Add(contact.getID(), contact);
            }
            Menu.ShowMenu();

        }
        public static void AddContact() 
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Contact newContact = new Contact(name, phoneNumber, email);

            contactList.Add(newContact.getID(), newContact);
            Console.WriteLine("Contact added successfully!");
        }
        public static void EditContact() 
        {
            Console.Write("Enter contact ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            if (contactList.ContainsKey(id))
            {
                Contact contact = contactList[id];
                Console.Write("Enter new name (leave blank to keep current): ");
                string name = Console.ReadLine();
                Console.Write("Enter new phone number (leave blank to keep current): ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter new email (leave blank to keep current): ");
                string email = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    contact.Name = name;
                if (!string.IsNullOrEmpty(phoneNumber))
                    contact.PhoneNumber = phoneNumber;
                if (!string.IsNullOrEmpty(email))
                    contact.Email = email;
                Console.WriteLine("Contact updated successfully!");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public static void DeleteContact() 
        {
            Console.Write("Enter contact ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            if (contactList.ContainsKey(id))
            {
                contactList.Remove(id);
                Console.WriteLine("Contact deleted successfully!");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public static void ViewContact() 
        {
            Console.Write("Enter contact ID to view: ");
            int id = int.Parse(Console.ReadLine());
            if (contactList.ContainsKey(id))
            {
                Console.WriteLine(contactList[id]);
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public static void FilterContacts()
        {
            List<Contact> contacts = contactList.Values.ToList();
            Console.Write("Enter filter by Name?:(y/n)");
            string filterChoiceName = Console.ReadLine().ToLower();
            if (filterChoiceName == "y")
            {
                Console.Write("Enter name to filter by: ");
                string nameFilter = Console.ReadLine().ToLower();
                contacts = contacts.Where(c => c.Name.ToLower().Contains(nameFilter)).ToList();
            }

            Console.Write("Enter filter by Date?:(y/n)");
            string filterChoiceDate = Console.ReadLine().ToLower();
            if (filterChoiceDate == "y")
            {
                Console.WriteLine("Enter Date:(MM/DD/YYYY)");
                string date = Console.ReadLine();
                contacts = contacts.Where(c => c.CurrentDateTime.Contains(date)).ToList();
            }
            Console.WriteLine("\nFiltered Contacts:");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
        }
        public static void ListContacts() 
        {
            if (contactList.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }
            foreach (var contact in contactList.Values)
            {
                Console.WriteLine(contact.ToString());
            }
        }
        public static void SearchContacts() 
        {
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine().ToLower();
            bool match = false;
            foreach (var contact in contactList.Values)
            {
                if (contact.Name.ToLower().Contains(searchTerm) || 
                    contact.PhoneNumber.Contains(searchTerm) || 
                    contact.Email.ToLower().Contains(searchTerm))
                {
                    Console.WriteLine(contact.ToString());
                    match = true;
                    break;
                }

            }
            if (!match)
            {
                Console.WriteLine("No matching contacts found.");
            }
        }
        public static void SaveContacts() 
        {
            JsonContactRepository.Save(contactList.Values.ToList());
            Console.WriteLine("Contacts saved successfully!");
        }
    }
}