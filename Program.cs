using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WstecznaPropagacjaZad2
{
    internal class Program
    {
        static public Random randomowa = new Random();
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
            double ParametrUczenia = 0.3;
            double B = 1;
            int liczbaWagNeurona = 3;
            int liczbaNauronowUkrytych = 2;
            int liczbaNauronowWyjsciowych = 2;
            int liczbaWag = (liczbaNauronowUkrytych + liczbaNauronowWyjsciowych) * liczbaWagNeurona;
            double[] WagiSieci = new double[liczbaWag];
            for(int i = 0; i<liczbaWag; i++)
            {
                WagiSieci[i] = (randomowa.NextDouble()*2)-1;
                Console.WriteLine("Waga"+i+" : " + WagiSieci[i]);
            }
            double[][] WejsciaSieci = new double[][]
            {
                new double[]{0, 0},
                new double[]{0, 1},
                new double[]{1, 0},
                new double[]{1, 1},
            };
            double[][] OczekiwaneWyjsciaSieci = new double[][]
            {
                new double[]{0, 1},
                new double[]{1, 0},
                new double[]{1, 0},
                new double[]{0, 1},
            };

            for (int epoki = 0; epoki<=2; epoki++)
            {
                double[][] wyniki = new double[WejsciaSieci.Length][];
                for (int i = 0; i<WejsciaSieci.Length; i++)
                {
                    (double N1, double N2, double N3, double N4) = SiecNeuronowa(WejsciaSieci[i], WagiSieci, liczbaWagNeurona);
                    //Console.WriteLine("Wyjscie sieci 1: "+N3);
                    //Console.WriteLine("Wyjscie sieci 2: " + N4);

                    double[] d = OczekiwaneWyjsciaSieci[i];
                    //double bladwyjscia1 = d - Nwyjsciowy;
                    //Console.WriteLine("Blad wyjsccia1 = "+bladwyjscia1);
                    double PoprawkaWyjscia1 = ParametrUczenia * (d[0] - N3) * (B * N3 * (1 - N3));//pmUczenia*błąd wyjscia * pochodna
                    double PoprawkaWyjscia2 = ParametrUczenia * (d[1] - N4) * (B * N4 * (1 - N4));

                    double PoprawkaNUkryty1 = PoprawkaWyjscia1 * WagiSieci[7] * (B * N1 * (1 - N1));
                    PoprawkaNUkryty1 += PoprawkaWyjscia2 * WagiSieci[11] * (B * N1 * (1 - N1));

                    double PoprawkaNUkryty2 = PoprawkaWyjscia1 * WagiSieci[8] * (B * N2 * (1 - N2));
                    PoprawkaNUkryty2 += PoprawkaWyjscia2 * WagiSieci[10] * (B * N2 * (1 - N2));

                    //Dla wag neurona wyjscia 1 (N3)
                    WagiSieci[6] += PoprawkaWyjscia1;
                    WagiSieci[7] += (PoprawkaWyjscia1 * N1);
                    WagiSieci[8] += (PoprawkaWyjscia1 * N2);

                    //Dla wag neurona wyjscia 2 (N4)
                    WagiSieci[9] += PoprawkaWyjscia1;
                    WagiSieci[11] += (PoprawkaWyjscia1 * N1);
                    WagiSieci[10] += (PoprawkaWyjscia1 * N2);

                    WagiSieci[0] += PoprawkaNUkryty1;
                    WagiSieci[1] += (PoprawkaNUkryty1 * WejsciaSieci[i][0]);
                    WagiSieci[2] += (PoprawkaNUkryty1 * WejsciaSieci[i][1]);

                    WagiSieci[3] += PoprawkaNUkryty2;
                    WagiSieci[4] += PoprawkaNUkryty2 * WejsciaSieci[i][0];
                    WagiSieci[5] += PoprawkaNUkryty2 * WejsciaSieci[i][1];
                }
            }
            Console.ReadKey();
        }
    }
}
