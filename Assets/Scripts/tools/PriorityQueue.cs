using System;
using System.Collections.Generic;

namespace tools {
    public class PriorityQueue<TData> {
        public bool IsEmpty => _data.Count < 1;

        public int Count => _indexes.Count;

        private readonly List<Tuple<TData, float>> _data;
        private readonly Dictionary<TData, int> _indexes;
        private readonly Func<float, float, bool> _critery;

        public PriorityQueue() {
            _data = new List<Tuple<TData, float>>();
            _indexes = new Dictionary<TData, int>();
            _critery = (x, y) => x.CompareTo(y) < 0;
        }

        public PriorityQueue(Func<float, float, bool> critery) {
            _data = new List<Tuple<TData, float>>();
            _indexes = new Dictionary<TData, int>();
            _critery = critery;
        }

        public void Enqueue(TData d, float priority) {
            Enqueue(new Tuple<TData, float>(d, priority));
        }

        public void Enqueue(Tuple<TData, float> dp) {
            int currentIndex;
            int parentIndex;

            if (_indexes.TryGetValue(dp.Item1, out int index)) {
                currentIndex = index;
                parentIndex = (currentIndex - 1) / 2;

                if (_critery(_data[currentIndex].Item2, dp.Item2)) return;

                _data[currentIndex] = dp;
            }
            else {
                _data.Add(dp);

                currentIndex = _data.Count - 1;
                parentIndex = (currentIndex - 1) / 2;

                _indexes.Add(dp.Item1, currentIndex);
            }

            while (currentIndex > 0 && _critery(_data[currentIndex].Item2, _data[parentIndex].Item2)) {
                Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }

        private void EnqueueData(Tuple<TData, float> dp) {
            _data.Add(dp);

            int currentIndex = _data.Count - 1;
            int parentIndex = (currentIndex - 1) / 2;

            _indexes.Add(dp.Item1, currentIndex);

            while (currentIndex > 0 && _critery(_data[currentIndex].Item2, _data[parentIndex].Item2)) {
                Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }

        public TData Peek() {
            return PeekTuple().Item1;
        }

        public Tuple<TData, float> PeekTuple() {
            return _data[0];
        }

        public TData Dequeue() {
            return DequeueTuple().Item1;
        }

        public Tuple<TData, float> DequeueTuple() {
            Tuple<TData, float> date = _data[0];

            _data[0] = _data[^1];
            _indexes[_data[0].Item1] = 0;

            _data.RemoveAt(_data.Count - 1);
            _indexes.Remove(date.Item1);

            int currentIndex = 0;
            int leftIndex = 1;
            int rightIndex = 2;
            int explorIndex = GetExplorerIndex(leftIndex, rightIndex);


            if (explorIndex == -1) return date;

            while (_critery(_data[explorIndex].Item2, _data[currentIndex].Item2)) {
                Swap(explorIndex, currentIndex);

                currentIndex = explorIndex;
                leftIndex = (currentIndex * 2) + 1;
                rightIndex = (currentIndex * 2) + 2;
                explorIndex = GetExplorerIndex(leftIndex, rightIndex);

                if (explorIndex == -1) break;
            }

            return date;
        }

        private int GetExplorerIndex(int leftIndex, int rightIndex) {
            if (_data.Count > rightIndex)
                return _critery(_data[leftIndex].Item2, _data[rightIndex].Item2) ? leftIndex : rightIndex;
            if (_data.Count > leftIndex)
                return leftIndex;

            return -1;
        }

        private void Swap(int from, int to) {
            _indexes[_data[from].Item1] = to;
            _indexes[_data[to].Item1] = from;

            (_data[from], _data[to]) = (_data[to], _data[from]);
        }
    }
}