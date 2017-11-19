//We always have to include the library
#include "LedControl.h"

/*
 Now we need a LedControl to work with.
 ***** These pin numbers will probably not work with your hardware *****
 pin 12 is connected to the DataIn 
 pin 11 is connected to the CLK 
 pin 10 is connected to LOAD
 We are using two displays
 */
LedControl lc=LedControl(12,11,10,2);
int counter = 0;
int value = 0;
byte buffer[16] = { 
  0 };
int lastvalue = 0;

void setup() {
  /*
   The MAX72XX is in power-saving mode on startup,
   we have to do a wakeup call
   */
  lc.shutdown(0,false);
  lc.shutdown(1,false);
  /* Set the brightness to a medium values */
  lc.setIntensity(0,4);
  lc.setIntensity(1,4);
  /* and clear the display */
  lc.clearDisplay(0);
  lc.clearDisplay(1);
  Serial.begin(115200);
}

//Set's a single column value
//In my case the displays are rotated 90 degrees
//so in the code I'm setting rows instead of colums actualy
void Set(int index, int value)
{
  int device = index / 8; //calculate device
  int row = index - (device * 8); //calculate row
  int leds = map(value, 0, 255, 0, 9); //map value to number of leds.
  //display data
  switch (leds)
  {
  case 0:
    lc.setRow(device,row, 0x00);
    return;
  case 1:
    lc.setRow(device,row, 0x80);
    return;
  case 2:
    lc.setRow(device,row, 0xc0);
    return;
  case 3:
    lc.setRow(device,row, 0xe0);
    return;
  case 4:
    lc.setRow(device,row, 0xf0);
    return;
  case 5:
    lc.setRow(device,row, 0xf8);
    return;
  case 6:
    lc.setRow(device,row, 0xfc);
    return;
  case 7:
    lc.setRow(device,row, 0xfe);
    return;
  case 8:
    lc.setRow(device,row, 0xff);
    return;
  }
}

void loop()
{
  if (Serial.available() >= 15)
  {
    value = Serial.read();
    Set(counter, value);
    counter++;
    if (counter > 15) counter = 0;
  }
}