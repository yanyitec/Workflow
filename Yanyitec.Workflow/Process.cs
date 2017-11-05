using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yanyitec.Workflow.Definations;

namespace Yanyitec.Workflow
{
    public class Process 
    {
        public IAcitivityFactory ActivityFactory { get; set; }
        public IStateRepository StateRepository { get; set; }

        public Guid Id { get; private set; }

        public Flow Flow { get;private set; }

        public InvocationState InvocationState { get; set; }

        protected ProcessState State{ get; set; } 

        public Process(Flow flow) {
            this.Flow = flow;
        }

        public void Start(JObject variables) {
            if (this.State != null) {
                throw new InvalidOperationException("已经具有State数据，该过程已开始，不能重复开始一个过程。");
            }
            var state = this.State = new ProcessState() {
                Id = this.Id = Guid.NewGuid(),
                Variables = variables,
                DealingActivityStates = new List<ActivityState>(),
                Status = ExecuteStates.Initial
            };
            
            var startNode = this.Flow.Nodes.FirstOrDefault(p=>p.Id == this.Flow.StartNodeId);
            var startActivityData = new ActivityState() {
                Id= Guid.NewGuid(),
                DefinationId = startNode.Id,
                ProcessId = this.Id,
                Status = ExecuteStates.Initial
            };
            state.DealingActivityStates.Add(startActivityData);
            this.StateRepository.AddProcessState(state);
            this.StateRepository.AddActivityState(startActivityData);
        }

        public ExecuteStates Execute()
        {
            var dealingActivityStates = new List<ActivityState>();
            
            var todoStates = new Queue<ActivityState>(this.State.DealingActivityStates);
            while(todoStates.Count>0) {
                var activityState = todoStates.Dequeue();
                var node = this.Flow.Nodes.FirstOrDefault(p=>p.Id== activityState.DefinationId);
                var activity = this.CreateActivity(node,activityState);
                var executeStatus = activityState.Status = activity.Execute();
                this.StateRepository.SaveActivityState(activityState);
                if (executeStatus == ExecuteStates.Done)
                {
                    if (node.Id == this.Flow.EndNodeId)
                    {
                        #region 尾节点，结束该Process
                        this.State.DealingActivityStates = null;
                        this.State.Status = ExecuteStates.Done;
                        this.StateRepository.SaveProcessState(this.State);
                        return ExecuteStates.Done;
                        #endregion
                    }
                    else
                    {
                        #region 添加后面的节点
                        var nextActivityDefinationIds = activity.GetNextActivityDefinationId();
                        foreach (var did in nextActivityDefinationIds)
                        {
                            ActivityState nextState = todoStates.FirstOrDefault(p=>p.DefinationId== did);
                            if (nextState == null) nextState = dealingActivityStates.FirstOrDefault(p=>p.DefinationId == did);
                            //todoStates
                            if (nextState == null)
                            {
                                nextState = new ActivityState()
                                {
                                    Id = Guid.NewGuid(),
                                    DefinationId = did,
                                    ProcessId = this.Id,
                                    Prevs = new List<ActivityIdentity>() { activity.Identity },
                                    Status = ExecuteStates.Initial
                                };
                                todoStates.Enqueue(nextState);
                                this.StateRepository.AddActivityState(nextState);
                            }
                            else {
                                nextState.Prevs.Add(activity.Identity);
                            }
                        }
                        #endregion
                    }
                }
                else {
                    dealingActivityStates.Add(activityState);
                }
            }
            if (dealingActivityStates.Count == 0) {
                throw new Exception("流程未能走到结束节点，单已经没有下一个节点可以执行");
            }
            this.State.DealingActivityStates = dealingActivityStates;
            this.State.Status = ExecuteStates.Suspended;
            return ExecuteStates.Suspended;
        }

        protected virtual Activity CreateActivity(Node node,ActivityState activityState) {
            Activity activity = null;
            return activity;
        }

        public T GetVariable<T>(string name) {
            JToken jvalue = this.State.Variables.SelectToken(name);
            if (jvalue == null || jvalue.Type== JTokenType.Undefined || jvalue.Type == JTokenType.Null) {
                return default(T);
            }
            return jvalue.ToObject<T>();
        }

        //protected virtual bool AddNextActivity(Activity prevActivity,IList<ActivityData> actDatas) {
        //    var data = prevActivity.GetNextActivityData();
        //    if (data != null) { actDatas.Add(data); return true; }
        //    else return false;
        //} 
    }
}
