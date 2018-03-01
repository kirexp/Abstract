using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DAL.Entities.Account;
using DAL.Repositories;
using Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Auth;
using WebApi.Auth.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AccountController : Controller{
        private SignInManager<User> SignInManager { get; }
        private ApplicationUserManager<User> UserManager { get; }
        private IConfiguration Configuration { get; }
        private readonly Repository<User> _signInRepository;
        public AccountController(ApplicationUserManager<User>userManager, ApplicationSignInManger<User> signInManager, IConfiguration configuration, SignInRepository rep)
        {
            this.SignInManager = signInManager;
            this.Configuration = configuration;
            this.UserManager = userManager;
            this._signInRepository = rep.Repository;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]UserViewModel model){
            using (var repository = new Repository<User>(this._signInRepository)) {
                var user = repository.Get(x => x.UserName == model.UserName).SingleOrDefault();
                if (user == null) {
                    return Json(SimpleResponse.Error(HttpStatusCode.Forbidden,"Данный пользователь не зарегистрирован"));
                }
                var result = await this.SignInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (!result.Succeeded) {
                    return Json(SimpleResponse.Error(HttpStatusCode.Forbidden, "Не верный пароль"));
                }
                else {
                    var requestAt = DateTime.Now;
                    var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                    var token = await this.GenerateToken(user, expiresIn);
                    return this.Json(new RequestResult {
                        State = RequestState.Success,
                        Data = new {
                            createdAt = requestAt,
                            expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                            tokenType = TokenAuthOption.TokenType,
                            accessToken = token
                        }
                    });
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model){
            using (var repository = new Repository<User>()) {
                var user = repository.Get(x => x.UserName == model.UserName).SingleOrDefault();
                if (user == null) {
                    user = new User {
                        UserName = model.UserName,
                        LastPasswordChangedDate = DateTime.Now,
                        UserType = UserTypeEnum.Client,
                        Email = model.Email,
                        Password = model.Password
                    };
                    var result = await this.UserManager.CreateAsync(user);
                    if (result.Succeeded) {
                        var requestAt = DateTime.Now;
                        var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                        var token = await this.GenerateToken(user, expiresIn);
                        return this.Json(new RequestResult {
                            State = RequestState.Success,
                            Data = new {
                                createdAt = requestAt,
                                expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                                tokenType = TokenAuthOption.TokenType,
                                accessToken = token
                            }
                        });
                    } else {
                        return Json(SimpleResponse.Error(HttpStatusCode.BadRequest, "Не удалось зарегистрироваться"));
                    }
                } else {
                    return Json(SimpleResponse.Error(HttpStatusCode.BadRequest, "Не удалось зарегистрироваться"));
                }
            }
        }
        [NonAction]
        private async Task<string> GenerateToken(User user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
            var сlaims = new List<Claim>(new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Type", user.UserType.ToString())
            });
            var roles = await this.UserManager.GetRolesAsync(user);
            foreach (var role in roles) {
                сlaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            //var roles = Enumerable.Range(1, 4).Select(x => new Claim($"role", $"qwe{x}")).ToArray();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                сlaims
            );
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires,
            });
            return handler.WriteToken(securityToken);
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "KindergartenListEdit")]
        //public IActionResult ForAdmin()
        //{
        //    return this.Json("Yes is Admin");
        //}
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult ForAuth()
        //{
        //    return this.Json("Yes is auth");
        //}
        //public IActionResult ForAll()
        //{
        //    return this.Json("Yes is All");
        //}
    }
}
