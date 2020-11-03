using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GATE_SCAN.Util
{
    public class CameraScan
    {
        //camera
        private FilterInfoCollection captureDevices;
        private VideoCaptureDevice videoSource = null;
        PictureBox pbCam;

        public CameraScan()
        {
            captureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }
        public CameraScan(ComboBox cbb, int cbbSelectedIndex, PictureBox pbCam)
        {
            captureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (cbb.Text.Equals(""))
                foreach (FilterInfo device in captureDevices)
                {
                    cbb.Items.Add(device.Name);
                }

            cbb.SelectedIndex = cbbSelectedIndex;
            videoSource = new VideoCaptureDevice();
            videoSource = new VideoCaptureDevice(captureDevices[cbbSelectedIndex].MonikerString);
            this.pbCam = pbCam;
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            this.pbCam.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        public Bitmap capture()
        {
            return (Bitmap)this.pbCam.Image.Clone();
        }


        public void start()
        {
            videoSource.Start();
        }
        public void stop()
        {
            if (videoSource != null && videoSource.IsRunning) videoSource.Stop();
        }

        public bool isNull()
        {
            return videoSource == null;
        }
        public bool isRunning()
        {
            return videoSource.IsRunning;
        }
    }
}
