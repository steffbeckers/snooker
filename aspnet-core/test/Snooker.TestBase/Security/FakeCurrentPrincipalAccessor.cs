using System.Collections.Generic;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace Snooker.Security
{
    [Dependency(ReplaceServices = true)]
    public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
    {
        private ClaimsPrincipal _principal;

        protected override ClaimsPrincipal GetClaimsPrincipal()
        {
            return GetPrincipal();
        }

        private ClaimsPrincipal GetPrincipal()
        {
            if (_principal == null)
            {
                lock (this)
                {
                    if (_principal == null)
                    {
                        _principal = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                new List<Claim>
                                {
                                    new Claim(AbpClaimTypes.UserId,"4a05a121-7e89-4998-bb46-9d88cc49973f"),
                                    new Claim(AbpClaimTypes.UserName,"john@doe.com"),
                                    new Claim(AbpClaimTypes.Email,"john@doe.com")
                                }
                            )
                        );
                    }
                }
            }

            return _principal;
        }
    }
}