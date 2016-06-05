#include <Adafruit_NeoPixel.h>
#include <SoftwareSerial.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

#define PIN        6
#define NUMPIXELS  26
#define NUMINPUTS  78
#define INPUTWRITE 13
#define FADESPEED 5

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);

SoftwareSerial bt(10,11);
int red,green,blue,red2,green2,blue2;
void setup() {
  bt.begin(9600);
  Serial.begin(9600);
  pixels.begin(); 
  pixels.show();
}

void writePixels(byte r1, byte g1, byte b1)
{
 for(int i=0;i<NUMPIXELS;i++){
    pixels.setPixelColor(i, pixels.Color(r1,b1,g1));
    pixels.show();
  }
}
void loop() {
  while (bt.available() > 0) {
   
   red = bt.parseInt();
   green = bt.parseInt();
   blue = bt.parseInt();
   /*red2 = bt.parseInt();
   green2 = bt.parseInt();
   blue2 = bt.parseInt();*/
   if(bt.read()=='\n')
   { 
     red = constrain(red, 0, 255);
     green = constrain(green, 0, 255);
     blue = constrain(blue, 0, 255);
    
     red2 = constrain(red2, 0, 255);
     green2 = constrain(green2, 0, 255);
     blue2 = constrain(blue2, 0, 255);
  
     // fade the red, green, and blue legs of the LED:
     writePixels(red,green,blue);
  
     // print the three numbers in one string as hexadecimal:
     /*Serial.print("Data Response : ");
     Serial.print(red, HEX);
     Serial.print(green, HEX);
     Serial.println(blue, HEX);*/
   }
   else
    while(bt.read()!='\n');
  }
}


