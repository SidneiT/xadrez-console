using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {

        public static void imprimirPartida(PartidaXadrez partida)
        {
            imprimirTabuleiro(partida.tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Jogador Atual: " + partida.jogadorAtual);

            if (partida.xeque)
            {
                Console.WriteLine("XEQUE!");
            }
        }


        public static void imprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças Capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.Write("Preta: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));

            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x);
            }
            Console.Write("]");
        }


        public static void imprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    imprimirPeca(tabuleiro.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tabuleiro, bool[,] possicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.colunas; j++)
                {
                    if (possicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    imprimirPeca(tabuleiro.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }


        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
            }
        }
    }
}
