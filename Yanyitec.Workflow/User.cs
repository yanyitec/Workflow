using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    public class User
    {
        public Guid Id { get; set; }

        public long No { get; set; }
        public string Type { get; set; }
        public string DisplayName{get;set;}
        public string Username { get; set; }
    }
}
