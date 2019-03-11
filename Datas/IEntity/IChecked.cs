#region 版权信息
// ------------------------------------------------------------------------------
// Copyright: (c) 2018  梅军章
// 项目名称：Amm.NetworkMark.Domain
// 文件名称：IPageListCheck.cs
// 版本号: V1.0.0.0
// 创建时间：2018-11-22 13:36
// 更改时间：2018-11-22 13:36
// ------------------------------------------------------------------------------
#endregion

namespace Amm.NetworkMark.Domain.Common
{
    /// <summary>
    ///  是否选中
    /// </summary>
    public interface IChecked
    {
        bool IsChecked { get; set; }
    }
}