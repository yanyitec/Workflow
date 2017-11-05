using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow.Definations
{
    public class Element
    {
        public Guid FlowId { get; set; }
        public Guid Id { get; set;}
        public Point P0 { get; set; }
        public Point P1 { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ElementTypes Type{ get; set; }
    }
}
