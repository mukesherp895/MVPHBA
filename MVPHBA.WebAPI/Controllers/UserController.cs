using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MVPHBA.Common;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVPHBA.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<Users> _signInManager;
        private readonly IConfiguration _iconfiguration;
        public UserController(UserManager<Users> userManager, IMapper mapper, SignInManager<Users> signInManager, IConfiguration iconfiguration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _iconfiguration = iconfiguration;
        }
        [HttpGet("userttypelist")]
        public IActionResult UserTypeListGet()
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                List<object> data = new List<object>();
                data.Add(new { PropertyType = UserTypes.Seeker });
                data.Add(new { PropertyType = UserTypes.Broker });
                resp.Message = "Success";
                resp.Status = APIRespCodes.Success;
                resp.Data = data;
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterReqVM dto)
        {
            ResponseVM responseVM = new ResponseVM();
            try
            {
                if(ModelState.IsValid)
                {
                    var user = _mapper.Map<Users>(dto);
                    user.UserName = dto.Email;
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        responseVM.Status = APIRespCodes.Success;
                        responseVM.Message = "Successfully User Register";
                    }
                    else
                    {
                        responseVM.Status = APIRespCodes.Fail;
                        responseVM.Message = result.Errors.Select(s => s.Description).FirstOrDefault();
                    }
                }
                else
                {
                    responseVM.Status = APIRespCodes.Fail;
                    responseVM.Message = "Bad Request";
                }
            }
            catch (Exception ex)
            {
                responseVM.Status = APIRespCodes.Fail;
                responseVM.Message = ex.Message;
            }
            return Ok(responseVM);
        }
        [HttpPost("auth")]
        public async Task<IActionResult> Authentication(UserAuthReqVM dto)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto.UserType == UserTypes.Broker || dto.UserType == UserTypes.Seeker)
                    {
                        var user = await _userManager.FindByNameAsync(dto.UserName);
                        if (user != null)
                        {
                            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
                            if (result.Succeeded)
                            {
                                var tokenHandler = new JwtSecurityTokenHandler();
                                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                                var tokenDescriptor = new SecurityTokenDescriptor
                                {
                                    Subject = new ClaimsIdentity(new Claim[]
                                    {
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim(ClaimTypes.GivenName, user.FullName),
                                        new Claim(ClaimTypes.Email, user.Email),
                                        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
                                    }),
                                    Expires = DateTime.UtcNow.AddMinutes(10),
                                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                                };
                                var token = tokenHandler.CreateToken(tokenDescriptor);
                                resp.Status = APIRespCodes.Success;
                                resp.Message = "Success";
                                resp.Data = new { Token = tokenHandler.WriteToken(token), TokenType = "Bearer" };
                                return Ok(resp);
                            }
                        }
                    }
                    resp.Status = APIRespCodes.Fail;
                    resp.Message = "Invalid UserName or Password";
                }
                else
                {
                    resp.Status = APIRespCodes.Fail;
                    resp.Message = "Bad Request";
                }
            }
            catch (Exception ex)
            {
                resp.Status = APIRespCodes.Fail;
                resp.Message = ex.Message;
            }
            return Ok(resp);
        }

    }
}
