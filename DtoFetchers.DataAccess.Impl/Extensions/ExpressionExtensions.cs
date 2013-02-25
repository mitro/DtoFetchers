using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DtoFetchers.DataAccess.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Возвращает информацию о свойстве по выражению свойства.
        /// </summary>
        /// <typeparam name="TSource">Тип объекта</typeparam>
        /// <typeparam name="TValue">Тип свойства</typeparam>
        /// <param name="propertyExpression">Выражение свойства</param>
        /// <returns>Информация о свойстве</returns>
        public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> propertyExpression)
        {
            return (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;
        }
    }
}