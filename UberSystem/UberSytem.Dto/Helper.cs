namespace UberSytem.Dto
{
    public class Helper
    {
        public static long GenerateRandomLong()
        {
            // Create an instance of Random
            Random random = new Random();

            long randomLong = random.Next(10000000, 100000000);

            return randomLong;
        }
    }
}
