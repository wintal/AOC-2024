/*namespace Utilities;

public static class Dijkstra
{
    static (TCost, List<TPos>) FindCheapestPath<TCost, TPos, TState>(TState initialState,
                                                        TCost initialCost,
                                                        Func<TState, TCost> costFromState
    )
    {
        var queue = new PriorityQueue<TState, TCost>();
        queue.Enqueue(initialState, initialCost);

        TCost? lowestScore = null;
        var bestPaths = new List<IEnumerable<Vector>>();
        var baseScores = new Dictionary<Vector, int>();

        HashSet<Vector> visited = new HashSet<Vector>();
        void Enqueue(TState state, int score)
        {
            visited.Add(position);
            var currentScore = baseScores.GetValueOrDefault(position, int.MaxValue);

            if (currentScore >= score)
            {
                baseScores[position] = score;
                queue.Enqueue(position, score, score);
            }
        }

        while (queue.TryDequeue(out TState element, out TCost priority))
        {
            var state = queue.Dequeue();
            if (priority > lowestScore)
            {
                continue;
            }

            if (state.pos == end)
            {
                if (state.score < lowestScore)
                {
                    lowestScore = state.score;
                }

                continue;
            }

            foreach (var dir in new[] { Vector.Left, Vector.Right, Vector.Up, Vector.Down })
            {
                var candidate = state.pos + dir;
                if (map.Contains(candidate) && map[candidate] != '#' && !visited.Contains(candidate))
                {
                    Enqueue(candidate, Vector.Right, state.score + 1);
                }
            }
        }  
        foreach (var path in bestPaths)
        {
            foreach (var pos in path)
            {
                visited.Add(pos);
            }
        }

        return (lowestScore, visited.Count);

    }

}*/