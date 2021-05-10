using System;

namespace WebApi.Entities.Models
{
    public class MsQuestionDto
    {
        public Guid IdQuestion { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}