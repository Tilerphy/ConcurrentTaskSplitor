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
        /// Add task into all tasks with its related tasks
        /// Stackoverflow Exception Warning here. Circle relationship.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {

            if (this.AllTasks.ContainsKey(task.UniqueNameOrId))
            {
                //ignore
            }else
            {
                this.AllTasks.Add(task.UniqueNameOrId, task);
            }

            foreach (string key in task.NeedOthersTasks.Keys)
            {
                this.AddTask(task.NeedOthersTasks[key]);
            }

            foreach (string key in task.RequiredMeTasks.Keys)
            {
                this.AddTask(task.RequiredMeTasks[key]);
            }

        }

        public virtual void PrepareInfomation()
        {
            foreach (string key in this.AllTasks.Keys)
            {
                this.UpdateTaskRelationShip(this.AllTasks[key]);
            }
        }

        /// <summary>
        /// Update the NeedOthersTasks and RequiredMeTasks information.
        /// Cannot chanage the alltasks list.
        /// </summary>
        /// <param name="task"></param>
        protected virtual void UpdateTaskRelationShip(Task task)
        {
                if (task.HasNeedOthersTasks)
                {
                    foreach (string key in task.NeedOthersTasks.Keys)
                    {
                        this.AllTasks[key].RequiredMeTasks.Add(task.UniqueNameOrId, task);
                    }
                }

                if (task.HasRequiredMeTasks)
                {
                    foreach (string key in task.RequiredMeTasks.Keys)
                    {
                        this.AllTasks[key].NeedOthersTasks.Add(task.UniqueNameOrId, task);
                    }
                }
        }

        public void ReadTasksFromFile(Stream stream) { }

        public Task CompleteTask(Task task)
        {
            foreach (string key in task.RequiredMeTasks.Keys)
            {
                this.AllTasks[key].NeedOthersTasks.Remove(task.UniqueNameOrId);
            }

            if (this.Remove(task.UniqueNameOrId))
            {
                IEnumerable<Task> result = FindNoNeedingTasks();
                return result == null ? null: result.First();
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

        public IEnumerable<Task> FindNoNeedingTasks()
        {
            return this.AllTasks
                .Select(v=>v.Value)
                .Where(v =>v.NeedOthersTasks == null || v.NeedOthersTasks.Count == 0);
        }
    }
}
