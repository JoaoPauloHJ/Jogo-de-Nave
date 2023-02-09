using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JogoNave
{
    public class FimDeJogo : Controle
    {

        public FimDeJogo(Size s, Graphics g) : base(s, g)
        {
            Esquerda = Limites.Width / 2 - Largura / 2;
            Velocidade = 0;
        }

        public override Bitmap GetSprite()
        {
            return Properties.Resources.GameOver;
        }
    }
}
