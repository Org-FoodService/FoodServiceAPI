using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodServiceAPI.Core.Wrapper.Interface
{
    public interface IUserManagerWrapper<TUser> where TUser : class
    {
        Task<int> CountUsersAsync();
    }
}
