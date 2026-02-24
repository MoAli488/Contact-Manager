using System;

namespace Contact_Manager
{
    public static class Menu
    {
        public static void ShowMenu()
        {
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
                            ContactManager.AddContact();
                            break;
                        case 2:
                            ContactManager.EditContact();
                            break;
                        case 3:
                            ContactManager.DeleteContact();
                            break;
                        case 4:
                            ContactManager.ViewContact();
                            break;
                        case 5:
                            ContactManager.ListContacts();
                            break;
                        case 6:
                            ContactManager.SearchContacts();
                            break;
                        case 7:
                            ContactManager.FilterContacts();
                            break;
                        case 8:
                            ContactManager.SaveContacts();
                            break;
                        case 9:
                            Console.WriteLine("Do you want to save before exiting? (y/n)");
                            string saveChoice = Console.ReadLine().ToLower();
                            if (saveChoice == "y") { ContactManager.SaveContacts(); }
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
    }
}
