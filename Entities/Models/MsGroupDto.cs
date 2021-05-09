using System;

namespace WebApi.Entities.Models
{
    public class MsGroupDto
    {
        public Guid? IdGroup { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public bool? IsActive { get; set; }
    }
}