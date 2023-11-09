﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace GUIproject
{
    public class NativeMehtods
    {


        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\CppDll.dll")]
        unsafe public static extern void inverse(IntPtr input, IntPtr output, int input_rows, int input_cols);

        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\CppDll.dll")]
        unsafe public static extern void sobelX(IntPtr input, IntPtr output, int input_rows, int input_cols);

        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\CppDll.dll")]
        unsafe public static extern void sobelY(IntPtr input, IntPtr output, int input_rows, int input_cols);

        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\CppDll.dll")]
        unsafe public static extern void roberts(IntPtr input, IntPtr output, int input_rows, int input_cols);

        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\CppDll.dll")]
        unsafe public static extern void sobel(IntPtr input, IntPtr output, int input_rows, int input_cols);






        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\AsmDll.dll")]
        unsafe public static extern void invertImage(IntPtr input, IntPtr output, int rows, int cols);

        [DllImport("C:\\Users\\Komputer\\source\\repos\\GUIproject\\x64\\Debug\\AsmDll.dll")]
        unsafe public static extern void sobelOperator(IntPtr input, IntPtr output, int rows, int cols);

    }
}