namespace Demo02.Services.Calculators
{
    public interface ICalculatorService
    {
        decimal CalculateTotal(decimal price, int quantity);
        string GetPromotionName(); // Để hiển thị tên chương trình khuyến mãi
    }
}
