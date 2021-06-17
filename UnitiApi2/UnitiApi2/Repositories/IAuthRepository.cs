using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitiApi2.Models;

namespace UnitiApi2.Repositories
{
    public interface IAuthRepository
    {

        Task<IEnumerable<Auth>> Get();
        Task<Auth> AuthUser(string username, string password);
        Task<Auth> Create(Auth auth);

        Task<Auth> Get(string username);


    }
}
