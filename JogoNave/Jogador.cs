using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JogoNave
{
    class Jogador : Controle
    {
        List<Controle> ListaDeObjetos;

        public Jogador(Size limites, Graphics graphics, List<Controle> objetos) : base(limites, graphics)
        {
            SetarPosicaoInicial();
            ListaDeObjetos = objetos;
            Velocidade = 10;
            Som = Properties.Resources.explosion_long;
        }

        public void SetarPosicaoInicial()
        {
            Esquerda = Limites.Width / 2 - Largura / 2;
            Topo = Limites.Height - Altura;
        }

        public override Bitmap GetSprite()
        {
            return Properties.Resources.jogador;
        }

        public Controle Atirar()
        {
            Tiro bullet = new Tiro(Limites, Tela, new Point(Esquerda + Largura / 2, Topo - Largura / 2));
            bullet.Esquerda -= bullet.Largura / 2;
            return bullet;
        }

        public override void AtualizarObjeto()
        {
            base.AtualizarObjeto();
        }

        public override void Mover_ParaBaixo()
        {
            if (Topo < Limites.Height - Altura)
                Topo += Velocidade;
        }

        public override void Mover_ParaCima()
        {
            if (Topo > 0)
                Topo -= Velocidade;
        }
    }
    
}
