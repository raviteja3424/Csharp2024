namespace ConcessionLibrary
{
    public class ConcessionCalculator
    {
        private const double TotalFare = 500.0;

        public string CalculateConcession(int age)
        {
            if (age <= 5)
            {
                return "Little Champs - Free Ticket";
            }
            else if (age > 60)
            {
                double concessionAmount = 0.3 * TotalFare;
                double discountedFare = TotalFare - concessionAmount;
                return $"Senior Citizen - Discounted Fare: {discountedFare:C}";
            }
            else
            {
                return $"Ticket Booked - Fare: {TotalFare:C}";
            }
        }
    }
}
