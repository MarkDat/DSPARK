using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATE_SCAN.Common
{
    public static class MyAdapter
    {
        public static string role(int role)
        {
            switch (role)
            {
                case 2: return "Lecturers";
                case 3: return "Student";
                case 4: return "Customer";
                default: return "Not define";
            }
        }
        public static string convertImageToStringBase64(Image image)
        {
            if (image == null) Console.WriteLine("NULL MCNR");
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, ImageFormat.Jpeg);
                byte[] imageBytes = m.ToArray();
                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Bitmap convertStringBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return (Bitmap)image;
        }

    }
}
