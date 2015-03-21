using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.GPU;

namespace FaceDetection
{
    public partial class Ducking_Wookie : Form
    {
        private Capture camera = null;
        private bool recording;

        public Ducking_Wookie()
        {
            InitializeComponent();
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            Width = resolution.Width;
            Height = resolution.Height - 100;
            pictureBox1.Width = Width;
            pictureBox1.Height = Height - 100;
            try
            {
                camera = new Capture();
                camera.ImageGrabbed += process;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }

        }

        private void process (object sender, EventArgs args)
        {
            Image<Bgr, Byte> image = camera.RetrieveBgrFrame();
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            DetectFace.Detect(image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", faces, eyes, out detectionTime);
            foreach (Rectangle face in faces)
                image.Draw(face, new Bgr(Color.Red), 2);
            foreach (Rectangle eye in eyes)
                image.Draw(eye, new Bgr(Color.Blue), 2);
            pictureBox1.Image = image.ToBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (camera != null)
            {
                if (recording)
                {  //stop the capture
                    button1.Text = "Start";
                    camera.Pause();
                }
                else
                {
                    //start the capture
                    button1.Text = "Stop";
                    camera.Start();
                }

                recording = !recording;
            }
            /*CameraCapture.startRecording();
            //while (true)
            //{
                Image<Bgr, Byte> image = CameraCapture.getFrame();
                long detectionTime;
                List<Rectangle> faces = new List<Rectangle>();
                List<Rectangle> eyes = new List<Rectangle>();
                DetectFace.Detect(image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml", faces, eyes, out detectionTime);
                foreach (Rectangle face in faces)
                    image.Draw(face, new Bgr(Color.Red), 2);
                foreach (Rectangle eye in eyes)
                    image.Draw(eye, new Bgr(Color.Blue), 2);

                //display the image 
                pictureBox1.Image = image.ToBitmap();
                //ImageViewer.Show(image, String.Format(
                //   "Completed face and eye detection using {0} in {1} milliseconds",
                //   GpuInvoke.HasCuda ? "GPU" : "CPU",
                //   detectionTime));
            //}*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
