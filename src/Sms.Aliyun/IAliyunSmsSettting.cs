﻿namespace Sms.Aliyun
{
    public interface IAliyunSmsSettting
    {
        string AccessKeyId { get; set; }
        string AccessKeySecret { get; set; }
        string TemplateCode { get; set; }
        string TemplateParam { get; set; }
        string SignName { get; set; }

        //string OutId { get; set; }

        string Domain { get; set; }
        string EndpointName { get; set; }
        string Product { get; set; }
        string RegionId { get; set; }
        //string ServerTemplateCode { get; set; }
        //string ServerTemplateParam { get; set; }
    }
}