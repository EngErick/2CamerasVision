# 2CamerasVision

Esse Projeto tem como objetivo criar um Scanner de Impressão 3D, scanneando todos os pontos de um objeto e criando ao final de seu processo um arquivo STL, para impressão.
+ **No entanto este projeto ainda esta em suas fases iniciais, tendo como principais funções detectar o objeto e indentificar a distancia entre a câmera e o mesmo**

## Começando
Neste projeto utilizamos o ESP32 para controlar um motor de passo para girar a base onde o objeto se encontra, a comunicação foi feita via Bluetooth que se comunicava com o computador e este se conectava o modulo da Camera e fazia todos os processos que eram necessarios.


### Pre-Requisitos

Nesse Projeto estamos utilizando 
+  1 ESP32 WROOM 

![ESP32 WROOM](https://user-images.githubusercontent.com/102180418/165987910-1f286904-60ef-468d-951c-4cf2e3d16c87.jpg)
+  1 Motor de Passo (28BYJ-48)

 ![28BYJ-48](https://user-images.githubusercontent.com/102180418/165987954-305cce62-91fd-49f5-8d3e-a18bb11f80fa.jpg)

+  Modulo ULN2003A

![Modulo ULN2003A](https://user-images.githubusercontent.com/102180418/165988073-d259923c-2420-4205-bbe6-46a98b2fc9c0.jpg)

+  Modulo Binocular HBVCAM-1780-2 s1.0

![Modulo Binocular HBVCAM-1780-2 s1 0](https://user-images.githubusercontent.com/102180418/165988231-772a9297-5afb-4335-8be3-d6df4eaea558.jpg)

### Instalação
 + Instalação do Esp32
 
 As portas usadas do ESP32 foram as **19,18,5,17**([ESP32 pinout reference guide](https://randomnerdtutorials.com/esp32-pinout-reference-gpios/)) conectadas respectivamentes na porta **IN1,IN2,IN3,IN4** do modulo ULN2003A
 Abaixo esta descrito o esquematico de ligação do ESP32
 
 ![Squematico-de-Ligação-ESP32](https://user-images.githubusercontent.com/102180418/165989013-d8b7d28f-269c-492a-86b2-5eaf294b979e.png)
 
 +Instalação do Modulo Binocular HBVCAM-1780-2 s1.0
 Para o modulo da Camera Basta fixa-la de forma que esteja linear e de Frente a Area onde o Objeto estara, a camera utiliza um Protocolo de Comunicação UVC com Windows 2000\ Windows XP\Windows 7/ Linux  e Android , não e necessario baixar nenhum driver para a mesma, apenas estar conectada via USB.
 
 Em nosso projeto contruimos uma estrutura circular de madeira e pintamos de preto para facilitar a observação.
 ![Caixa Fechada](https://user-images.githubusercontent.com/102180418/165991247-31c2b02d-caeb-4cf9-9ac6-0110d50ad797.jpeg)![Caixa Aberta](https://user-images.githubusercontent.com/102180418/165991260-72eb5cd8-ec1e-484c-813e-716de045c4fd.jpeg)


A step by step series of examples that tell you how to get a development
environment running

Say what the step will be

    Give the example

And repeat

    until finished

End with an example of getting some data out of the system or using it
for a little demo

## Deployment

Add additional notes to deploy this on a live system

## Built With

  - [Contributor Covenant](https://www.contributor-covenant.org/) - Used
    for the Code of Conduct
  - [Creative Commons](https://creativecommons.org/) - Used to choose
    the license

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code
of conduct, and the process for submitting pull requests to us.

## Versioning

We use [Semantic Versioning](http://semver.org/) for versioning. For the versions
available, see the [tags on this
repository](https://github.com/PurpleBooth/a-good-readme-template/tags).

## Authors

  - **Billie Thompson** - *Provided README Template* -
    [PurpleBooth](https://github.com/PurpleBooth)

See also the list of
[contributors](https://github.com/PurpleBooth/a-good-readme-template/contributors)
who participated in this project.

## License

This project is licensed under the [CC0 1.0 Universal](LICENSE.md)
Creative Commons License - see the [LICENSE.md](LICENSE.md) file for
details

## Acknowledgments

  - Hat tip to anyone whose code is used
  - Inspiration
  - etc
