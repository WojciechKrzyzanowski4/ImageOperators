using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace GUIproject
{
    public partial class Form1 : Form
    {

        public string fileNamePath;
        public Form1()
        {
            InitializeComponent();

        }

        public bool asm;

        public bool cpp;


        private void chooseInputImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Open Image File";

                openFileDialog.ValidateNames = true; // This prevents users from selecting folders

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileNamePath = openFileDialog.FileName;

                    button1.Enabled = true; 
                    button2.Enabled = true; 
                    button3.Enabled = true; 
                    button4.Enabled = true;
                    button5.Enabled = true;

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);    
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMsobelXOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT HORIZONTAL SOBEL FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPPsobelXOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT HORIZONTAL SOBEL FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMsobelYOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT VERTICAL SOBEL FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPPsobelYOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT VERTICAL SOBEL FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMrobertsOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT ROBERTS FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPProbertsOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT ROBERTS FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e){

            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked) {
                //to do
            }
            if (radioButton2.Checked) {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.CPPinvertImageWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED IMAGE FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked)
            {
                //to do
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.CPPsobelOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED GREYSCALE IMAGE FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
 
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInfo info = new FormInfo(); 
            info.ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
