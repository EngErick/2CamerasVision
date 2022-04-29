#include <Stepper.h>
#include <BluetoothSerial.h>

#if !defined(CONFIG_BT_ENABLED) || !defined(CONFIG_BLUEDROID_ENABLED)
#error Bluetooth is not enabled! Please run `make menuconfig` to and enable it
#endif

BluetoothSerial SerialBT;


const int stepsPerRevolution = 2048;  
const int steps = 86; // revolução= 2048 ; para girar 90º dividimos por 4 

// ULN2003 Motor Driver Pins
#define IN1 19
#define IN2 18
#define IN3 5
#define IN4 17

// initialize the stepper library
Stepper myStepper(stepsPerRevolution, IN1, IN3, IN2, IN4);


void setup() {
  
  // set the speed at 5 rpm
  myStepper.setSpeed(5);

  
  // initialize the serial port
  Serial.begin(115200);
  SerialBT.begin("ESP32CameraMotor"); //Bluetooth device name
  Serial.println("O Dispositivo ja esta pronto para parear");
}

void loop() {
     switch (SerialBT.available() > 0) {
  case true:
    Serial.println("Serial.Available");
    myStepper.step(steps);
    delay(1000);
    Serial.println(SerialBT.read());
    break;
  default:
    break;
    }
  }
