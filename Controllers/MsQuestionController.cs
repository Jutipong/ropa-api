using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services.MsQuestion;

namespace ropa_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MsQuestionController : ControllerBase
    {
        private readonly IMsQuestionService _msQuestionService;
        public MsQuestionController(IMsQuestionService msQuestionService)
        {
            _msQuestionService = msQuestionService;
        }

        [HttpPost("Inquiry")]
        public ResponseModels<MsQuestion> Inquiry(MsQuestionReqModel req)
        {
            var result = new ResponseModels<MsQuestion>();
            try
            {
                result = _msQuestionService.Inquiry(req);
                return result;
            }
            catch
            {
                return new ResponseModels<MsQuestion>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost("Create")]
        public ResponseModel Create(MsQuestionDto msQuestionDto)
        {
            var result = new ResponseModel();
            try
            {
                result = _msQuestionService.Create(msQuestionDto);
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

        [HttpPost("Update")]
        public ResponseModel Update(MsQuestion msQuestion)
        {
            var result = new ResponseModel();
            try
            {
                result = _msQuestionService.Update(msQuestion);
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

        [HttpPost("Delete")]
        public ResponseModel Delete(MsQuestion msQuestion)
        {
            var result = new ResponseModel();
            try
            {
                result = _msQuestionService.Delete(msQuestion);
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

        [HttpPost("GetAll")]
        public ResponseModel GetAll(MsQuestion msQuestion)
        {
            var result = new ResponseModel();
            try
            {
                result = _msQuestionService.GetAll();
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
