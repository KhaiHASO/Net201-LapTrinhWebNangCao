namespace Demo02.Services.Calculators
{
    public class BlackFridayCalculator : ICalculatorService
    {
        public decimal CalculateTotal(decimal price, int quantity)
        {
            // Giảm giá 50%
            return (price * quantity) * 0.5m;
        }

        public string GetPromotionName()
        {
            return "🔥 BLACK FRIDAY 🔥 (Giảm 50% toàn sàn)";
        }
    }
}
