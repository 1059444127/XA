using System.Linq.Expressions;
using System.Reflection;

namespace UIH.XA.PAUtilityCSharp.Global
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Converts an expression into a <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MemberInfo GetMemberInfo(this System.Linq.Expressions.Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}
