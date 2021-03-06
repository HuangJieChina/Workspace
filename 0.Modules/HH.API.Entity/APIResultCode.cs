﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API
{
    /// <summary>
    /// API接口返回统一编码
    /// </summary>
    public enum APIResultCode
    {
        /// <summary>
        /// 返回成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 返回未知错误
        /// </summary>
        Error = -1,

        #region 公共服务以10开头 ---------------
        /// <summary>
        /// 权限不足
        /// </summary>
        PermissionDenied = 100000,
        /// <summary>
        /// 实体数据格式校验错误
        /// </summary>
        DataFromatError = 100001,
        /// <summary>
        /// 名称重复
        /// </summary>
        NameDuplicate = 100002,
        /// <summary>
        /// 编码重复
        /// </summary>
        CodeDuplicate = 100003,
        #endregion

        #region 组织机构服务以20开头 -----------
        /// <summary>
        /// 父组织不存在
        /// </summary>
        ParentNotExists = 200002,
        /// <summary>
        /// 相同名称或编码组织已经存在
        /// </summary>
        UnitIsExists = 200003,
        /// <summary>
        /// 名称不允许为空
        /// </summary>
        NameIsEmpty = 200004,
        /// <summary>
        /// 堆栈溢出(死循环)
        /// </summary>
        StackOverflow = 200005,
        /// <summary>
        /// 组织Id不合法
        /// </summary>
        UnitIdNotExists = 200006,
        /// <summary>
        /// 角色已应用岗位，请先删除岗位再删除角色
        /// </summary>
        RoleCannotRemove = 200007,
        /// <summary>
        /// 部门不允许被禁用，因为存在启用的子组织
        /// </summary>
        UnitCannotDisabled = 200008,
        /// <summary>
        /// 不能被启用，因为上级组织是禁用的
        /// </summary>
        UnitCannotEnabled = 200009,
        /// <summary>
        /// 密码无效
        /// </summary>
        PasswordError = 200010,
        /// <summary>
        /// 存在子对象，不允许被该操作
        /// </summary>
        ExistsChildren = 200011,
        #endregion

        #region 业务模型服务以30开头 -----------
        /// <summary>
        /// 业务模型不存在
        /// </summary>
        SchemaNotExists = 300001,
        /// <summary>
        /// 应用包不存在
        /// </summary>
        AppPackageNotExists = 300002,
        #endregion

        /// <summary>
        /// 不合法的请求
        /// </summary>
        BadRequest = 900001,
        /// <summary>
        /// 不合法的参数
        /// </summary>
        BadParameter = 900002

    }
}
