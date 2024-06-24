using System;
using System.Collections.Generic;
using System.Linq;
using grid.points;
using UnityEngine;

namespace grid.pathfinding {
    public abstract class Bfs {
        public static List<IPoint> Run(IPoint start, Func<IPoint, bool> isSatisfies,
            int watchdog = 0) {
            Queue<IPoint> pending = new();
            HashSet<IPoint> visited = new();
            Dictionary<IPoint, IPoint> parents = new();

            pending.Enqueue(start);

            while (pending.Count > 0) {
                watchdog++;
                IPoint current = pending.Dequeue();
                if (isSatisfies(current)) {
                    List<IPoint> path = new() { current };
                    while (parents.ContainsKey(path[^1])) {
                        path.Add(parents[path[^1]]);
                    }

                    path.Reverse();

                    return path;
                }

                visited.Add(current);
                IEnumerable<IPoint> connections = current.GetNeighborsPoints();
                foreach (IPoint child in connections.Where(
                             child => !visited.Contains(child) && !pending.Contains(child))) {
                    pending.Enqueue(child);
                    parents[child] = current;
                }
            }

            return new List<IPoint>();
        }
    }
}