namespace Aoc2021;

public sealed class Day10 : BaseDay
{
    private readonly List<string> _lines;

    public Day10()
    {
        _lines = File.ReadAllLines(InputFilePath).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>(_lines.Sum(GetLineCorruptionScore).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>(GetMiddleIncompleteScore().ToString());
    }


    private readonly Dictionary<char, int> _corruptedScoreTable = new()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private readonly Dictionary<char, int> _incompleteScoreTable = new()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
    };

    private int GetLineCorruptionScore(string line)
    {
        var stack = new Stack<char>();

        foreach (var c in line)
        {
            if (IsOpening(c))
            {
                stack.Push(c);
                continue;
            }

            if (!IsClosing(c)) continue;

            if (GetOpeningFromClosing(c) != stack.Peek())
                return _corruptedScoreTable[c];

            stack.Pop();
        }

        return 0;
    }

    private long GetMiddleIncompleteScore()
    {
        var scores = _lines.Where(line => GetLineCorruptionScore(line) == 0)
            .Select(GetLineIncompleteScore)
            .ToList();

        var sorted = scores.OrderBy(x => x).ToArray();
        return sorted[sorted.Length / 2];
    }

    private long GetLineIncompleteScore(string line)
    {
        long sum = 0;
        var stack = new Stack<char>();
        foreach (var c in line)
        {
            if (IsOpening(c))
            {
                stack.Push(c);
                continue;
            }

            if (!IsClosing(c)) continue;

            stack.Pop();
        }

        while (stack.Count > 0)
        {
            var opening = stack.Pop();
            var closing = GetClosingFromOpening(opening);
            var score = _incompleteScoreTable[closing];

            sum = sum * 5 + score;
        }

        return sum;
    }

    private bool IsOpening(char c) => c is '{' or '[' or '<' or '(';
    private bool IsClosing(char c) => c is '}' or ']' or '>' or ')';

    private char GetOpeningFromClosing(char c) => c switch
    {
        '}' => '{',
        ']' => '[',
        '>' => '<',
        ')' => '(',
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
    };

    private char GetClosingFromOpening(char c) => c switch
    {
        '{' => '}',
        '[' => ']',
        '<' => '>',
        '(' => ')',
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
    };
}