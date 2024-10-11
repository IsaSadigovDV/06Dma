namespace Testing
{
    public class CalculationService
    {
        public double Sum(double a, double b)
        {
            return a + b;
        }

        public double Substract(double a, double b)
        {
            return a - b;
        }
        public double Multiple(double a, double b)
        {
            return a * b;
        }
        public double Divide(double a, double b)
        {
            if(b== 0)
                return 0;

            return a / b;
        }
    }
}
