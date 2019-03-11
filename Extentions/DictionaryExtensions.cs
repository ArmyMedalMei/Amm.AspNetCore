#region 版权信息

// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Core
// 文件名称：DictionaryExtensions.cs
// 版本号: V1.0.0.0
// 创建时间：2018-12-25 11:17
// 更改时间：2018-12-25 11:17
// ------------------------------------------------------------------------------

#endregion

#region 项目引用

using System;
using System.Collections.Generic;

#endregion

namespace Amm.AspNetCore.Extentions
{
    /// <summary>
    ///     字典辅助扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     从字典中获取值，不存在则返回字典<typeparamref name="TValue" />类型的默认值
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="TValue">字典值类型</typeparam>
        /// <param name="dictionary">要操作的字典</param>
        /// <param name="key">指定键名</param>
        /// <returns>获取到的值</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default(TValue);
        }

        /// <summary>
        ///     获取指定键的值，不存在则按指定委托添加值
        /// </summary>
        /// <typeparam name="TKey">字典键类型</typeparam>
        /// <typeparam name="TValue">字典值类型</typeparam>
        /// <param name="dictionary">要操作的字典</param>
        /// <param name="key">指定键名</param>
        /// <param name="addFunc">添加值的委托</param>
        /// <returns>获取到的值</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> addFunc)
        {
            if (dictionary.TryGetValue(key, out var value)) return value;
            return dictionary[key] = addFunc();
        }
    }
}