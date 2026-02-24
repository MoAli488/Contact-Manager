using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace ConsoleApp1
{
    class ContactManager
    {
        private static Dictionary<int, Contact> contactList = new Dictionary<int, Contact>();
        static void Main(String[] args)
        {
            try
            {
                string jsonString = File.ReadAllText("contacts.json");
                Contact[] contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString).ToArray();
                foreach (Contact contact in contacts)
                {
                    ContactManager.contactList.Add(contact.getID(), contact);
                }
            }
            catch(Exception e) { }
            finally
            {
                Menu();
            }
        }
        static void Menu() {
            while (true)
            {
                Console.WriteLine("\n===== Contact Manager Menu =====");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Edit Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. View Contact");
                Console.WriteLine("5. List Contacts");
                Console.WriteLine("6. Search");
                Console.WriteLine("7. Filter");
                Console.WriteLine("8. Save");
                Console.WriteLine("9. Exit");
                Console.WriteLine("================================");
                Console.Write("Please select an option: ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddContact();
                            break;
                        case 2:
                            EditContact();
                            break;
                        case 3:
                            DeleteContact();
                            break;
                        case 4:
                            ViewContact();
                            break;
                        case 5:
                            ListContacts();
                            break;
                        case 6:
                            SearchContacts();
                            break;
                        case 7:
                            FilterContacts();
                            break;
                        case 8:
                            SaveContacts();
                            break;
                        case 9:
                            Console.WriteLine("Do you want to save before exiting? (y/n)");
                            string saveChoice = Console.ReadLine().ToLower();
                            if (saveChoice == "y")
                            {
                                SaveContacts();
                            }
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
        static void AddContact() 
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
        static void EditContact() 
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
        static void DeleteContact() 
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
        static void ViewContact() 
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
        static void FilterContacts() 
        {
            List<Contact> contacts = contactList.Values.ToList();
            Console.Write("Enter filter by Name?:(y/n)");
            string filterChoiceName = Console.ReadLine().ToLower();
            if (filterChoiceName == "y")
            {
                Console.Write("Enter name to filter by: ");
                string nameFilter = Console.ReadLine().ToLower();
                foreach (Contact contact in contacts)
                {
                    if (contact.Name.ToLower() != nameFilter)
                    {
                        contacts.Remove(contact);
                    }
                }
            }

            Console.Write("Enter filter by Date?:(y/n)");
            string filterChoiceDate = Console.ReadLine().ToLower();
            if (filterChoiceDate == "y")
            {
                Console.WriteLine("Enter Date:(MM/DD/YYYY)");
                string date = Console.ReadLine();
                foreach (Contact contact in contacts)
                {
                    if (!contact.CurrentDateTime.Contains(date))
                    {
                        contacts.Remove(contact);
                    }
                }
            }
            Console.WriteLine("\nFiltered Contacts:");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
        }
        static void ListContacts() 
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
        static void SearchContacts() 
        {
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine().ToLower();
            foreach (var contact in contactList.Values)
            {
                bool match = false;
                if (contact.Name.ToLower().Contains(searchTerm) || 
                    contact.PhoneNumber.Contains(searchTerm) || 
                    contact.Email.ToLower().Contains(searchTerm))
                {
                    Console.WriteLine(contact.ToString());
                    match = true;
                }
                if(!match)
                {
                    Console.WriteLine("No matching contacts found.");
                }
            }
        }
        static void SaveContacts() 
        {
            string jsonFile = JsonSerializer.Serialize(contactList.Values.ToList());
            File.WriteAllText("contacts.json", jsonFile);
            Console.WriteLine("Contacts saved successfully!");
        }
    }
}