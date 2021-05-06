using System.Collections.Generic;
using WebApi.Models;
using static WebApi.Models.GroupsModel;

namespace WebApi.Services.Group
{
    public interface IMsGroupService
    {
        public ResponseModels<Entities.Models.MsGroup> Inquiry(GroupReqModel req);
        public ResponseModels<Entities.Models.MsGroup> Create(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Update(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Delete(List<Entities.Models.MsGroup> msGroups);
    }
}
