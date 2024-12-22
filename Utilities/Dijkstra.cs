/*namespace Utilities;

public static class Dijkstra
{
    static (TCost, List<TPos>) FindCheapestPath<TCost, TPos, TState>(TState initialState,
                                                                     TCost initialCost,
                                                                     TCost maxCost,
                                                                     Func<TState, bool> endState,
                                                                     Func<TState, TCost, List<(TState state, TCost cost)>> getNeighbours
    ) where TCost: IComparable
    {
        var queue = new PriorityQueue<TState, TCost>();
        queue.Enqueue(initialState, initialCost);

        TCost lowestScore = maxCost;
        var bestPaths = new List<IEnumerable<Vector>>();
        var baseScores = new Dictionary<TState, TCost>();

        HashSet<TState> visited = new HashSet<TState>();
        void Enqueue(TState state, TCost score)
        {
            visited.Add(state);
            var currentScore = baseScores.GetValueOrDefault(state, maxCost);

            if (currentScore.CompareTo(score) >= 0)
            {
                baseScores[state] = score;
                queue.Enqueue(state, score);
            }
        }

        while (queue.TryDequeue(out TState element, out TCost priority))
        {
            var state = queue.Dequeue();
            if (priority.CompareTo(lowestScore) > 0)
            {
                continue;
            }

            if (endState(state))
            {
                if (priority.CompareTo(lowestScore) > 0)
                {
                    lowestScore = priority;
                }

                continue;
            }

            foreach (var candidate in getNeighbours(state, priority))
            {
                Enqueue(candidate.state, candidate.cost);
            }
             
        }  

        return (lowestScore, visited.Count);

    }

}*/