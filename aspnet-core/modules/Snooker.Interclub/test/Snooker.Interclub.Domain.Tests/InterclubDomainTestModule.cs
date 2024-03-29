﻿using Snooker.Interclub.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Snooker.Interclub;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(InterclubEntityFrameworkCoreTestModule)
    )]
public class InterclubDomainTestModule : AbpModule
{

}
