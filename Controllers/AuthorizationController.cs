using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core_Server.Models.Authentication;
using Core_Server.Helpers;
using Core_Server.Models.Client.Authentication;
using Microsoft.AspNetCore.Http.Authentication;

namespace Core_Server.Controllers
{
    public class AuthenticationController : Controller 
    {
        private readonly UserContext db;

        public AuthenticationController(UserContext context) {
            db = context;
        }

        [HttpPost("/auth/register")]
        public IActionResult Register([FromBody]LoginInfo user){
            var a = db.Users.FirstOrDefault(x => x.Login == user.Login);
            if(a != null) {
                return BadRequest("Such user already exists");
            } else {
                User newUser = new User { Login = user.Login, Password = user.Password, Role = "User" };
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok();
            }
        }


        [HttpPost("/auth/token")]
        public IActionResult Token([FromBody]LoginInfo user)
        { 
            var username = user.Login;
            var password = user.Password;
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return NotFound();
            }
 
            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthenticationHelper.ISSUER,
                    audience: AuthenticationHelper.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthenticationHelper.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthenticationHelper.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new AuthResponce
            {
                AccessToken = encodedJwt,
                Username = identity.Name
            };
            
            return Ok(response);
        }

        [HttpGet("/auth/googlelogin")]
        public IActionResult GoogleLog()
        {
            var authProperties = new AuthenticationProperties
            {
                RedirectUri = "/api/ExternalRedirect"
            };

            return new ChallengeResult("Google", authProperties);
        }

        [HttpGet("/auth/external")]
        public IActionResult ExternalRedirect() {
            var identity = (ClaimsIdentity)User.Identity;
            Claim emailClaim = identity.Claims.Where(x => x.Type == ClaimTypes.Email).SingleOrDefault();
            
            if(db.Users.FirstOrDefault(x => x.Email == emailClaim.Value) != null ) {
                return Redirect("/Catalog");
            } else {
                return Redirect("/Settings");
            }
        }
 
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            return null;
        }
    }
}