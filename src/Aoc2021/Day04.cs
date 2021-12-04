namespace Aoc2021;

public class Day04 : BaseDay
{
    private List<int> _numbers;
    private List<Board> _boards;

    public Day04()
    {
        ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        Board? winnerBoard = null;
        var lastNumber = 0;
        foreach (var number in _numbers)
        {
            foreach (var board in _boards)
            {
                board.CheckNumber(number);
                if (board.Bingo())
                {
                    winnerBoard = board;
                    lastNumber = number;
                    break;
                }
            }

            if (winnerBoard != null)
                break;
        }

        var score = winnerBoard!.CalculateScore();

        return new ValueTask<string>((score * lastNumber).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Board? winnerBoard = null;
        var lastNumber = 0;
        foreach (var number in _numbers)
        {
            foreach (var board in _boards.Where(x => !x.Winner))
            {
                board.CheckNumber(number);
                if (board.Bingo())
                {
                    winnerBoard = board;
                    lastNumber = number;
                }
            }

            if (_boards.All(x => x.Winner))
                break;
        }

        var score = winnerBoard!.CalculateScore();

        return new ValueTask<string>((score * lastNumber).ToString());
    }

    private void ParseInput()
    {
        var lines = File.ReadAllLines(InputFilePath);
        _numbers = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        _boards = ParseBoards(lines.Skip(2));
    }

    private List<Board> ParseBoards(IEnumerable<string> lines)
    {
        var output = new List<Board>();

        var board = new Board();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                output.Add(board);
                board = new Board();
                continue;
            }

            var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x.Trim()));
            board.AddRow(numbers);
        }

        output.Add(board);
        return output;
    }

    private class Board
    {
        private readonly List<List<CheckableNumber>> _data;

        public Board()
        {
            _data = new List<List<CheckableNumber>>();
        }

        public void AddRow(IEnumerable<int> numbers)
        {
            _data.Add(numbers.Select(x => new CheckableNumber(x)).ToList());
        }

        public void CheckNumber(int number)
        {
            var checkableNumber = _data.SelectMany(x => x.Select(y => y))
                .FirstOrDefault(x => x.Number == number);
            checkableNumber?.Check();
        }

        public bool Bingo()
        {
            if (_data.Any(row => row.All(number => number.Checked)))
            {
                Winner = true;
                return true;
            }

            for (var i = 0; i < _data[0].Count; i++)
            {
                if (_data.All(row => row[i].Checked))
                {
                    Winner = true;
                    return true;
                }
            }

            return false;
        }


        public int CalculateScore()
        {
            return _data.SelectMany(x => x.Where(y => !y.Checked)).Sum(x => x.Number);
        }

        public bool Winner { get; set; }

        private class CheckableNumber
        {
            public CheckableNumber(int number)
            {
                Number = number;
                Checked = false;
            }

            public void Check()
            {
                Checked = true;
            }

            public bool Checked { get; private set; }
            public int Number { get; }
        }
    }
}