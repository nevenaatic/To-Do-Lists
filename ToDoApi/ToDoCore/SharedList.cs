using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCore
{
    public class SharedList
    {
        public Guid ListId { get; set; }
        public Guid GeneratedLink { get; set; }
        public DateTime ValidTime { get; set; }
    }
}
