using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIproject
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "The Sobel operator is an edge detection algorithm in image processing. It uses convolution with horizontal and vertical 3x3 kernels to calculate the gradient, highlighting changes in intensity. The combined gradient magnitude is used to identify edges, and thresholding is often applied for better results. It's a simple and effective method for edge detection in computer vision.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "The Roberts Cross operator is an edge detection algorithm in image processing. It uses 2x2 convolution kernels for horizontal and vertical changes to quickly approximate the gradient. The resulting gradient magnitude highlights edges, and thresholding can be applied for edge identification. While computationally efficient, the Roberts operator may be more sensitive to noise compared to other edge detection methods.";
        }
    }
}
