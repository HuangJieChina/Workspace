using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HH.API.Entity.Database
{
    /// <summary>
    /// 标记为是否唯一
    /// </summary>
    public class UniquedAttribute : ValidationAttribute
    {
        public UniquedAttribute()
        {

        }
    }
}
