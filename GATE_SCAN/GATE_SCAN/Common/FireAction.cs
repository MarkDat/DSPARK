using FireSharp.Response;
using GATE_SCAN.Model;
using GATE_SCAN.Util;
using Newtonsoft.Json.Linq;
using shortid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Common
{
    public static class FireAction
    {
        public static bool pushUserBundleAsync(string id,UserBundle user,int location)
        {
            FirebaseUtil.cl.Set("APIParking/Data/Location/" + location + "/idUser/" + id, id);
            FirebaseResponse res =   FirebaseUtil.cl.Update("APIParking/Data/Location/" +location+"/information/"+ id, user);
            if (res.Body.Equals("null")) return false;
            return true;
        }
        public static string checkVersion()
        {
            FirebaseResponse res = FirebaseUtil.cl.Get(@"Version");
            if (res.Body.Equals("null")) return "";
            return res.ResultAs<string>();
            
        }
        public static bool checkParking(string id, int location)
        {
            FirebaseResponse res = FirebaseUtil.cl.Get("APIParking/Data/Location/" + location + "/information/" + id);
            if (res.Body.Equals("null")) return false;
            return true;
        }
        public static bool checkUserExist(string id,int location)
        {
            FirebaseResponse res = FirebaseUtil.cl.Get("APIParking/Data/Location/" + location + "/information/" + id);
            if (res.Body.Equals("null")) return false;
            return true;
        }
        public static bool removeUser(string id, int location)
        {
            FirebaseResponse rmID = FirebaseUtil.cl.Delete("APIParking/Data/Location/" + location + "/idUser/" + id);
            FirebaseResponse res = FirebaseUtil.cl.Delete("APIParking/Data/Location/" + location + "/information/" + id);        
            if (res.Body.Equals("null")) return false;
            if (rmID.Equals("null")) return false;
            return true;
        }
        public static void addHistoty(string id,History hs)
        {
            FirebaseUtil.cl.Push("History/parkingMan/moneyOut/" + id, hs);
        }
        public static bool checkUserPlate(string id, int location,string yourPlate)
        {
            FirebaseResponse res = FirebaseUtil.cl.Get("APIParking/Data/Location/" + location + "/information/" + id+ "/txtPlate");
            if (res.Body.Equals("null")) return false;
            string dt = res.ResultAs<string>();
            if (yourPlate.Trim().Equals(dt.Trim())) return true;
            return false;
        }
        public static bool checkSecretValid(string id,string secret)
        {
            FirebaseResponse res = FirebaseUtil.cl.Get(@"User/information/parkingMan/" + id + "/secretNum");
            if (res.Body.Equals("null")) return false;
            string dt = res.ResultAs<string>();
            if (secret.Trim().Equals(dt.Trim())) return true;
            return false;
        }
    }
}
