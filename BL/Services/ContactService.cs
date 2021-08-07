using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Persistent;
using Newtonsoft.Json;

namespace BL.Services
{
    public class ContactService
    {
        private static List<Contact> _contacts;

        public ContactService()
        {
            _contacts = new List<Contact>();
            LoadContacts();
        }

        private void LoadContacts()
        {
            if (File.Exists("data.json"))
            {
                var jsonFile = File.ReadAllText("data.json");
                _contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonFile);
            }
        }

        private void SaveContacts()
        {
            var json = JsonConvert.SerializeObject(_contacts);
            File.WriteAllText("data.json", json);
        }

        public void AddContact()
        {
            var contact = ReadDataFromConsole();

            if (_contacts.Any(c => c.mobilePhone == contact.mobilePhone))
            {
                Console.WriteLine($"{contact.mobilePhone} данный мобильный телефон уже привязан к другому контакту.");
                return;
            }

            if (_contacts.Any(c => c.phone == contact.phone))
            {
                Console.WriteLine($"{contact.phone} данный телефон уже привязан к другому контакту.");
                return;
            }

            _contacts.Add(contact);

            SaveContacts();

            Console.WriteLine("Контакт добавлен!");
        }

        public void ShowAllContacts()
        {
            foreach(var contact in _contacts)
            {
                Console.WriteLine($"{contact.name}; {contact.mobilePhone} {contact.phone}");
            }
        }

        public void EditContact()
        {
            var contact = ReadDataFromConsole();

            var contactForEdit = _contacts.Where(c => c.mobilePhone == contact.mobilePhone).FirstOrDefault();

            if(contactForEdit == null)
            {
                Console.WriteLine("Контакт не найден!");
            }

            Console.WriteLine("Введите новый номер мобильного телефона или оставьте пустым если не надо изменять:");

            string newMobilePhone = Console.ReadLine();

            if(!string.IsNullOrEmpty(newMobilePhone))
            {
                contactForEdit.mobilePhone = newMobilePhone;
            }

            contactForEdit.name = contact.name;
            contactForEdit.phone = contact.phone;            

            SaveContacts();

            Console.WriteLine("Контакт успешно изменен!");
        }

        public void ExportContacts()
        {
            Console.WriteLine("Введите имя файла");
            string fileName = Console.ReadLine();

            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                foreach(var contact in _contacts)
                {
                    writetext.WriteLine($"{contact.name}; {contact.mobilePhone} {contact.phone}");
                }               
            }

            Console.WriteLine("Данные экспортированны!");
        }

        public void AddContactsFromFile()
        {
            Console.WriteLine("Введите имя файла");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден!");
            }

            string fileBody = File.ReadAllText(fileName);

            try
            {
                var addedContacts = JsonConvert.DeserializeObject<List<Contact>>(fileBody);
                _contacts.AddRange(addedContacts);

                Console.WriteLine("Файл загружен!");
            }
            catch(Exception)
            {
                Console.WriteLine("не верный формат файла!");
            }           
        }

        private Contact ReadDataFromConsole()
        {
            Contact contact = new Contact();

            Console.WriteLine("Введите имя:");
            contact.name = Console.ReadLine();

            Console.WriteLine("Введите мобильный телефон:");
            contact.mobilePhone = Console.ReadLine();

            Console.WriteLine("Введите телефон");
            contact.phone = Console.ReadLine();

            return contact;
        }
    }
}
