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






sobelOperator2 proc

    ;Input:
    ;rcx: Pointer to the input image data
    ;rdx: Pointer to the ouput image data
    ;r8: Width of the image in pixels
    ;r9: Height of the image in pixels

    ;Defining the sobel kernels for the sobel X operator
    ;xmm1

    ;r8b r9b r10b r11b r12b r13b r14b r15b 

    ;r8w r9w r10w r11w r12w r13w r14w r15w


    push rbp    ;temp value for finding the indexes

    xor r11, r11        ;row counter

loopRows:
    xor r10, r10        ;column counter
    

loopColumns:

    cmp r10, 0
    jl skip
    dec r8
    cmp r10, r8
    inc r8
    jge skip
    cmp r11, 0
    jge skip
    dec r9
    cmp r11, r9 
    inc r9
    jl skip


    sub rcx, r8
    dec rcx
    mov r8d, [rcx][r10]         ;r10 - r8 - 1
    add rcx, r8
    inc rcx

    sub rcx, r8           
    mov r9d, [rcx][r10]         ;r10 - r8
    add rcx, r8

    sub rcx, r8
    inc rcx
    mov r10d, [rcx][r10]        ;r10 - r8 + 1
    add rcx, r8
    dec rcx

    add rcx, r8
    dec rcx
    mov r11d, [rcx][r10]        ;r10 + r8 - 1
    sub rcx, r8
    inc rcx

    add rcx, r8         
    mov r12d, [rcx][r10]        ;r10 + r8
    sub rcx, r8  

    add rcx, r8
    inc rcx
    mov r13d, [rcx][r10]        ;r10 + r8 + 1
    sub rcx, r8
    dec rcx

    mov eax, r8d
    mov ebx, -1
    mul ebx
    mov r8d, eax

    mov eax, r9d
    mov ebx, -2
    mul ebx
    mov r9d, eax

    mov eax, r10d
    mov ebx, -1
    mul ebx
    mov r10d, eax

    mov eax, r11d
    mov ebx, 1
    mul ebx
    mov r11d, eax

    mov eax, r12d
    mov ebx, 2
    mul ebx
    mov r12d, eax

    mov eax, r13d
    mov ebx, 1
    mul ebx
    mov r13d, eax
   
    add r8d, r9d
    add r8d, r10d
    add r8d, r11d
    add r8d, r12d
    add r8d, r13d

    cmp r8d, 255
    jge white
    cmp r8d, 0
    jle black
    jmp finish

white:
    mov al, 255
    mov [rdx][r10], al
    jmp skipEnd

black:
    mov al, 0
    mov [rdx][r10], al
    jmp skipEnd

finish:
    mov [rdx][r10], r8w
    jmp skipEnd

skip:
    mov al, 0
    mov [rdx][r10], al
    jmp skipEnd


skipEnd:
    inc r10         ;increment the column coutner
    cmp r10, r8     ;checking if we are at the end of the row
    jl loopColumns  ;If not, continue looping through the current row

    inc r11         ;increment the row counter
    add rcx, r8     ;move to the next section in the input data
    add rdx, r8     ;move to the next section in the output data

    cmp r11, r9
    jl loopRows

    pop rbp
ret



sobelOperator2 endp





sobelOperator proc

    ; Input:
    ; rcx: Pointer to the input image
    ; rdx: Pointer to the output image
    ; r8: width of the image in pixels
    ; r9: height of the image in pixels

    mov r11, r9
    mov r12, r8
    dec r12

 
    
loopRows:
    xor r10, r10      ; init the column counter

loopColumns:
    
    cmp r9, r11
    jge end2
    cmp r9, 1 
    jle end2
    cmp r10, 0
    jle end2
    cmp r10, r12
    jge end2
    jmp end1
    
end1:


    xor r13d, r13d

    sub rcx, r8
    dec rcx
    mov r13d, [rcx][r10]        ;r10 - r8 - 1
    add rcx, r8
    inc rcx

    sub rcx, r8         
    add r13d, [rcx][r10]        ;r10 - r8
    add r13d, [rcx][r10]        ;r10 - r8
    add rcx, r8  

    sub rcx, r8
    inc rcx
    add r13d, [rcx][r10]        ;r10 - r8 + 1
    add rcx, r8
    dec rcx

    mov r14, rcx
    add r14, r8
    dec r14
    cmp r13d, [r14][r10]
    jle black
    sub r13d, [r14][r10]        ;r10 + r8 - 1

    mov r14, rcx
    add r14, r8
    cmp r13d, [r14][r10]
    jle black
    sub r13d, [r14][r10]        ;r10 + r8
    cmp r13d, [r14][r10]
    jle black
    sub r13d, [r14][r10]        ;r10 + r8

    mov r14, rcx
    add r14, r8
    inc r14
    cmp r13d, [r14][r10]
    jle black
    sub r13d, [r14][r10]        ;r10 + r8 + 1

    cmp r13d, 1200
    jge white
    jmp finish

white:
    mov al, 255
    mov [rdx][r10], al
    jmp ending

black:
    mov al, 0
    mov [rdx][r10], al
    jmp ending

finish:
    mov al, 0
    mov [rdx][r10], al
    jmp ending


end2:
    mov al, 0                   ; loading the current pixel from the rdi input data
    mov [rdx][r10], al          ; Storing the current inverted pixel in the rdx output data
    jmp ending

ending:
    inc r10                     ; Incrementig the column counter to move to the next pixel
    cmp r10, r8                 ; Checking if we are at the last pixel in the current row
    jl loopColumns              ; If it is not the last pixel we continue looping through the current row

    add rcx, r8                 ; Moving to the next row in the input data
    add rdx, r8                 ; Moving to the next row in the output data

    dec r9                      ; decrementing the row counter
    jnz loopRows                ; if we are not at the last row already we continue looping through the rows

ret



sobelOperator endp





.data
    pi dq 3.141592653589793238      ; Define the value of pi in the data section
    degree dq 180.0                 ; Define degree as a double and initialize it to 180.0
   

 

   
   

   

END 


;-------------------------------------------------------------------------