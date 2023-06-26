using System.Threading.Tasks;

namespace ValuePerf;

[MemoryDiagnoser]
public class Table
{
    private const int Rows = 10000;
    private const int Columns = 10;
    private const int Requests = 1000;

    [Benchmark(Baseline = true)]
    public long ObjectTableSum()
    {
        var tasks = new Task[Requests];
        long sum = 0;
        for (int i = 0; i < Requests; i++)
        {
            var task = Task.Run(() =>
            {
                var table = new ObjectTable(Rows, Columns);
                sum += (int)table._rows[823][5];
            });
            tasks[i] = task;
        }

        Task.WaitAll(tasks);
        return sum;
    }

    [Benchmark]
    public long ValueTableSum()
    {
        var tasks = new Task[Requests];
        long sum = 0;
        for (int i = 0; i < Requests; i++)
        {
            var task = Task.Run(() =>
            {
                var table = new ValueTable(Rows, Columns);
                sum += (int)table._rows[823][5];
            });
            tasks[i] = task;
        }

        Task.WaitAll(tasks);
        return sum;
    }

    private class ObjectTable
    {
        public object[][] _rows;

        public ObjectTable(int rows, int columns)
        {
            _rows = new object[rows][];
            for (int r = 0; r < rows; r++)
            {
                _rows[r] = new object[columns];
                for (int c = 0; c < columns; c++)
                {
                    _rows[r][c] = 10;
                }
            }
        }
    }

    private class ValueTable
    {
        public Value[][] _rows;

        public ValueTable(int rows, int columns)
        {
            _rows = new Value[rows][];
            for (int r = 0; r < rows; r++)
            {
                _rows[r] = new Value[columns];
                for (int c = 0; c < columns; c++)
                {
                    _rows[r][c] = 10;
                }
            }
        }
    }
}
