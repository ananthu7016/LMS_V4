using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.Repository;
using LoanManagementSystem_V3_API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LoanManagementSystem_V3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        // this is a controller for Login here we need to get the instance of Login Repository throughg dependency injection 

        private readonly ILoginRepository _repository;
        private IConfiguration _configuration;
        // this private readonly variable will be instanciated through dependency injection 

        public LoginController(ILoginRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }




        #region Validate User using credentials provided 

        [HttpPost("ValidateUser")]
        public async Task<ActionResult<User>> ValidateUser(string username, string password)
        {

            if (_repository != null)
            {

                //defining an instance to store the details of Customer
                User user = new User();

                user = await _repository.ValidateUser(username, password);

                // then we need to get the token for the user 
                string Token = GenerateJWTToken();

                if(user.UserId != 0)
                {
                    return Ok(new
                    {
                        Id = user.UserId,
                        UserName = user.FullName,
                        RoleId = user.RoleId,
                        Token = Token
                    });
                }
                else
                    return Unauthorized();
    
            }
            else
                return NotFound();

        }

        #endregion



        #region Validate Customer using Credentials Provided 
        [HttpPost("ValidateCustomer")]
       public async Task<ActionResult<Customer>> ValidateCustomer(string username, string password)
        {
            if(_repository != null)
            {

                //defining an instance to store the details of Customer
                Customer customer = new Customer();

                customer = await _repository.ValidateCustomer(username, password);

                // then we need to get the token for the user 
                string Token = GenerateJWTToken();
    
                if(customer.CustomerId != 0)
                {
                    return Ok(new
                    {
                        Id = customer.CustomerId,
                        UserName = customer.FullName,
                        RoleId = 2,
                        Token = Token
                    });
                }
                else
                    return Unauthorized();
                    

            }
            else
                return Unauthorized();
        }

        #endregion




        #region Generate Token Jwt 

        private string GenerateJWTToken()
        {
            // Security key - - we can get the security key from App settings 


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Credentials or Algorithm 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // JWT Token 
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion




    }


}
