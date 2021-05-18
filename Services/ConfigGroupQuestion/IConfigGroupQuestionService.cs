using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services.ConfigGroupQuestion
{
    public interface IConfigGroupQuestionService
    {
        public ResponseModel GetQuestionById(Entities.Models.MsGroup msGroup);
        public ResponseModel Action(Entities.Models.ConfigGroupQuestionDto configGroupQuestionDtos);
    }
}
