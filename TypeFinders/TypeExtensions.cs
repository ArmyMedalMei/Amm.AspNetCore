﻿#region 版权信息

//  ---------------------------------------------------------------------
// - 文件名: TypeExtensions.cs
// - 项目名: Amm.AspNetCore
// - 作   者：梅军章
// - 创建时间：20181227
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#endregion

namespace Amm.AspNetCore.TypeFinders
{
    /// <summary>
    ///     类型<see cref="Type" />辅助扩展方法类
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     判断当前类型是否可由指定类型派生
        /// </summary>
        public static bool IsDeriveClassFrom<TBaseType>(this Type type, bool canAbstract = false)
        {
            return IsDeriveClassFrom(type, typeof(TBaseType), canAbstract);
        }

        /// <summary>
        ///     判断当前类型是否可由指定类型派生
        /// </summary>
        public static bool IsDeriveClassFrom(this Type type, Type baseType, bool canAbstract = false)
        {
            return type.IsClass && !canAbstract && !type.IsAbstract && type.IsBaseOn(baseType);
        }

        /// <summary>
        ///     判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"> 要处理的类型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        ///     由类型的Nullable类型返回实际类型
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetNonNummableType(this Type type)
        {
            if (IsNullableType(type)) return type.GetGenericArguments()[0];
            return type;
        }

        /// <summary>
        ///     通过类型转换器获取Nullable类型的基础类型
        /// </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                var nullableConverter = new NullableConverter(type);
                return nullableConverter.UnderlyingType;
            }

            return type;
        }

        /// <summary>
        ///     获取类型的Description特性描述信息
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <param name="inherit">是否搜索类型的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回类型的全名</returns>
        public static string GetDescription(this Type type, bool inherit = false)
        {
            var desc = type.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? type.FullName : desc.Description;
        }

        /// <summary>
        ///     获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">成员元数据对象</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称</returns>
        public static string GetDescription(this MemberInfo member, bool inherit = false)
        {
            var desc = member.GetAttribute<DescriptionAttribute>(inherit);
            if (desc != null) return desc.Description;
            var displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
            if (displayName != null) return displayName.DisplayName;
            var display = member.GetAttribute<DisplayAttribute>(inherit);
            if (display != null) return display.Name;
            return member.Name;
        }

        /// <summary>
        ///     检查指定指定类型成员中是否存在指定的Attribute特性
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        ///     从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            var descripts = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return descripts.FirstOrDefault() as T;
        }

        /// <summary>
        ///     从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>返回所有指定Attribute特性的数组</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        /// <summary>
        ///     判断类型是否为集合类型
        /// </summary>
        /// <param name="type">要处理的类型</param>
        /// <returns>是返回True，不是返回False</returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type == typeof(string)) return false;
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        ///     判断当前泛型类型是否可由指定类型的实例填充
        /// </summary>
        /// <param name="genericType">泛型类型</param>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericType, Type type)
        {
            if (!genericType.IsGenericType) throw new ArgumentException("该功能只支持泛型类型的调用，非泛型类型可使用 IsAssignableFrom 方法。");

            var allOthers = new List<Type> {type};
            if (genericType.IsInterface) allOthers.AddRange(type.GetInterfaces());

            foreach (var other in allOthers)
            {
                var cur = other;
                while (cur != null)
                {
                    if (cur.IsGenericType) cur = cur.GetGenericTypeDefinition();
                    if (cur.IsSubclassOf(genericType) || cur == genericType) return true;
                    cur = cur.BaseType;
                }
            }

            return false;
        }

        /// <summary>
        ///     方法是否是异步
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                   || method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }

        /// <summary>
        ///     返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="baseType">要判断的基类型</param>
        /// <returns></returns>
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition) return baseType.IsGenericAssignableFrom(type);
            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        ///     返回当前类型是否是指定基类的派生类
        /// </summary>
        /// <typeparam name="TBaseType">要判断的基类型</typeparam>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsBaseOn<TBaseType>(this Type type)
        {
            var baseType = typeof(TBaseType);
            return type.IsBaseOn(baseType);
        }

        /// <summary>
        ///     获取类型的全名，附带所在类库
        /// </summary>
        public static string GetFullNameWithModule(this Type type)
        {
            return $"{type.FullName},{type.Module.Name.Replace(".dll", "").Replace(".exe", "")}";
        }
    }
}