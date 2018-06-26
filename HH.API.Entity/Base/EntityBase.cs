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
        /// <summary>
        /// 基类构造函数
        /// </summary>
        public EntityBase()
        {
            this.CreatedTime = DateTime.Now;
            this.ModifiedTime = DateTime.Now;
        }

        /// <summary>
        /// 基类构造函数
        /// </summary>
        /// <param name="objectId"></param>
        public EntityBase(string objectId) : base()
        {
            this.ObjectId = ObjectId;
        }

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
                    if (propertyValue.Length >= stringLength.MaximumLength
                        || propertyValue.Length <= stringLength.MinimumLength)
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
            string outString = string.Empty;
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                outString = outString + s[i].ToString("X");
            }
            return outString;
        }

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetType().FullName + ",ObjectID=" + this.ObjectId;
            // return base.ToString();
        }
    }
}
