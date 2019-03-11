#region 版权信息
//  ---------------------------------------------------------------------
// - 文件名: MapTupleProfile.cs
// - 项目名: Amm.NetworkMark.Core
// - 作   者：梅军章
// - 创建时间：20181125
//  - © 2002-2030. All rights reserved.
// ---------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Amm.AspNetCore.Collections;
using Amm.AspNetCore.TypeFinders;
using AutoMapper;

namespace Amm.AspNetCore.AutoMappers
{
    /// <summary>
    ///   映射集合组合列表
    /// </summary>
    public class MapTupleProfile : Profile, IMapTuple
    {

        private readonly IAutoMapAttributeFinder _atuAutoMapAttributeFinder;

        /// <summary>
        /// MapTupleProfile
        /// </summary>
        public MapTupleProfile(IAutoMapAttributeFinder atuAutoMapAttributeFinder)
        {
            _atuAutoMapAttributeFinder = atuAutoMapAttributeFinder;
        }

        /// <summary>
        ///   创建map
        /// </summary>
        public void CreateMap()
        {
            var tuples = new List<(Type Source, Type Target)>();

            var types = _atuAutoMapAttributeFinder.FindAttributeClassItems();
            foreach (var targetType in types)
            {
                //如果有基础继承的类型的，则也一同进行实体映射
                if (targetType.BaseType != null)
                {
                    var baseTypeAttribute = targetType.BaseType.GetAttribute<AutoMapAttribute>();
                    tuples.AddIfNotExist(ValueTuple.Create(targetType, targetType.BaseType));
                    if (baseTypeAttribute != null)
                    {
                        //遍历来源类型集合
                        foreach (var sourceType in baseTypeAttribute.SourceTypes)
                        {
                            var tuple = ValueTuple.Create(sourceType, targetType);
                            tuples.AddIfNotExist(tuple);
                        }
                    }
                }

                //没有获取到属性标签的话，则继续
                var attribute = targetType.GetAttribute<AutoMapAttribute>();
                if (attribute == null) continue;

                //遍历来源类型集合
                foreach (var sourceType in attribute.SourceTypes)
                {
                    var tuple = ValueTuple.Create(sourceType, targetType);
                    tuples.AddIfNotExist(tuple);
                }
            }

            //遍历元祖，创建类的映射关系
            foreach (var tuple in tuples)
            {
                CreateMap(tuple.Source, tuple.Target);
                CreateMap(tuple.Target, tuple.Source);
            }
        }
    }
}