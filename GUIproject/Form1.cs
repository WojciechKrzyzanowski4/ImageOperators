using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public string outputFileNamePath;
        public string destinationFilePath;

        public int m = 0;
        public Form1()
        {
            InitializeComponent();

        }
        //true means the asm dll is being used to implement the operator functions
        public bool asm;
        //true means the cpp dll is being used to implement the operator functions
        public bool cpp;

        public int bttnsEnebale = 0;


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
                    bttnsEnebale++;
                    if (bttnsEnebale > 1)
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFileNamePath = folderBrowserDialog.SelectedPath;
                    bttnsEnebale++;
                    if (bttnsEnebale > 1)
                    {
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                    }
                }
            }
        }
        //this button handles the Horizontal sobel operator and displays its results
        private void button1_Click(object sender, EventArgs e)
        {
            createOutputImageInFolder();

            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);    
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.mapDTO(mat);
            mat2 = ImageHandler.mapDTO(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMsobelXOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT HORIZONTAL SOBEL FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPPsobelXOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT HORIZONTAL SOBEL FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
        }
      
        //this button handles the vertical sobel operator and displays its results
        private void button2_Click(object sender, EventArgs e)
        {
            createOutputImageInFolder();

            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.mapDTO(mat);
            mat2 = ImageHandler.mapDTO(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMsobelYOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT VERTICAL SOBEL FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPPsobelYOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT VERTICAL SOBEL FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
        }
        //this button handles the roberts operator and displays its results
        private void button3_Click(object sender, EventArgs e)
        {
            createOutputImageInFolder();

            Mat mat0 = ImageHandler.loadImageFull(fileNamePath);
            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.mapDTO(mat);
            mat2 = ImageHandler.mapDTO(mat2);

            if (radioButton1.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.ASMrobertsOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT ROBERTS FROM ASM", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
            if (radioButton2.Checked)
            {
                Cv2.ImShow("INPUT IMAGE", mat0);
                TimeSpan timeSpan = ImageHandler.CPProbertsOperatorWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT ROBERTS FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
        }
        //this button handles the image inversion and displays its results
        private void button4_Click(object sender, EventArgs e){

            createOutputImageInFolder();

            Mat mat = ImageHandler.loadImage(fileNamePath);
            Mat mat2 = ImageHandler.loadImage(fileNamePath);
            mat = ImageHandler.mapDTO(mat);
            mat2 = ImageHandler.mapDTO(mat2);

            if (radioButton1.Checked) {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.ASMinvertImageWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED IMAGE FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
            if (radioButton2.Checked) {
                Cv2.ImShow("INPUT GREYSCALE IMAGE", mat2);
                TimeSpan timeSpan = ImageHandler.CPPinvertImageWrapper(mat, mat2);
                Cv2.ImShow("OUTPUT INVERTED IMAGE FROM CPP", mat2);
                MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
                Cv2.ImWrite(destinationFilePath, mat2);
            }
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //this in not neccessary
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //this is not neccessary
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Showing info Form that 
            FormInfo info = new FormInfo(); 
            info.ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this is not necessary
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this is not necessary
        }

        public void createOutputImageInFolder() {

            m++;

            string sourceFilePath = fileNamePath;
            string destinationFolderPath = outputFileNamePath;
            string name = Path.GetFileName(sourceFilePath);
            string[] split = name.Split('.');
            string nameNew = split[0] + m.ToString() + "." + split[split.Length - 1];
            
            destinationFilePath = Path.Combine(destinationFolderPath, nameNew);

            try
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
    
}
