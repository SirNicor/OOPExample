using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCore;

namespace Repository
{
    internal interface IUserStateRepository
    {
        public UserStateRegistration Get(long Id);
        public long Update(UserStateRegistration registration);
        public long Create(UserStateRegistration registration);
    }
}
