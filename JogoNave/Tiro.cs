using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoNave
{
    class Tiro : Controle
    {
        public Tiro(Size limites, Graphics graphics, Point position) : base(limites, graphics)
        {
            Topo = position.Y;
            Esquerda = position.X;
            Velocidade = 20;

            Som = Properties.Resources.Missile;
            TocarSom();
        }

        public override Bitmap GetSprite()
        {
            return Properties.Resources.projetil;
        }

        public override void AtualizarObjeto()
        {
            Mover_ParaCima();
            base.AtualizarObjeto();
        }
    }
}
