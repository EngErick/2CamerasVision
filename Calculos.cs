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
        public List<float> listaDistancia { get; set; }

        public Calculos()
        {

        }

        public float calDistancia()
        {
            //Calculo
            Distancia = ConstanteCamera / (X1Meio - X2Meio);
            return Distancia;
        }
        public float calConstante(float distancia)
        {
            try
            {
                Distancia = distancia;
                ConstanteCamera += Distancia * (X1Meio - X2Meio);
                ConstanteCamera += Distancia * (X1Esquerda - X2Esquerda);
                ConstanteCamera += Distancia * (X1Direita - X2Direita);
                ConstanteCamera /= 3;

                return ConstanteCamera;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }
    }
}
