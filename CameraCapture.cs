using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace FaceDetection
{
    public static  class CameraCapture
    {
        private static Capture camera = null;
        public static void startRecording()
        {
            try
            {
                camera = new Capture();
                camera.Start();
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        public static Image<Bgr, Byte> getFrame()
        {
            return camera.RetrieveBgrFrame();
        }


        

    }
}
