using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HH.API.Entity.BizObject
{
    /// <summary>
    /// 所有实体类的基类
    /// </summary>
    [Serializable]
    public class BizSchema : EntityBase
    {
        public BizSchema() { }

        /// <summary>
        /// 用于新建的构造函数
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="createdBy"></param>
        public BizSchema(string schemaCode, string createdBy)
        {
            this.SchemaCode = schemaCode;
            this.CreatedBy = createdBy;
            this.InitialDefaultProperties();
        }

        /// <summary>
        /// 获取或设置业务对象编码
        /// </summary>
        public string SchemaCode { get; set; }

        /// <summary>
        /// 获取或设置业务对象显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        public string Description { get; set; }

        ///// <summary>
        ///// 用来存储的属性内容
        ///// </summary>
        //public string PropertiesContent
        //{
        //    get
        //    {
        //        if (this.Properties != null)
        //        {
        //            return JsonConvert.SerializeObject(this.Properties);
        //        }
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        if (!string.IsNullOrEmpty(this.PropertiesContent))
        //        {
        //            this.Properties = JsonConvert.DeserializeObject<List<Property>>(value);
        //        }
        //    }
        //}

        /// <summary>
        /// 获取或设置属性集合
        /// </summary>
        [NotMapped]
        public List<BizProperty> Properties { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizSchema;
            }
        }

        /// <summary>
        /// 初始化默认属性
        /// </summary>
        public void InitialDefaultProperties()
        {
            this.Properties = new List<BizProperty>();
            this.Properties.Add(new BizProperty()
            {
                PropertyName = PropertyName_ObjectId,
                IsPrimaryKey = true,
                IsRequired = true,
                CreatedBy = this.CreatedBy
            });
            this.Properties.Add(new BizProperty()
            {
                PropertyName = PropertyName_Name,
                LogicType = LogicType.String,
                CreatedBy = this.CreatedBy
            });
            this.Properties.Add(new BizProperty()
            {
                PropertyName = PropertyName_CreateBy,
                LogicType = LogicType.String,
                CreatedBy = this.CreatedBy
            });

            this.Properties.Add(new BizProperty()
            {
                PropertyName = PropertyName_CreatedTime,
                LogicType = LogicType.DateTime,
                CreatedBy = this.CreatedBy
            });
        }

        #region 默认的属性名称 -----------------------
        /// <summary>
        /// 获取或设置业务对象的显示名称字段
        /// </summary>
        public const string PropertyName_Name = "Name";
        #endregion

        /// <summary>
        /// 转换为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("SchemaCode={0},CnName={1},Properties={2}",
                this.SchemaCode,
                this.DisplayName,
                JsonConvert.SerializeObject(this.Properties));
        }
    }

    /// <summary>
    /// 业务对象属性
    /// </summary>
    [Serializable]
    public class BizProperty : EntityBase
    {
        /// <summary>
        /// 属性的显示名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 获取或设置所归属的业务对象编码
        /// </summary>
        public string ParentSchemaCode { get; set; }

        /// <summary>
        /// 获取或设置当前属性的业务逻辑类型
        /// </summary>
        public LogicType LogicType { get; set; }

        /// <summary>
        /// 获取或设置存储的最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 获取或设置是否系统保留字段
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 获取或设置是否需要建立索引
        /// </summary>
        public bool IsIndexKey { get; set; }

        /// <summary>
        /// 获取或设置是否必填项
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 获取或设置验证正则表达式
        /// </summary>
        public string RegularExpression { get; set; }

        /// <summary>
        /// 获取或设置子表存储结构
        /// </summary>
        public List<BizProperty> Properties { get; set; }

        /// <summary>
        /// 获取或设置是否已经发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 获取或设置是否是虚拟属性(虚拟属性不对应数据库表)
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 获取或设置属性的公式表达式(虚拟属性公式才生效)
        /// </summary>
        public string FormulaExpression { get; set; }

        /// <summary>
        /// 获取或设置排序顺序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 获取数据库表名
        /// </summary>
        public override string TableName
        {
            get
            {
                return EntityConfig.Table.BizProperty;
            }
        }
    }

    /// <summary>
    /// 业务逻辑类型
    /// </summary>
    [Serializable]
    public enum LogicType
    {
        /// <summary>
        /// 单一结构：字符类型
        /// </summary>
        String = 0,
        /// <summary>
        /// 单一结构：长文本类型
        /// </summary>
        Html = 1,
        /// <summary>
        /// 单一结构：日期类型
        /// </summary>
        DateTime = 2,
        /// <summary>
        /// 单一结构：整数类型
        /// </summary>
        Int = 3,
        /// <summary>
        /// 单一结构：数值类型
        /// </summary>
        Decimal = 4,
        /// <summary>
        /// 单一结构：时间段，存储结构为 Ticks(long)
        /// </summary>
        TimeSpan = 5,
        /// <summary>
        /// 单一结构：组织类型,存储结构为JSON格式,[{ID:"xxx1",Name:"yyy1"},{ID:"xxx2",Name:"yyy2"}]
        /// </summary>
        Orgainzation = 6,

        #region 复合类型
        /// <summary>
        /// 复合结构：附件类型，存储为附件表中
        /// </summary>
        Attachment = 7,
        /// <summary>
        /// 复合结构：审批意见类型，存储为审批意见表中
        /// </summary>
        Comment = 8,
        /// <summary>
        /// 复合结构：子表类型，存储为子表
        /// </summary>
        SubTable = 9
        #endregion
    }
}
