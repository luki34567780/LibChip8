#Chip-32 technical reference


## 1. General information

The Chip-32 is a little-endian 32-bit RISC processor.  
It's main goal is to be a chip that is powerfull, yet easy to use and emulate.

### 1.1 Memory

The Chip-32 can address up to 2GB of Memory.  
It's Stack is going from the top down. The maximum stack size is defined by the Emulator ( see 1.1.1 Emulator spec )  

Memory layout:

<pre>
+---------------+= 0x7FFFFFFF (2147483647) End of Chip-32 RAM  
|               |  
|    Display    |  
|               |  
+===============+= 0x7FFFF82F (2147481647) End of program memory
|               |  
|   0x200 to    |  
|   0x7FFFFFFE  |  
|    Chip-32    |  
| Program/Data  |  
|     Space     |  
|               |  
|               |  
|               |  
|               |  
|               |  
|               |  
|               |  
+---------------+= 0x200 (512) Start of Chip-32 programs  
| 0x000 to 0x1FF|  
| Reserved for  |  
| system info   |  
+---------------+= 0x000 (0) Start of Chip-32 RAM  
</pre>
#### 1.1.1 System info

The system info is defined in the first 512 bytes of the Chip-32 memory.
Data types refere to C data types unless specified otherwhise.

| Start |  End  |   Description  | Data type |
|-------|-------|----------------|-----------|
| 0x000 | 0x003 | max stack size | uint32    |
| 0x004 | 0x007 | emulator speed | uint32    |
| 0x008 | 0x00B | screen width   | uint32    |
| 0x00C | 0x00F | screen height  | uint32    |
| 0x010 | 0x011 | screen mode    | byte      |
| 0x010 | 0x1FF | reserved		 | -         |

### 1.2 Registers

Chip-32 has 16 general purpose 32-bit registers, usually referred to as Vx, where x is a hexadecimal digit (0 through F).  
The VF register is used as a flag by some instructions, thus it should be avoided.  

The PC register is 32-bit and is used to store the currently executing address.
It can only be accessed using a special instruction (see 2.1 Instructions)

The SP register is 32-bit and is used to store the current stack pointer.
It can only be accessed using a special instruction (see 2.1 Instructions)

### 1.3 Display

There are currently 2 Screen modes:
 - Text mode:
	- Mapped in Memory (see 1.1 Memory)
	- Ascii, 1 byte per character
	- 25 lines, 80 characters per line
- Image mode:
	- Can be drawn to using Instructions (see 2.1 Instructions)
	- Resolution specified in system info
	- 8 bits per color, 3 channel RGB

Screen modes can be switched using Instructions (see 2.1 Instructions)

### 1.4 Input

The Chip-32 can check what the last pressed key was (see 2.1 Instructions)



## 2. Instruction set

Every instruction is 32-bit long, except some Loads and Stores, which are 64 bit. Allignment isn't required.

Instruction structure:

The first 16 bits are the opcode, and the last 16 bits are the arguments.

The first byte specifies the type of instruction it is (i.E. logic, math. etc.)

shortcuts: 
 - `x` is the register index `Vx`
 - `y` is the register index `Vy`
-  `z` is the register index `Vz`
 - `n` is a 4-bit number
 - `bb` is a 8-bit number
 - `cccc` is a 16-bit number
 - `oooo oooo` is a 32-bit number


### 2.1 Instructions

0001 0000 - **NOP** 

It does nothing but takes a cycle.  
**Watch out, that the last bit is a 1, not a 0**  
This is so you notice if when the Chip is executing empty memory.  

<br><br>
0002 0000 - **CLS**

Clears the screen.  
<br><br>
0100 0000 - **RET**

Pops the stack and sets the `PC` to the popped value.  
<br><br>
0101 000x - **JP address** 

Sets the `PC` to the Value of `Vx`.  
<br><br>
0102 000x - **CALL address**

Pushes the current PC to the stack and sets the `PC` to the Value of `Vx`.  
<br><br>
0103 00xy - **SE Vx, Vy**  

Skips the next instruction if `Vx` equals `Vy`.  
<br><br>
0104 00xy - **SNE Vx, Vy**

Skips the next instruction if `Vx` doesn't equal `Vy`.  
<br><br>
0105 00xy - **JP x + y**

Sets the `PC` to `x + y`.  
<br><br>
0200 000x oooo oooo - **LD Vx, oooo oooo**  
  
