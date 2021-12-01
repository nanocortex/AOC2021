namespace Aoc2021.Puzzle1;

public class AnswerCalculator
{
    public long Calculate1(IList<int> values)
    {
        var increments = 0L;
        for (var i = 1; i < values.Count; i++)
        {
            if (values[i] > values[i - 1])
                increments++;
        }

        return increments;
    }

    public long Calculate2(IList<int> values)
    {
        var increments = 0L;
        var previousSum = values[0] + values[1] + values[2];
        for (var i = 0; i < values.Count; i++)
        {
            if (i + 3 > values.Count)
                break;

            var sum = values[i] + values[i + 1] + values[i + 2];

            if (sum > previousSum)
                increments++;

            previousSum = sum;
        }

        return increments;
    }
}