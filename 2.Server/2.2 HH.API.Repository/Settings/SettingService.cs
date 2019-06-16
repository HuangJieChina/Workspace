using System;
using System.Collections.Generic;
using System.Text;

namespace HH.API.Services.Settings
{
    public class SettingService : ISettingService
    {
        public bool GetDingtalkAppKey(string appKey)
        {
            throw new NotImplementedException();
        }

        public bool GetDingtalkAppSecret(string secret)
        {
            throw new NotImplementedException();
        }

        public bool GetDingtalkCorpId(string corpId)
        {
            throw new NotImplementedException();
        }

        public T GetKeyValue<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetDingtalkAppKey(string appKey)
        {
            throw new NotImplementedException();
        }

        public bool SetDingtalkAppSecret(string secret)
        {
            throw new NotImplementedException();
        }

        public bool SetDingtalkCorpId(string corpId)
        {
            throw new NotImplementedException();
        }

        public bool SetKeyValue<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISettingService
    {
        bool SetKeyValue<T>(string key, T value);

        T GetKeyValue<T>(string key);

        bool SetDingtalkCorpId(string corpId);
        bool GetDingtalkCorpId(string corpId);

        bool SetDingtalkAppKey(string appKey);
        bool GetDingtalkAppKey(string appKey);

        bool SetDingtalkAppSecret(string secret);
        bool GetDingtalkAppSecret(string secret);
    }
}
