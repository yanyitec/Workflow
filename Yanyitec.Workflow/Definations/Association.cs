using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Workflow.Definations
{
    public class Association:Element
    {
        public Guid From { get; set; }
        public Guid To { get; set; }

        public string Value { get; set; }

        public IList<Point> Points { get; set; }
    }
}
