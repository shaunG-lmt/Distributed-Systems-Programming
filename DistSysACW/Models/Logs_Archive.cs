using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public class Logs_Archive
    {
        public Logs_Archive() { }

        [Key]
        public int LogID { get; set; }
        public string LogApiKey { get; set; }
        public string LogString { get; set; }
        public DateTime LogDateTime { get; set; }
    }
}