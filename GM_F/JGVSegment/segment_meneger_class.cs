using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
namespace GM_F; // faz parte de "GM_F"

// Essa class irá conter funções para gerar imagens simples     
 static public partial class JGVSegment
{
    public class Segment_meneger
    {
    // ########### Variáveis 
        public List<Segment> segments_list; // lista de objetos "Segment"
        public Vector2 possition; // posição geral
    // ###########

    // ########### Constructor 
        public Segment_meneger(List<Segment> _segment_list,Vector2 _possition) // constructor Vector2
        {
        this.segments_list = _segment_list;
        this.possition = _possition;

        var size_size = matriz_size_return(segments_list.Count());
        //matriz = new List<Vector2>(size_size,size_size); // definindo tamanho de "matriz"
        }
        public Segment_meneger(List<Segment> _segment_list,int _x,int _y) // Constructor sem Vector2
        {
        this.segments_list = _segment_list;
        this.possition.X = _x;
        this.possition.Y = _y;

        var size_size = matriz_size_return(segments_list.Count());
        //matriz = new Vector2[size_size,size_size]; // definindo tamanho de "matriz"
        }
    // ###########

    // ########### Funções 
        public int matriz_size_return(int _lista_quantidade) // retorna o tamanho da array "matriz";
        {
        int size_size;
        size_size = Convert.ToInt16(System.Math.Sqrt(_lista_quantidade)); // raiz quadrada de "_lista_quantidade", gerando o tamanho de "segments_matriz"
        return size_size;   
        }
        public void matriz_create(int _generate_type) // cria um "jogo da velha"
        {
            if (_generate_type == 1) // desenhando segmentos do tipo de padrão 1
            {
                int lista_quantidade = segments_list.Count(); // quantos itens existem na lista 
                int size_size = matriz_size_return(lista_quantidade); // raiz quadrada de "lista_quantidade", gerando o tamanho do quadrado desenhado E convertendo para int                
                int x; // x adicional 
                int y; // y adicional
                int id_x;
                int id_y;
                for(int i=0;i<lista_quantidade;) // loop
                {
                    for(int ii=0;ii<size_size;ii++) // loop X
                    {
                    id_x = ii;
                        if (i<lista_quantidade)  // se ainda existir algum segment disponivel [X]
                        {
                            for(int iii=0;iii<size_size;iii++) // loop Y
                            {
                                if(i<lista_quantidade) // se ainda existir algum segment disponivel [Y]
                                {
                                Segment i_segment = segments_list[i]; // segmento especifico
                                Texture2D textura_especifica = i_segment.textura; // textura especifica 
                                x = textura_especifica.Width * ii;
                                    if(i+1>size_size*size_size) // se "i" for maior que raiz de "lista_quantidade"
                                    {
                                    y = textura_especifica.Height * size_size;
                                    x = textura_especifica.Width * iii; // ajustando "x" pois esse é o ultimo loop
                                    id_x = iii;
                                    id_y = size_size;
                                    }
                                    else // se "i" for menor que "lista_quantidade"
                                    {
                                    y = textura_especifica.Height * iii;
                                    id_y = iii;
                                    }
                                int _x = Convert.ToInt16(possition.X); // posição de "Segment_meneger"
                                int _y = Convert.ToInt16(possition.Y);

                                i_segment.Id[0] = id_x;
                                i_segment.Id[1] = id_y;
                                //matriz[id_x,id_y] = new Vector2(id_x,id_y);
                                i_segment.me_update_location(_x+x,_y+y);
                                i++;
                                }                                
                                else
                                {iii = size_size; /* acabando com loop (Y) */}
                            }
                        }
                        else
                        {ii = size_size; /* acabando com loop (X)*/ }
                    }
                }
            }
            else // padrão não encontrado
            {
            Console.WriteLine("Não existe");
            }
        }
        public void matriz_draw() // desenhar matriz
        {
            foreach(var i in segments_list)
            {
            i.me_draw();
            i.id_draw();
            }
        }
        // fazer função que retorna numa lista, uma linha de segmentos que faz velha.
        public void matriz_line_check(Vector2 _Id_possition,int _check_size) // checa se fez um velha,
        {
        Segment seg_inicial = segment_get_segment_by_Id(_Id_possition);
        Teams ini_team = seg_inicial.fragmento.team; 

        Vector2 ini_posi = seg_inicial.possition; // posição que irá criar chamar os "matriz_line_check_timer()"
        List<Segment> lista = new List<Segment>(); // lista que irá os segmentos "in_line"

            
            for(int i=-1;i<2;i++)
            {
                for(int ii=-1;ii<2;ii++)
                {
                var pos = new Vector2(i,ii); 
                float x_new_possition = _Id_possition.X + pos.X; 
                float y_new_possition = _Id_possition.Y + pos.Y; 
                Vector2 new_possition = new Vector2(x_new_possition,y_new_possition);

                List<Segment>? tes = matriz_line_check_reverse(new_possition,pos,_check_size,_check_size,new List<Segment>(lista),ini_team);
                //Segment? teste = matriz_line_check_reverse(_Id_possition,new_possition,_check_size-1,_check_size-1,tes,ini_team);
                    if(tes != null) // se a lista retornar alguma coisa 
                    {
                        if (i!=0 || ii!=0) // se não for (0,0)
                        {
                        segment_in_line_put(tes,pos);
                        return;  
                        }
                    }
                }        
            }
        }
        private List<Segment>? matriz_line_check_timer(Vector2 _Id_possition_atual,Vector2 _vector_flex,int _check_size_timer,List<Segment> _lista_in_line,Teams _team) // confere e retorna "Segments" que foram "velha" numa linha reta.
        {
        Segment? my_seg = segment_get_segment_by_Id(_Id_possition_atual);
            if (my_seg != null) // se existir
            {
                if(my_seg.ocupado == true && my_seg.in_line == false && _team == my_seg.fragmento.team) // se estiver ocupado E não tiver "in_line" E "team" for igual a "_team"
                {
                _lista_in_line.Add(my_seg); // adicioando segment "my_seg"
                        if(_check_size_timer > 0) // se ainda estiver num loop
                        {
                        var _x = _Id_possition_atual.X+_vector_flex.X;
                        var _y = _Id_possition_atual.Y+_vector_flex.Y;
                        return matriz_line_check_timer(new Vector2(_x,_y),_vector_flex,_check_size_timer-1,_lista_in_line,_team);
                        }
                        else // se o loop tiver acabado
                        {
                        return _lista_in_line;
                        }
                }
                else {return null;} // se o segmento não estiver ocupado  
            }
            else {return null;} // se não "my_seg" não existe
        }
        private List<Segment>? matriz_line_check_reverse(Vector2 _Id_possition_atual,Vector2 _vector_flex,int _check_size_timer,int _check_size_true,List<Segment> _lista_in_line,Teams _team)
        {
        var _x = _Id_possition_atual.X+_vector_flex.X * -1;
        var _y = _Id_possition_atual.Y+_vector_flex.Y * -1;
        Vector2 reverse_vector = new Vector2(_x,_y);
        Segment? my_seg_reverse = segment_get_segment_by_Id(reverse_vector);
            if(my_seg_reverse != null) // se existe segmento ao contrario.
            {
                if(my_seg_reverse.ocupado == true && my_seg_reverse.in_line == false && _team == my_seg_reverse.fragmento.team)
                {
                    if(_check_size_timer>0) // estiver no loop
                    {
                    return matriz_line_check_reverse(reverse_vector,_vector_flex,_check_size_timer-1,_check_size_true,_lista_in_line,_team);
                    }
                    else // se loop acabou
                    {
                    return matriz_line_check_timer(_Id_possition_atual,_vector_flex,_check_size_true,_lista_in_line,_team);
                    }
                }
                else
                {
                return matriz_line_check_timer(_Id_possition_atual,_vector_flex,_check_size_true,_lista_in_line,_team);
                }
            }
            else // se não existe 
            {
            return matriz_line_check_timer(_Id_possition_atual,_vector_flex,_check_size_true,_lista_in_line,_team);
            }
        }
        public void segment_in_line_put(List<Segment> _lista_segmentos,Vector2 _pos) // coloca segmentos no estado "in_line"
        {
        int list_size = _lista_segmentos.Count();
        string textura_to_set;
        Console.WriteLine(_pos);

            for(var i=0;i<list_size;i++)
            {
            var i_segment = _lista_segmentos[i];
            int _x = i_segment.size * 20/100;
            int _y = i_segment.size + i_segment.size  * 20/100;
            Vector2 size =  new Vector2(_x,_y); // definindo tamanho da linha
            Color i_cor = i_segment.fragmento.cor; // cor do "fragmento"
            int animacao_iniciar_timer = 10*i; 

                float angle = 0; // definindo angulo de "line"
                if (_pos.X == -1 && _pos.Y == -1)
                {angle= 315;}
                if (_pos.X == -1 && _pos.Y ==  0)
                {angle= 90;}
                if (_pos.X == -1 && _pos.Y ==  1)
                {angle= 45;}
                if (_pos.X == 0 && _pos.Y == -1)
                {angle= 180;}
                if (_pos.X == 0 && _pos.Y == 1)
                {angle= 180; }
                if (_pos.X == 1 && _pos.Y == -1)
                {angle= 45;}
                if (_pos.X == 1 && _pos.Y == 1)
                {angle= 135;}
                if (_pos.X == 1 && _pos.Y == 0)
                {angle= 90;}


                if(i == list_size-1 | i== 0) // definindo textura de "line"
                {
                textura_to_set = "assets/imagens/line_2_jogo_da_velha.png";
                }
                else
                {
                textura_to_set = "assets/imagens/line_1_jogo_da_velha.png";    
                size.Y +=  i_segment.size * 50/100;
                }        
            Texture2D textura_line = Raylib.LoadTexture(textura_to_set); // textura do line
            i_segment.line_set(textura_line,angle,size,i_cor,i_segment.possition,animacao_iniciar_timer); // colocando linha em "i_segment"
            //i_segment.fragmento.my_linha.animacao_iniciar_timer = animacao_iniciar_timer; // colocando timer da animação
            }   
        }
        public Segment segment_get_segment_by_Id(Vector2 _Id_possition) // retorna um "segment" de "segment_list" atráves da "Id"
        {
        Segment? seg_especifico = null;
            for(int i =0;i<segments_list.Count;i++)
            {
            Segment i_segment = segments_list[i];
                if (i_segment.Id == _Id_possition) // se "Id" for igual a "_Id_possition"
                {
                seg_especifico = i_segment; // pegando o "segment" da "_Id_possition" colocada
                i = segments_list.Count; // encerrando for
                }
            }
        #pragma warning disable CS8603 // tirar o erro "Possible null reference return"
        return seg_especifico;
        #pragma warning restore CS8603 
        }
        public bool segment_collision_point_bool(Vector2 _point) // retorna true ou false se algum segmento tiver sido colidido
        {
        bool colisao_check = false;
            for(var i=0;i<segments_list.Count();i++)
            {
            var i_segment = segments_list[i].hit_box;
            colisao_check = Raylib.CheckCollisionPointRec(_point,i_segment);
                if (colisao_check == true) // se algum segmento tiver sido colidido
                {
                i = segments_list.Count();
                return colisao_check;
                } 
            }
        return colisao_check; // se nenhum segmento tiver sido colidido
        }
        public Segment segment_collision_point_return(Vector2 _point)
        {
        Segment? segmento_retorno = null; 
            for(var i=0;i<segments_list.Count();i++)
            {
            var i_segment = segments_list[i];
            var colidiu = Raylib.CheckCollisionPointRec(_point,i_segment.hit_box);
                if (colidiu == true) // se colidiu
                {
                segmento_retorno = i_segment; // atribuindo "segmento_especifico" o valor "segments_list" 
                i = segments_list.Count(); // encerrando loop
                }
            }
        #pragma warning disable CS8603 // tirar o erro "Possible null reference return"
        return segmento_retorno; // caso não seja colidido com nada, irá retornar "null"
        #pragma warning restore CS8603 
        }
    // ###########
    }
}