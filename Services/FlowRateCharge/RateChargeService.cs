using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using WebApi.Entities;
using WebApi.Models;
using Utf8Json;
using WebApi.Entities.DdContextTcrb;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace WebApi.Services.FlowRateCharge
{
    public class LateChargeService : ILateChargeService
    {

        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly DdContextTcrb _context;

        public LateChargeService(ILogger<LateChargeService> logger,
            IMapper mapper,
            DdContextTcrb context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public ResponseModel<LateCharge> CalculationRate(LateChargeReqModel req)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            var LateCharges = req.LateCharges;
            var header = req.Header;
            var rateMax = new LateChargeModel();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(req)}");

                #region Step 1 check parameter

                var parameter = _context.Parameter.FirstOrDefault(r => r.Name == "FirstLateCharge");
                if (parameter == null)
                {
                    return new ResponseModel<LateCharge>
                    {
                        Message = $"ไม่พบ FirstLateCharge ในฐานข้อมูล"
                    };
                }

                #endregion

                #region Step 2 หาค่า ratemax จากค่าทั้งหมด

                if (LateCharges.Count(r => !r.RateCode.HasValue && string.IsNullOrEmpty(r.Sign)) == LateCharges.Count)
                {//Rate Fix
                    var late = LateCharges.OrderByDescending(r => r.RateAmount).FirstOrDefault();
                    late.RateCal = late.RateAmount;
                    rateMax = late;
                }
                else
                {
                    //AlternateRate
                    var rateCodes = LateCharges.Select(r => r.RateCode).ToList();
                    var sSRATEs = _context.SSRATE.Where(r => rateCodes.Contains(r.JRRATN)).ToList();
                    if (!sSRATEs.Any())
                    {
                        return new ResponseModel<LateCharge>
                        {
                            Message = $"ไม่พบ RateCode: {string.Join(",", rateCodes)} ในฐานข้อมูล"
                        };
                    }

                    LateCharges.ForEach(LateCharge =>
                    {
                        var rateByInterfaceDate = (decimal?)null;
                        if (LateCharge.InterfaceDate.HasValue)
                        {
                            var rateByDate = sSRATEs.FirstOrDefault(sRate
                                => (sRate.JRDATE_FUTURE.HasValue && sRate.JRCRAT_FUTURE.HasValue)
                                && (sRate.JRRATN == LateCharge.RateCode)
                                && (LateCharge.InterfaceDate.Value.Date >= sRate.JRDATE_FUTURE.Value.Date));

                            if (rateByDate != null)
                            {
                                rateByInterfaceDate = rateByDate.JRCRAT_FUTURE.Value;
                                //LateCharge.Late = rateByInterfaceDate;
                                LateCharge.MrrEffectiveRateDate = rateByDate.JRDATE_FUTURE;
                            }
                        }

                        //Initial rate
                        var late = rateByInterfaceDate ?? (sSRATEs.FirstOrDefault(sRate => sRate.JRRATN == LateCharge.RateCode)?.JRCRAT ?? 0);
                        LateCharge.MrrRate = late;

                        LateCharge.RateCal = LateCharge.Sign switch
                        {
                            "+" => (late + LateCharge.RateAmount),
                            "-" => (late - LateCharge.RateAmount),
                            _ => LateCharge.RateAmount,
                        };

                    });

                    rateMax = LateCharges.OrderByDescending(r => r.RateCal).FirstOrDefault();
                }


                #endregion

                #region Step 3 validate product type and business logic

                //Validate ProductType
                var productType = _context.LNPAR2.FirstOrDefault(r => r.PTYPE == rateMax.Type);

                //Variable response
                var result = new LateCharge
                {
                    ApplicationNo = rateMax.ApplicationNo,
                    MrrRate = rateMax.MrrRate,
                    MrrEffectiveRateDate = rateMax.MrrEffectiveRateDate
                };

                if (productType != null)
                {
                    var calRateMax = 0M;
                    if (productType.LATCHGCOD.HasValue)
                    {
                        _ = decimal.TryParse(parameter.Value, out var firstLate);
                        calRateMax = (rateMax.RateCal + firstLate);
                    }
                    else
                    {
                        calRateMax = rateMax.RateCal;
                    }

                    var type = rateMax.Type;
                    var cELRATE = _context.CELRATE.FirstOrDefault(r => r.PTYPE == type);
                    if (cELRATE == null)
                    {
                        return new ResponseModel<LateCharge>
                        {
                            Message = $"ไม่พบ Celrate Type: {type} ในฐานข้อมูล"
                        };
                    }

                    var prrcelRATE = cELRATE.PRRCELRATE;
                    result.FirstLateCharge = (calRateMax > prrcelRATE ? prrcelRATE : calRateMax);
                }
                else
                {
                    //result.FirstLateCharge = 0;
                }

                #endregion

                #region Step 4 commit transation to db and  data response

                var obj = _context.LateCharge.FirstOrDefault(r => r.ApplicationNo == result.ApplicationNo);
                if (obj == null)
                {
                    var _dateNow = DateTime.Now;
                    result.CreateBy = header.UserID;
                    result.CreateDate = _dateNow;
                    result.UpdateBy = header.UserID;
                    result.UpdateDate = _dateNow;
                    _context.LateCharge.Add(result);
                    _context.SaveChanges();
                    _logger.LogInformation($"Finish (New Insert) Function => {methodName}, Result => {JsonSerializer.ToJsonString(result)}");
                    return new ResponseModel<LateCharge>
                    {
                        Success = true,
                        Datas = result
                    };
                }
                else
                {
                    obj.FirstLateCharge = result.FirstLateCharge;
                    obj.MrrRate = result.MrrRate;
                    obj.MrrEffectiveRateDate = result.MrrEffectiveRateDate;
                    obj.UpdateBy = header.UserID;
                    obj.UpdateDate = DateTime.Now;
                    _context.SaveChanges();
                    _logger.LogInformation($"Finish (Update) Function => {methodName}, Result => {JsonSerializer.ToJsonString(result)}");

                    return new ResponseModel<LateCharge>
                    {
                        Success = true,
                        Datas = obj
                    };
                }

                #endregion
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel<LateCharge> GetLateChage(LateCharge req)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;

            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(req)}");
                var result = _context.LateCharge.FirstOrDefault(r => r.ApplicationNo == req.ApplicationNo);
                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(result)}");
                return new ResponseModel<LateCharge>
                {
                    Success = true,
                    Datas = result
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }
    }
}
