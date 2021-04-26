using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Models
{
    public class LateChargeReqModel
    {
        public HeaderModel Header { get; set; }
        public List<LateChargeModel> LateCharges { get; set; }
    }

    public class HeaderModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string SystemId { get; set; }
    }

    public class LateChargeModel
    {
        [Required]
        public string ApplicationNo { get; set; }
        public decimal? RateCode { get; set; }
        public string Sign { get; set; }
        public string Type { get; set; }
        //public decimal FirstLateCharge { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal RateAmount { get; set; }
        public DateTime? InterfaceDate { get; set; }
        //rate ที่ผ่านการคำนวนแล้ว
        [JsonIgnore]
        public decimal RateCal { get; set; } = 0;
        public decimal? MrrRate { get; set; }
        public DateTime? MrrEffectiveRateDate { get; set; }
    }

}
