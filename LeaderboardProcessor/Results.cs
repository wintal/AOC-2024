namespace LeaderboardProcessor
{
    public class Results
    {
        public int day1_ts { get; set; }
        public int owner_id { get; set; }
        public Dictionary<int, Member> members { get; set; }
    }

    public class Member
    {
        public int stars { get; set; }
        public int local_score { get; set; }
        public int global_score { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Dictionary<int, Dictionary<int, PartResult>> completion_day_level { get; set; }
    }

    public class DayResult
    {
        public Dictionary<int, PartResult> part_results { get; set; }
    }

    public class PartResult
    {
        public long get_star_ts { get; set; }
        public int star_index { get; set; }
    }
}