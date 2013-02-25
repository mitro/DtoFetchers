using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DtoFetchers.DataAccess.Extensions;
using DtoFetchers.Domain;
using DtoFetchers.Dto;

namespace DtoFetchers.DataAccess.DtoFetchers
{
    public class FetchMap<TSource, TTarget> : IFetchMap<TSource, TTarget>
        where TSource : Entity
        where TTarget : BaseDto
    {
        // Имя параметра лямбда-выражения для маппинга свойств объектов
        private const string LambdaParameterName = "a";

        // Выражения инициализации свойств
        private readonly IList<MemberAssignment> _propertyInitializers;

        // Делегаты, содержащие сложную логику маппинга свойств
        private readonly IList<Action<IQueryable<TSource>, IEnumerable<TTarget>>> _customFetchOperations;

        // Параметр лямбда-выражения для маппинга свойств объектов
        private readonly ParameterExpression _lambdaParameter;

        // Лямбда-выражение проекции
        private Expression<Func<TSource, TTarget>> _selectExpression;

        /// <summary>
        /// Лямбда-выражение проекции.
        /// </summary>
        /// <example>
        /// Пример возвращаемого выражения:
        /// a => new SomeDto
        ///      {
        ///          Id = a.Id // Инициализация свойства
        ///      }
        /// </example>
        public Expression<Func<TSource, TTarget>> SelectExpression
        {
            get
            {
                return _selectExpression ?? (_selectExpression = CreateSelectExpression());
            }
        }

        /// <summary>
        /// Делегаты, содержащие сложную логику маппинга свойств
        /// </summary>
        public IEnumerable<Action<IQueryable<TSource>, IEnumerable<TTarget>>> CustomFetchOperations
        {
            get { return _customFetchOperations; }
        }

        public FetchMap()
        {
            _propertyInitializers = new List<MemberAssignment>();
            _customFetchOperations = new List<Action<IQueryable<TSource>, IEnumerable<TTarget>>>();
            
            _lambdaParameter = Expression.Parameter(typeof(TSource), LambdaParameterName);
        }

        public IFetchMap<TSource, TTarget> Map<TProperty>(
            Expression<Func<TTarget, TProperty>> targetProperty,
            Expression<Func<TSource, TProperty>> sourceProperty)
        {
            if (sourceProperty.Body is MemberExpression)
            {
                AddInitializerForMemberExpression(targetProperty, sourceProperty);
            }
            else if (sourceProperty.Body is UnaryExpression)
            {
                AddInitializerForUnaryExpression(targetProperty, sourceProperty);
            }
            else
            {
                throw new ArgumentException("Неподдерживаемое выражение");
            }

            return this;
        }

        public IFetchMap<TSource, TTarget> CustomMap(Action<IQueryable<TSource>, IEnumerable<TTarget>> fetchOperation)
        {
            _customFetchOperations.Add(fetchOperation);

            return this;
        }

        /// <summary>
        /// Добавляет инициализатор для свойства, доступ к которому представлен через MemberExpression.
        /// </summary>
        /// <typeparam name="TProperty">Тип свойства</typeparam>
        /// <param name="targetProperty">Выражение для доступа к свойству извлекаемого объекта</param>
        /// <param name="sourceProperty">Выражения для доступа к свойству хранимого объекта</param>
        private void AddInitializerForMemberExpression<TProperty>(Expression<Func<TTarget, TProperty>> targetProperty, Expression<Func<TSource, TProperty>> sourceProperty)
        {
            var sourcePropertyMemberExpression = BuildPropertyMemberExpression((MemberExpression)sourceProperty.Body);
            var propertyInitializer = Expression.Bind(targetProperty.GetPropertyInfo(), sourcePropertyMemberExpression);
            _propertyInitializers.Add(propertyInitializer);
        }

        /// <summary>
        /// Добавляет инициализатор для свойства, доступ к которому представлен через UnaryExpression (в особенности, nullable типы).
        /// </summary>
        /// <typeparam name="TProperty">Тип свойства</typeparam>
        /// <param name="targetProperty">Выражение для доступа к свойству извлекаемого объекта</param>
        /// <param name="sourceProperty">Выражения для доступа к свойству хранимого объекта</param>
        private void AddInitializerForUnaryExpression<TProperty>(Expression<Func<TTarget, TProperty>> targetProperty, Expression<Func<TSource, TProperty>> sourceProperty)
        {
            var unaryExpression = (UnaryExpression)sourceProperty.Body;
            var memberExpression = (MemberExpression)unaryExpression.Operand;
            var type = unaryExpression.Type;
            var sourcePropertyMemberExpression = BuildPropertyMemberExpression(memberExpression);
            var propertyUnaryExpression = Expression.Convert(sourcePropertyMemberExpression, type);
            var propertyInitializer = Expression.Bind(targetProperty.GetPropertyInfo(), propertyUnaryExpression);
            _propertyInitializers.Add(propertyInitializer);
        }

        /// <summary>
        /// Рекурсивно опускается по выражению доступа к свойству и строит новое выражение с нашим параметром лямбда-выражения.
        /// </summary>
        /// <param name="expression">Разбираемое выражение</param>
        /// <param name="properties">Список свойств, которые уже были разобраны</param>
        /// <example>
        /// Обрабатывает как простой доступ к свойству типа a => a.Number,
        /// так и доступ по цепочке типа a => a.Person.Address.Street
        /// Для второго варианта и используется рекурсия
        /// </example>
        /// <returns>
        /// В нетерминальном случае следующее разбираемое подвыражение;
        /// в терминальном - итоговое выражение доступа к свойству с нашим параметром лямба-выражения.
        /// </returns>
        private MemberExpression BuildPropertyMemberExpression(MemberExpression expression, IList<PropertyInfo> properties = null)
        {
            if (properties == null) properties = new List<PropertyInfo>();

            if (expression.Expression == null)
            {
                throw new ArgumentException("Неверное выражение доступа к свойству");
            }

            if (expression.Expression.NodeType == ExpressionType.Parameter)
            {
                properties.Add((PropertyInfo)expression.Member);
                return UnwindPropertyAccessesToMemberExpression(properties);
            }
            if (expression.Expression.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Выражение содержит не только вызовы свойств");
            }

            properties.Add((PropertyInfo)expression.Member);

            return BuildPropertyMemberExpression((MemberExpression)expression.Expression, properties);
        }

        /// <summary>
        /// Строит выражение доступа к свойству с конкретным параметром лямбда-выражения.
        /// </summary>
        /// <param name="properties">Список свойств</param>
        /// <returns>Выражение доступа</returns>
        private MemberExpression UnwindPropertyAccessesToMemberExpression(
            IEnumerable<PropertyInfo> properties)
        {
            List<PropertyInfo> reversedMembers = properties.Reverse().ToList();

            Expression result = _lambdaParameter;

            // Идем по инвертированному списку и соединяем доступ к свойствам уже с нашим параметром лямбда-выражением
            reversedMembers.ForEach(m =>
            {
                result = Expression.MakeMemberAccess(result, m);
            });

            return (MemberExpression)result;
        }

        private Expression<Func<TSource, TTarget>> CreateSelectExpression()
        {
            var objectInitializationExpression = Expression.MemberInit(
                Expression.New(typeof(TTarget)),
                _propertyInitializers
            );

            var selectExpression = Expression.Lambda<Func<TSource, TTarget>>(objectInitializationExpression, _lambdaParameter);

            return selectExpression;
        }
    }
}