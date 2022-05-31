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
 
 + Instalação do Modulo Binocular HBVCAM-1780-2 s1.0
 
 Para o modulo da Camera Basta fixa-la de forma que esteja linear e de Frente a Area onde o Objeto estara, a camera utiliza um Protocolo de Comunicação UVC com Windows 2000\ Windows XP\Windows 7/ Linux  e Android , não e necessario baixar nenhum driver para a mesma, apenas estar conectada via USB.
 
 Em nosso projeto contruimos uma estrutura circular de madeira e pintamos de preto para facilitar a observação.
 ![Caixa Fechada](https://user-images.githubusercontent.com/102180418/165991247-31c2b02d-caeb-4cf9-9ac6-0110d50ad797.jpeg)![Caixa Aberta](https://user-images.githubusercontent.com/102180418/165991260-72eb5cd8-ec1e-484c-813e-716de045c4fd.jpeg)

 +Observações sobre o Codigo Fonte
 
 Nosso codigo teve como base de um exemplo de esterioscopia utilizando duas Webcam, porem quando fomos utilizar o modulo binocular varias alterações tiveram que ser feitas.
 A mais importante e significativa era sobre o tratamento que era dado ao processamento da imagem. O Modulo HBVCAM nos envia uma unica imagem inteira contendo a imagem das duas cameras por tal razão tivemos que "dividir" a imagem em duas para facilitar o processo de binarização e consequentemente detecção do Objeto
 
 ![image](https://user-images.githubusercontent.com/102180418/166150936-da77c915-2080-4c7b-91e0-9f0c44053294.png)

 
Codigo da divisão da imagem

    //divide a imagem da camera em duas para o processamento
    BitmapData objectData1 = objectImage1.LockBits(new Rectangle(0, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
    BitmapData objectData2 = objectImage2.LockBits(new Rectangle(image.Width / 2, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

Contudo mesmo dividindo a imagem em duas ainda temos o problema com as coordenadas em pixel do objeto que ainda continham o tamanho total da imagem.Fizemos o mesmo tipo de processamento para adquiri-las
![image](https://user-images.githubusercontent.com/102180418/166151011-8f6da1f3-2f12-4e27-ad8e-6cc652090d7e.png)

O Objeto da direita carrega o valor em vermelho e não o de amarelo por isso tivemos que subtrair metada de imagem

Codigo do tratamento do objeto

    if (objectRect.Left < (metadeImagem) && objectRect.Right < (metadeImagem)) // Caso as coordenadas sejam do objeto a esquerda
                    {

                        x1e = objectRect.Left;
                        x1d = objectRect.Right;
                        x1m = (x1e + x1d) / 2;
                        y1m = (objectImage.Height - (objectRect.Top + objectRect.Bottom)) / 2;
                    }
                    //Caso o Objeto tenha coordenadas mistas(Ambas a direita e esqueda da imagem)
                    else if ((objectRect.Left >= (metadeImagem) && objectRect.Right < (metadeImagem)) || (objectRect.Left < (metadeImagem) && objectRect.Right >= (metadeImagem))) 
                    {

                    }
                    else //Caso as coordenadas sejam do objeto a direita 
                    {
                        x1e = (objectRect.Left - metadeImagem);
                        x1d = (objectRect.Right - metadeImagem);

                        x1m = (x1e + x1d) / 2;
                        y1m = (objectImage.Height - (objectRect.Top + objectRect.Bottom)) / 2;

                    }


Definido as coordenadas do objeto em pixel podemos agora partir para o calculo da mesma. Para calcular a distancia de um objeto ate as cameras se faz uso da seguinte equação:

![image](https://user-images.githubusercontent.com/102180418/166150390-e906e3b3-816d-466a-8532-7bb34f811f29.png)

Contudo O fator fX depende de duas características que se mantem fixas portanto para podermos adquirir esse fator e necessario no primeiro momento em que iniciarmos o programa setar o objeto em uma distancia conhecida para adquirirmos esse fator 

![image](https://user-images.githubusercontent.com/102180418/166150643-8cdc8c1d-5e26-48f9-8893-5e1c2c1a48e7.png)

e assim quando confirmarmos esse valor nosso programa ira calcular e guardar esse fator para futuros calculos da imagem

![image](https://user-images.githubusercontent.com/102180418/166150835-6100ed89-1cce-437a-9905-c6acdf081307.png)

## Referencias
Nesse Projeto nos utilizamos as bibliotecas do AForge bem como um de seus exemplos de visão estéreo e detecção objeto utilizando 2 Webcams para saber mais deixaremos o link de accesso:http://www.aforgenet.com/articles/step_to_stereo_vision/

Para o calculo da visão estereoscópica utilizamos o projeto "Estereoscopia no cálculo de distância e controle de 
plataforma robótica"  tambem deixaremos o link de acesso :http://www.decom.ufop.br/sibgrapi2012/eproceedings/wuw/102815_1.pdf

## Autores

 - Erick Silva Barros
 - Matheus Souza Cavalcante

## Agradecimentos

  - Ao nosso Professor Alberto Willian Mascarenhas que nos inspirou nessa ideia e nos ajudou em todo processo de desenvolvimento do projeto
  - 

