namespace CustomerAccount.Domain.Helpers
{
    public class RotationHelper
    {
        public static string Rotate(int[] a, int times)
        {
            for (int i = 0; i < times; i++)
            {
                RotateByOne(a);
            }

            return string.Join("", a);
        }

        private static void RotateByOne(int[] a)
        {
            int last = a[a.Length - 1];

            for (int i = a.Length - 2; i >= 0; i--)
            {
                a[i + 1] = a[i];
            }
            a[0] = last;
        }
    }
}
