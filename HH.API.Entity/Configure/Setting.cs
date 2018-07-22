using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Entity.Sso
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    [Serializable]
    public class Setting : EntityBase
    {
        public Setting() { }

        /// <summary>
        /// 获取或设置是否允许传阅任务进行消息推送
        /// </summary>
        public bool AllowCirculateItemPush { get; set; } = true;

        /// <summary>
        /// 获取或设置初始化用户密码
        /// </summary>
        public string InitialPassword { get; set; } = "123456";

        #region 微信集成 ---------------
        /// <summary>
        /// 获取或设置微信的 CorpId
        /// </summary>
        public string WeChatCorpId { get; set; }

        #endregion

        #region 钉钉集成 ---------------
        public string DingTalkCorpId { get; set; }
        public string DingTalkCorpSecret { get; set; }
        public string DingTalkAppId { get; set; }
        public string DingTalkMessagePCUrl { get; set; }
        public string DingTalkMessageMobileUrl { get; set; }
        #endregion

        #region App推送 ----------------
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public string AppName { get; set; }
        #endregion

        public override string TableName => throw new NotImplementedException();
    }
}