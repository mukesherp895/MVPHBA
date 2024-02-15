using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPHBA.API.Attributes;
using MVPHBA.Common;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;
using MVPHBA.Service.Interfaces;

namespace MVPHBA.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/property")]
    [ApiController]
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    public class PropertyInfoController : ControllerBase
    {
        private readonly IPropertyInfoService _propertyInfoService;
        private readonly UserManager<Users> _userManager;
        private readonly IMapper _mapper;
        public PropertyInfoController(IPropertyInfoService propertyInfoService, UserManager<Users> userManager, IMapper mapper)
        {
            _propertyInfoService = propertyInfoService;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet("propertylist")]
        [Consumes("application/json")]
        public async Task<IActionResult> PropertyListGet(int displayStart, int displayLength, string? sortDir, int sortCol, string? location, int price, string? propertyType)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                SearchVM dto = new SearchVM
                {
                    DisplayStart = displayStart,
                    DisplayLength = displayLength,
                    SortDir = sortDir, //asc or desc
                    SortCol = sortCol,
                    Location = location,
                    Price = price,
                    PropertyType = propertyType
                };
                resp.Message = "Success";
                resp.Status = APIRespCodes.Success;
                resp.Data = await _propertyInfoService.PropertyInfoList(dto);
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
        [Consumes("multipart/form-data")]
        [HttpPost("addupdate")]
        public async Task<IActionResult> AddUpdate([FromForm] PropertyInfoReqVM dto)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(user != null && user.UserType == UserTypes.Broker)
                {
                    if (ModelState.IsValid)
                    {
                        dto.BrokerId = user.Id;
                        if(dto.Id == null || dto.Id == 0)
                        {
                            if(dto.PropertyType == PropertyTypes.Land || dto.PropertyType == PropertyTypes.House || dto.PropertyType == PropertyTypes.Building)
                            {
                                if (await _propertyInfoService.Add(dto) == EnumData.DBAttempt.Success)
                                {
                                    resp.Message = "Success";
                                    resp.Status = APIRespCodes.Success;
                                }
                                else
                                {
                                    resp.Message = "Fail";
                                    resp.Status = APIRespCodes.Fail;
                                }
                            }
                            else
                            {
                                resp.Message = "Invalid Property Type";
                                resp.Status = APIRespCodes.Fail;
                            }
                        }
                        else
                        {
                            if (dto.PropertyType == PropertyTypes.Land || dto.PropertyType == PropertyTypes.House || dto.PropertyType == PropertyTypes.Building)
                            {
                                if (await _propertyInfoService.Edit(dto) == EnumData.DBAttempt.Success)
                                {
                                    resp.Message = "Success";
                                    resp.Status = APIRespCodes.Success;
                                }
                                else
                                {
                                    resp.Message = "Fail";
                                    resp.Status = APIRespCodes.Fail;
                                }
                            }
                            else
                            {
                                resp.Message = "Invalid Property Type";
                                resp.Status = APIRespCodes.Fail;
                            }
                        }
                    }
                    else
                    {
                        resp.Message = "Bad Request";
                        resp.Status = APIRespCodes.Fail;
                    }
                }
                else
                {
                    resp.Message = "Invalid User Type";
                    resp.Status = APIRespCodes.Fail;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null && user.UserType == UserTypes.Broker)
                {
                    if (id > 0)
                    {
                        if (await _propertyInfoService.Delete(id) == EnumData.DBAttempt.Success)
                        {
                            resp.Message = "Success";
                            resp.Status = APIRespCodes.Success;
                        }
                        else
                        {
                            resp.Message = "Fail";
                            resp.Status = APIRespCodes.Fail;
                        }
                    }
                    else
                    {
                        resp.Message = "Bad Request";
                        resp.Status = APIRespCodes.Fail;
                    }
                }
                else
                {
                    resp.Message = "Invalid User Type";
                    resp.Status = APIRespCodes.Fail;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
        [HttpPost("detail/{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null && user.UserType == UserTypes.Broker)
                {
                    if (id > 0)
                    {
                        var propertyInfo = _propertyInfoService.GetById(id);
                        var data = _mapper.Map<PropertyDetailVM>(propertyInfo);
                        data.ImagePath = $"https://localhost:7177/api/imageget/{data.Id}";
                        data.BrokerName = propertyInfo.Broker.FullName;
                        data.BrokerContactNo = propertyInfo.Broker.PhoneNumber ?? "";
                        resp.Status = "Success";
                        resp.Message = APIRespCodes.Success;
                        resp.Data = data;
                    }
                    else
                    {
                        resp.Message = "Bad Request";
                        resp.Status = APIRespCodes.Fail;
                    }
                }
                else
                {
                    resp.Message = "Invalid User Type";
                    resp.Status = APIRespCodes.Fail;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
        [HttpGet("propertytypelist")]
        public IActionResult PropertyTypeListGet()
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                List<object> data = new List<object>();
                data.Add(new { PropertyType = PropertyTypes.Land });
                data.Add(new { PropertyType = PropertyTypes.House });
                data.Add(new { PropertyType = PropertyTypes.Building });
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
        [Route("imageget/{id}")]
        [HttpGet]
        public async Task<IActionResult> ImageGet(long id)
        {
            ResponseVM resp = new ResponseVM();
            try
            {
                if(id > 0)
                {
                    var data = _propertyInfoService.GetById(id);
                    if(data != null)
                    {
                        var fileData = await Others.ToArrayAsync(data.ImagePath);
                        return File(fileData.Byte, fileData.MIMEType);
                    }
                    else
                    {
                        resp.Message = "Invalid Request";
                        resp.Status = APIRespCodes.Fail;
                    }
                }
                else
                {
                    resp.Message = "Bad Request";
                    resp.Status = APIRespCodes.Fail;
                }
            }
            catch(Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = APIRespCodes.Fail;
            }
            return Ok(resp);
        }
    }
}
