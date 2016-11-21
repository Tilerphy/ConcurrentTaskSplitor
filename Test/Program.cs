using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tilerphy.ConcurrentTaskSplitor;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Tilerphy.ConcurrentTaskSplitor.Task t = new Tilerphy.ConcurrentTaskSplitor.Task();
            t.UniqueNameOrId = "Task1";
            t.NeedOthersTasks.Add("Task2");

            Tilerphy.ConcurrentTaskSplitor.Task t2 = new Tilerphy.ConcurrentTaskSplitor.Task();
            t2.UniqueNameOrId = "Task2";
            t2.NeedOthersTasks.Add("Task3");

            Tilerphy.ConcurrentTaskSplitor.Task t3 = new Tilerphy.ConcurrentTaskSplitor.Task();
            t3.UniqueNameOrId = "Task3";

            Tilerphy.ConcurrentTaskSplitor.Task t4 = new Tilerphy.ConcurrentTaskSplitor.Task();
            t4.UniqueNameOrId = "Task4";
            t4.NeedOthersTasks.Add("Task2");
            TasksManager manager = new TasksManager();
            manager.AddTask(t);
            manager.AddTask(t2);
            manager.AddTask(t3);
            manager.AddTask(t4);
            manager.PrepareInfomation();
            var xx = manager.FindNoNeedingTasks();

            foreach (var tx in xx)
            {
                manager.CompleteTask(tx);
            }

            Console.WriteLine();
        }
    }
}
