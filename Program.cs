using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WstecznaPropagacjaZad2
{
    internal class Program
    {
        public static (double, double, double,double) SiecNeuronowa(double[] wejscie, double[] Wagi, int LiczbaWagNeurona)
        {
            double[] neurony = new double[Wagi.Length / LiczbaWagNeurona];
            int y = 0;
            for (int i = 0; i < neurony.Length; i++)
            {
                double[] PmTymczasowe = new double[LiczbaWagNeurona];
                for (int j = 0; j < LiczbaWagNeurona; j++)
                {
                    PmTymczasowe[j] = Wagi[y];
                    y++;
                }
                if (i <= 1)
                {
                    neurony[i] = FAktywacji(Neuron(PmTymczasowe, wejscie));
                }
                else
                {
                    double[] WejścieWarstwyWyjsciowej = {
                        neurony[0],
                        neurony[1]
                    };
                    neurony[i] = FAktywacji(Neuron(PmTymczasowe, WejścieWarstwyWyjsciowej));
                }

            }
            return (neurony[0], neurony[1], neurony[2], neurony[3]);
        }
        public static double FAktywacji(double WartNeurona, double B = 1.0)
        {
            return 1.0 / (1.0 + Math.Exp(-B * WartNeurona));
        }

        public static double Neuron(double[] Wagi, double[] wejscie)
        {
            double neuron = 0;
            neuron += Wagi[0];
            for (int j = 1; j <= wejscie.Length; j++)
            {
                neuron += Wagi[j] * wejscie[j - 1];
            }
            return neuron;
        }
        static void Main(string[] args)
        {

        }
    }
}
