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
    /// 业务表单
    /// </summary>
    [Serializable]
    public class BizSheet : EntityBase
    {
        public BizSheet() { }

        /// <summary>
        /// 用于新建的构造函数
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="sheetCode"></param>
        /// <param name="createdBy"></param>
        public BizSheet(string schemaCode, string sheetCode, string createdBy)
        {
            this.SchemaCode = schemaCode;
            this.SheetCode = sheetCode;
            this.CreatedBy = createdBy;
        }

        /// <summary>
        /// 获取或设置所属业务模型编码
        /// </summary>
        public string SchemaCode { get; set; }

        public const string PropertyName_SheetCode = "SheetCode";

        /// <summary>
        /// 获取或设置表单编码
        /// </summary>
        public string SheetCode { get; set; }

        /// <summary>
        /// 获取或设置表单显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置是否系统表单
        /// </summary>
        public bool IsSystemSheet { get; set; }

        /// <summary>
        /// 获取或设置PC表单URL地址
        /// </summary>
        [StringLength(512)]
        public string PCSheetUrl { get; set; }

        /// <summary>
        /// 获取或设置打印的表单URL地址
        /// </summary>
        [StringLength(512)]
        public string PrintUrl { get; set; }

        /// <summary>
        /// 获取或设置移动端表单URL地址
        [StringLength(512)]
        /// </summary>
        public string MobileSheetUrl { get; set; }

        /// <summary>
        /// 获取或设置默认表单HTML设计内容
        /// </summary>
        public string DesignerContent { get; set; }

        /// <summary>
        /// 获取或设置默认表单运行内容
        /// </summary>
        public string RuntimeContent { get; set; }

        /// <summary>
        /// 获取或设置默认表单打印模板
        /// </summary>
        public string PrintContent { get; set; }

        /// <summary>
        /// 获取或设置默认表单运行的Javascript
        /// </summary>
        public string Javascript { get; set; }

        /// <summary>
        /// 获取或设置是否启用Controller程序
        /// </summary>
        public bool IsEnabledCode { get; set; }

        /// <summary>
        /// 获取或设置默认表单业务逻辑代码
        /// </summary>
        public string ControllerContent { get; set; }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置表单排序值
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 设置默认表单属性
        /// </summary>
        /// <param name="schemaCode"></param>
        /// <param name="createdBy"></param>
        public void Initial(string schemaCode, string createdBy)
        {
            this.SchemaCode = schemaCode;
            this.SheetCode = "Sheet_" + schemaCode;
            this.CreatedBy = createdBy;
        }

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
                return EntityConfig.Table.BizSheet;
            }
        }

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

        // End Class
    }
}