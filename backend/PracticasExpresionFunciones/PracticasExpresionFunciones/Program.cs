// Definir una función que puedan llamar: Tipos de datos simples
using System.Linq.Expressions;

Func<int, int, int> Sumar = (num1, num2) =>
{
    return num1 + num2;
};

// Definir una expresión: Tanto con funciones que usen un tipo o un modelo (class)
Expression<Func<int, bool>> expresion = x => x > 10;
Expression<Func<Usuario, bool> validarEdadUsuario = x => x.edad >= 18;

// Un modelo (class), con el que usen su función, para practicar el uso de propiedades
public class Usuario()
{
    string Nombre { get; set; } = null!;
    int edad { get; set; }
}

// Creen un repositorio, donde se manejen un genérico y hagan uso de expresiones. Colocar constraint para unicamente clases
