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
                            Console.WriteLine("Do you want to save your project? (y/n)");
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
                    if(commandWord.Length != 1)
                    {
                        PrintList(todoList, commandWord[1]);
                    }
                    else
                    {
                        commandWord[1] = " ";
                        PrintList(todoList, commandWord[1]);
                    }
                }
                else if (commandWord[0] == "move")
                {
                    MoveInList(todoList, commandWord[1], commandWord[2]);
                }
                else if (commandWord[0] == "delete")
                {
                    DelInList(todoList, commandWord[1]);
                }
                else if (commandWord[0] == "set")
                {
                    SetInList(todoList, commandWord[1], commandWord[2]);
                }
                else if (commandWord[0] == "add")
                {
                    AddToListFile(todoList, commandWord[1], commandWord[2]);
                }
                else if (commandWord[0] == "save")
                {
                    if (commandWord.Length == 1)
                    {
                        SaveTodoListFile(todoList, path);
                    }
                    else
                    {
                        SaveTodoListFile(todoList, commandWord[1]);
                        path = commandWord[1];
                        Console.WriteLine($"New path: {path}");
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: {commandWord[0]}");
                }
            } while (commandWord[0] != "quit");
        }
        static void PrintList(List<Activity> todoList, string whatToShow)
        {
            Console.WriteLine("N  datum  S  rubrik");
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < todoList.Count; i++)
            {
                if (todoList[i] != null)
                {
                    if (whatToShow == "all")
                    {
                        Console.WriteLine($"{i + 1}: - {todoList[i].date} - {todoList[i].state} - {todoList[i].title}");
                    }
                    else if (whatToShow == "done")
                    {
                        if (todoList[i].state == "*")
                        {
                            Console.WriteLine($"{i + 1}: - {todoList[i].date} - {todoList[i].state} - {todoList[i].title}");
                        }
                    }
                    else
                    {
                        if (todoList[i].state != "*")
                        {
                            Console.WriteLine($"{i + 1}: - {todoList[i].date} - {todoList[i].state} - {todoList[i].title}");
                        }
                    }
                }
            }
            Console.WriteLine("-------------------------------------------");
        }
        static void MoveInList(List<Activity> todoList, string place, string upOrDown)
        {
            int oldPos = int.Parse(place) - 1;
            if (upOrDown == "up")
            {
                Activity temp = todoList[oldPos];
                todoList.RemoveAt(oldPos);
                todoList.Insert(oldPos - 1, temp);
            }
            else if (upOrDown == "down")
            {
                Activity temp = todoList[oldPos];
                todoList.RemoveAt(oldPos);
                todoList.Insert(oldPos + 1, temp);
            }
        }
        static void SetInList(List<Activity> todoList, string place, string newState)
        {
            int setNum = int.Parse(place) - 1;
            todoList[setNum].state = newState;
        }
        static void DelInList(List<Activity> todoList, string place)
        {
            int delNum = int.Parse(place) - 1;
            Activity tempDel = todoList[delNum];
            todoList.RemoveAt(delNum);
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
                    todoList.Add(A);
                }
                // streamReader.Close();
            }
            return todoList;
        }
        private static void SaveTodoListFile(List<Activity> todoList, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < todoList.Count(); i++)
                {
                    writer.WriteLine($"{todoList[i].date}#{todoList[i].state}#{todoList[i].title}");
                }
                Console.WriteLine("Todolist is saved!");
            }
        }
        private static void AddToListFile(List<Activity> todoList, string date, string title)
        {
            string state = "v";
            todoList.Add(new Activity(date, state, title));
        }
    }
}
