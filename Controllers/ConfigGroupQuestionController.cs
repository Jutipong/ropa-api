using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services.ConfigGroupQuestion;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigGroupQuestionController : Controller
    {
        private readonly IConfigGroupQuestionService _configGroupQuestionService;
        public ConfigGroupQuestionController(IConfigGroupQuestionService configGroupQuestionService)
        {
            _configGroupQuestionService = configGroupQuestionService;
        }

        [HttpGet("GetQuestionById")]
        public ResponseModel GetQuestionById(Guid IdGroup)
        {
            var result = new ResponseModel();
            try
            {
                result = _configGroupQuestionService.GetQuestionById(new MsGroup { IdGroup = IdGroup });
                return result;
            }
            catch
            {
                return new ResponseModel
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Action")]
        public ResponseModel Action([FromBody]ConfigGroupQuestionDto configGroupQuestionDtos)
        {
            var result = new ResponseModel();
            try
            {
                result = _configGroupQuestionService.Action(configGroupQuestionDtos);
                return result;
            }
            catch
            {
                return new ResponseModel
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
