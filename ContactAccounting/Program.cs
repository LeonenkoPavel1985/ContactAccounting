using System;
using BL.Services;

namespace ContactAccounting
{
    class Program
    {
        static ContactService _contactService = new ContactService();

        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("|-----Программа для учета контактов пользователя.------|");
            Console.WriteLine("--------------------------------------------------------");
            while (true)
            {
                Console.WriteLine("МЕНЮ ПРОГРАММЫ:");
                Console.WriteLine("1. Добавление нового контакта.");
                Console.WriteLine("2. Показать весь список контактов.");
                Console.WriteLine("3. Редактировать существующий контакт.");
                Console.WriteLine("4. Экспорт контактов в указанный файл.");
                Console.WriteLine("5. Добавление контактов из файла.");
                Console.WriteLine("6. Выход.");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _contactService.AddContact();
                        break;
                    case "2":
                        _contactService.ShowAllContacts();
                        break;
                    case "3":
                        _contactService.EditContact();
                        break;
                    case "4":
                        _contactService.ExportContacts();
                        break;
                    case "5":
                        _contactService.AddContactsFromFile();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
