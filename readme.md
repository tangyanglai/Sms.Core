# Sms.Core

## 简介

```
原作者GitHub:https://github.com/xin-lai/Magicodes.Sms
```

基于阿里云短信的封装库，提供Abp模块的封装。



## Nuget

| 名称           | 说明              | Nuget                                                        |
| -------------- | ----------------- | ------------------------------------------------------------ |
| Sms.Aliyun     | 阿里云短信库      | [![Nuget](https://buildstats.info/nuget/Sms.Aliyun)](https://www.nuget.org/packages/Sms.Aliyun/) |
| Sms.Core       | 短信核心库        | [![Nuget](https://buildstats.info/nuget/Sms.Core)](https://www.nuget.org/packages/Sms.Core/) |
| Sms.Aliyun.Abp | 阿里云短信Abp模块 | [![Nuget](https://buildstats.info/nuget/Sms.Aliyun.Abp)](https://www.nuget.org/packages/Sms.Aliyun.Abp/) |



## 开始使用

如果使用Abp相关模块，则使用起来比较简单，具体您可以参考相关单元测试的编写。主要有以下步骤：

1. 引用对应的Nuget包
   如：

   | 名称           | 说明       | Nuget                                                        |
   | -------------- | ---------- | ------------------------------------------------------------ |
   | Sms.Aliyun.Abp | 短信核心库 | [![Nuget](https://buildstats.info/nuget/Sms.Aliyun.Abp)](https://www.nuget.org/packages/Sms.Aliyun.Abp/) |

2. 添加模块依赖
   在对应工程的Abp的模块（AbpModule）中，添加对“AliyunSmsModule”的依赖，如：

````C#
    [DependsOn(typeof(AliyunSmsModule))]
````

3. 配置

默认支持两种配置方式，配置文件和SettingManager。下面以配置文件为例，格式为：

````json
{
  "AliyunSmsSettings": {
    "AccessKeyId": "",
    "AccessKeySecret": "",
    "SignName": "",//SendCodeAsync发送验证码使用
    "TemplateCode": "",//SendCodeAsync发送验证码使用
    "TemplateParam": ""//SendCodeAsync发送验证码使用
  } 
}
````

4. 使用短信API

通过容器获得ISmsTemplateSender，然后调用发送方法即可。如单元测试中：

````C#
        private readonly ISmsTemplateSender _smsTemplateSender;

        public SmsTest()
        {
            this._smsTemplateSender = Resolve<ISmsTemplateSender>();
        }

        public async Task SendCodeAsync(string phone, string code)
        {
            await _smsTemplateSender.SmsService.SendCodeAsync(phone, code);
        }
````

## 非ABP集成

### 配置

            AliyunSmsBuilder.Create()
                //设置日志记录
                .WithLoggerAction((tag, message) =>
                {
                    Console.WriteLine(string.Format("Tag:{0}\tMessage:{1}", tag, message));
                }).SetSettingsFunc(() =>
                {
                    //TODO:请自行配置自己的配置
                    //如果是一个项目多个配置,请使用key来获取相关配置
                    return ConfigHelper.LoadConfig("aliyun_app");
                }).Build();


### 阿里云短信发送

        [Theory(DisplayName = "短信发送测试")]
        [InlineData("你的手机号码", "验证码")]
        public async Task SendCodeAsync_Test(string phone, string code)
        {
            var smsService = new AliyunSmsService();
            var result = await smsService.SendCodeAsync(phone, code);
            result.Success.ShouldBeTrue();
        }

