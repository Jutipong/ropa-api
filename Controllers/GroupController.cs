using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities.Models;
using WebApi.Models;
using WebApi.Services.Group;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public ResponseModels<MsGroup> Inquiry(List<MsGroup> msGroups)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _groupService.Inquiry(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModels<MsGroup>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost]
        public ResponseModels<MsGroup> Create(List<MsGroup> msGroups)
        {
            var result = new ResponseModels<MsGroup>();
            try
            {
                result = _groupService.Create(msGroups);
                return result;
            }
            catch
            {
                return new ResponseModels<MsGroup>
                {
                    Message = result.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost]
        public ResponseModel Update(List<MsGroup> msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _groupService.Update(msGroups);
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

        [HttpPost]
        public ResponseModel Delete(List<MsGroup> msGroups)
        {
            var result = new ResponseModel();
            try
            {
                result = _groupService.Delete(msGroups);
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
