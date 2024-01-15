;-------------------------------------------------------------------------
;ramka


;Autor: Wojciech Krzyzanowski
;Grupa: 1
;Sekcja: 2
;Prowadzacy: mgr inz. Oleg Antemijczuk



;-------------------------------------------------------------------------

.CODE

DllMain PROC hInstDLL:DWORD, reason:DWORD, reserved1:DWORD

mov	eax, 1 	;TRUE
ret

DllMain ENDP

;-------------------------------------------------------------------------


;-------------------------------------------------------------------------


invertImage proc

    ; Input:
    ; rcx: Pointer to the input image
    ; rdx: Pointer to the output image
    ; r8: width of the image in pixels
    ; r9: height of the image in pixels
    
loopRows:
    xor r10, r10      ; init the column counter

loopColumns:
    mov al, [rcx][r10]          ; loading the current pixel from the rdi input data

    not al                      ; inversion of the current pixel

    mov [rdx][r10], al          ; Storing the current inverted pixel in the rdx output data

    inc r10                     ; Incrementig the column counter to move to the next pixel
    cmp r10, r8                 ; Checking if we are at the last pixel in the current row
    jl loopColumns              ; If it is not the last pixel we continue looping through the current row

    add rcx, r8                 ; Moving to the next row in the input data
    add rdx, r8                 ; Moving to the next row in the output data

    dec r9                      ; decrementing the row counter
    jnz loopRows                ; if we are not at the last row already we continue looping through the rows

ret

invertImage endp



sobelOperatorX proc

    ; Input:
    ; rcx: Pointer to the input image
    ; rdx: Pointer to the output image
    ; r8: width of the image in pixels
    ; r9: height of the image in pixels

    ;we will be using this kernel:

    ;;;;;;;;;;;;;
    ; +1 +2 +1  ;
    ;  0  0  0  ;
    ; -1 -2 -1  ;
    ;;;;;;;;;;;;; 

    mov r11, r9
    mov r12, r8
    dec r12

loopRows:
    xor r10, r10                ; init the column counter

loopColumns:
    
    cmp r9, r11                 ;check if we are in the top for 
    jge border                  ;jump to border if we are
    cmp r9, 1                   ;check if we are in the bottom row
    jle border                  ;jump ot border if we are
    cmp r10, 0                  ;check if we are in the first column
    jle border                  ;jump to border if we are
    cmp r10, r12                ;check if we are in the last column
    jge border                  ;jump to border if we are
    jmp notBorder               ;jump to the pixel value calculation portion of the algorithm
    
notBorder:


    xor r13d, r13d              ;preparing the necessary registers
    xor si, si
    xor r14, r14

                                ;according to the kernel ( +1 ) we add this value once
    sub rcx, r8                 ;moving the pointer one line up
    dec rcx                     ;moving the pointer one pixel left
    mov r13b, [rcx][r10]        ;storing the byte value in the result register
    add rcx, r8                 ;moving the pointer one line down
    inc rcx                     ;moving the pointer one pixel right

                                ;according to the kernel ( +2 ) we add this value twice
    sub rcx, r8                 ;moving the pointer one line up
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the byte value in the intermediate register
    add r13w, si                ;adding the value to the result register
    add r13w, si                ;adding the value to the result register
    add rcx, r8                 ;moving the pointer one line down

                                ;according to the kernel ( +1 ) we add this value once
    sub rcx, r8                 ;moving the pointer one line up
    inc rcx                     ;moving the pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the byte value in the intermediate register
    add r13w, si                ;adding the value to the result register
    add rcx, r8                 ;moving the pointer one line down
    dec rcx                     ;moving the pointer one pixel left

                                ;according to the kernel ( -1 ) we subtract this value once
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    add r14, r8                 ;moving the temporary pointer one line down
    dec r14                     ;moving the temporary pointer one pixel left
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register

                                ;according to the kernel ( -2 ) we subtract this value twice
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    add r14, r8                 ;moving the temporary pointer one line down
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register         
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register 

                                ;according to the kernel ( -1 ) we subtract this value once
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    add r14, r8                 ;moving the temporary pointer one line down
    inc r14                     ;moving the temporary pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black    
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register 

    cmp r13w, 255               ;checking if the result is bigger then 255
    jge white                   ;if it is the output color should be white
    jmp finish                  ;if it is not then the output color is the value of the result register

white:
    mov al, 255                 ;white
    mov [rdx][r10], al          ;setting the output pixel color to white
    jmp ending                  ;jumping to the ending

black:
    mov al, 0                   ;black
    mov [rdx][r10], al          ;setting the output pixel color to black
    jmp ending                  ;jumping to the ending

finish:             
    mov [rdx][r10], r13w        ;setting the output pixel color to the calculated value
    jmp ending                  ;jumping to the ending

