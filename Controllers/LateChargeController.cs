using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services.FlowRateCharge;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LateChargeController : ControllerBase
    {
        private readonly ILateChargeService _rateChargeService;

        public LateChargeController(ILateChargeService rateChargeService)
        {
            _rateChargeService = rateChargeService;
        }

        [HttpPost]
        public ResponseModel Rate(LateChargeReqModel req)
        {
            var res = new ResponseModel();

            try
            {
                var result = _rateChargeService.CalculationRate(req);
                if (result.Success)
                {
                    res.Datas = result.Datas;
                    res.Success = result.Success;
                }
                else
                {
                    res.Message = result.Message;
                }
                return res;
            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpGet("GetLateChage")]
        public ResponseModel GetLateChage(string applicationNo)
        {
            var res = new ResponseModel();

            try
            {
                //Validate
                if (string.IsNullOrEmpty(applicationNo))
                {
                    res.Message = "กรุณาระบุ ApplicationNo";
                    return res;
                }

                var result = _rateChargeService.GetLateChage(new LateCharge { ApplicationNo = applicationNo });
                if (result.Success)
                {
                    res.Datas = result.Datas;
                    res.Success = result.Success;
                }
                else
                {
                    res.Message = result.Message;
                }
                return res;
            }
            catch
            {
                return new ResponseModel
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpGet("TEST REQUEST")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ResponseModel TestRate()
        {
            var req = new LateChargeReqModel
            {
                Header = new HeaderModel
                {
                    SystemId = "000000000",
                    UserID = "TEST"
                },
                LateCharges = new List<LateChargeModel>{
                    new LateChargeModel
                    {
                        ApplicationNo = "SB-2513-xx1",
                        From = 1,
                        To = 10,
                        RateCode = 666,
                        Sign = "-",
                        RateAmount = 2,
                        Type = "A1"
                    },
                    new LateChargeModel
                    {
                        ApplicationNo = "SB-2513-xx1",
                        From = 12,
                        To = 36,
                        RateCode = 665,
                        Sign = "+",
                        RateAmount = 2.5M,
                        Type = "A1"
                    },

                    new LateChargeModel
                    {
                        ApplicationNo = "SB-2513-xx1",
                        From = 37,
                        To = 44,
                        RateCode = null,
                        Sign = null,
                        RateAmount = 36,
                        Type = "A1"
                    }
                }
            };

            var result = Rate(req);
            return result;
        }

    }
}
