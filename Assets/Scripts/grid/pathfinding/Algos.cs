using System;
using System.Collections.Generic;
using System.Linq;
using grid.points;
using tools;
using UnityEngine;

namespace grid.pathfinding {
    public abstract class Algos {
        public static List<IPoint> Bfs(IPoint start,
            Func<IPoint, bool> isSatisfies,
            int watchdog = 3000
        ) {
            Queue<IPoint> pending = new();
            HashSet<IPoint> visited = new();
            Dictionary<IPoint, IPoint> parents = new();

            pending.Enqueue(start);

            while (pending.Count > 0) {
                IPoint current = pending.Dequeue();
                if (isSatisfies(current) || --watchdog <= 0) {
                    List<IPoint> path = new() { current };
                    while (parents.ContainsKey(path[^1])) {
                        path.Add(parents[path[^1]]);
                    }

                    path.Reverse();

                    return path;
                }

                visited.Add(current);
                foreach (IPoint child in current.GetNeighborsPoints().Where(child => !visited.Contains(child) && !pending.Contains(child))) {
                    pending.Enqueue(child);
                    parents[child] = current;
                }
            }

            return new List<IPoint>();
        }

        public static List<IPoint> Astar(IPoint start,
            Func<IPoint, bool> isSatisfies,
            Func<IPoint, float> getCost,
            Func<IPoint, float> heuristic,
            int watchdog = 3000
        ) {
            PriorityQueue<IPoint> pending = new();
            HashSet<IPoint> visited = new();
            Dictionary<IPoint, IPoint> parents = new();
            Dictionary<IPoint, float> cost = new();

            pending.Enqueue(start, 0);
            cost[start] = 0;
            while (!pending.IsEmpty) {
                IPoint current = pending.Dequeue();
                if (isSatisfies(current) || --watchdog <= 0) {
                    List<IPoint> path = new() { current };

                    while (parents.ContainsKey(path[^1])) {
                        path.Add(parents[path[^1]]);
                    }

                    path.Reverse();
                    return path;
                }

                visited.Add(current);
                foreach (IPoint child in current.GetNeighborsPoints()) {
                    if (visited.Contains(child)) continue;
                    float currentCost = cost[current] + getCost(current);
                    if (cost.ContainsKey(child) && currentCost >= cost[child]) continue;
                    cost[child] = currentCost;
                    pending.Enqueue(child, currentCost + heuristic(child));
                    parents[child] = current;
                }
            }

            return new List<IPoint>();
        }
    }
}