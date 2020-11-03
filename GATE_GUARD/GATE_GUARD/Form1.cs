using FireSharp.EventStreaming;
using FireSharp.Response;
using GATE_GUARD.Model.Dao;
using GATE_GUARD.Model.User;
using GATE_GUARD.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GATE_GUARD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FirebaseUtil.configDb();
            uD = new UserDao();
            //gvShow.AutoGenerateColumns = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            gvShow.DataSource = uD.getListUser();
            listen();
        }
        UserDao uD;
        int LOCATION = Common.Common.LOCATION;
        async void listen()
        {
            await FirebaseUtil.cl.OnAsync("APIParking/Data/Location/" + LOCATION + "/idUser", added: (s, args, d) =>
            {
                Console.WriteLine(args.Data);
                if (uD.checkParking(args.Data)) {
                    Console.WriteLine("Nguoi nay dang do");
                    return; 
                }
                string idGet = args.Data;

                FirebaseUtil.cl.OnAsync("APIParking/Data/Location/" + LOCATION + "/information/" + idGet, added: (a, b, c)=> {
                    if (uD.checkParking(idGet))
                    {
                        Console.WriteLine("Nguoi nay dang do");
                        return;
                    }
                    addUser(idGet);
                });

            }, removed: (s, rm, d) =>
            {
                Console.WriteLine("REMOVE");
                string idRm = rm.Path.Substring(1);
                if (uD.setLeave(idRm))
                {
                    gvShow.DataSource = uD.getListUser();
                    Console.WriteLine("Rời đi thành công");
                    return;
                }else Console.WriteLine("Rời đi không");
                
            });

           
        }
        void addUser(string idGet)
        {
            FirebaseResponse res = FirebaseUtil.cl.Get("APIParking/Data/Location/" + LOCATION + "/information/" + idGet);
            if (res.Body.Equals("null")) return;
            Console.WriteLine("===================" + idGet);

            if (uD.checkParking(idGet))
            {
                Console.WriteLine("Nguoi nay dang do");
                return;
            }
            JObject obj = res.ResultAs<JObject>();
            //add user_vihecle
            if (!uD.findUser(idGet))
            {
                Console.WriteLine("VÔ USER");              
                uD.addUser(obj["idS"].ToString(), idGet, obj["name"].ToString(), int.Parse(obj["role"].ToString()), obj["faculty"].ToString());
            }
            //add plate
            if (!uD.findPlate(obj["txtPlate"].ToString()))
            {
                Console.WriteLine("VÔ Plate");
                uD.addPlate("","", obj["txtPlate"].ToString());
            }
            //add parking
            uD.addParking(idGet, obj["txtPlate"].ToString(), DateTime.Parse(obj["timeIn"].ToString()),true, obj["timeOut"].ToString().Equals("none") ? DateTime.Now : DateTime.Parse(obj["timeOut"].ToString()));
             gvShow.DataSource = uD.getListUser();
            Console.WriteLine("ADD USER SUCCESS");
        }
        
       


    }
}
