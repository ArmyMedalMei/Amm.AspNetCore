using System.ComponentModel.DataAnnotations.Schema;
using Amm.NetworkMark.Domain.Common;

namespace Amm.AspNetCore.Datas.Entity
{
    /// <summary>
    ///   实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class EntityBase<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        [Column("id")]
        public virtual TPrimaryKey Id { get; set; }
    }
}