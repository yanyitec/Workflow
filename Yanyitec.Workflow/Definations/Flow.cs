using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow.Definations
{
    public class Flow
    {
        public Guid Id { get; set; }
        public List< Node> Nodes { get; set; }

        public List<Association> Associations { get; set; }

        public Guid StartNodeId { get; set; }

        public Guid EndNodeId { get; set; }

       

        
    }
}
