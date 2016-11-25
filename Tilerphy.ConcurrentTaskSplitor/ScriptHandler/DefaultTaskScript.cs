using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilerphy.ConcurrentTaskSplitor.ScriptHandler
{
    public class DefaultTaskScript
    {
        public static List<TaskDescriptor> Read(string path)
        {
            using (StreamReader reader  = new StreamReader(path))
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TaskDescriptor>>(reader.ReadToEnd());
                return result;
            }
        }
    }

    public class TaskDescriptor
    {
        public string Name { get; set; }
        public List<string> Refs { get; set; }

        public Tilerphy.ConcurrentTaskSplitor.TaskItem ToTaskItem()
        {
            Tilerphy.ConcurrentTaskSplitor.TaskItem task = new Tilerphy.ConcurrentTaskSplitor.TaskItem();
            task.UniqueNameOrId = this.Name;
            if (this.Refs != null)
            {
                foreach (string r in this.Refs)
                {
                    task.PostActions.Add(r);
                }
            }
            return task;
        }
    }
}
