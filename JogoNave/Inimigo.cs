using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JogoNave
{
    class Inimigo : Controle
    {

        public Inimigo(Size limites, Graphics graphics, Point position) : base(limites, graphics)
        {
            Esquerda = position.X;
            Topo = position.Y;
            Velocidade = 5;
            Som = Properties.Resources.exploshion_short;
        }

        public override Bitmap GetSprite()
        {
            return Properties.Resources.inimigo;
        }

        public override void AtualizarObjeto()
        {
            Mover_ParaBaixo();
            base.AtualizarObjeto();
        }
    }
}
