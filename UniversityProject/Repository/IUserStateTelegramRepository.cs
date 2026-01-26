using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCore;

namespace Repository
{
    public interface IUserStateTelegramRepository
    {
        public UserStateRegistration? Get(long Id);
        public void Delete(long Id);
        public long Update(UserStateRegistration registration);
        public long Create(UserStateRegistration registration);
    }
}
