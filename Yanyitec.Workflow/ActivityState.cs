using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    /// <summary>
    /// 活动状态信息
    /// </summary>
    public class ActivityState
    {
        /// <summary>
        /// 活动状态Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 该活动所属流程
        /// </summary>
        public Guid ProcessId { get; set; }
        /// <summary>
        /// 该活动的定义Id
        /// </summary>
        public Guid DefinationId { get; set; }
        /// <summary>
        /// 该活动的显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 活动执行状态
        /// </summary>

        
        public ExecuteStates Status { get; set; }

        /// <summary>
        /// 该活动的前置活动
        /// </summary>
        public List<ActivityIdentity> Prevs { get; set; }

        /// <summary>
        /// 该活动的执行结果
        /// </summary>
        public object Result { get; set; }

        /// <summary>
        /// 活动的变量
        /// </summary>
        
        public Dictionary<string, object> Variables { get; set; }

    }
}
