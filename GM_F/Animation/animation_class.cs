using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using Raylib_cs;
namespace GM_F; // faz parte de "GM_F"

static public partial class Animations
{
    public class Animation
    {
    // ############### Variáveis 
            public class Instance {
                public Vector2 possition_true;
                public Color cor_true;
                public int alpha_true;
                public float angle_true;
                public Vector2 size_true;

                public Instance(Vector2 _possition,Color _cor, int _alpha, float _angle, Vector2 _size) // constructor
                {
                this.possition_true = _possition;
                this.cor_true = _cor;
                this.alpha_true = _alpha;
                this.angle_true = _angle;
                this.size_true = _size;
                }
            }
            public class Animator {
                public  Vector2[] possition_timer;
                public  Color[] cor_timer; 
                public  int[] alpha_timer;
                public  float[] angle_timer;
                public  Vector2[] size_timer;

                public Animator(int _size_of_arrays) // constructor que define o tamanho das arrays
                {
                possition_timer = new Vector2[_size_of_arrays];
                cor_timer = new Color[_size_of_arrays];
                alpha_timer = new int[_size_of_arrays];
                angle_timer = new float[_size_of_arrays];
                size_timer = new Vector2[_size_of_arrays];
                }
            }

        public Instance instancia; // obj com as infromações da instancia verdadeira
        public Animator animacao; // obj que contem as informações da animação
        public Texture2D textura = new Texture2D(); // guarda a textura que será desenhada
        public Vector2 pivot = new Vector2();
        public int animacao_iniciar_timer = 0; // tempo para a animação iniciar 
        public bool animacao_terminou = false; // se a animação terminou 
        public int frame; // quantidade de frames que a animação possui
        public int frame_timer = 0; // freme atual da animação 
    // ###############

    // ############### Constructor
        
        public Animation(Vector2 _possition_true,Color _cor_true,int _alpha_true,float _angle_true ,Vector2 _size_true,int _frame,Texture2D _textura, Vector2 _pivot, int _animacao_iniciar_timer)
        {
        this.frame = _frame;
        this.textura = _textura;
        this.pivot = _pivot;
        this.animacao_iniciar_timer = _animacao_iniciar_timer;

        instancia = new Instance(_possition_true,_cor_true,_alpha_true,_angle_true,_size_true); // criando obj da class "Instance"

        animacao = new Animator(_frame); // criando obj da class "Animator"
        this.animacao.possition_timer = new Vector2[_frame]; // definindo tamnho das arrays do objeto "animacao"
        this.animacao.cor_timer = new Color[_frame];
        this.animacao.alpha_timer = new int[_frame];
        this.animacao.angle_timer = new float[_frame];
        this.animacao.size_timer = new Vector2[_frame];
        }
        
    // ###############
    
    // ############### Funções 
        public void frame_next() // aumenta o valor de frame pra mais um
        {
            if (animacao_iniciar_timer <= 0)
            {
                if (frame_timer != frame-1)
                {
                frame_timer += 1;              
                }
                else
                {
                animacao_finished(); // terminou a animação
                }
            }
            else
            {animacao_iniciar_timer -=1;}
        }
        public void animacao_finished()
        {
        animacao_terminou = true; // terminou a animação
        frame_timer = frame;    
        }
        public void animation_draw() // desenhar usando os dados do obj "animacao"
        {
        //Console.WriteLine(animacao.alpha_timer[frame+1]);
            try // tenta fazer "array[frame_timer]"
            {
            var ani_angle = animacao.angle_timer[frame_timer]; // valores de cada array 
            var ani_possition = animacao.possition_timer[frame_timer];
            var ani_alpha = animacao.alpha_timer[frame_timer];
            var ani_cor = animacao.cor_timer[frame_timer];
            var ani_size = animacao.size_timer[frame_timer];


            var source = new Rectangle(0,0,textura.Width,textura.Height); // sprite que será desenhado
            var dest = new Rectangle(instancia.possition_true,instancia.size_true); // tamanho e posição do retangulo
            //var dest = new Rectangle(ani_possition,ani_size);
                if (animacao_iniciar_timer <=0) // se "animacao_iniciar_timer" existir
                {
                Raylib.DrawTexturePro(textura,source,dest,pivot,ani_angle,instancia.cor_true); 
                //Raylib.DrawTexturePro(textura,source,dest,pivot,animacao.angle_timer,animacao.cor_timer);
                Raylib.DrawTexturePro(textura,source,dest,pivot,ani_angle,ani_cor);
                }
            }
            catch // caso não exista um item na posição "array[frame_timer]"
            {
            animacao_finished();
            Console.WriteLine("Animação não encontrada");
            }
        }
    // ###############
    }
}  