using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Media;

namespace JogoNave
{
    public class Controle
    {
        public bool Ativo;
        public int Velocidade;
        public int Esquerda;
        public int Topo;
        public Bitmap Sprite;
        public Size Limites;
        public Graphics Tela;
        public Rectangle Retangulo;

        public int Largura { get { return this.Sprite.Width; } }
        
        public int Altura { get { return this.Sprite.Height; } }
        public Stream Som;

        private SoundPlayer EmitirSom { get; set; }

        public Controle(Size limite, Graphics graphics)
        {
            Inicializar();

            Limites = limite;
            Tela = graphics;
            Ativo = true;
            EmitirSom = new SoundPlayer();
        }

        public void Inicializar()
        {
            Sprite = GetSprite();
            Retangulo = new Rectangle(Esquerda, Topo, Largura, Altura);

        }

        public virtual Bitmap GetSprite() { return null; }

        public virtual void AtualizarObjeto()
        {
            Retangulo = new Rectangle(Esquerda, Topo, Largura, Altura);
            Tela.DrawImage(Sprite, Retangulo);
        }

        public virtual void Mover_ParaEsquera()
        {
            if (Esquerda > 0)
                Esquerda -= Velocidade;
        }

        public virtual void Mover_ParaDireita()
        {
            if (Esquerda < Limites.Width - Largura)
                Esquerda += Velocidade;
        }
        public virtual void Mover_ParaBaixo()
        {
            Topo += Velocidade;
        }

        public virtual void Mover_ParaCima()
        {
            Topo -= Velocidade;
        }

        public bool Verificar_se_esta_fora_dos_limites()
        {
            return
                (Topo > Limites.Height + Altura) ||
                (Topo < -Altura) ||
                (Esquerda > Limites.Width + Largura) ||
                (Esquerda < -Largura);
        }

        public bool VerificarColisao(Controle objetoJogo)
        {
            if (Retangulo.IntersectsWith(objetoJogo.Retangulo))
            {
                TocarSom();

                return true;
            }
            else return false;
        }
        public void Destruir()
        {
            Ativo = false;
        }
        public void TocarSom()
        {
            EmitirSom.Stream = Som;
            EmitirSom.Play();
        }
    }

}
