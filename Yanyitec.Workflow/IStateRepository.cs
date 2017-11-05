using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    public interface IStateRepository
    {
        bool AddProcessState(ProcessState process);
        bool SaveProcessState(ProcessState process);
        bool AddActivityState(ActivityState activity);
        bool SaveActivityState(ActivityState activity);
    }
}
