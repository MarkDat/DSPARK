using GATE_SCAN.Common;
using GATE_SCAN.Model;
using GATE_SCAN.Util;
using IPSS;
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
using ZXing;

namespace GATE_SCAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FirebaseUtil.configDb();
            detector.CropResultImage = true;
           
        }
        int LINE_GATE = 1;
        int LOCATION = Common.Common.LOCATION;
        CameraScan cam, camQr;
        IPSSbike detector = new IPSSbike();
        BikePlate bp;



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cam != null && cam.isRunning()) cam.stop();
            if (camQr != null && camQr.isRunning()) camQr.stop();
        }

        private async void timerQr_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("scan");
            if (rbCamIn.Checked) { pbPlateOld.Visible = false; camIn(); }
            else
            {
                pbPlateOld.Visible = true;
                camOut();
            }
        }
        private void camIn() {
            if (camQr != null && camQr != null)
            {
                UserBundle user;
                if (checkAndAssign(out user, lbWarning, true) == null)
                {
                    lbWait.Visible = false; return;
                }

                lbId.Text = user.idS;
                lbName.Text = user.name;
                lbRole.Text = MyAdapter.role(user.role);
                lbFaculty.Text = user.faculty;
                //Quét biên số
                Bitmap bm = (Bitmap)pbCam.Image;
                

                bp = detector.ReadPlate(bm);
                pbPlate.Image = bp.bitmap;
                if (bp.isValid && bp.hasPlate) // Nếu biển số OK thì đẩy lên firebase
                {
                    lbStatus.Text = "OK";
                    lbPlate.Text = bp.text;
                    lbStatus.ForeColor = Color.Green;
                    
                    if(FireAction.checkParking(user.id,LOCATION))
                    {
                        messErr(false, "You're parking !");
                        return;
                    }
                    user.txtPlate = lbPlate.Text;
                    user.img = MyAdapter.convertImageToStringBase64(bm);
                    user.imgPlate = MyAdapter.convertImageToStringBase64(bp.bitmap);

                    //Đẩy lên firebase 
                    bool check = FireAction.pushUserBundleAsync(user.id, user, LOCATION);
                    
                    if (!check)
                    {
                        messErr(false, "Sorry ! Err connect to sever");
                    }
                }
                else
                {
                    lbStatus.Text = "ERR";
                    lbStatus.ForeColor = Color.DarkRed;
                }
            }
        }
        private void camOut()
        {
            if (camQr != null && camQr != null)
            {
                UserBundle user;
                if (checkAndAssign(out user, lbWarning, true) == null)
                {
                    lbWait.Visible = false; return;
                }

                lbId.Text = user.idS;
                lbName.Text = user.name;
                lbRole.Text = MyAdapter.role(user.role);
                lbFaculty.Text = user.faculty;
                //Quét biên số
                Bitmap bm = (Bitmap)pbCam.Image;


                bp = detector.ReadPlate(bm);
                pbPlate.Image = bp.bitmap;
                if (bp.isValid && bp.hasPlate) // Nếu biển số OK thì xóa obj trên firebase
                {
                    lbStatus.Text = "OK";
                    lbPlate.Text = bp.text;
                    lbStatus.ForeColor = Color.Green;
                    user.txtPlate = lbPlate.Text;

                    if (FireAction.checkUserExist(user.id, LOCATION))
                    {
                        if (FireAction.checkSecretValid(user.id, user.secretNum))
                        {
                            if (FireAction.checkUserPlate(user.id,LOCATION,user.txtPlate))
                            {
                                FireAction.removeUser(user.id, LOCATION);
                                FireAction.addHistoty(user.id, new History
                                {
                                    dateSend  = DateTime.Parse(user.timeIn).ToString("yyyy-MM-dd hh:mm:ss"),
                                    dateGet = DateTime.Now.ToString(),
                                    plateLicense = user.txtPlate
                                });
                                messErr(true, "OK! Have a good day !");
                            }
                            else messErr(false, "Your license plate is not the same as when you in !");
                        }
                        else messErr(false, "Please reload your QR !");
                    }
                    else messErr(false, "You're not parking here !");

                }
                else
                {
                    lbStatus.Text = "ERR";
                    lbStatus.ForeColor = Color.DarkRed;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cam = new CameraScan(cbbCam,0,pbCam);
            camQr = new CameraScan(cbbQr,0,pbCamQr);
            if (!string.IsNullOrEmpty(FireAction.checkVersion())) Console.WriteLine("LET'S START");
        }
        private JObject readQRIn(out JObject rs, PictureBox camQRIn)
        {
            rs = null;
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)camQRIn.Image.Clone());
            if (result == null) return rs;   //don't read anymore  
            //not json
            try
            {
                rs = JObject.Parse(result.ToString());
            }
            catch (Exception)
            {
                rs = null;
                Console.WriteLine("Err function: readQRIn()");
            }
            return rs;
        }

      

        private UserBundle checkAndAssign(out UserBundle user, Label lb,bool checkTurn)
        {
           
            user = null;
            //Doc QR tu mayquet
            JObject obj;
            readQRIn(out obj, pbCamQr);
            
            if (obj == null) return user;
            //Nếu doc dc QR thi hien lable WAITING
            lbWait.Visible = true;
            JObject dataFire=null;
            try
            {
                Console.WriteLine("check");
                dataFire = GetInfoFire.getInfoParkingMan(obj["id"].ToString());
                lb.Text = dataFire!=null ? "" : "Wrong user";
            }
            catch (Exception)
            {
                lb.Text = "Wrong QR";
                return user;
            }


            if (dataFire==null) return user;
            

            try
            {
                user = new UserBundle
                {
                    id = obj["id"].ToString(),
                    idS = dataFire["position"].ToString().Equals("3") ? dataFire["idStudent"].ToString() : dataFire["idLecturers"].ToString(),
                    faculty = dataFire["classS"].ToString(),
                    status = true,
                    name = dataFire["name"].ToString(),
                    role = int.Parse(dataFire["position"].ToString()),
                    timeIn = DateTime.Now.ToString(),
                    timeOut = checkTurn ? "none" : DateTime.Now.ToString(), //Kiem tra xem di hay ve
                    img = "",
                    imgPlate = "",
                    txtPlate = "",
                    gate = LINE_GATE,
                    secretNum = obj["secretNum"].ToString(),
                    requestCode = "1"
                };
            }
            catch (Exception)
            {
                user = null;
                messErr(false,"Reload your QR !") ;
                Console.WriteLine("Err function: checkAndAssign()");
            }
            return user;
        }
        void messErr(bool check,string s)
        {
            if(check)
            {
                lbStatus.Text = "OK";
                lbStatus.ForeColor = Color.Green;
                lbWarning.Text = s;
            }
            else
            {
                lbStatus.Text = "ERR";
                lbStatus.ForeColor = Color.Red;
                lbWarning.Text = s;
            }
        }
    }
}
