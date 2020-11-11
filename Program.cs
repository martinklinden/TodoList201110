using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoList201110
{
    class Program
    {
        class Activity
        {
            public string date;
            public string state;
            public string title;
            public Activity(string D, string S, string T)
            {
                date = D; state = S; title = T;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello and welcome to the todolist!");
            string command, path = "";
            string[] commandWord;
            List<Activity> todoList = new List<Activity>();

            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                commandWord = command.Split(' ');
                if (commandWord[0] == "quit")
                {
                    Console.WriteLine("Do you want to save your project? (y/n)");
                    bool quitAnswer = false;
                    while (quitAnswer == false)
                    {
                        string answer = Console.ReadLine();
                        if (answer == "y")
                        {
                            SaveTodoListFile(todoList, path);
                            quitAnswer = true;
                            Console.WriteLine("Goodbye!");
                        }
                        else if (answer == "n")
                        {
                            quitAnswer = true;
                            Console.WriteLine("Goodbye!");
                        }
                        else
                        {
                            Console.WriteLine("Answer y or n!");
                        }
                    }
                }
                else if (commandWord[0] == "load")
                {
                    Console.WriteLine("Reading file...");
                    path = commandWord[1];
                    todoList = ReadTodoListFile(commandWord[1]);
                }
                else if (commandWord[0] == "show")
                {
                    if (commandWord[1] == "path")
                    {
                        Console.WriteLine(path);
                    }
                    else if (commandWord[1] == "list")
                    {
                        PrintList(todoList);
                    }
                    else
                    {
                        Console.WriteLine("Show what?");
                    }
                }
                else if (commandWord[0] == "add")
                {
                    AddToListFile(todoList);
                }
                else if (commandWord[0] == "save")
                {
                    SaveTodoListFile(todoList, path);
                }
                else
                {
                    Console.WriteLine($"Unknown command: {commandWord[0]}");
                }
            } while (commandWord[0] != "quit");
        }
        static void PrintList(List<Activity> todoList)
        {
            Console.WriteLine("N  datum  S  rubrik");
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < todoList.Count; i++)
            {
                if (todoList[i] != null)
                {
                    Console.WriteLine($"{i + 1}: - {todoList[i].date} - {todoList[i].state} - {todoList[i].title}");
                }
            }
            Console.WriteLine("-------------------------------------------");
        }
        private static List<Activity> ReadTodoListFile(string fileName)
        {
            List<Activity> todoList = new List<Activity>();
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                while (streamReader.Peek() >= 0)
                {
                    string[] lineWord = streamReader.ReadLine().Split('#');
                    Activity A = new Activity(lineWord[0], lineWord[1], lineWord[2]);
                    //Console.WriteLine($"{A.date} - {A.state} - {A.title}");
                    todoList.Add(A);
                }
                // streamReader.Close();
            }
            return todoList;
        }
        private static void SaveTodoListFile(List<Activity> todoList, string fileName)
        {
            // Console.Write("What path?: ");
            // fileName = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < todoList.Count(); i++)
                {
                    writer.WriteLine($"{todoList[i].date}#{todoList[i].state}#{todoList[i].title}");
                }
                Console.WriteLine("Todolist is saved!");
            }
        }
        private static void AddToListFile(List<Activity> todoList)
        {
            Console.Write("What's the date: ");
            string date = Console.ReadLine();
            Console.Write("What's the state: ");
            string state = Console.ReadLine();
            Console.Write("What's the title: ");
            string title = Console.ReadLine();
            todoList.Add(new Activity(date, state, title));
            PrintList(todoList);
        }
    }
}
