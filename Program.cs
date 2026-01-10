using System.Numerics;
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

		var lista_de_segments = GM_F.JGVSegment.segment_create_list(local_da_imagem,9);
		int x = 100;
		int y = 200;

		GM_F.JGVSegment.Segment_meneger jogo_da_velha = new GM_F.JGVSegment.Segment_meneger(lista_de_segments,x,y);

		jogo_da_velha.matriz_create(1);


	 
	 	string imagem_x = "assets/imagens/x_jogo_da_velha.png";
	 	string imagem_o = "assets/imagens/o_jogo_da_velha.png";
		var textura_x = Raylib.LoadTexture(imagem_x);
		var textura_o = Raylib.LoadTexture(imagem_o);

		while (!Raylib.WindowShouldClose())
		{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(Color.White);
		Raylib.DrawFPS(0,0);
		jogo_da_velha.matriz_draw();
		Raylib.EndDrawing();

		jogo_da_velha.possition.X = x;
		jogo_da_velha.possition.Y = y;

			if (Raylib.IsMouseButtonPressed(MouseButton.Left))
			{
			Vector2 mouse = Raylib.GetMousePosition();
			var teste = jogo_da_velha.segment_collision_point_bool(mouse);
				if (teste== true) // se a posição do mouse colidir com algum "segment"
				{
				var segmento = jogo_da_velha.segment_collision_point_return(mouse); // pegando segmento que o mouse clicou

				segmento.fragment_set(textura_x,Color.Red,GM_F.JGVSegment.Teams.Xis);
				segmento.fragment_draw();
				jogo_da_velha.matriz_line_check(segmento.Id,2);
				}
			}
			if (Raylib.IsMouseButtonPressed(MouseButton.Right))
			{
			Vector2 mouse = Raylib.GetMousePosition();
			var teste = jogo_da_velha.segment_collision_point_bool(mouse);
				if (teste== true) // se a posição do mouse colidir com algum "segment"
				{
				var segmento = jogo_da_velha.segment_collision_point_return(mouse); // pegando segmento que o mouse clicou

				segmento.fragment_set(textura_o,Color.Blue,GM_F.JGVSegment.Teams.Circulo);
				segmento.fragment_draw();
				jogo_da_velha.matriz_line_check(segmento.Id,2);
				}
			}
		}
		Raylib.CloseWindow();
	}
}