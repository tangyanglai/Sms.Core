using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Sms.Aliyun.Helper;
using Sms.Core;
using Sms.Core.Models;
using System.Linq;
using Newtonsoft.Json;

namespace Sms.Aliyun
{
    public class AliyunSmsService : ISmsService
    {
        /// <summary>
        ///     发送短信验证码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        public Task<SmsResult> SendCodeAsync(string phone, string code)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new SmsException("手机号码不能为空!");
            }
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new SmsException("验证码不能为空!");
            }
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new SendSmsRequest();
            var result = new SmsResult();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = phone;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = client.AliyunSmsSettting.SignName;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = client.AliyunSmsSettting.TemplateCode;
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = string.Format(client.AliyunSmsSettting.TemplateParam, code);
                ////可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                //request.OutId = client.AliyunSmsSettting.OutId;
                //请求失败这里会抛ClientException异常
                var sendSmsResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(sendSmsResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = sendSmsResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="input">模板消息</param>
        /// <returns></returns>
        public Task<SmsResult> SendTemplateMessageAsync(SendTemplateMessageInput input)
        {
            if (string.IsNullOrWhiteSpace(input.PhoneNumber))
            {
                throw new SmsException("手机号码不能为空!");
            }
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new SendSmsRequest();
            var result = new SmsResult();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = input.PhoneNumber;
                //必填:短信签名-可在短信控制台中找到                                                                                                                                                              
                request.SignName = input.SignName;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = input.TemplateCode;
                //可选:模板中的变量替换JSON串
                if (input.HaveParm)
                    request.TemplateParam = JsonConvert.SerializeObject(input.SendParmData);
                ////可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者 
                //request.OutId = client.AliyunSmsSettting.OutId;
                //请求失败这里会抛ClientException异常
                var sendSmsResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(sendSmsResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = sendSmsResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 批量发送消息模板
        /// </summary>
        /// <param name="input">模板消息</param>
        /// <returns></returns>
        public Task<SmsResult> SendBatchTemplateMessageAsync(SendBatchTemplateMessageInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new SendBatchSmsRequest();
            var result = new SmsResult();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumberJson = JsonConvert.SerializeObject(input.SendTemplateMessageInputs.Select(a => a.PhoneNumber).ToList());
                //必填:短信签名-可在短信控制台中找到                                                                                                                                                              
                request.SignNameJson = JsonConvert.SerializeObject(input.SendTemplateMessageInputs.Select(a => a.PhoneNumber).ToList());
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = input.TemplateCode;
                //可选:模板中的变量替换JSON串
                if (input.HaveParm)
                    request.TemplateParamJson = JsonConvert.SerializeObject(input.SendTemplateMessageInputs.Select(a => a.SendParmData).ToList());

                //请求失败这里会抛ClientException异常
                var sendSmsResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(sendSmsResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = sendSmsResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 查看短信发送记录和发送状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<QuerySendDetailsResult> QuerySendDetailsAsync(QuerySendDetailsInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new QuerySendDetailsRequest();
            var result = new QuerySendDetailsResult();
            try
            {
                request.PhoneNumber = input.PhoneNumber;
                request.SendDate = input.SendDate.ToString("yyyyMMDD");
                request.PageSize = input.PageSize;
                request.CurrentPage = input.CurrentPage;

                //请求失败这里会抛ClientException异常
                var querySendDetailsResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(querySendDetailsResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                    result.SmsSendDetailDTOs = querySendDetailsResponse.SmsSendDetailDTOs;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = querySendDetailsResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 查看短信签名状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<QuerySmsSignResult> QuerySmsSignAsync(QuerySmsSignInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new QuerySmsSignRequest();
            var result = new QuerySmsSignResult();
            try
            {
                request.SignName = input.SignName;

                //请求失败这里会抛ClientException异常
                var querySmsSignResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(querySmsSignResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                    result.SignStatus = querySmsSignResponse.SignStatus;
                    result.Reason = querySmsSignResponse.Reason;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = querySmsSignResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }



        /// <summary>
        /// 查看短信模板状态。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<QuerySmsTemplateResult> QuerySmsTemplateAsync(QuerySmsTemplateInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new QuerySmsTemplateRequest();
            var result = new QuerySmsTemplateResult();
            try
            {
                request.TemplateCode = input.TemplateCode;

                //请求失败这里会抛ClientException异常
                var QuerySmsTemplateResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(QuerySmsTemplateResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                    result.TemplateStatus = QuerySmsTemplateResponse.TemplateStatus;
                    result.Reason = QuerySmsTemplateResponse.Reason;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = QuerySmsTemplateResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 新增短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> AddSmsSignAsync(AddSmsSignInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new AddSmsSignRequest();
            var result = new SmsResult();
            try
            {
                request.SignName = input.SignName;
                request.SignSource = input.SignSource;
                request.Remark = input.Remark;
                request.SignFileLists = input.SignFileLists;

                //请求失败这里会抛ClientException异常
                var AddSmsSignResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(AddSmsSignResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = AddSmsSignResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 新增短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> AddSmsTemplateAsync(AddSmsTemplateInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new AddSmsTemplateRequest();
            var result = new SmsResult();
            try
            {
                request.TemplateType = input.TemplateType;
                request.TemplateName = input.TemplateName;
                request.TemplateContent = input.TemplateContent;
                request.Remark = input.Remark;

                //请求失败这里会抛ClientException异常
                var AddSmsTemplateResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(AddSmsTemplateResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = AddSmsTemplateResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }


        /// <summary>
        /// 删除短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> DeleteSmsSignAsync(DeleteSmsSignInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new DeleteSmsSignRequest();
            var result = new SmsResult();
            try
            {
                request.SignName = input.SignName;

                //请求失败这里会抛ClientException异常
                var DeleteSmsSignResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(DeleteSmsSignResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = DeleteSmsSignResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 删除短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> DeleteSmsTemplateAsync(DeleteSmsTemplateInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new DeleteSmsTemplateRequest();
            var result = new SmsResult();
            try
            {
                request.TemplateCode = input.TemplateCode;
                //请求失败这里会抛ClientException异常
                var DeleteSmsTemplateResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(DeleteSmsTemplateResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = DeleteSmsTemplateResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }


        /// <summary>
        /// 修改短信签名。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> ModifySmsSignAsync(ModifySmsSignInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new ModifySmsSignRequest();
            var result = new SmsResult();
            try
            {
                request.SignName = input.SignName;
                request.SignSource = input.SignSource;
                request.Remark = input.Remark;
                request.SignFileLists = input.SignFileLists;

                //请求失败这里会抛ClientException异常
                var ModifySmsSignResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(ModifySmsSignResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = ModifySmsSignResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }

        /// <summary>
        /// 修改短信模板。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<SmsResult> ModifySmsTemplateAsync(ModifySmsTemplateInput input)
        {
            var client = new AliyunSmsClient();
            var acsClient = client.AcsClient;
            var request = new ModifySmsTemplateRequest();
            var result = new SmsResult();
            try
            {
                request.TemplateCode = input.TemplateCode;
                request.TemplateType = input.TemplateType;
                request.TemplateName = input.TemplateName;
                request.TemplateContent = input.TemplateContent;
                request.Remark = input.Remark;

                //请求失败这里会抛ClientException异常
                var ModifySmsTemplateResponse = acsClient.GetAcsResponse(request);
                //发送成功判断
                if ("OK".Equals(ModifySmsTemplateResponse.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = ModifySmsTemplateResponse.Message;
                }
            }
            catch (ClientException e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.ErrorMessage;
            }
            catch (Exception e)
            {
                AliyunSmsHelper.LoggerAction("Error", e.ToString());
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return Task.FromResult(result);
        }



    }
}
