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
        public List<string> RequiredMeTasks { get; set; }

        /// <summary>
        /// This task is requiring which tasks.
        /// </summary>
        public List<string> NeedOthersTasks { get; set; }

        public Task()
        {
            this.NeedOthersTasks = new List<string>();
            this.RequiredMeTasks = new List<string>();
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