border:
    mov al, 0                   ;loading the current pixel from the rdi input data
    mov [rdx][r10], al          ;storing the current inverted pixel in the rdx output data
    jmp ending                  ;jumping to the ending 

ending:
    inc r10                     ;incrementig the column counter to move to the next pixel
    cmp r10, r8                 ;checking if we are at the last pixel in the current row
    jl loopColumns              ;if it is not the last pixel we continue looping through the current row

    add rcx, r8                 ;moving to the next row in the input data
    add rdx, r8                 ;moving to the next row in the output data

    dec r9                      ;decrementing the row counter
    jnz loopRows                ;if we are not at the last row already we continue looping through the rows

ret

sobelOperatorX endp

sobelOperatorY proc

    ; Input:
    ; rcx: Pointer to the input image
    ; rdx: Pointer to the output image
    ; r8: width of the image in pixels
    ; r9: height of the image in pixels

    ;we will be using this kernel:

    ;;;;;;;;;;;;;
    ; +1  0 -1  ;
    ; +2  0 -2  ;
    ; +1  0 -1  ;
    ;;;;;;;;;;;;; 

    mov r11, r9
    mov r12, r8
    dec r12

loopRows:
    xor r10, r10                ; init the column counter

loopColumns:
    
    cmp r9, r11                 ;check if we are in the top for 
    jge border                  ;jump to border if we are
    cmp r9, 1                   ;check if we are in the bottom row
    jle border                  ;jump ot border if we are
    cmp r10, 0                  ;check if we are in the first column
    jle border                  ;jump to border if we are
    cmp r10, r12                ;check if we are in the last column
    jge border                  ;jump to border if we are
    jmp notBorder               ;jump to the pixel value calculation portion of the algorithm
    
