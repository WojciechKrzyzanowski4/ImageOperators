#pragma once


#ifdef CPPDLLLIBRARY_EXPORTS
#define CPPDLLLIBRARY_API __declspec(dllexport)
#else
#define CPPDLLLIBRARY_API __declspec(dllimport)
#endif

extern "C" CPPDLLLIBRARY_API void sobel(unsigned char* data_in, unsigned char* data_out, int rows, int cols);

extern "C" CPPDLLLIBRARY_API void roberts(unsigned char* data_in, unsigned char* data_out, int input_rows, int input_cols);

extern "C" CPPDLLLIBRARY_API void sobelX(unsigned char* data_in, unsigned char* data_out, int input_rows, int input_cols);

extern "C" CPPDLLLIBRARY_API void sobelY(unsigned char* data_in, unsigned char* data_out, int input_rows, int input_cols);

extern "C" CPPDLLLIBRARY_API void inverse(unsigned char* data_in, unsigned char* data_out, int input_rows, int input_cols);

extern "C" CPPDLLLIBRARY_API void testCall();
