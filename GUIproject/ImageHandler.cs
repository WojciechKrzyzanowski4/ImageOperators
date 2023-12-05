using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;



namespace GUIproject
{
    internal class ImageHandler
    {
        public ImageHandler() { }

        //handlig the openCvSharp Functions
        public static Mat loadImage(string imagePath)
        {
            //the image in read in with greyscale
            return Cv2.ImRead(imagePath, ImreadModes.Grayscale);
        }

        public static Mat loadImageFull(string imagePath)
        {
            //the image in read in with greyscale
            return Cv2.ImRead(imagePath);
        }

        public static Mat mapDTO(Mat image)
        {
            //passing through the map object 
            return image;
        }

        unsafe public static Mat createMat(int rows, int cols, byte* data)
        {
            //creating OpenCVSharp mat with the same dimensions 
            Mat mat = new Mat(rows, cols, MatType.CV_8UC1, (IntPtr)data);
            return mat;
        }

        //image inversion wrappers
        unsafe public static TimeSpan ASMinvertImageWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();    
            NativeMehtods.invertImage(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();  
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }
        unsafe public static TimeSpan CPPinvertImageWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.inverse(input.Data, output.Data, input.Rows, input.Cols);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }

        //sobelX operator Wrappers
        unsafe public static TimeSpan ASMsobelXOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobelOperatorX(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }

        unsafe public static TimeSpan CPPsobelXOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobelX(input.Data, output.Data, input.Rows, input.Cols);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }

        //sobelY operator Wrappers
        unsafe public static TimeSpan ASMsobelYOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobelOperatorY(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }
        unsafe public static TimeSpan CPPsobelYOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobelY(input.Data, output.Data, input.Rows, input.Cols);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
        }



        //full roberts opertor Wrappers
        unsafe public static TimeSpan CPProbertsOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.roberts(input.Data, output.Data, input.Rows, input.Cols);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;

        }
        unsafe public static TimeSpan ASMrobertsOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.robertsOperator(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;

        }

    }
}
