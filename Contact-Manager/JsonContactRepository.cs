using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Contact_Manager
{
    public static class JsonContactRepository
    {
        private const string FileName = "contacts.json";

        public static List<Contact> Load()
        {
            if (!File.Exists(FileName)) return new List<Contact>();
            string json = File.ReadAllText(FileName);
            return json == "" ? JsonSerializer.Deserialize<List<Contact>>(json) : new List<Contact>();
        }

        public static void Save(List<Contact> contacts)
        {
            string jsonFile = JsonSerializer.Serialize(contacts);
            File.WriteAllText(FileName, jsonFile);
        }
    }
}
