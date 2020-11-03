using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_GUARD.Model.User
{
    public class NewUser
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Faculty { get; set; }
        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }
        public string Status { get; set; }
    }
}
