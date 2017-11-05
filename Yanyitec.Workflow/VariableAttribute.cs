using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow
{
    public class VariableAttribute : Attribute
    {
        public VariableAttribute(string path) { this.Path = path; }
        public string Path { get; private set; }
    }
}
