using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor
{
    public class TaskItem
    {
        public string UniqueNameOrId { get; set; }

        /// <summary>
        /// Which tasks are requiring this task.
        /// </summary>
        public List<string> PreActions { get; set; }

        /// <summary>
        /// This task is requiring which tasks.
        /// </summary>
        public List<string> PostActions { get; set; }

        public TaskItem()
        {
            this.PostActions = new List<string>();
            this.PreActions = new List<string>();
        }
        public bool HasRequiredMeTasks
        {
            get
            {
                return this.PreActions != null && this.PreActions.Count != 0;
            }
        }

        public bool HasNeedOthersTasks
        {
            get
            {
                return this.PostActions != null && this.PostActions.Count != 0;
            }
        }
    }
}
