#define INLENGTH 6
#define INTERMINATOR 13
char inString[INLENGTH+1];
int inCount;

int redLedPin = 9;
int greenLedPin = 10;
int blueLedPin = 11;
int randomCycle = 0;

int red, green, blue;

float RGB1[3];
float RGB2[3];
float INC[3];


void setup() { 

  for (int x=0; x<3; x++) {
    RGB1[x] = random(256);
    RGB2[x] = random(256); 
  }

  Serial.begin(115200); 

  analogWrite(redLedPin, 255);
  analogWrite(greenLedPin, 255);
  analogWrite(blueLedPin, 255);
}

void loop() { 
  inCount = 0;

if (Serial.available()){
  do {
    while (!Serial.available());             // wait for input
    inString[inCount] = Serial.read();       // get it
    if (inString [inCount] == INTERMINATOR){
      Serial.flush(); 
      break;
    }
  } 
  while(++inCount < INLENGTH);

  inString[inCount] = 0;                     // null terminate the string

  Serial.print(inString);
}
  if(strcmp(inString,"ALLOFF") == 0){
    randomCycle = 0;
    red = 0;
    green = 0;
    blue = 0;
  }
  if(strcmp(inString,"WHITE#") == 0){
    red = 255;
    green = 255;
    blue = 255;
    randomCycle = 0;
  }
  if(strcmp(inString,"GREEN#") == 0){
    red = 0;
    green = 255;
    blue = 0;
    randomCycle = 0;    
  }
  if(strcmp(inString,"BLUE##") == 0){
    red = 0;
    green = 0;
    blue = 255;
    randomCycle = 0;    
  }
  if(strcmp(inString,"RED###") == 0){
    red = 255;
    green = 0;
    blue = 0;
    randomCycle = 0;    
  }
  if(strcmp(inString,"PURPLE") == 0){
    red = 127;
    green = 0;
    blue = 255;    
    randomCycle = 0;    
  }  
  if(strcmp(inString,"YELLOW") == 0){
    red = 255;
    green = 255;
    blue = 0;
    randomCycle = 0;    
  }  
  if(strcmp(inString,"ORANGE") == 0){
    red = 255;
    green = 50;
    blue = 0;
    randomCycle = 0;    
  }  
  if(strcmp(inString,"RANDOM") == 0){
    randomCycle = 1;
  }  

  if(randomCycle == 1){
    randomSeed(analogRead(0)+analogRead(1));

    for (int x=0; x<3; x++) {
      INC[x] = (RGB1[x] - RGB2[x]) / 256; 
    }

    for (int x=0; x<256; x++) {

      red = int(RGB1[0]);
      green = int(RGB1[1]);
      blue = int(RGB1[2]);

      // we are sinking current, not sourcing so we need to reverse RGB  
      // we could use MAP but this is more effecient really  
      analogWrite (redLedPin, 255-red);  
      analogWrite (greenLedPin, 255-green);  
      analogWrite (blueLedPin, 255-blue);    

      delay(15);  

      for (int x=0; x<3; x++) {
        RGB1[x] -= INC[x];
      }

    }
    for (int x=0; x<3; x++) {
      RGB2[x] = random(956)-700;
      RGB2[x] = constrain(RGB2[x], 0, 255);
      delay(15);
    }

  }else{
    // we are sinking current, not sourcing so we need to reverse RGB  
    // we could use MAP but this is more effecient really  
    analogWrite(redLedPin, 255-red);
    analogWrite(greenLedPin, 255-green);
    analogWrite(blueLedPin, 255-blue);
  }
}




