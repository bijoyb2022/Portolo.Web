using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Portolo.Framework.Data
{
    public static class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains");

        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
            {
                return null;
            }

            var param = Expression.Parameter(typeof(T), "t");
            Expression expression = null;

            if (filters.Count == 1)
            {
                expression = GetExpression<T>(param, filters[0]);
            }
            else if (filters.Count == 2)
            {
                expression = GetExpression<T>(param, filters[0], filters[1]);
            }
            else
            {
                while (filters.Any())
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (expression == null)
                    {
                        expression = GetExpression<T>(param, filters[0], filters[1]);
                    }
                    else
                    {
                        expression = Expression.AndAlso(expression, GetExpression<T>(param, filters[0], filters[1]));
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        expression = Expression.AndAlso(expression, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            var member = Expression.Property(param, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);
            switch (filter.Operation)
            {
                case FilterOperation.Equals:
                    return Expression.Equal(member, constant);

                case FilterOperation.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case FilterOperation.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case FilterOperation.LessThan:
                    return Expression.LessThan(member, constant);

                case FilterOperation.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case FilterOperation.Contains:
                    return Expression.Call(member, ContainsMethod, constant);

                case FilterOperation.StartsWith:
                    return Expression.Call(member, StartsWithMethod, constant);

                case FilterOperation.EndsWith:
                    return Expression.Call(member, EndsWithMethod, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, Filter filter1, Filter filter2)
        {
            var bin1 = GetExpression<T>(param, filter1);
            var bin2 = GetExpression<T>(param, filter2);

            return Expression.AndAlso(bin1, bin2);
        }
    }
}