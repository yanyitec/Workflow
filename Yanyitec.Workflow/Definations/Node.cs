using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow.Definations
{
    public class Node :Element
    {
        public string Alias { get; set; }
        public string ActivityType { get; set; }
        public Dictionary<string, string> Variables { get; set; }


    }
}
