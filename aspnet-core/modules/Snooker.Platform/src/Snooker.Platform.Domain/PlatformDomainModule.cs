﻿using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Snooker.Platform;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(PlatformDomainSharedModule)
)]
public class PlatformDomainModule : AbpModule
{

}
