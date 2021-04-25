using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Snooker.ClubManagement.Clubs
{
    public class ClubAlreadyExistsException : BusinessException
    {
        public ClubAlreadyExistsException(string name)
            : base(ClubManagementErrorCodes.ClubAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
