using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.API.Entity
{
    /// <summary>
    /// 唯一性
    /// </summary>
    public class UniquedAttribute : Attribute
    {
        public UniquedAttribute(string errorMessage)
        {

        }

        /// <summary>
        /// 获取或设置错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}