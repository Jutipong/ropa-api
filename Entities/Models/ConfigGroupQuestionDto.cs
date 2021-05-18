using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities.Models
{
    public class ConfigGroupQuestionDto
    {
        public Guid IdGroup { get; set; }
        public List<ConfigGroupQuestionModel> Questions { get; set; } = new List<ConfigGroupQuestionModel>();
    }


    public class ConfigGroupQuestionModel
    {
        public Guid IdConfigGroupQuestion { get; set; }
        public Guid IdGroup { get; set; }
        public Guid IdQuestion { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
    }

}
