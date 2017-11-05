using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    /// <summary>
    /// 流程实例状态信息
    /// </summary>
    public class ProcessState
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 该流程被哪个流程所调用
        /// </summary>
        public Guid? CallerId { get; set; }

        /// <summary>
        /// 流程定义Id
        /// </summary>
        public Guid DefinationId { get; set; }

        /// <summary>
        /// 流程显示名
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        /// 流程变量(Input)
        /// </summary>
        public Dictionary<string, object> Variables { get; set; }

        public object Result { get; set; }

        /// <summary>
        /// 流程执行状态
        /// </summary>
        public ExecuteStates Status { get; set; }

        /// <summary>
        /// 即将处理的Activity
        /// </summary>
        public List<ActivityState> DealingActivityStates{ get; set; }

        

        

    }
}
