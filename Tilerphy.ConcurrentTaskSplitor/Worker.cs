using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor
{
    public class Worker
    {
        public Tilerphy.ConcurrentTaskSplitor.Task Task { get; set; }
        public TasksManager Manager { get; set; }
    }
}
