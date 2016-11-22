using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor
{
    public class TasksSplitor
    {
        public Dictionary<string, TaskItem> AllTasks { get; set; }

        public TasksSplitor()
        {
            this.AllTasks = new Dictionary<string, TaskItem>();
        }
        /// <summary>
        /// Add task
        /// </summary>
        /// <param name="task"></param>
        public virtual void AddTask(TaskItem task)
        {
           this.AllTasks.Add(task.UniqueNameOrId, task);
        }

        public virtual void PrepareInfomation()
        {
            foreach (string key in this.AllTasks.Keys)
            {
                this.FixRelationship(this.AllTasks[key]);
            }
        }

        /// <summary>
        /// Find which task need the param<task>, and add it into alltasks
        /// </summary>
        /// <param name="task"></param>
        protected virtual void FixRelationship(TaskItem task)
        {
                if (task.HasNeedOthersTasks)
                {
                    foreach (string key in task.NeedOthersTasks)
                    {
                        this.AllTasks[key].RequiredMeTasks.Add(task.UniqueNameOrId);
                    }
                }
        }

        public virtual void CompleteTask(TaskItem task)
        {
            foreach (string key in task.RequiredMeTasks)
            {
                this.AllTasks[key].NeedOthersTasks.Remove(task.UniqueNameOrId);
            }
            this.Remove(task.UniqueNameOrId);
        }

        protected virtual bool Remove(string name)
        {
            return this.AllTasks.Remove(name);
        }

        /// <summary>
        /// Get the tasks.
        /// </summary>
        /// <returns></returns>
        public virtual  List<TaskItem> PeekConcurrentTasks()
        {
            return this.AllTasks
                .Select(v=>v.Value)
                .Where(v =>v.NeedOthersTasks == null || v.NeedOthersTasks.Count == 0).ToList();
        }

        /// <summary>
        /// Get the tasks and delete them from the list.
        /// </summary>
        /// <returns></returns>
        public virtual List<TaskItem> PopConcurrentTasks()
        {
            List<TaskItem> items = PeekConcurrentTasks();
            foreach (TaskItem item in items)
            {
                this.CompleteTask(item);
            }
            return items;
        }
    }
}
