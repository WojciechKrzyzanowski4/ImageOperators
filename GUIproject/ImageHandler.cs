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


        public static Mat loadImage(string imagePath)
        {
            return Cv2.ImRead(imagePath, ImreadModes.Color);
        }

        public static Mat convertToGrayscale(Mat image)
        {
            var grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            return grayImage;
        }
        unsafe public static Mat createMat(int rows, int cols, byte* data)
        {

            Mat mat = new Mat(rows, cols, MatType.CV_8UC1, (IntPtr)data);

            return mat;
        }


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
            NativeMehtods.inverse(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;
            
        }


        unsafe public static TimeSpan ASMsobelOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobelOperator(input.Data, output.Data, input.Cols, input.Rows);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;

        }

        unsafe public static TimeSpan CPPsobelOperatorWrapper(Mat input, Mat output)
        {
            Stopwatch sw = Stopwatch.StartNew();
            NativeMehtods.sobel(input.Data, output.Data, input.Rows, input.Cols);
            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;
            return timeSpan;

        }


        unsafe public static Mat sobelXWrapper(Mat input, Mat output)
        {

            NativeMehtods.sobelX(input.Data, output.Data, input.Rows, input.Cols);

            return output;
        }

        unsafe public static Mat sobelYWrapper(Mat input, Mat output)
        {

            NativeMehtods.sobelY(input.Data, output.Data, input.Rows, input.Cols);

            return output;
        }

        unsafe public static Mat robertsWrapper(Mat input, Mat output)
        {

            NativeMehtods.roberts(input.Data, output.Data, input.Rows, input.Cols);

            return output;
        }
    }
}
