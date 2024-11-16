#include "DHT.h"

#define DHTTYPE DHT11 
#define DHT11_PIN 6

DHT dht(DHT11_PIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  dht.begin();
}

void loop() {
  delay(2000);

  float humidity = dht.readHumidity();
  float temperature = dht.readTemperature();

  if (isnan(humidity) || isnan(temperature)) {
    Serial.println("Error al leer del sensor DHT11");
    return;
  }

  Serial.print("Humedad: ");
  Serial.print(humidity);
  Serial.print(" %\temperature");
  Serial.print("Temperatura: ");
  Serial.print(temperature);
  Serial.println(" *C");
}
