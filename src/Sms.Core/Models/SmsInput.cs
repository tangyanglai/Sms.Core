using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Core.Models
{

    /// <summary>
    ///     模板消息内容
    /// </summary>
    public class SendTemplateMessageInput
    {
        /// <summary>
        /// 是否有模板参数
        /// </summary>
        public virtual bool HaveParm { get; set; }

        /// <summary>
        /// 模板Id
        /// </summary>
        public virtual string TemplateCode { get; set; }
        public SendTemplateMessageInput()
        {
        }
        /// <summary>
        ///     接收服务目标，多个手机号码请用逗号分隔
        ///     支持单个或多个手机号码，传入号码为11位手机号码，不能加0或+86。群发短信需传入多个号码，以英文逗号分隔，一次调用最多传入200个号码。示例：18600000000,13911111111,13322222222
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        ///// <summary>
        /////     模板参数
        ///// </summary>
        public virtual SendData SendParmData { get; set; }
        //public virtual Dictionary<string, string> Data { get; set; }




        ///// <summary>
        ///// 是否有模板参数
        ///// </summary>
        //public virtual bool HaveParm { get; set; }

        ///// <summary>
        ///// 扩展参数
        ///// 会作为公共回传参数，在“消息返回”中会透传回该参数；举例：用户可以传入自己下级的会员ID，在消息返回时，该会员ID会包含在内，用户可以根据该会员ID识别是哪位会员使用了你的应用
        ///// </summary>
        //public virtual string ExtendParam { get; set; }

        ///// <summary>
        ///// 外部流水扩展字段。
        ///// </summary>
        //public virtual string OutId { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        public virtual string SignName { get; set; }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    //sb.AppendFormat("Destination:{0};", Destination);
        //    //sb.AppendFormat("SignName:{0};", SignName);
        //    //sb.AppendFormat("ExtendParam:{0};", ExtendParam);
        //    //sb.AppendFormat("TemplateCode:{0};", TemplateCode);
        //    //sb.AppendLine();
        //    //sb.AppendLine("Data:");
        //    sb.Append("{");
        //    foreach (var item in Data)
        //    {
        //        sb.AppendFormat("\"{0}\":\"{1}\",", item.Key, item.Value);
        //    }
        //    return sb.ToString().TrimEnd(',') + "};";
        //}
    }

    public class SendData
    { 
    }

    /// <summary>
    ///     模板消息内容
    /// </summary>
    public class SendBatchTemplateMessageInput
    {
        /// <summary>
        /// 是否有模板参数
        /// </summary>
        public virtual bool HaveParm { get; set; }

        /// <summary>
        /// 模板Id
        /// </summary>
        public virtual string TemplateCode { get; set; }

        public List<SendTemplateMessageInput> SendTemplateMessageInputs { get; set; }

    }

    public class QuerySendDetailsInput
    {
        /// <summary>
        ///     接收短信的手机号码。
        ///格式：国内短信：11位手机号码，例如15900000000。国际/港澳台消息：国际区号+号码，例如85200000000。
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 短信发送日期，支持查询最近30天的记录。        格式为yyyyMMdd，例如20181225。
        /// </summary>
        public virtual DateTime SendDate { get; set; }

        /// <summary>
        /// 分页查看发送记录，指定每页显示的短信记录数量。       取值范围为1 ~50。
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// 分页查看发送记录，指定发送记录的的当前页码。
        /// </summary>
        public virtual int CurrentPage { get; set; }
    }


    public class QuerySmsSignInput
    {
        /// <summary>
        /// SignName
        /// </summary>
        public string SignName { get; set; }
    }
    public class QuerySmsTemplateInput
    {
        /// <summary>
        /// TemplateCode
        /// </summary>
        public string TemplateCode { get; set; }
    }


    public class AddSmsSignInput
    {
        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 短信签名申请说明。请在申请说明中详细描述您的业务使用场景，申请工信部备案网站的全称或简称请在此处填写域名，长度不超过200个字符。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 资料
        /// </summary>
        public List<Aliyun.Acs.Dysmsapi.Model.V20170525.AddSmsSignRequest.SignFileList> SignFileLists { get; set; }

        /// <summary>
        /// 签名来源。其中：
        ///0：企事业单位的全称或简称。
        ///1：工信部备案网站的全称或简称。
        ///2：APP应用的全称或简称。
        ///3：公众号或小程序的全称或简称。
        ///4：电商平台店铺名的全称或简称。
        ///5：商标名的全称或简称
        ///签名来源为1时，请在申请说明中添加网站域名，加快审核速度。
        /// </summary>
        public int? SignSource { get; set; }
    }


    public class AddSmsTemplateInput
    {
        /// <summary>
        /// 模板类型0：验证码 1：其他
        /// </summary>
        public int? TemplateType { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 短信模板申请说明。
        /// </summary>
        public string Remark { get; set; }

    }

    public class ModifySmsSignInput
    {
        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 短信签名申请说明。请在申请说明中详细描述您的业务使用场景，申请工信部备案网站的全称或简称请在此处填写域名，长度不超过200个字符。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 资料
        /// </summary>
        public List<Aliyun.Acs.Dysmsapi.Model.V20170525.ModifySmsSignRequest.SignFileList> SignFileLists { get; set; }

        /// <summary>
        /// 签名来源。其中：
        ///0：企事业单位的全称或简称。
        ///1：工信部备案网站的全称或简称。
        ///2：APP应用的全称或简称。
        ///3：公众号或小程序的全称或简称。
        ///4：电商平台店铺名的全称或简称。
        ///5：商标名的全称或简称
        ///签名来源为1时，请在申请说明中添加网站域名，加快审核速度。
        /// </summary>
        public int? SignSource { get; set; }
    }


    public class ModifySmsTemplateInput
    {
        /// <summary>
        /// 模板类型0：验证码 1：其他
        /// </summary>
        public int? TemplateType { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 短信模板申请说明。
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// TemplateCode
        /// </summary>
        public string TemplateCode { get; set; }
    }

    public class DeleteSmsSignInput
    {
        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignName { get; set; }
    }

    public class DeleteSmsTemplateInput
    {
        /// <summary>
        /// TemplateCode
        /// </summary>
        public string TemplateCode { get; set; }
    }

}
