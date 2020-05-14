using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public class Log
    {
        public Log() { }

        [Key]
        public int LogId { get; set; }
        public string LogString { get; set; }
        public DateTime LogDateTime { get; set; }
    }
}