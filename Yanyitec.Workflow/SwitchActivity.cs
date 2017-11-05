using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    public abstract class SwitchActivity: Activity
    {
        protected internal override IList<Guid> GetNextActivityDefinationId()
        {
            return this.Process.Flow.Associations
                .Where(p => p.From == this.DefinationId && p.Value == this.Result.ToString())
                .Select(p=>p.Id).ToList();
        }
    }
}
