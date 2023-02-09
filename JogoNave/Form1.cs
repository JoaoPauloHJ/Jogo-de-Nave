using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Input;
using System.Windows.Threading;

//Importante ******* adicionar referencia para assembly: WindowsBase.dll e Presentation.core

namespace JogoNave
{
    public partial class FormPrincipal : Form
    {

        DispatcherTimer TimerJogo;
        DispatcherTimer Temporizador_GeradorInimigo;
        bool JogoAcabou;
        bool podeAtirar;
        Random NumeroAleatorio;
        Graphics Tela;
        Bitmap ImagemFundo;
        Background objetobackground;
        Jogador objetoJogador;
        FimDeJogo objetoFimDeJogo;
        List<Controle> ListaDeObjetos;

        public FormPrincipal()
        {
            InitializeComponent();

            float Largura_EixoX = Screen.PrimaryScreen.WorkingArea.Width / 1920.0F;
            //redimentsionar o tamanho da tela para o tamanho da imagem de fundo
            ClientSize = new Size((int)(775 / Largura_EixoX), (int)(572 / Largura_EixoX));
            ImagemFundo = new Bitmap(Properties.Resources.fundo.Width, Properties.Resources.fundo.Height);
            Tela = Graphics.FromImage(ImagemFundo);

            objetobackground = new Background(ImagemFundo.Size, Tela);
            objetoJogador = new Jogador(ImagemFundo.Size, Tela, ListaDeObjetos);
            objetoFimDeJogo = new FimDeJogo(ImagemFundo.Size, Tela);
            ListaDeObjetos = new List<Controle>();

            IniciarJogo();
        }

        public void IniciarJogo()
        {
            NumeroAleatorio = new Random();
            JogoAcabou = false;

            ListaDeObjetos.Clear();
            ListaDeObjetos.Add(objetobackground);
            ListaDeObjetos.Add(objetoJogador);

            objetoJogador.SetarPosicaoInicial();

            TimerJogo = new DispatcherTimer(DispatcherPriority.Render);
            TimerJogo.Interval = System.TimeSpan.FromMilliseconds(16.666);//10000 ms / 60 quadros = 16.666 -- 60 quadros por segundo
            TimerJogo.Tick += ControlaJogo;
            TimerJogo.Start();

            Temporizador_GeradorInimigo = new DispatcherTimer();
            Temporizador_GeradorInimigo.Interval = TimeSpan.FromMilliseconds(1000);
            Temporizador_GeradorInimigo.Tick += GeradorDeInimigo;
            Temporizador_GeradorInimigo.Start();
        }

        private void GeradorDeInimigo(object sender, EventArgs e)
        {
            Point PosicaoDoInimigo = new Point(NumeroAleatorio.Next(2, ImagemFundo.Size.Width - 66), -62);

            Inimigo objetoInimigo = new Inimigo(ImagemFundo.Size, Tela, PosicaoDoInimigo);

            this.ListaDeObjetos.Add(objetoInimigo);
        }

        private void ControlaJogo(object sender, EventArgs e)
        {
            if (JogoAcabou)
            {
                FinalizarJogo();
                return;
            }

            ListaDeObjetos.RemoveAll(x => !x.Ativo);

            LeituraTeclado();

            foreach (Controle objetoA in this.ListaDeObjetos)
            {
                objetoA.AtualizarObjeto();

                if (objetoA.Verificar_se_esta_fora_dos_limites())
                {
                    objetoA.Destruir();
                    continue; // nao precisa testar mais nada! vá para a proxima interecao
                }

                if ( objetoA is Inimigo)
                {
                    if (JogoAcabou) return;

                    if(objetoA.VerificarColisao(objetoJogador))
                    {
                        JogoAcabou = true;
                        return;
                    }

                    foreach (Controle objetoB in this.ListaDeObjetos.Where(x => x is Tiro))
                    {
                        if (objetoA.VerificarColisao(objetoB))
                        {
                            objetoA.Destruir();
                            objetoB.Destruir();
                        }
                    }
                }
            }

            this.Invalidate(); //atualizar a tela
        }

        private void FinalizarJogo()
        {
            ListaDeObjetos.RemoveAll(x => !(x is Background));
            TimerJogo.Stop();
            Temporizador_GeradorInimigo.Stop();
            objetobackground.AtualizarObjeto();
            objetoFimDeJogo.AtualizarObjeto();

            Invalidate();// atualizar tela
        }

        private void LeituraTeclado()
        {
            if (Keyboard.IsKeyDown(Key.Left))
                objetoJogador.Mover_ParaEsquera();

            if (Keyboard.IsKeyDown(Key.Right))
                objetoJogador.Mover_ParaDireita();

            if (Keyboard.IsKeyDown(Key.Up))
                objetoJogador.Mover_ParaCima();

            if (Keyboard.IsKeyDown(Key.Down))
                objetoJogador.Mover_ParaBaixo();

            if (podeAtirar && Keyboard.IsKeyDown(Key.Space))
            {
                this.ListaDeObjetos.Add(objetoJogador.Atirar());
                this.podeAtirar = false;
            }
            if (Keyboard.IsKeyUp(Key.Space))
            {
                this.podeAtirar = true;
            }
        }

        private void FormPrincipal_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void FormPrincipal_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.DrawImage(ImagemFundo, 0, 0);
        }
    }
}
