using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor
{
    public class TasksManager
    {
        public List<Worker> Workers { get; set; }
        public Dictionary<string, Task> AllTasks { get; set; }

        public TasksManager()
        {
            this.AllTasks = new Dictionary<string, Task>();
            this.Workers = new List<Worker>();
            this.Workers.Add(new Worker());
            this.Workers.Add(new Worker());
        }
        /// <summary>
        /// Add task
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {
           this.AllTasks.Add(task.UniqueNameOrId, task);
        }

        protected virtual Task FindTaskDetail(string uniqueNameOrId)
        {
            return new Task();
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
        protected virtual void FixRelationship(Task task)
        {
                if (task.HasNeedOthersTasks)
                {
                    foreach (string key in task.NeedOthersTasks)
                    {
                        this.AllTasks[key].RequiredMeTasks.Add(task.UniqueNameOrId);
                    }
                }
        }

        public void ReadTasksFromFile(Stream stream) { }

        public Task CompleteTask(Task task)
        {
            foreach (string key in task.RequiredMeTasks)
            {
                this.AllTasks[key].NeedOthersTasks.Remove(task.UniqueNameOrId);
            }

            if (this.Remove(task.UniqueNameOrId))
            {
                List<Task> result = FindNoNeedingTasks();
                return result == null || result.Count == 0 ? null: result.First();
            }
            else
            {
                throw new Exception("Cannot complete the tasks, the TasksManager cannot run well.");
            }
        }

        public bool Remove(string name)
        {
            return this.AllTasks.Remove(name);
        }

        public List<Task> FindNoNeedingTasks()
        {
            return this.AllTasks
                .Select(v=>v.Value)
                .Where(v =>v.NeedOthersTasks == null || v.NeedOthersTasks.Count == 0).ToList();
        }
    }
}
