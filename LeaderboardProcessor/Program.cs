namespace LeaderboardProcessor;
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {
        var leaderBoardData = System.IO.File.ReadAllText(args[0]);

        var data = JsonConvert.DeserializeObject<Results>(leaderBoardData);
        int? dayToReport = null;
        if (args.Length > 1 && int.TryParse(args[1], out var dayId))
        {
            dayToReport = dayId;
        }

        for (int day = 1; day <= 25; day++)
        {
            if (dayToReport.HasValue && dayToReport.Value != day) continue;
            
            List<(string Name, DateTime Time)> leaderboardA = new List<(string Name, DateTime Time)>();
            List<(string Name, DateTime Time)> leaderboardB = new List<(string Name, DateTime Time)>();

            foreach (var member in data.members.Select(kvp => kvp.Value))
            {
                if (member.completion_day_level.TryGetValue(day, out var dayResult))
                {

                    if (dayResult?.TryGetValue(1, out var partResult) ?? false)
                    {
                        leaderboardA.Add((member.name, UnixTimeStampToDateTime(partResult.get_star_ts)));
                    }
                    
                    if (dayResult?.TryGetValue(2, out var partResult2) ?? false)
                    {
                        leaderboardB.Add((member.name, UnixTimeStampToDateTime(partResult2.get_star_ts)));
                    }
                }
            }

            leaderboardA.Sort((a, b) => a.Time.CompareTo(b.Time));
            leaderboardB.Sort((a, b) => a.Time.CompareTo(b.Time));

            if (leaderboardA.Any())
            {
                System.Console.WriteLine($"\nResults for Day {day} part 1\n");
                Console.WriteLine("|Name| Time (AWST)|");
                Console.WriteLine("|------------|-----------------|");
                foreach (var member in leaderboardA)
                {
                    // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
                    Console.WriteLine("|{0,-20}|{1,10}|",
                        member.Name,
                        member.Time);
                }
            }
            Console.WriteLine("\n  ");

            if (leaderboardA.Any())
            {
                System.Console.WriteLine($"\nResults for Day {day} part 2\n");
                
                Console.WriteLine("|Name| Time (AWST)|");
                Console.WriteLine("|------------|-----------------|");
                foreach (var member in leaderboardB)
                {
                    // System.Console.WriteLine($"{member.Name}\t\t{member.Time}");
                    Console.WriteLine("|{0,-20}|{1,10}|",
                        member.Name,
                        member.Time);
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
    }
}