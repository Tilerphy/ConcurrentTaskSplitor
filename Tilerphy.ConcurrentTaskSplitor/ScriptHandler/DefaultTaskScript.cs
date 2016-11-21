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
        public List<string> Ref { get; set; }
    }
}
