using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JogoNave
{
    public class Background : Controle
    {
        public Background(Size limites, Graphics g) : base(limites, g)
        {
            Esquerda = 0;
            Topo = 0;
            Velocidade = 0;
        }
        public override Bitmap GetSprite()
        {
            return Properties.Resources.fundo;
        }
    }
}
