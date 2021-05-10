using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using static WebApi.Models.GroupsModel;

namespace WebApi.Services.MsQuestion
{
    public interface IMsQuestionService
    {
        public ResponseModels<Entities.Models.MsQuestion> Inquiry(MsQuestionReqModel req);
        public ResponseModel Create(Entities.Models.MsQuestionDto msQuestionDto);
        public ResponseModel Update(Entities.Models.MsQuestion msQuestion);
        public ResponseModel Delete(Entities.Models.MsQuestion msQuestion);
    }
}
