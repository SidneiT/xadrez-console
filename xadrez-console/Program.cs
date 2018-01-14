using System;
using tabuleiro;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tabuleiro = new Tabuleiro(9, 8);

            Tela.imprimirTabuleiro(tabuleiro);

            Console.ReadLine();

        }
    }
}
