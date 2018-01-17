using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }

        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }

        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;

        public bool xeque { get; private set; }


        public PartidaXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }


        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tabuleiro.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void defazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tabuleiro.retirarPeca(destino);
            p.decrementarQtdMovimentos();

            if (pecaCapturada != null)
            {
                tabuleiro.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tabuleiro.colocarPeca(p, origem);
        }



        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmCheque(jogadorAtual))
            {
                defazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque!");
            }

            if (estaEmCheque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            turno++;
            mudaJogador();

        }


        public void validarPosicaoDeOrigem(Posicao pos)
        {
            tabuleiro.validarPosicao(pos);

            if (tabuleiro.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peca nessa posição!");
            }

            if (jogadorAtual != tabuleiro.peca(pos).cor)
            {
                throw new TabuleiroException("Não é o turno da peça escolhida!");
            }

            if (!tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há possibilidades de movimentos para essa peça!");
            }

        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }


        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }


        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }

            return Cor.Branca;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmCheque(Cor cor)
        {
            Peca R = rei(cor);

            if (R == null)
            {
                throw new Exception("Não tem rei da cor " + cor + " no tabuleiro");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();

                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public void colocarPecas()
        {
            colocarNovaPeca('c', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('f', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tabuleiro, Cor.Preta));

            colocarNovaPeca('b', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('f', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tabuleiro, Cor.Branca));
        }

    }
}
