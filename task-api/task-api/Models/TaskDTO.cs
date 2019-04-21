using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_api.Models
{
    public class TaskDTO
    {
        public long id { get; set; }
        public string nomeTask { get; set; }
        public string descricaoTask { get; set; }
        public string statusTask { get; set; }

    }
}
