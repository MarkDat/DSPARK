using FireSharp.Response;
using GATE_SCAN.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Common
{
    public static class GetInfoFire
    {
        public static JObject getInfoParkingMan(string id)
        {
            JObject dt = null;
            FirebaseResponse res = FirebaseUtil.cl.Get(@"User/information/parkingMan/" + id);
            if (res.Body.Equals("null")) return dt;
            dt = res.ResultAs<JObject>();
            Console.WriteLine(dt.ToString());
            return dt;
        }
    }
}
