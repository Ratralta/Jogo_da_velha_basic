using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using Raylib_cs;
namespace GM_F; // faz parte de "GM_F"

public static partial class Animations
{
/*
Criar função que receba uma das arrays da classe "Animator", e acrescente um valor crescente, de uma possição da array até outra.

public static set_value_accretion(T _array_animator,Vector2 _de_ate_possition,int _value_start, int _value_end)
{
int array_size = _array_animator.Lenght; // tamanho da array
int value = _value_start; // valor que vai ser acresentado a possição de cada item da array "_array_animator"
	for(array_size)
	{
	value += _value_end+_value_start/array_size
	}
}
*/

	public static void set_value_accretion(float[] _array_animator,int _de,int _ate,float _value_start, float _value_end)
	{
	int array_size = _array_animator.Length; // tamanho da array
	float value = _value_start; // valor que vai ser acresentado a possição de cada item da array "_array_animator"
		for(int i = _de;i<_ate;i++)
		{
			try // tenta "_array_animator[i]"
			{
			float i_value = (_value_end-_value_start)/(_ate - _de); // dividindo valor para cada posição 	
			value += i_value;
				if(i != _ate-1)
				{
				_array_animator[i] = value; // colocando valor numa possição 
				}
				else
				{
				value = _value_end;
				_array_animator[i] = value; // colocando ultimo valor para ser o que ele é suposto a ser
				}
			}
			catch // caso não exista um item na posição "_array_animator[i]"
			{
			i = _ate; // encerrando o "for".
			}
		}
	}
	//public static void set_valuer_equal()
}