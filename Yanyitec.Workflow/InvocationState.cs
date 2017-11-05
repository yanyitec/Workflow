using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    /// <summary>
    /// 一次工作流调用的状态信息
    /// </summary>
    public class InvocationState
    {
        /// <summary>
        /// 调用Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public ProcessState ProcessState { get; set; }
        /// <summary>
        /// 本次调用的输入参数
        /// </summary>
        public Dictionary<string, object> Inputs { get; set; }

        public ExecuteStates Status { get; set; }



        /// <summary>
        /// 本次调用使用过的Activity
        /// </summary>
        public List<ActivityState> ActivityStates { get; set; }
        /// <summary>
        /// 调用开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 调用结束时间
        /// </summary>

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 调用错误
        /// </summary>
        public Exception Error { get; set; }

        

        
    }
}
