
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Sms.Aliyun.Abp
{

    public class AliyunSmsModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AliyunSmsModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}