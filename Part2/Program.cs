namespace Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new Dictionary<string, string[]>();
            foreach (string line in lines.Skip(2))
            {
                var parts = line.Split(" = ");
                var src = parts[0];
                var dst = parts[1].Trim('(', ')').Split(", ");
                map[src] = dst;
            }

            var locs = map.Keys.Where(p => p.EndsWith('A')).ToArray();

            var info = locs.Select(loc => new PathInfo() {  Start = loc }).ToArray();

            int steps = 0;
            while (true)
            {
                var cmd = lines[0].ToCharArray()[steps % lines[0].Length];

                for (int i = 0; i < info.Length; i++)
                {
                    string key = locs[i] + (steps % lines[0].Length).ToString();

                    if (!info[i].Complete)
                    {
                        if (locs[i].EndsWith('Z'))
                        {
                            info[i].Solutions.Add(steps);
                        }


                        if (info[i].VisitedNodes.ContainsKey(key))
                        {
                            //Console.WriteLine($"{info[i].Start} complete at {steps}");
                            info[i].Complete = true;
                            info[i].CycleStart = info[i].VisitedNodes[key];
                            info[i].CycleLength = steps - info[i].VisitedNodes[key];
                        }
                    }

                    info[i].VisitedNodes[key] = steps;
                }

                if (info.All(p => p.Complete))
                    break;

                steps++;
                for (int i = 0; i < locs.Length; i++)
                {
                    switch (cmd)
                    {
                        case 'L':
                            locs[i] = map[locs[i]][0];
                            break;
                        case 'R':
                            locs[i] = map[locs[i]][1];
                            break;
                    }
                }
            }

            foreach (var item in info)
            {
                Console.WriteLine($"{item.Start}: {string.Join(' ', item.Solutions.Select(i => i.ToString()))}");
                Console.WriteLine($"Cycle start: {item.CycleStart}, length: {item.CycleLength}");
                Console.WriteLine($"Solutions: {string.Join(',',item.Solutions)}");
                Console.WriteLine();
            }

            var answer = CalculateLCM(info.Select(i => (long)i.CycleLength).ToArray());
            Console.WriteLine($"LCM: {answer}");
        }

        static long CalculateLCM(params long[] numbers)
        {
            if (numbers.Length < 2)
            {
                return numbers[0];
            }

            long lcm = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                lcm = CalculateLCM(lcm, numbers[i]);
            }

            return lcm;
        }

        static long CalculateLCM(long a, long b)
        {
            return (a * b) / CalculateGCD(a, b);
        }

        static long CalculateGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}