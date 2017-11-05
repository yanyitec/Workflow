using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yanyitec.Workflow.Definations;

namespace Yanyitec.Workflow
{
    public class InjectionGenerator
    {

        public static Action<Activity, JObject, JObject> Generate(Type activityType,Node node) {
            //JObject global = null;
            JObject jv = null;
            JToken jt = jv.SelectToken("Abc");
            
            
            var members = activityType.GetProperties();
            var exprs = new List<Expression>();
            var activity = Expression.Parameter(typeof(Activity),"activity");
            var global = Expression.Parameter(typeof(JObject),"var_global");
            var local = Expression.Parameter(typeof(JObject), "var_local");
            var jvalue = Expression.Parameter(typeof(JToken),"jvalue");
            var codes = new List<Expression>();
            foreach (var prop in members) {
                var vAttr = prop.GetCustomAttribute<VariableAttribute>();
                if (vAttr == null) continue;
                string path = vAttr.Path;
                if (!node.Variables.TryGetValue(prop.Name, out path)) {
                    if (string.IsNullOrWhiteSpace(path)) continue;
                }
                var method = GetVariableMethod.MakeGenericMethod(prop.PropertyType);

                var assign = Expression.Assign(Expression.Property(activity,prop), Expression.Call(activity,method,Expression.Constant(path)));
                codes.Add(assign);

                //codes.Add(GenPropSetting(prop,path,node,jvalue,local,global));
            }
            var block = Expression.Block(new List<ParameterExpression>() { jvalue },codes);
            var lamda = Expression.Lambda<Action<Activity, JObject, JObject>>(block, activity,global,local);
            return lamda.Compile();
            //global.SelectToken
            //return null;
        }

        //static MethodInfo SelectTokenMethod = typeof(Activity).GetMethod("SelectToken");
        static MethodInfo GetVariableMethod = typeof(Activity).GetMethod("GetVariable`1");

        static void GenPropSetting(List<Expression> codes,ParameterExpression activity,PropertyInfo propInfo,string path,ParameterExpression jvalue,ParameterExpression varExpr) {
            var method = GetVariableMethod.MakeGenericMethod(propInfo.PropertyType);
            var assignExpr = Expression.Assign(jvalue, Expression.Call(varExpr, method, Expression.Constant(path)));
            var conditionExpr = Expression.AndAlso(
                Expression.NotEqual(jvalue,Expression.Constant(null,typeof(JToken)))
                ,Expression.AndAlso(
                    Expression.NotEqual(Expression.Property(jvalue,"Type"),Expression.Constant(JTokenType.Null))
                    ,Expression.NotEqual(Expression.Property(jvalue, "Type"), Expression.Constant(JTokenType.Undefined))
                    )
                );
            
            var ifThenExpr = Expression.IfThen(conditionExpr, Expression.Assign(
                    Expression.Property(activity,propInfo)
                    ,jvalue
                ));
            codes.Add(assignExpr);
            codes.Add(ifThenExpr);

        }
    }
}
