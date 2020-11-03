using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Util
{
   public static class FirebaseUtil
    {
        static IFirebaseConfig config;
        static public IFirebaseClient cl;
        static public void configDb()
        {
            config = new FirebaseConfig
            {
                AuthSecret = "60NwcUMNV5rvqZSD6adULFT2tkfJgRT4cbw4Q0hs",
                BasePath = "https://dtuparking.firebaseio.com/"
            };
            cl = new FirebaseClient(config);
        }
    }
}
