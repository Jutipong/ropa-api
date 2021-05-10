using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class GroupsModel
    {
        public class GroupReqModel  : QTableModel
        {
            public string Filter { get; set; }
        }
    }
}
