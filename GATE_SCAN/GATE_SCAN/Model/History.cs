using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Model
{
   public class History
    {
        public string dateGet { get; set; } 
        public string idPay { get; set; } =ShortId.Generate(true,false, 9);
        public string dateSend { get; set; }
        public string method { get; set; } = "0";
        public string payMoney { get; set; } = "1000";
        public string place { get; set; } = Common.Common.LOCATION+"";
        public string plateLicense { get; set; }

    }
}
