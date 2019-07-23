namespace Abstractions.Models
{
    public interface IShipDetailsModel
    {
        string Name { get; set; }
        string Range { get; set; }
        int NumericRange { get; }
        string Supplies { get; set; }
        int HoursSuppliesLastFor { get; set; }
        string NumberOfJumpsForDistance(long distance);
    }
}
