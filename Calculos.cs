/* Classe Calculo
 * Esta Classe e a responsavel por receber as coordenadas do Objeto e utilizar essas coordenadas para calcular a distancia e/ou calcular a constante FX da camera
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TwoCamerasVision
{
    public class Calculos
    {
        public float ConstanteCamera { get; set; }
        public float Distancia { get; set; }
        public float X1Meio { get; set; }
        public float X1Esquerda { get; set; }
        public float X1Direita { get; set; }
        public float Y1 { get; set; }
        public float X2Meio { get; set; }
        public float X2Esquerda { get; set; }
        public float X2Direita { get; set; }
        public float Y2 { get; set; }
        public List<float> ListaDistancia { get; set; }

        public Calculos()
        {
            ListaDistancia = new List<float>();
        }

        //Metodo Para calcular a distancia do Objeto a Camera
        public float CalDistancia() 
        {
            //Calculo
            Distancia = ConstanteCamera / (X1Meio - X2Meio);
            return Distancia;
        }

        //Metodo para calcular a Constante FX descrita como "Constante da Camera"
        public float CalConstante(float distancia)
        {
            try
            {
                //Define a constante da camera
                Distancia = distancia;
                //Calcula a Constante em 3 pontos e pega a media
                ConstanteCamera += Distancia * (X1Meio - X2Meio);
                ConstanteCamera += Distancia * (X1Esquerda - X2Esquerda);
                ConstanteCamera += Distancia * (X1Direita - X2Direita);
                ConstanteCamera /= 3;

                return ConstanteCamera;
            }
            catch
            {
                return 0;
            }
            
        }
    }
}