notBorder:


    xor r13d, r13d              ;preparing the necessary registers
    xor si, si
    xor r14, r14

                                ;according to the kernel ( +1 ) we add this value once
    sub rcx, r8                 ;moving the pointer one line up
    dec rcx                     ;moving the pointer one pixel left
    mov r13b, [rcx][r10]        ;storing the byte value in the result register
    add rcx, r8                 ;moving the pointer one line down
    inc rcx                     ;moving the pointer one pixel right

                                ;according to the kernel ( +2 ) we add this value twice
    dec rcx                     ;moving the pointer one pixel left
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the byte value in the intermediate register
    add r13w, si                ;adding the value to the result register
    add r13w, si                ;adding the value to the result register
    inc rcx                     ;moving the pointer one line down

                                ;according to the kernel ( +1 ) we add this value once
    add rcx, r8                 ;moving the pointer one line down
    dec rcx                     ;moving the pointer one pixel left
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the byte value in the intermediate register
    add r13w, si                ;adding the value to the result register
    sub rcx, r8                 ;moving the pointer one line up
    inc rcx                     ;moving the pointer one pixel right

                                ;according to the kernel ( -1 ) we subtract this value once
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    dec r14                     ;moving the temporary pointer one line up
    inc r14                     ;moving the temporary pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register

                                ;according to the kernel ( -2 ) we subtract this value twice
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    inc r14                     ;moving the temporary pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register         
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register 

                                ;according to the kernel ( -1 ) we subtract this value once
    mov r14, rcx                ;storing the pointer value as not to lose it in case of a jump
    add r14, r8                 ;moving the temporary pointer one line down
    inc r14                     ;moving the temporary pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [r14][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black    
    jle black                   ;jumping if the output color should be black
    sub r13w, si                ;subtracting the value from the result register 

    cmp r13w, 255               ;checking if the result is bigger then 255
    jge white                   ;if it is the output color should be white
    jmp finish                  ;if it is not then the output color is the value of the result register

white:
    mov al, 255                 ;white
    mov [rdx][r10], al          ;setting the output pixel color to white
    jmp ending                  ;jumping to the ending

black:
    mov al, 0                   ;black
    mov [rdx][r10], al          ;setting the output pixel color to black
    jmp ending                  ;jumping to the ending

finish:             
    mov [rdx][r10], r13w        ;setting the output pixel color to the calculated value
    jmp ending                  ;jumping to the ending

border:
    mov al, 0                   ;loading the current pixel from the rdi input data
    mov [rdx][r10], al          ;storing the current inverted pixel in the rdx output data
    jmp ending                  ;jumping to the ending 

ending:
    inc r10                     ;incrementig the column counter to move to the next pixel
    cmp r10, r8                 ;checking if we are at the last pixel in the current row
    jl loopColumns              ;if it is not the last pixel we continue looping through the current row

    add rcx, r8                 ;moving to the next row in the input data
    add rdx, r8                 ;moving to the next row in the output data

    dec r9                      ;decrementing the row counter
    jnz loopRows                ;if we are not at the last row already we continue looping through the rows

ret

sobelOperatorY endp

robertsOperator proc

    ; Input:
    ; rcx: Pointer to the input image
    ; rdx: Pointer to the output image
    ; r8: width of the image in pixels
    ; r9: height of the image in pixels

    ;we will be using this kernel:

    ;;;;;;;;;;
    ; +1  0  ;
    ;  0 -1  ;
    ;;;;;;;;;;

    ;and this kernel:

    ;;;;;;;;;;
    ; -1  0  ;
    ;  0 +1  ;
    ;;;;;;;;;;


    mov r11, r9
    mov r12, r8
    dec r12

loopRows:
    xor r10, r10                ; init the column counter

loopColumns:
    
    cmp r9, r11                 ;check if we are in the top for 
    jge border                  ;jump to border if we are
    cmp r9, 1                   ;check if we are in the bottom row
    jle border                  ;jump ot border if we are
    cmp r10, 0                  ;check if we are in the first column
    jle border                  ;jump to border if we are
    cmp r10, r12                ;check if we are in the last column
    jge border                  ;jump to border if we are
    jmp notBorder               ;jump to the pixel value calculation portion of the algorithm
    
notBorder:


    xor r13d, r13d              ;preparing the necessary registers
    xor r14d, r14d
    xor si, si
    xor r14, r14

                                ;according to the kernel ( +1 ) we add this value once
    mov r13b, [rcx][r10]        ;storing the byte value in the result register
  
                                ;according to the first kernel ( -1 ) we subtract this value once
    mov r15, rcx                ;storing the pointer value as not to lose it in case of a jump
    add r15, r8                 ;moving the temporary pointer one line down
    inc r15                     ;moving the temporary pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [r15][r10]         ;storing the byte value in the intermediate register
    cmp r13w, si                ;chcecking if after the subtraction the color should already be black    
    jle invert1                 ;this changes the sign of the operation making sure we stop an overflow
    sub r13w, si                ;subtracting the value from the result register 

                                ;according to the second kernel ( +1 ) we add this value once
    add rcx, r8                 ;moving the pointer one down
    inc rcx                     ;moving the pointer one pixel right
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the byte value in the intermediate register
    add r14w, si                ;adding the value to the result register
    sub rcx, r8                 ;moving the pointer one line up
    dec rcx                     ;moving the pointer one pixel left

                                ;according to the kernel ( -1 ) we add this value once
    xor si, si                  ;clearing the intermediate register
    mov sil, [rcx][r10]         ;storing the value in the intermediate register
    cmp r14w, si                ;checking if after the subtraction the color should already be black
    jle invert2                 ;this changes the sign of the operation making sure we stop an overflow
    sub r14w, si                ;subtracting the value from the result register

  
    jmp finish                  ;if it is not then the output color is the value of the result register



invert1:
    sub si, r13w                ;inverting the order of operations to get a positive result
    mov r13w, si                ;setting the result register to the new positive value
    jmp finish                  ;jumping to the finish

invert2:
    sub si, r14w                ;inverting the order of operations to get a positive result
    mov r14w, si                ;setting the result register to the new positive value
    jmp finish                  ;jumping to the finish

finish:     

    movzx rax, r13w             ;zero-extend r13w to 64 bits and store in rax
    imul rax, rax               ;square the value in rax

    movzx rbx, r14w             ;zero-extend r14w to 64 bits and store in rbx
    imul rbx, rbx               ;square the value in rbx

    add rax, rbx                ;Add the squared values
    cvtsi2ss xmm1, rax
    sqrtss xmm0, xmm1           ;Assuming rax contains a 32-bit integer, xmm0 will contain the square root

    xor rax, rax                ;clearing the rax register
    cvttss2si rax, xmm0         ;converting fp to integer value

    cmp rax, 255                 ;compare if the value of bigger than 255
    jge white
    mov [rdx][r10], al          ;setting the output pixel color to the calculated value
    jmp ending                  ;jumping to the ending

white:
    mov al, 255                 ;loading 255
    mov [rdx][r10], al          ;stornig the white pixel in the rdx output data
    jmp ending                  ;jumping to the ending

border:
    mov al, 0                   ;loading 0
    mov [rdx][r10], al          ;storing the black pixel in the rdx output data
    jmp ending                  ;jumping to the ending 

ending:
    inc r10                     ;incrementig the column counter to move to the next pixel
    cmp r10, r8                 ;checking if we are at the last pixel in the current row
    jl loopColumns              ;if it is not the last pixel we continue looping through the current row

    add rcx, r8                 ;moving to the next row in the input data
    add rdx, r8                 ;moving to the next row in the output data

    dec r9                      ;decrementing the row counter
    jnz loopRows                ;if we are not at the last row already we continue looping through the rows

ret

robertsOperator endp

ASMtestCall proc

    ret

ASMtestCall endp



.data

   

END 


;-------------------------------------------------------------------------