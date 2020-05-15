using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public class Log_Archive
    {
        public Log_Archive() { }
        [Key]
        public int LogArchiveID { get; set; }
        public string LogString { get; set; }
        public DateTime LogDateTime { get; set; }
    }
}