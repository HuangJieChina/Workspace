using HH.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HH.API.Entity
{
    /// <summary>
    /// 所有实体类的基类
    /// </summary>
    [Serializable]
    public abstract class EntityBase
    {
        #region 构造函数 -----------------------
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public EntityBase() : this(null)
        {
        }

        /// <summary>
        /// 基类构造函数
        /// </summary>
        /// <param name="objectId"></param>
        public EntityBase(string objectId)
        {
            this.ObjectId = ObjectId;
            this.CreatedTime = DateTime.Now;
            this.ModifiedTime = DateTime.Now;
        }
        #endregion

        #region 公共属性定义 -------------------
        /// <summary>
        /// 获取数据库表名称
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// ObjectID
        /// </summary>
        public const string PropertyName_ObjectId = "ObjectId";

        private string _ObjectId = null;

        /// <summary>
        /// 获取或设置主键字段
        /// </summary>
        [Key]
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ObjectId
        {
            get
            {
                if (this._ObjectId == null)
                {
                    this._ObjectId = Guid.NewGuid().ToString();
                }
                return this._ObjectId;
            }
            set { this._ObjectId = value; }
        }

        /// <summary>
        /// CreateBy
        /// </summary>
        public const string PropertyName_CreateBy = "CreateBy";

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// CreatedTime
        /// </summary>
        public const string PropertyName_CreatedTime = "CreatedTime";

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置修改人
        /// </summary>
        [Column(TypeName = "char")]
        [StringLength(36)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 获取或设置修改时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 获取或设置扩展字段1
        /// </summary>
        public string ExtendField1 { get; set; }

        /// <summary>
        /// 获取或设置扩展字段2
        /// </summary>
        public string ExtendField2 { get; set; }

        /// <summary>
        /// 获取或设置扩展字段3
        /// </summary>
        public string ExtendField3 { get; set; }
        #endregion

        #region 基类方法定义 -------------------
        /// <summary>
        /// 数据格式校验
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool Validate(ref List<string> errors)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                RequiredAttribute required = property.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {// 检查必填项
                    string propertyValue = (property.GetValue(this) + string.Empty).Trim();
                    if (string.IsNullOrEmpty(propertyValue))
                    {
                        if (!string.IsNullOrEmpty(required.ErrorMessage))
                        {
                            errors.Add(required.ErrorMessage);
                        }
                        else
                        {
                            errors.Add(string.Format("The attribute name [{0}] is not allowed to be empty",
                                property.Name));
                        }
                    }
                }
                StringLengthAttribute stringLength = property.GetCustomAttribute<StringLengthAttribute>();
                if (stringLength != null)
                {// 长度是否有超出
                    string propertyValue = (property.GetValue(this) + string.Empty).Trim();
                    if (propertyValue.Length > stringLength.MaximumLength
                        || propertyValue.Length < stringLength.MinimumLength)
                    {
                        if (!string.IsNullOrEmpty(stringLength.ErrorMessage))
                        {
                            errors.Add(stringLength.ErrorMessage);
                        }
                        else
                        {
                            errors.Add(string.Format("The attribute name [{0}] content length must between {1} and {2}.",
                                property.Name, stringLength.MinimumLength, stringLength.MaximumLength));
                        }
                    }
                }
            }
            return errors.Count == 0;
        }

        /// <summary>
        /// 获取Md5加密结果(32位)
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public string GetMD5Encrypt32(string inputValue)
        {
            return MD5Encryptor.GetMD5Encrypt32(inputValue);
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Type={0},ObjectID={1},CreatedTime={2}",
                this.GetType().FullName,
                this.ObjectId,
                this.CreatedTime.ToLongTimeString());
        }
        #endregion
    }
}
