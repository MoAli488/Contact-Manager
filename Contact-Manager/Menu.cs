using System;

namespace Contact_Manager
{
    public static class Menu
    {
        public static void ShowMenu()
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

        }
    }
}
