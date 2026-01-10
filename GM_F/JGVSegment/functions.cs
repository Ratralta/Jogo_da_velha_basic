using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
namespace GM_F; // faz parte de "GM_F"

static public partial class JGVSegment
{
    static public List<Segment> segment_create_list(string _textura_possition, int _quantidade) // cria uma lista
    {
    List<Segment> lista= new List<Segment>();
        for(int i=0;i<_quantidade;i++)
        {
        Texture2D textura = Raylib.LoadTexture(_textura_possition); // carregando uma textura.
        Segment new_segment = new Segment(textura); // gerando novo objeto "Segment".
        lista.Add(new_segment); // adicionando objeto a lista. 
        }
    return lista;     
    }
    public enum Teams
    {
        Circulo,
        Xis
    }
}