using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    public class ConcurrentJoinActivity : Activity
    {
        public override ExecuteStates Execute()
        {
            var prevs = this.Process.Flow.Associations.Where(p=>p.To == this.DefinationId).ToList();
            if (this.State.Prevs == null || this.State.Prevs.Count==0) throw new Exception("不正确的流程，CurrentJoinActivity至少要有一个前置节点");
            if (prevs.Count != this.State.Prevs.Count) return ExecuteStates.Suspended;
            foreach (var prev in prevs) {
                if (!this.State.Prevs.Any(p => p.ActivityId == prev.From)) return ExecuteStates.Suspended;
            }
            return ExecuteStates.Done;
        }
    }
}
