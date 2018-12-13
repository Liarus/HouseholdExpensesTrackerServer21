using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Identities.Queries;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Helper;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Requests;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {

        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            if (request == null)
            {
                return BadRequest("invalid credential request");
            }

            var user = await this.GetQueryAsync<UserDto>(
                new UserGetByCredentialsQuery(request.Email, Md5HashHelper.ComputeHash(request.Password)));
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:54707",
                audience: "http://localhost:4000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new SignInDto
            {
                UserName = user.Name,
                AccessToken = tokenString
            });
        }
    }
}