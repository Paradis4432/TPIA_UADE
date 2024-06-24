using System.Collections.Generic;
using entities;
using UnityEngine;

namespace grid.points.impls {
    public class Point : MonoBehaviour, IPoint {
        private readonly List<IPoint> _neighbors = new();
        public float weight = 0;

        private void Start() {
            RefreshPoints();

            //GetComponent<MeshRenderer>().enabled = false;
        }

        public IEnumerable<IPoint> GetNeighborsPoints() {
            return _neighbors;
        }

        public void GetNeighborsPoints(Vector3 dir) {
            for (int i = 0; i < GridManager.Leaps; i++) {
                // find Leaps points
                if (!Physics.Raycast(transform.position, dir, out RaycastHit hit, GridManager.LeapAmount)) continue;
                Point point = hit.collider.GetComponent<Point>();
                if (point is null) continue;
                _neighbors.Add(point);

                //Debug.Log("found neighbor: " + point + " from " + this);
            }
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public float Weight {
            get => weight;
            set => weight = value;
        }

        private void RefreshPoints() {
            GetNeighborsPoints(Vector3.right);
            GetNeighborsPoints(Vector3.left);
            GetNeighborsPoints(Vector3.forward);
            GetNeighborsPoints(Vector3.back);
        }

        private void OnTriggerEnter(Collider other) {
            IEntity entity = other.GetComponent<IEntity>();
            if (entity != null) GridManager.SavePointForEntityCache(entity, this);
            //Debug.Log("registering:" + entity + " to:" + this);
        }


        public override string ToString() {
            return "Point{" +
                   "neighbors=" + _neighbors +
                   "name=" + gameObject.name +
                   '}';
        }
    }
}