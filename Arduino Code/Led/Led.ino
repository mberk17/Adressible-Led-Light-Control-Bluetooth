#include <Adafruit_NeoPixel.h>
#include <SoftwareSerial.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

#define PIN        6
#define NUMPIXELS  26
#define INPUTWRITE 13
#define BYTESPERMESSAGE 42
int TOTALBYTES = NUMPIXELS*3;

struct RGB{
  byte r;
  byte g;
  byte b;
};

Adafruit_NeoPixel pixels = Adafruit_NeoPixel(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
int numberOfMessage, curMessageNumber, multiplier;
RGB ledInfo[NUMPIXELS], temp;
int numberOfBytesRead = 0;
int red,green,blue;

SoftwareSerial bt(10,11);

void setup() {
  numberOfMessage = (TOTALBYTES-1) / BYTESPERMESSAGE + 1;
  curMessageNumber = 1;
  bt.begin(9600);
  pixels.begin(); 
  pixels.show();
}

void writePixels()
{
 for(int i = 0; i < NUMPIXELS; i++){
    pixels.setPixelColor(i, ledInfo[i].r, ledInfo[i].b, ledInfo[i].g);
    pixels.show();
  }
}
void loop() {
  while (bt.available() > 2) {
    
   temp.r = (int)bt.read();
   temp.g = (int)bt.read();
   temp.b = (int)bt.read();
   ledInfo[numberOfBytesRead / 3] = temp;  
   numberOfBytesRead+=3;
   if(numberOfBytesRead == TOTALBYTES)
   {
      numberOfBytesRead = 0;
      writePixels();
      bt.write(numberOfMessage);
      curMessageNumber = 1;
   }
   else
    if(numberOfBytesRead % BYTESPERMESSAGE == 0)
    {
      bt.write(curMessageNumber);
      curMessageNumber++;
    }
  }
}


