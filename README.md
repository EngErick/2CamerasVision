# 2CamerasVision

Esse Projeto tem como objetivo criar um Scanner de Impressão 3D, scanneando todos os pontos de um objeto e criando ao final de seu processo um arquivo STL, para impressão.
+ **No entanto este projeto ainda esta em suas fases iniciais, tendo como principais funções detectar o objeto e indentificar a distancia entre a câmera e o mesmo**

## Começando
Neste projeto utilizamos o ESP32 para controlar um motor de passo para girar a base onde o objeto se encontra, a comunicação foi feita via Bluetooth que se comunicava com o computador e este se conectava o modulo da Camera e fazia todos os processos que eram necessarios.


### Pre-Requisitos

Nesse Projeto estamos utilizando 
+  1 ESP32 WROOM 

![ESP32 WROOM](https://user-images.githubusercontent.com/102180418/165987910-1f286904-60ef-468d-951c-4cf2e3d16c87.jpg)

+  1 Coller Fan 12v 

![Coller Fan 12v](https://user-images.githubusercontent.com/102180418/171273683-7635ad5e-0491-4d16-ba89-3431696e53d0.jpg)

+  Sensor Ultrassônico HC-SR04

![Sensor Ultrasonico](https://user-images.githubusercontent.com/102180418/171274073-f3bd9406-5c67-4970-873c-1a1546eeed5a.png)

+  Potenciometro

![Potenciometro](https://user-images.githubusercontent.com/102180418/171274089-58c0e729-3723-4459-a9dc-9ce2403b3308.jpg)

+  Diodo 1N4001

![1n4001](https://user-images.githubusercontent.com/102180418/171274114-4414477d-b978-4634-98b3-43d4c59fa45d.jpg)

+  TIP31

![tip31c_1](https://user-images.githubusercontent.com/102180418/171274138-742eef81-c4de-43c9-abc7-2d298476bf92.jpg)

+  Optoacoplador 4N35

![4n35](https://user-images.githubusercontent.com/102180418/171274164-90e87a6a-c34d-4514-b2d6-1ddaf95401aa.jpg)



### Instalação
 + Instalação do Esp32
 
 As portas usadas do ESP32 foram as **32,23,22,21**([ESP32 pinout reference guide](https://randomnerdtutorials.com/esp32-pinout-reference-gpios/)) conectadas respectivamentes no **Potenciometro,TriggerPin,EchoPin,Optoacoplador Pin 1** 
 Abaixo esta descrito o esquematico de ligação do ESP32
 
 ![Esquematico Projeto MF](https://user-images.githubusercontent.com/102180418/171274659-9ab780cc-6614-4f9b-93c5-3c691caf1321.png)
 
 Em nosso projeto utilizamos um cano PVC 50mm, uma bola isopor 40 mm, com o Coller com o diametro maior funilamos a entreda de ar e criamos uma estrutura de papelão em formato cruz para melhorar o direcionamento do ar e assim a estabilidade da bola.
 
![WhatsApp Image 2022-05-31 at 16 05 03](https://user-images.githubusercontent.com/102180418/171275627-8d9cfff4-e36b-4307-bc41-e5408f3c087a.jpeg)
![WhatsApp Image 2022-05-31 at 16 05 18](https://user-images.githubusercontent.com/102180418/171275636-297f2613-6255-4904-8cee-d7a2940f49a9.jpeg)
![WhatsApp Image 2022-05-31 at 16 04 54](https://user-images.githubusercontent.com/102180418/171275642-75d3b94c-783f-4dc9-be05-f64418984e25.jpeg)


 

## Autores

 - Erick Silva Barros
 - Matheus Souza Cavalcante

## Agradecimentos

  - Ao nosso Professor Alberto Willian Mascarenhas que nos inspirou nessa ideia e nos ajudou em todo processo de desenvolvimento do projeto
  - 
