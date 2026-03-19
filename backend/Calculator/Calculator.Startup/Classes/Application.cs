using Calculator.Startup.Models;
using Calculator.Startup.Interfaces;

namespace Calculator.Startup.Classes
{
    internal class Application : IApplication
    {
        private List<Option> _options = [];
        private void InitOptions()
        {
            _options.Add(new Option()
            {
                Id = 1,
                DisplayName = "Sumar dos números",
                Usage = "<numero> <numero>",
                Return = "Resultado: <numero>"
            });
            _options.Add(new Option()
            {
                Id = 2,
                DisplayName = "Restar dos números",
                Usage = "<numero> <numero>",
                Return = "Resultado: <numero>"
            });
            _options.Add(new Option()
            {
                Id = 3,
                DisplayName = "Multiplicar dos números",
                Usage = "<numero> <numero>",
                Return = "Resultado: <numero>"
            });
            _options.Add(new Option()
            {
                Id = 4,
                DisplayName = "Dividir dos números",
                Usage = "<numero> <numero>",
                Return = "Resultado: <numero>"
            });
        }
        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();

        }
        public string ShowQuestion(string question, ConsoleColor color = ConsoleColor.White)
        {
            ShowMessage(question, color);
            return Console.ReadLine();
        }
        public void ShowMenu()
        {
            var message = $"""
                ---------------------------------

                CALCULADORA

                ---------------------------------

                Opciones:
                {string.Join("\n", _options.Select((op) => $"{op.Id}. {op.DisplayName}\n{op.Usage}\n{op.Return}"))}

                ---------------------------------
                """;
            ShowMessage(message, ConsoleColor.Green);
        }
        public double Sum(double number1, double number2)
        {
            return number1 + number2;
        }
        public double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }
        public double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }
        public double Divide(double number1, double number2)
        {
            if(number2==0) throw new DivideByZeroException();
            return number1 / number2;
        }
        public void Start()
        {
            InitOptions();
            while (true)
            {
                ShowMenu();
                try
                {
                    var optionID = Convert.ToInt32(ShowQuestion("Seleccione una opción: ", ConsoleColor.Blue));

                    var findOption = _options.Where((op) => op.Id == optionID).FirstOrDefault();

                    if (findOption is null)
                    {
                        throw new Exception("Error: Comando no encontrado");
                    }

                    var number1 = Convert.ToDouble(ShowQuestion("Numero 1: ", ConsoleColor.Yellow));
                    var number2 = Convert.ToDouble(ShowQuestion("Numero 2: ", ConsoleColor.Yellow));

                    if (findOption.Id == optionID)
                    {
                        if (optionID == 1) Console.WriteLine($"Suma: {Sum(number1, number2)}");
                        if (optionID == 2) Console.WriteLine($"Resta: {Subtract(number1, number2)}");
                        if (optionID == 3) Console.WriteLine($"Multiplicacion: {Multiply(number1, number2)}");
                        if (optionID == 4) Console.WriteLine($"Division: {Divide(number1, number2)}");
                    }
                }
                catch (FormatException)
                {
                    ShowMessage("Error: Entrada no válida. Por favor, ingrese un número.", ConsoleColor.Red);
                }   
                catch(DivideByZeroException)
                {
                    ShowMessage("Error: No se puede dividir por cero.", ConsoleColor.Red);
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message, ConsoleColor.Red);
                }
            }
        }
    }
}
