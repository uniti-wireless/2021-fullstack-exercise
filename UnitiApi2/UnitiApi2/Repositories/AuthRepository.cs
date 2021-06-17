using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitiApi2.Models;
using UnitiApi2.Repositories;
using UnitiApi2.Controllers;
using Microsoft.EntityFrameworkCore;

namespace UnitiApi2.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly AuthContext _context;

        public AuthRepository(AuthContext context)
        {
            _context = context;
        }

        public async Task<Auth> AuthUser(string username, string password)
        {
            /*Auth b = */



            return await _context.Auths.FindAsync();
        }

        public async Task<Auth> Create(Auth auth)
        {
            _context.Auths.Add(auth);
            await _context.SaveChangesAsync();
            return auth;
        }

        public async Task<IEnumerable<Auth>> Get()
        {
            return await _context.Auths.ToListAsync();
        }

        public async Task<Auth> Get(string username)
        {
            return await _context.Auths.FindAsync();
        }

    }
}
