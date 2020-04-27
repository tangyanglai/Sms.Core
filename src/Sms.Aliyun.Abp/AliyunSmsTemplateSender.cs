using System;
using System.Threading.Tasks;
using Abp;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Json;
using Abp.UI;
using Castle.Core.Logging;
using Sms.Aliyun.Builder;
using Sms.Core;
using Sms.Core.Models;
using Microsoft.Extensions.Configuration;

namespace Sms.Aliyun.Abp
{
    /// <summary>
    ///     短信发送服务
    /// </summary>
    public class AliyunSmsTemplateSender : IShouldInitialize, ISingletonDependency, ISmsTemplateSender
    {
        public AliyunSmsTemplateSender(IConfiguration appConfiguration, IIocManager iocManager)
        {
            AppConfiguration = appConfiguration;
            IocManager = iocManager;
            Logger = NullLogger.Instance;
        }

        public IConfiguration AppConfiguration { get; set; }

        public IIocManager IocManager { get; set; }

        public ILogger Logger { get; set; }

        public const string Key = "AliyunSmsSettings";

        /// <summary>
        /// 根据key从站点配置文件或设置中获取支付配置
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        private Task<TConfig> GetConfigFromConfigOrSettingsByKey<TConfig>() where TConfig : class, new()
        {
            var settings = AppConfiguration?.GetSection(key: Key)?.Get<TConfig>();
            if (settings != null) return Task.FromResult(settings);

            using (var obj = IocManager.ResolveAsDisposable<ISettingManager>())
            {
                var value = obj.Object.GetSettingValue(Key);
                if (string.IsNullOrWhiteSpace(value))
                {
                    return Task.FromResult<TConfig>(null);
                }
                settings = value.FromJsonString<TConfig>();
                return Task.FromResult(settings);
            }
        }

        public void Initialize()
        {
            //日志函数
            void LogAction(string tag, string message)
            {
                if (tag.Equals("error", StringComparison.CurrentCultureIgnoreCase))
                    Logger.Error(message);
                else
                    Logger.Debug(message);
            }

            try
            {
                //阿里云短信设置
                AliyunSmsBuilder.Create()
                    //设置日志记录
                    .WithLoggerAction(LogAction)
                    .SetSettingsFunc(() => GetConfigFromConfigOrSettingsByKey<AliyunSmsSettting>().Result)
                    .Build();

                SmsService = new AliyunSmsService();
            }
            catch (Exception ex)
            {
                Logger.Error("阿里云短信未配置或者配置错误！", ex);
            }
        }


        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        public async Task<SmsResult> SendCodeAsync(string phone, string code)
        {
            var sms = new AliyunSmsService();
            return await SmsService.SendCodeAsync(phone, code);
        }

        /// <summary>
        ///     发送模板消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<SmsResult> SendTemplateMessageAsync(SendTemplateMessageInput input)
        {
            return await SmsService.SendTemplateMessageAsync(input);
        }

        /// <summary>
        /// 批量发送消息模板
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<SmsResult> SendBatchTemplateMessageAsync(SendBatchTemplateMessageInput input)
        {
            return await SmsService.SendBatchTemplateMessageAsync(input);
        }

        /// <summary>
        /// 查看短信发送记录和发送状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> QuerySendDetailsAsync(QuerySendDetailsInput input)
        {
            return await SmsService.QuerySendDetailsAsync(input);
        }




        /// <summary>
        /// 查看短信签名状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<QuerySmsSignResult> QuerySmsSignAsync(QuerySmsSignInput input)
        {
            return await SmsService.QuerySmsSignAsync(input);
        }


        /// <summary>
        /// 查看短信模板状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<QuerySmsTemplateResult> QuerySmsTemplateAsync(QuerySmsTemplateInput input)
        {
            return await SmsService.QuerySmsTemplateAsync(input);
        }

        /// <summary>
        /// 新增短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> AddSmsSignAsync(AddSmsSignInput input)
        {
            return await SmsService.AddSmsSignAsync(input);
        }

        /// <summary>
        /// 新增短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> AddSmsTemplateAsync(AddSmsTemplateInput input)
        {
            return await SmsService.AddSmsTemplateAsync(input);
        }

        /// <summary>
        /// 删除短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> DeleteSmsSignAsync(DeleteSmsSignInput input)
        {
            return await SmsService.DeleteSmsSignAsync(input);
        }

        /// <summary>
        /// 删除短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> DeleteSmsTemplateAsync(DeleteSmsTemplateInput input)
        {
            return await SmsService.DeleteSmsTemplateAsync(input);
        }


        /// <summary>
        /// 修改短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> ModifySmsSignAsync(ModifySmsSignInput input)
        {
            return await SmsService.ModifySmsSignAsync(input);
        }

        /// <summary>
        /// 修改短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SmsResult> ModifySmsTemplateAsync(ModifySmsTemplateInput input)
        {
            return await SmsService.ModifySmsTemplateAsync(input);
        }


        public ISmsService SmsService { get; set; }
    }
}