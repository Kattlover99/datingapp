namespace DatingApp.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dab)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - dab.Year;

            if (dab > today.AddYears(-age)) age--;

            return age;
        }
    }
}
