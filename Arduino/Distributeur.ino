#include <Servo.h>

#define ROTATION_DIRECT 1
#define ROTATION_CLOCK -1
#define SERVO_PIN 9
#define ANGLE 60
#define INIT_ANGLE 89

#define TIMEOUT 10000

Servo servo;
String cmd;
unsigned long time;

void initServo()
{
  servo.write(INIT_ANGLE);
}

void rotateServo(int rotationDirection)
{
  servo.write(INIT_ANGLE + INIT_ANGLE*rotationDirection);
  delay (500);
  servo.write(INIT_ANGLE);
  delay (500);
}

String readLine()
{
  char data;
  String line = "";
  
  Serial.flush();
  delay(100);
  
  while (Serial.available() <= 0){
    delay(10);
  }
  
  time=millis();
  for (int index=0; millis()-time < TIMEOUT; index++){
    if (Serial.available() > 0)
    {
      data = Serial.read();
      if (data == '\n')
      {
        while (Serial.available() > 0){
          Serial.read();
        }
        break;
      }
      line += data;
    }
  }
  return line;
}

void setup()
{ 
  Serial.begin(9600);
  servo.attach(SERVO_PIN);
  initServo();
}

void loop()
{
  //*
  cmd = readLine();
  
  if (cmd == "direct"){
    rotateServo(ROTATION_DIRECT);
  }
  else if (cmd == "clock"){
    rotateServo(ROTATION_CLOCK);
  }
  //*/
}
