using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Models
{
    internal class Column
    {
        public string Name { get; set; }
        public Task[] Tasks { get; set; }
    }
}
