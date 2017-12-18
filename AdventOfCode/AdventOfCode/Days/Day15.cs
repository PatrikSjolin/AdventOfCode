namespace AdventOfCode.Days
{
    public class Day15 : IPuzzle
    {
        public string RunOne()
        {
            int genAFactor = 16807;
            int genBFactor = 48271;

            int div = 2147483647;

            long resultA = 883;
            long resultB = 879;

            int pairs = 0;

            for (int i = 0; i < 40000000; i++)
            {
                resultA = resultA * genAFactor % div;
                resultB = resultB * genBFactor % div;

                if ((resultA & 65535) == (resultB & 65535))
                    pairs++;
            }

            return pairs.ToString();
        }

        public string RunTwo()
        {
            int genAFactor = 16807;
            int genBFactor = 48271;

            int div = 2147483647;

            long resultA = 883;
            long resultB = 879;

            int pairs = 0;

            int numberOfRuns = 5000000;

            for (int i = 0; i < numberOfRuns; i++)
            {
                do
                {
                    resultA = resultA * genAFactor % div;
                }
                while ((resultA & 3) != 0);
                do
                {
                    resultB = resultB * genBFactor % div;
                }
                while ((resultB & 7) != 0);

                if ((resultA & 65535) == (resultB & 65535))
                    pairs++;
            }

            return pairs.ToString();
        }
    }
}