Loads the value `oooo oooo` into the register `Vx`.  
  
**Note: This is a 64-bit instruction**  
<br><br>  
0300 00xy - **ADD Vx, Vy**  
  
Adds `Vy` to `Vx` and stores the result in `Vx`.  
<br><br>
0301 00xy - **SUB Vx, Vy**  
  
Subtracts `Vy` from `Vx` and stores the result in `Vx`.  
<br><br>
0302 00xy - **MUL Vx, Vy**  
  
Multiplies `Vx` with `Vy` and stores the result in `Vx`.  
<br><br>
0303 00xy - **DIV Vx, Vy**  
  
Divides `Vx` by `Vy` and stores the result in `Vx`.  
<br><br>
0304 00xy - **MOD Vx, Vy**

Divides `Vx` by `Vy` and stores the remainder in `Vx`.  
<br><br>
0305 00xy - **DIVMOD Vx, Vy**

Divides `Vx` by `Vy` and stores the result in `Vx` and the remainder in `Vy`.  
<br><br>
0400 n0xy - **AND Vx, Vy**

ANDs the `nth` byte of `Vx` with the `nth` byte of `Vy` and stores the result in `Vx`.  
<br><br>
0401 00xy - **AND Vx, Vy**

ANDs the value of `Vx` with the value of `Vy` and stores the result in `Vx`.  
<br><br>
0402 n0xy - **OR Vx, Vy**

ORs the `nth` byte of `Vx` with the `nth` byte of `Vy` and stores the result in `Vx`.  
<br><br>
0403  00xy - **OR Vx, Vy**

ORs the value of `Vx` with the value of `Vy` and stores the result in `Vx`.  
<br><br>
0404  n0xy - **XOR Vx, Vy**

XORs the `nth` byte of `Vx` with the `nth` byte of `Vy` and stores the result in `Vx`.  
<br><br>
0405 00xy - **XOR Vx, Vy**

XORs the value of `Vx` with the value of `Vy` and stores the result in `Vx`.  
<br><br>
0406 00xy - **NOT Vx, Vy**

NOTs the value of `Vy` and stores the result in `Vx`.  
<br><br>
0407 00xy - **SHL Vx, Vy**

Shifts the value of `Vx` left by the value of `Vy` and stores the result in `Vx`.
This shift doesn't wrap around.
<br><br>
040701xy - **SHL Vx, Vy**

Shifts the value of `Vx` left by the value of `Vy` and stores the result in `Vx`.
This shift does wrap arount.  
<br><br>
0408 00xy - **SHR Vx, Vy**

Shifts the value of `Vx` right by the value of `Vy` and stores the result in `Vx`.
This shift doesn't wrap arount.
<br><br>
0408 01xy - **SHR Vx, Vy**

Shifts the value of `Vx` right by the value of `Vy` and stores the result in `Vx`.
This shift does wrap arount.  
<br><br>
0500 000x - **RND Vx**

Generates a random number between 0 and 255 and stores it in `Vx`.  
<br><br>
0600 0xyz - **DRWPX Vx, Vy, Vz**

Draws the pixel specified in `Vz` at the position specified in `Vx` and `Vy`.
The pixel must be a 24 bit RGB value.  

<br><br>
0601 000x - **FILLRECT Vx**

Fills the Rectangle specified in `V0`, `V1`, `V2` and `V3` with the color specified in `Vx`.  
<br><br>
0602 000x - **SCREENMODE Vx**

Sets the screenmode to the value of `Vx`.

Valid values:
 - 0: Text mode
 - 1: Image Mode

<br><br>
0603 0000 - **SHOW**

Shows the current Text

**Note: this is only needed in Text mode**  
<br><br>
0703 000x - **GETPC**

Stores the value of `PC` into `Vx`  
<br><br>
0800 000x - **PUSH Vx**

Pushes the Register `Vx` onto the stack.  
<br><br>
`0801 000x - **POP Vx**

Pops the stack and stores the value in `Vx`.  
<br><br>
0802 0000 **PUSHA**

Pushes Registers `V0` to `VF` onto the stack
0803 0000 **POPA**

Pops the stack and stores the values in `V0` to `VF`.  
<br><br>
0803 000x **GETSB Vx**

Stores the value of the Stackpointer (`SP`) into `Vx`.  
<br><br>
