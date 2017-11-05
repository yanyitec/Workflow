using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yanyitec.Workflow.Definations;

namespace Yanyitec.Workflow
{
    public interface IAcitivityFactory
    {
        Activity CreateActivity(Node node,JObject global,JObject local);
    }
}
