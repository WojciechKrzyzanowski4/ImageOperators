#include "pch.h"
#include "CppDll.h"
#include <vector>
#include <cmath>






void roberts(unsigned char* data_in, unsigned char* data_out, int rows, int cols) {
    // Define the Roberts Cross kernels
    std::vector<int> kernel_x({ 1, 0, 0, -1 });
    std::vector<int> kernel_y({ 0, 1, -1, 0 });

    //set the kernel size
    int kernel_size = 2;

    //loop through each pixel in the input matrix
    for (int row = 0; row < rows; ++row) {
        for (int col = 0; col < cols; col += 1) {
            // Set the output value to black for pixels near the border
            if (row >= rows - 1 || col >= cols - 1) {
                data_out[cols * row + col] = 0;
                continue;
            }

            int sum_x = 0, sum_y = 0;
            int k_ind = 0;

            //iterate through the 2x2 Roberts Cross kernel
            for (int k_row = 0; k_row < kernel_size; ++k_row) {
                for (int k_col = 0; k_col < kernel_size; ++k_col) {
                    sum_x += kernel_x[k_ind] * data_in[cols * (row + k_row) + col + k_col];
                    sum_y += kernel_y[k_ind] * data_in[cols * (row + k_row) + col + k_col];
                    k_ind++;
                }
            }

            //calculate the gradient magnitude
            int G = unsigned(std::sqrt(sum_x * sum_x + sum_y * sum_y));
            //sine the root is always positive we only need the min function
            data_out[cols * row + col] = min(G, 255);
        }
    }
}

void sobelX(unsigned char* data_in, unsigned char* data_out, int rows, int cols) {

    std::vector<int> kernel_x({ 1,2,1,0,0,0,-1,-2,-1 });
    int kernel_size = 3;

    for (int row = 0; row < rows; row++) {
        for (int col = 0; col < cols; col += 1) {

            if (row <= kernel_size / 2 || row >= rows - kernel_size / 2 ||
                col <= kernel_size / 2 || col >= cols - kernel_size / 2) {
                data_out[cols * row + col] = 0;
                continue;
            }
            int sum = 0;
            int k_ind = 0;
            for (int k_row = -kernel_size / 2; k_row <= kernel_size / 2; ++k_row) {
                for (int k_col = -kernel_size / 2; k_col <= kernel_size / 2; ++k_col) {
                    sum += kernel_x[k_ind] * data_in[cols * (row + k_row) + col + k_col];
                    k_ind++;
                }
            }
            data_out[cols * row + col] = max(min(sum, 255), 0);
        }
    }
}

void sobelY(unsigned char* data_in, unsigned char* data_out, int rows, int cols) {

    std::vector<int> kernel_y({ 1,0,-1,2,0,-2,1,0,-1 });
    int kernel_size = 3;

    for (int row = 0; row < rows; row++) {
        for (int col = 0; col < cols; col += 1) {

            if (row <= kernel_size / 2 || row >= rows - kernel_size / 2 ||
                col <= kernel_size / 2 || col >= cols - kernel_size / 2) {
                data_out[cols * row + col] = 0;
                continue;
            }
            int sum = 0;
            int k_ind = 0;
            for (int k_row = -kernel_size / 2; k_row <= kernel_size / 2; ++k_row) {
                for (int k_col = -kernel_size / 2; k_col <= kernel_size / 2; ++k_col) {
                    sum += kernel_y[k_ind] * data_in[cols * (row + k_row) + col + k_col];
                    k_ind++;
                }
            }
            data_out[cols * row + col] = max(min(sum, 255), 0);
        }
    }
}

void inverse(unsigned char* data_in, unsigned char* data_out, int input_rows, int input_cols) {
    for (int row = 0; row < input_rows; row++) {
        for (int col = 0; col < input_cols; col++) {
            data_out[input_cols * row + col] = 255 - data_in[input_cols * row + col];
        }
    }
}

void testCall() {

}