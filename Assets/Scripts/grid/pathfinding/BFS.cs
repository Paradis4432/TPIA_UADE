using System;
using System.Collections.Generic;
using grid.points;
using UnityEngine;

namespace grid.pathfinding {
    public class Bfs {
        public static List<IPoint> Run(IPoint start, Func<IPoint, bool> isSatisfies,
            int watchdog = 0) {
            Queue<IPoint> pending = new();
            HashSet<IPoint> visited = new();
            Dictionary<IPoint, IPoint> parents = new();

            pending.Enqueue(start);
            while (pending.Count > 0) {
                Debug.Log(pending.Count);
                watchdog++;
                //if (watchdog <= 0) break;
                IPoint current = pending.Dequeue();
                if (isSatisfies(current)) {
                    List<IPoint> path = new() { current };
                    while (parents.ContainsKey(path[^1])) {
                        path.Add(parents[path[^1]]);
                    }

                    path.Reverse();

                    Debug.Log("watchdog: " + watchdog);
                    return path;
                }

                visited.Add(current);
                List<IPoint> connections = current.GetNeighborsPoints();
                for (int i = 0; i < connections.Count; i++) {
                    IPoint child = connections[i];
                    Debug.Log(visited.Contains(child));
                    if (visited.Contains(child)) continue;
                    pending.Enqueue(child);
                    parents[child] = current;
                }
            }

            Debug.Log("watchdog: " + watchdog);
            return new List<IPoint>();
        }
    }
}