using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services.MsQuestion
{
    public interface IMsQuestionService
    {
        public ResponseModels<Entities.Models.MsGroup> Inquiry(GroupReqModel req);
        public ResponseModels<Entities.Models.MsGroup> Create(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Update(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Delete(List<Entities.Models.MsGroup> msGroups);
    }
}
