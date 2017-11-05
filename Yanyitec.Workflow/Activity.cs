using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yanyitec.Workflow.Definations;

namespace Yanyitec.Workflow
{
    public abstract class Activity
    {
        //internal ActionData Data { get; set; }

        

        public Guid Id { get; private set; }

        public Guid DefinationId { get; private set; }

        public ActivityIdentity Identity {
            get {
                return new ActivityIdentity() { ActivityId = this.Id,DefinationId = this.DefinationId };
            }
        }

        protected ActivityState State { get;private set; }

        
        public Process Process { get; set; }

        public JObject Variables { get; set; }

        

        public object Result { get; set; }

        public ExecuteStates ExecuteStatus { get; protected set; }
        //public Dictionary<string, string> Outputs { get { return this.Data.Outputs; } }

        public abstract ExecuteStates Execute();

        protected internal virtual IList<Guid> GetNextActivityDefinationId() {
            return this.Process.Flow.Associations.Where(p => p.From == this.DefinationId).Select(p => p.To).ToList();


        }

        public bool TryGetValue<T>(string key, out T value) {
            JObject data = null;
            JToken jvalue = data[key];
            if (jvalue == null ) {
                value = default(T);
                return false;
            }
            if (jvalue.Type == JTokenType.Null || jvalue.Type == JTokenType.Undefined) {
                value = default(T);
                return true;
            }
            value = jvalue.ToObject<T>();
            return true;
        }

        public T TryGetValue<T>(string key) {
            JObject data = null;
            JToken jvalue = data[key];
            if (jvalue == null)
            {
                return default(T);
            }
            if (jvalue.Type == JTokenType.Null || jvalue.Type == JTokenType.Undefined)
            {
                return default(T);
            }
            return jvalue.ToObject<T>();
        }

        public T GetVariable<T>(string name)
        {
            JToken jvalue = null;
            if (!this.State.Variables.TryGetValue(name, out jvalue))
            {
                return default(T);
            }
            return jvalue.ToObject<T>();
        }


    }
}
