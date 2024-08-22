namespace proyecto_javier.Models
{
    public class CalculadoraPS
    {
        public int firstNumber {  get; set; }
        public int secondNumber { get; set; }
        public int Add()
        {
            return firstNumber + secondNumber;
        }
    }
}
