using System;
using DAL.Entities.Account;
using DAL.Repositories;

namespace WebApi.Auth
{
    public class SignInRepository : IDisposable
    {
        public Repository<User> Repository { get; set; }
        public SignInRepository()
        {
            this.Repository = new Repository<User>();
        }
        public void Dispose()
        {
            this.Repository?.Dispose();
        }
    }
}
