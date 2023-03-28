#include <RotaryEncoder.h>

#define PIN_A   D5 //ky-040 clk pin, interrupt & add 100nF/0.1uF capacitors between pin & ground!!!
#define PIN_B   D6 //ky-040 dt  pin,             add 100nF/0.1uF capacitors between pin & ground!!!
#define BUTTON  D7 //ky-040 sw  pin, interrupt & add 100nF/0.1uF capacitors between pin & ground!!!

int16_t position = 0;

RotaryEncoder encoder(PIN_A, PIN_B, BUTTON);


void ICACHE_RAM_ATTR encoderISR()                                            //interrupt service routines need to be in ram
{
  encoder.readAB();
}

void ICACHE_RAM_ATTR encoderButtonISR()
{
  encoder.readPushButton();
}

void setup()
{
  encoder.begin();                                                           //set encoders pins as input & enable built-in pullup resistors

  attachInterrupt(digitalPinToInterrupt(PIN_A),  encoderISR,       CHANGE);  //call encoderISR()       every high->low or low->high changes
  attachInterrupt(digitalPinToInterrupt(BUTTON), encoderButtonISR, FALLING); //call encoderButtonISR() every high->low              changes

  Serial.begin(115200);
}

void loop()
{
  if (position != encoder.getPosition())
  {
    position = encoder.getPosition();
    Serial.println(position);
  }
  
  if (encoder.getPushButton() == true) 
  {
    Keyboard.press(97)
    Serial.println(F("PRESSED"));       
  }
      //(F()) saves string to flash & keeps dynamic memory free
}