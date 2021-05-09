using System.Collections.Generic;
using WebApi.Models;
using static WebApi.Models.GroupsModel;

namespace WebApi.Services.Group
{
    public interface IMsGroupService
    {
        public ResponseModels<Entities.Models.MsGroup> Inquiry(GroupReqModel req);
        public ResponseModel<Entities.Models.MsGroup> Create(Entities.Models.MsGroupDto msGroup);
        public ResponseModel Update(Entities.Models.MsGroup msGroups);
        public ResponseModel Delete(Entities.Models.MsGroup msGroups);
    }
}
