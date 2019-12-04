namespace AdventOfCode.Days_2019
{
    public class Day04 : IPuzzle
    {
        public const int start = 123257;
        public const int stop = 647015;

        public bool Active => true;

        private bool Match(int number)
        {
            string n = number.ToString();

            bool met = false;

            for (int i = 0; i < n.Length - 1; i++)
            {
                if (n[i] > n[i + 1])
                    return false;
                if (n[i] == n[i + 1])
                    met = true;
            }

            return met;
        }

        public string RunOne()
        {
            int sum = 0;

            for (int i = start; i <= stop; i++)
            {
                sum += Match(i) ? 1 : 0;
            }

            return sum.ToString();
        }

        private int GetNumber(string n, int index)
        {
            if (index < 0)
                return -1;
            if (index >= n.Length)
                return -1;
            return n[index];
        }

        private bool Match2(int number)
        {
            string n = number.ToString();

            bool met = false;

            for (int i = 0; i < n.Length - 1; i++)
            {
                if (n[i] > n[i + 1])
                    return false;

                if (n[i] == GetNumber(n, i + 1) &&
                    n[i] != GetNumber(n, i + 2) &&
                    n[i] != GetNumber(n, i - 1))
                {
                    met = true;
                }
            }

            return met;
        }

        public string RunTwo()
        {
            int sum = 0;

            for (int i = start; i <= stop; i++)
            {
                sum += Match2(i) ? 1 : 0;
            }

            return sum.ToString();
        }
    }
}
