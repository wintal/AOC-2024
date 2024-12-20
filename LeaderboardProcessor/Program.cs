namespace LeaderboardProcessor;
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {
        var leaderBoardData = System.IO.File.ReadAllText(args[0]);

        var data = JsonConvert.DeserializeObject<Results>(leaderBoardData);
        HashSet<string> allPlayers = new HashSet<string>();
        if (args.Length > 1)
        {
            foreach (var arg in args[1..])
            {
                allPlayers.Add(arg);
            }
        }
        else
        {
            allPlayers = data.members.Select(p => p.Value.name).ToHashSet();
        }
        

        Dictionary<string, int> mattStars = new Dictionary<string, int>();
        Dictionary<string, int> actualStars = new Dictionary<string, int>();

        int numberPeople = data.members.Count(p => allPlayers.Contains(p.Value.name));
        for (int day = 1; day <= 25; day++)
        {
            List<(string Name, (DateTime Time, long rank))> leaderboardA = new ();
            List<(string Name, (DateTime Time, long rank))> leaderboardB = new ();
            List<(string Name, long Time)> leaderboardDelta = new List<(string Name, long Time)>();

            foreach (var member in data.members.Where(p => allPlayers.Contains(p.Value.name)).Select(kvp => kvp.Value))
            {
                if (member.completion_day_level.TryGetValue(day, out var dayResult))
                {

                    if (dayResult?.TryGetValue(1, out var partResult) ?? false)
                    {
                        leaderboardA.Add((member.name, (UnixTimeStampToDateTime(partResult.get_star_ts), partResult.star_index)));
                        if (!actualStars.ContainsKey(member.name))
                        {
                            actualStars.Add(member.name, 0);
                        }

                        if (dayResult?.TryGetValue(2, out var partResult2) ?? false)
                        {
                            leaderboardB.Add((member.name, (UnixTimeStampToDateTime(partResult2.get_star_ts), partResult.star_index)));
                            leaderboardDelta.Add((member.name,
                                partResult2.get_star_ts - partResult.get_star_ts));
                            if (!mattStars.ContainsKey(member.name))
                            {
                                mattStars.Add(member.name, 0);
                            }
                        }
                    }
                }
            }

            leaderboardA.Sort((a, b) =>
            {
                if (a.Item2.Time == b.Item2.Time) return -a.Item2.rank.CompareTo(b.Item2.rank);
                return a.Item2.Time.CompareTo(b.Item2.Time);
            });
            leaderboardB.Sort((a, b) =>
            {
                if (a.Item2.Time == b.Item2.Time) return -a.Item2.rank.CompareTo(b.Item2.rank);
                return a.Item2.Time.CompareTo(b.Item2.Time);
            });
            leaderboardDelta.Sort((a, b) => a.Time.CompareTo(b.Time));

            if (leaderboardA.Any())
            {
                System.Console.WriteLine($"\nResults for Day {day} part 1\n");
                Console.WriteLine("|Name| Time (AWST)|");
                Console.WriteLine("|------------|-----------------|");
                int points = numberPeople;
                foreach (var member in leaderboardA)
                {
                    // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
                    Console.WriteLine("|{0,-20}|{1,10}|",
                        member.Name,
                        member.Item2.Time);
                    actualStars[member.Name] += points;
                    points--;
                }
            }
            Console.WriteLine("\n  ");

            if (leaderboardB.Any())
            {
                System.Console.WriteLine($"\nResults for Day {day} part 2\n");
                
                Console.WriteLine("|Name| Time (AWST)|");
                Console.WriteLine("|------------|-----------------|");
                int points = numberPeople;
                foreach (var member in leaderboardB)
                {
                    // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
                    Console.WriteLine("|{0,-20}|{1,10}|",
                        member.Name,
                        member.Item2.Time);
                    actualStars[member.Name] += points;
                    points--;
                }
                Console.WriteLine("\n  ");
            }
            
            if (leaderboardDelta.Any())
            {
                System.Console.WriteLine($"\nResults for Day {day} fastest part 2\n");
                
                Console.WriteLine("|Name| Time (AWST)|");
                Console.WriteLine("|------------|-----------------|");
                int points = numberPeople;
                foreach (var member in leaderboardDelta)
                {
                    // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
                    Console.WriteLine("|{0,-20}|{1,10}|",
                        member.Name,
                        member.Time);
                    
                    mattStars[member.Name] += points;
                    points--;
                }
                Console.WriteLine("\n  ");
            }

            DateTime UnixTimeStampToDateTime( double unixTimeStamp )
            {
                // Unix timestamp is seconds past epoch
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
                return dateTime;
            }
        }
        var overallResults = actualStars.OrderByDescending(kvp => kvp.Value);
        System.Console.WriteLine($"Overall Leaderboard\n");
                
        Console.WriteLine("|Name| Time (AWST)|");
        Console.WriteLine("|------------|-----------------|");
        foreach (var member in overallResults)
        {
            // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
            Console.WriteLine("|{0,-20}|{1,10}|",
                member.Key,
                member.Value);
                    
        }
        Console.WriteLine("\n  ");
        
        var overallMattStars = mattStars.OrderByDescending(kvp => kvp.Value);
        System.Console.WriteLine($"Matt's Leaderboard - fastest by part 2\n");
                
        Console.WriteLine("|Name| Time (AWST)|");
        Console.WriteLine("|------------|-----------------|");
        foreach (var member in overallMattStars)
        {
            // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
            Console.WriteLine("|{0,-20}|{1,10}|",
                member.Key,
                member.Value);
                    
        }
        Console.WriteLine("\n  ");
    }
}