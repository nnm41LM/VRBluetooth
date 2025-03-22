#include <M5Stack.h>
#include "BluetoothSerial.h"

BluetoothSerial bts;

void setup() {
  M5.begin();
  bts.begin("Sample_01");
  M5.Lcd.setTextColor(WHITE);
  M5.Lcd.setTextSize(1);
  M5.Lcd.setTextDatum(4);
}

void loop() {
  int value = random(0, 1000);
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.drawNumber(value, 160, 120, 8);
  bts.println(value);
  delay(1000);
}
