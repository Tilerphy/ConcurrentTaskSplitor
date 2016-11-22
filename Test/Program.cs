using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tilerphy.ConcurrentTaskSplitor;
using Tilerphy.ConcurrentTaskSplitor.ScriptHandler;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<TaskDescriptor> des = DefaultTaskScript.Read("tasks.txt");
            TasksSplitor manager = new TasksSplitor();
            foreach (TaskDescriptor i in des)
            {
                manager.AddTask(i.ToTaskItem());
            }
            manager.PrepareInfomation();
            int level = 0;
            while (true)
            {
                var tasks = manager.PeekConcurrentTasks();
                if (tasks == null || tasks.Count == 0)
                {
                    Console.WriteLine("Finished.");
                    break;
                }
                else
                {
                    foreach (var tx in tasks)
                    {
                        manager.CompleteTask(tx);
                        Console.WriteLine("[LEVEL: {1}]   {0} is ok.", tx.UniqueNameOrId, level);
                    }
                    level++;
                }
            }
           
            Console.ReadLine();
        }
    }

    
}
