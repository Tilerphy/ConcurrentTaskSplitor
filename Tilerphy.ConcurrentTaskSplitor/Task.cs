using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor
{
    public class Task
    {
        public string UniqueNameOrId { get; set; }
        /// <summary>
        /// Work
        /// </summary>
        public object Invoker { get; set; }

        /// <summary>
        /// Which tasks are requiring this task.
        /// </summary>
        public Dictionary<string, Tilerphy.ConcurrentTaskSplitor.Task> RequiredMeTasks { get; set; }

        /// <summary>
        /// This task is requiring which tasks.
        /// </summary>
        public Dictionary<string, Tilerphy.ConcurrentTaskSplitor.Task> NeedOthersTasks { get; set; }

        public Task()
        {
            this.NeedOthersTasks = new Dictionary<string, Task>();
            this.RequiredMeTasks = new Dictionary<string, Task>();
        }
        public bool HasRequiredMeTasks
        {
            get
            {
                return this.RequiredMeTasks != null && this.RequiredMeTasks.Count != 0;
            }
        }

        public bool HasNeedOthersTasks
        {
            get
            {
                return this.NeedOthersTasks != null && this.NeedOthersTasks.Count != 0;
            }
        }
    }
}
