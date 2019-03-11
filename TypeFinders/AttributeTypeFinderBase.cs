using System;
using System.Linq;

namespace Amm.AspNetCore.TypeFinders
{
    /// <summary>
    ///   属性查找基类
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public class AttributeTypeFinderBase<TAttribute> where TAttribute : Attribute
    {
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// AttributeTypeFinderBase
        /// </summary>
        /// <param name="finder"></param>
        public AttributeTypeFinderBase(ITypeFinder finder)
        {
            _typeFinder = finder;
        }

        /// <summary>
        ///  查找所有项
        /// </summary>
        /// <returns></returns>
        public Type[] FindAttributeClassItems()
        {
            var assemblies = _typeFinder.GetAssemblies();
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && type.HasAttribute<TAttribute>()).Distinct().ToArray();
        }
    }
}
