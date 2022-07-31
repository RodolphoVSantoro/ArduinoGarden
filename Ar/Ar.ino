int incomingByte = 0;
void setup() {
  Serial.begin(9600);

}
int lerSensor(){
  return 0;
}

void loop() {
  if (Serial.available() > 0)
   {
    incomingByte = Serial.read();
    switch(incomingByte)
    {
      case 48:
        Serial.println("Sair");
        break;
      //48 == "0" na tabela ASCII
      //52 == "4" na tabela ASCII
      case 52:
        Serial.println("Leitura dos sensores");
        lerSensor();
        break;
      default:
        Serial.println("nada");
        break;
    }
        
}

}
