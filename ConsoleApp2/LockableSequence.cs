using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class LockableSequence
    {
        private int[] _sequence;
        private bool[] _listOfLocked;
        private int[] _listOfIndexes;

        public LockableSequence(List<int> sequence)
        {
            _sequence = new List<int>(sequence).ToArray();
            _listOfLocked = new bool[sequence.Count];
            _listOfIndexes = new int[sequence.Count];
            for (int i = 0; i < _sequence.Length; i++)
            {
                _listOfLocked[i] = (false);
                _listOfIndexes[i] = i;
            }
        }

        public int GetLastIndex()
        {
            for (int i = _listOfLocked.Length - 1; i >= 0; i--)
            {
                if (_listOfLocked[i] == false)
                {
                    return i;
                }
            }

            throw new Exception("No end point. Every index is locked!");
        }
        public int GetStartIndex()
        {
            for (int i = 0; i < _listOfLocked.Length; i++)
            {
                if (_listOfLocked[i] == false)
                {
                    return i;
                }
            }

            throw new Exception("No start point. Every index is locked!");
        }

        public bool IsLocked(int index)
        {
            return _listOfLocked[index];
        }

        public bool IsFullyLocked()
        {
            for (int i = 0; i < _listOfLocked.Length; i++)
            {
                if (_listOfLocked[i] == false) return false;
            }
            return true;
        }
        public int Length
        {
            get
            {

                return _sequence.Length;
            }
        }
        public int GetValue(int index)
        {
            if (_listOfLocked[index] == false)
            {
                return _sequence[index];
            }
            throw new Exception($"Index {index} is locked!");
        }
        public void Lock(int index)
        {
            _listOfLocked[index] = true;
        }
        public void Unlock(int index)
        {
            _listOfLocked[index] = false;
        }
        public void LockRange(int startIndex, int endIndexIncl)
        {
            for (int i = startIndex; i <= endIndexIncl; i++)
            {
                _listOfLocked[i] = true;
            }
        }
        public void LockRangeFromStartToIndex(int endIndexIncl)
        {
            LockRange(0, endIndexIncl);
        }
        public void LockRangeFromIndexToTheEnd(int startIndex)
        {
            LockRange(startIndex, _listOfLocked.Length - 1);
        }
        public void UnlockAll()
        {
            for (int i = 0; i < _listOfLocked.Length; i++)
            {
                _listOfLocked[i] = false;
            }
        }


    }
}
