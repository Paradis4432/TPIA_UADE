using System.Collections.Generic;
using entities;
using entities.enemies;
using UnityEngine;

namespace grid.points.impls {
    public class Point : MonoBehaviour, IPoint {
        public readonly List<IPoint> Neighbors = new();

        private void Start() {
            RefreshPoints();

            //GetComponent<MeshRenderer>().enabled = false;
        }

        public List<IPoint> GetNeighborsPoints() {
            return Neighbors;
        }

        public void GetNeighborsPoints(Vector3 dir) {
            for (int i = 0; i < GridManager.Leaps; i++) {
                // find Leaps points
                if (!Physics.Raycast(transform.position, dir, out RaycastHit hit, GridManager.LeapAmount)) continue;
                Point point = hit.collider.GetComponent<Point>();
                if (point is null) continue;
                Neighbors.Add(point);

                //Debug.Log("found neighbor: " + point + " from " + this);
            }
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        private void RefreshPoints() {
            GetNeighborsPoints(Vector3.right);
            GetNeighborsPoints(Vector3.left);
            GetNeighborsPoints(Vector3.forward);
            GetNeighborsPoints(Vector3.back);
        }

        private void OnTriggerEnter(Collider other) {
            IEntity entity = other.GetComponent<IEntity>();
            if (entity != null) GridManager.Cache[entity] = this;
        }


        public override string ToString() {
            return "Point{" +
                   "neighbors=" + Neighbors +
                   "name=" + gameObject.name +
                   '}';
        }
    }
}