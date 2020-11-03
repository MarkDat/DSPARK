using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Model
{
   public class UserBundle
    {
        public int gate { get; set; }
        public string id { get; set; }
        public string idS { get; set; }
        public string name { get; set; }
        public int role { get; set; }
        public bool status { get; set; }
        public string faculty { get; set; }
        public string txtPlate { get; set; }
        public string img { get; set; }
        public string imgPlate { get; set; }
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string secretNum { get; set; }
        public string requestCode { get; set; } = "1";
        //0 cho qua (nhận từ bảo vệ)
        //1 yêu cầu được gửi ok
        //2 yêu cầu được gửi bị lỗi cần bảo vệ gửi 0 để cho qua hoặc 3 để không cho qua
        //3 không cho qua (nhận từ bảo vệ)
    }
}
