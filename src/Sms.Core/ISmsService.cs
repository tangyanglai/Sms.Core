using System.Threading.Tasks;
using Sms.Core.Models;

namespace Sms.Core
{
    /// <summary>
    ///     短信服务
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        Task<SmsResult> SendCodeAsync(string phone, string code);

        /// <summary>
        ///     发送模板消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SmsResult> SendTemplateMessageAsync(SendTemplateMessageInput input);

        /// <summary>
        ///     发送模板消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SmsResult> SendBatchTemplateMessageAsync(SendBatchTemplateMessageInput input);

        /// <summary>
        /// 查看短信发送记录和发送状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QuerySendDetailsResult> QuerySendDetailsAsync(QuerySendDetailsInput input);

        /// <summary>
        /// 查看短信签名状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QuerySmsSignResult> QuerySmsSignAsync(QuerySmsSignInput input);


        /// <summary>
        /// 查看短信模板状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QuerySmsTemplateResult> QuerySmsTemplateAsync(QuerySmsTemplateInput input);

        /// <summary>
        /// 新增短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> AddSmsSignAsync(AddSmsSignInput input);

        /// <summary>
        /// 新增短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> AddSmsTemplateAsync(AddSmsTemplateInput input);

        /// <summary>
        /// 删除短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> DeleteSmsSignAsync(DeleteSmsSignInput input);

        /// <summary>
        /// 删除短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> DeleteSmsTemplateAsync(DeleteSmsTemplateInput input);


        /// <summary>
        /// 修改短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> ModifySmsSignAsync(ModifySmsSignInput input);

        /// <summary>
        /// 修改短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SmsResult> ModifySmsTemplateAsync(ModifySmsTemplateInput input);

    }
}