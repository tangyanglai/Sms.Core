using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aliyun.Acs.Dysmsapi.Model.V20170525.QuerySendDetailsResponse;

namespace Sms.Core.Models
{
    /// <summary>
    /// 发送结果
    /// </summary>
    public class SmsResult
    {
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 发送结果
    /// </summary>
    public class QuerySendDetailsResult : SmsResult
    {
        /// <summary>
        /// 查询结果
        /// </summary>
        public List<QuerySendDetails_SmsSendDetailDTO> SmsSendDetailDTOs { get; set; }
    }

    /// <summary>
    /// 发送结果
    /// </summary>
    public class QuerySmsSignResult : SmsResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int? SignStatus { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }

    /// <summary>
    /// 发送结果
    /// </summary>
    public class QuerySmsTemplateResult : SmsResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int? TemplateStatus { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
