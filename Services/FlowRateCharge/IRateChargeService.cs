using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services.FlowRateCharge
{
    public interface ILateChargeService
    {
        public ResponseModel<LateCharge> CalculationRate(LateChargeReqModel lateCharge);
        public ResponseModel<LateCharge> GetLateChage(LateCharge lateCharge);
    }
}
