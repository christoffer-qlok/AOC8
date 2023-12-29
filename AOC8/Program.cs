namespace AOC8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new Dictionary<string, string[]>();
            foreach (var line in lines.Skip(2)) 
            { 
                var parts = line.Split(" = ");
                var src = parts[0];
                var dst = parts[1].Trim('(', ')').Split(", ");
                map[src] = dst;
            }

            string loc = "AAA";
            int steps = 0;
            while (true)
            {
                var cmd = lines[0].ToCharArray()[steps % lines[0].Length];
                if (loc == "ZZZ")
                    break;
                steps++;
                switch (cmd)
                {
                    case 'L':
                        loc = map[loc][0];
                        break;
                    case 'R':
                        loc = map[loc][1];
                        break;
                }
            }
            Console.WriteLine(steps);
        }
    }
}