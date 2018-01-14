using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('a', 5);

            Console.Write(pos.toPosicao());

            Console.ReadLine();

        }
    }
}
