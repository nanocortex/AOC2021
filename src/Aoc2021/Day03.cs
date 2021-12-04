namespace Aoc2021;

public class Day03 : BaseDay
{
    private readonly string[] _numbers;

    public Day03()
    {
        _numbers = Utils.GetLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var length = _numbers[0].Length;
        var gamma = string.Empty;
        var epsilon = string.Empty;

        for (var i = 0; i < length; i++)
        {
            var ones = 0;
            var zeroes = 0;
            foreach (var t in _numbers)
            {
                if (t[i] == '1')
                    ones++;
                else
                    zeroes++;
            }

            if (ones > zeroes)
            {
                gamma += "1";
                epsilon += "0";
            }
            else
            {
                gamma += "0";
                epsilon += "1";
            }
        }

        var result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var oxygen = GetNumbers(_numbers.ToList(), 0, (ones, zeroes) => ones >= zeroes);
        var co2 = GetNumbers(_numbers.ToList(), 0, (ones, zeroes) => ones < zeroes);
        return new ValueTask<string>((oxygen * co2).ToString());
    }

    private int GetNumbers(List<string> numbers, int pos, Func<int, int, bool> predicate)
    {
        if (numbers.Count == 1)
            return Convert.ToInt32(numbers[0], 2);

        var ones = 0;
        var zeroes = 0;
        foreach (var number in numbers)
        {
            if (number[pos] == '1')
                ones++;
            else if (number[pos] == '0')
                zeroes++;
        }

        var bit = predicate(ones, zeroes) ? '1' : '0';
        var newNumbers = numbers.Where(x => x[pos] == bit);

        return GetNumbers(newNumbers.ToList(), pos + 1, predicate);
    }
}