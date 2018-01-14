using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 5));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(6, 3));

                tab.colocarPeca(new Torre(tab, Cor.Branca), new Posicao(1, 6));
                tab.colocarPeca(new Torre(tab, Cor.Branca), new Posicao(6, 6));

                Tela.imprimirTabuleiro(tab);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
