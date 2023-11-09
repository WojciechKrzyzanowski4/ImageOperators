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
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);
            Mat sobelX = ImageHandler.sobelXWrapper(mat, mat2);
            Cv2.ImShow("OUTPUT IMAGE FROM THE SOBEL-X FILTER", sobelX);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);
            Mat sobelY = ImageHandler.sobelYWrapper(mat, mat2);
            Cv2.ImShow("OUTPUT IMAGE FROM THE SOBEL-Y FILTER", sobelY);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);
            Mat roberts = ImageHandler.robertsWrapper(mat, mat2);
            Cv2.ImShow("OUTPUT IMAGE FROM THE ROBERTS FILTER", roberts);
        }

        private void button4_Click(object sender, EventArgs e){

            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.convertToGrayscale(mat);
            mat2 = ImageHandler.convertToGrayscale(mat2);

            if (radioButton1.Checked) {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.ASMinvertImageWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED GREYSCALE IMAGE FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
            }
            if (radioButton2.Checked) {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.CPPinvertImageWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED GREYSCALE IMAGE FROM CPP", mat2);
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
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.ASMsobelOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED GREYSCALE IMAGE FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
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


    }
}
