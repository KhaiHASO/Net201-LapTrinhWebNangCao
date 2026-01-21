namespace Demo02.Services.Calculators
{
    public class StandardCalculator : ICalculatorService
    {
        public decimal CalculateTotal(decimal price, int quantity)
        {
            return price * quantity;
        }

        public string GetPromotionName()
        {
            return "Giá Niêm Yết (Không giảm giá)";
        }
    }
}
