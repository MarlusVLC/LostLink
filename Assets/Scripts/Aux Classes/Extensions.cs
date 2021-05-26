namespace Aux_Classes
{
    public static class Extensions
    {
        public static int CountSetBits(this int n)
        {
            if (n == 0)
                return 0;

            return (n & 1) + CountSetBits(n >> 1);
        }
    }
}