using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services.Group
{
    public interface IMsGroupService
    {
        public ResponseModels<Entities.Models.MsGroup> Inquiry(List<Entities.Models.MsGroup> msGroups);
        public ResponseModels<Entities.Models.MsGroup> Create(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Update(List<Entities.Models.MsGroup> msGroups);
        public ResponseModel Delete(List<Entities.Models.MsGroup> msGroups);
    }
}
