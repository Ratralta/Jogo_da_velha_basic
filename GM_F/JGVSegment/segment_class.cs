using System.ComponentModel;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
namespace GM_F; // faz parte de "GM_F"

static public partial class JGVSegment
{
    public class Segment // classe dos segmentos
    {
    // ########### Variáveis :
        public Texture2D textura; 
        public Vector2 Id; // Posição na array 2D "segments_matriz"
        public Rectangle hit_box; // colisão 
        public Color cor = Color.Black;
        public int size = 70;
        public bool ocupado = false;
        public bool in_line = false; // se alguma forma foi marcada
        public Vector2 possition; // posição do segmento  
            public class Fragment
            {
            public Texture2D textura;
            public Color cor;
            public Teams team; // time que está ocupando esse fragmento
            public Vector2 possition;
            public Line my_linha = new Line();
            }
            public class Line
            {
            public Texture2D textura; // textura da linha
            public float angle;
            public Vector2 size;
            }
        public Fragment fragmento = new Fragment();
    // ########### 

    // ########### Constructor 
        public Segment(Texture2D _textura)
        {
        this.textura = _textura;
        this.textura.Width = size; 
        this.textura.Height = size; 

        hit_box_criar();
        }
    // ###########

    // ########### Funções 
        public void me_update_location(int _x, int _y) // muda o vector2 "possition" e a posição de "hit_box"
        {
        possition.X = _x; // atualizando minha posição
        possition.Y = _y;
        hit_box_follow_texture(); // atualizando posição da hitbox
        }
        public void me_draw() // desenha o segment 
        {
        int _x = Convert.ToInt16(possition.X);
        int _y = Convert.ToInt16(possition.Y);
        Raylib.DrawTexture(textura,_x,_y,cor);
        fragment_draw(); // dsenha fragmento se possuir um 
        line_draw();
        }
        public void id_draw()
        {
        int _x = Convert.ToInt16(possition.X) + textura.Width/2 - 22;
        int _y = Convert.ToInt16(possition.Y) + textura.Height/2;
        string _id = "{";
        _id += "x: "+ Convert.ToString(Id.X) + ",";
        _id += "y: "+Convert.ToString(Id.Y) + "}";

        Raylib.DrawText(_id,_x,_y,15,Color.Black);
        }
        public void hit_box_draw()
        {
        Color _cor = new Color(0,0,175,160);
        Raylib.DrawRectangleRec(hit_box,_cor);
        }
        private void hit_box_follow_texture() // coloca hitbox na mesma possição 
        {
        hit_box.X = possition.X;   
        hit_box.Y = possition.Y;   
        }
        private void hit_box_criar() // cria uma hitbox, usado nos "constructor"
        {
        int _widht = textura.Width - (textura.Width*7/100);
        int _height = textura.Height- (textura.Height*7/100);
        hit_box = new Rectangle(0,0,_widht,_height);
        }
        public void fragment_set(Texture2D _textura_team, Color _cor,Teams _team) // definindo valores de "fragmento" 
        {
            if( ocupado == false)
            {
            ocupado = true;
            fragmento.cor = _cor;
            int _x = Convert.ToInt16(possition.X);
            int _y = Convert.ToInt16(possition.Y);
            fragmento.possition = new Vector2(_x,_y);
            fragmento.textura = _textura_team;
            fragmento.textura.Width = size;
            fragmento.textura.Height = size;
            fragmento.team = _team;
            }
        }
        public void fragment_draw()
        {
            if (ocupado == true) // possui fragmento ativo
            {
                int _x = Convert.ToInt16(fragmento.possition.X);
                int _y = Convert.ToInt16(fragmento.possition.Y);
                Raylib.DrawTexture(fragmento.textura,_x,_y,fragmento.cor);
            }
        }
        public void line_set(Texture2D _textura ,float _angle,Vector2 _size)
        {
        in_line = true; // deixando o "segment" em "in line"
        fragmento.my_linha.angle = _angle; // definindo angulo de "line"
        fragmento.my_linha.textura = _textura; // adicionando textura a "line"
        fragmento.my_linha.size = _size; // definindo tamanho de "line"
        }
        public void line_draw()
        {
            if(in_line == true)
            {               
                var linha = fragmento.my_linha;
                //fragmento.line_angle += 1;
                int _x = Convert.ToInt16(fragmento.possition.X+textura.Width/2);
                int _y = Convert.ToInt16(fragmento.possition.Y+textura.Height/2);
                //linha.size.X += 1;
                
                Rectangle source = new Rectangle(0,0,linha.textura.Width,linha.textura.Height);
                Rectangle dest= new Rectangle(_x,_y,linha.size.X,linha.size.Y);
                Vector2 pivot = new Vector2(textura.Width/2,textura.Height/2); 
                Raylib.DrawTexturePro(linha.textura,source,dest,pivot,linha.angle,fragmento.cor);
            }
        }
    // ###########      
    }
}