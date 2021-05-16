using WebApi.Models;

namespace WebApi.Services.MsQuestion
{
    public interface IMsQuestionService
    {
        public ResponseModels<Entities.Models.MsQuestion> Inquiry(MsQuestionReqModel req);
        public ResponseModel Create(Entities.Models.MsQuestionDto msQuestionDto);
        public ResponseModel Update(Entities.Models.MsQuestion msQuestion);
        public ResponseModel Delete(Entities.Models.MsQuestion msQuestion);
        public ResponseModel GetAll();
        public ResponseModel GetQuestionById(Entities.Models.MsGroup msGroup);
    }
}
