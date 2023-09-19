using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Data;
using NewProject.Models.Authentication.Login;
using NewProject.Models.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NewProject.Models;
using Microsoft.Data.SqlClient;
using NewProject.Models.Authentication.SignUp;
using AutoMapper;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(ApplicationDbContext context,IConfiguration configuration,IMapper mapper) 
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var username=_context.Users.SingleOrDefault(p=>p.UserName==registerUser.UserName);
            if (username != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
            }
            var userEmail=_context.Users.SingleOrDefault(u=>u.Email==registerUser.Email);
            if (userEmail != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "Email already exists!" });
            }
            var roleID = _context.Roles.SingleOrDefault(r => r.RoleID == registerUser.RoleID);
            if (roleID == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "This Role Doesn't Exist!" });
            }


            var newUser = _mapper.Map<User>(registerUser);
            _context.Users!.Add(newUser);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "User Created SuccessFully!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user=_context.Users.SingleOrDefault(u=>u.UserName == loginModel.UserName &&
               loginModel.Password==u.Password);
            if (user != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),                   
                };

                string Constr = @"server=.;database=NewProjectDB;Integrated security=True";
                string userRole="";
                using (SqlConnection con=new SqlConnection(Constr))
                {
                    string sql = string.Format(
                        @"Select a.*,b.RoleName from dbo.[User] a inner join dbo.[Role] b on a.RoleID=b.RoleID
                            where a.UserName='{0}' and a.Password='{1}'",
                        loginModel.UserName, loginModel.Password) ;
                    SqlCommand cmd= new SqlCommand(sql, con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while(rd.Read())
                    {
                        userRole = rd["RoleName"].ToString();
                    }
                   
                }

                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();

        }        
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
