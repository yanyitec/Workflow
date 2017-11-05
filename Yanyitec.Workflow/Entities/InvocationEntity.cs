using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow.Entities
{
    public class InvocationEntity
    {
        /// <summary>
        /// 调用Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 调用的流程
        /// </summary>
        public Guid ProcessId { get; set; }
        /// <summary>
        /// 调用的输入,Dictionary<string,object>的json化
        /// </summary>
        public string Inputs { get; set; }
    }
}
