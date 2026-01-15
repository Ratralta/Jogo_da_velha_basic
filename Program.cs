using System.Numerics;
using System.Reflection;
using Raylib_cs;
namespace HelloWorld;
internal static class Program
{
	[System.STAThread]
	public static void Main()
	{
		Raylib.InitWindow(600, 800, "Hello World");
		Raylib.SetTargetFPS(60);
        string local_da_imagem = "assets/imagens/segment_jogo_da_velha.png";

		var lista_de_segments = GM_F.JGVSegment.segment_create_list(local_da_imagem,18);
		var lista_de_segments2 = GM_F.JGVSegment.segment_create_list(local_da_imagem,18);
		int x = 100;
		int y = 200;

		GM_F.JGVSegment.Segment_meneger jogo_da_velha = new GM_F.JGVSegment.Segment_meneger(lista_de_segments,x,y-200);
		GM_F.JGVSegment.Segment_meneger jogo_da_velha2 = new GM_F.JGVSegment.Segment_meneger(lista_de_segments2,x,y+200);
		List<GM_F.JGVSegment.Segment_meneger> lista_jogo_da_velha = new List<GM_F.JGVSegment.Segment_meneger>(){jogo_da_velha,jogo_da_velha2};
		foreach(var i  in lista_jogo_da_velha)
		{
		i.matriz_create(1);
		}
	 
	 	string imagem_x = "assets/imagens/x_jogo_da_velha.png";
	 	string imagem_o = "assets/imagens/o_jogo_da_velha.png";
		var textura_x = Raylib.LoadTexture(imagem_x);
		var textura_o = Raylib.LoadTexture(imagem_o);

		while (!Raylib.WindowShouldClose())
		{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.White);
		Raylib.DrawFPS(0,0);
		foreach(var i in lista_jogo_da_velha)
		{
		i.matriz_draw();
		}
		Raylib.EndDrawing();

		jogo_da_velha.possition.X = x;
		jogo_da_velha.possition.Y = y;

			if (Raylib.IsMouseButtonPressed(MouseButton.Left))
			{
			Vector2 mouse = Raylib.GetMousePosition();
			bool teste;
				foreach(var i in lista_jogo_da_velha)
				{
				teste = i.segment_collision_point_bool(mouse);
					if (teste == true)
					{					
					var segmento = i.segment_collision_point_return(mouse);
					segmento.fragment_set(textura_x,Color.Red,GM_F.JGVSegment.Teams.Xis);
					i.matriz_line_check(segmento.Id,2);
					}
				}
			}
			if (Raylib.IsMouseButtonPressed(MouseButton.Right))
			{
			Vector2 mouse = Raylib.GetMousePosition();
			bool teste;
				foreach(var i in lista_jogo_da_velha)
				{
				teste = i.segment_collision_point_bool(mouse);
					if (teste == true)
					{					
					var segmento = i.segment_collision_point_return(mouse);
					segmento.fragment_set(textura_o,Color.Blue,GM_F.JGVSegment.Teams.Circulo);
					i.matriz_line_check(segmento.Id,2);
					}
				}
			}
		}
		Raylib.CloseWindow();
	}
}