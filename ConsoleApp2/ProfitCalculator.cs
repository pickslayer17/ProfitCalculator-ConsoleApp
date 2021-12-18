using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class ProfitCalculator
    {
        LockableSequence _lockableSequence;
        List<Deal> _allDeals = new List<Deal>();
        int _dealCount;


        public ProfitCalculator(List<int> sequence)
        {
            _lockableSequence = new LockableSequence(sequence);
        }

        public int GetMaxProfit(int dealCount)
        {
            _dealCount = dealCount;
            GenerateListOfAllPossibleDeals(0, 1);
            var combos = SplitAllDealsToRealCombinatios();//each combo means sequence starts from "0" deal index

            List<Deal[]> allDealsArrays = new List<Deal[]>();
            for (int i = 0; i < combos.Count; i++)
            {
                var dealsArrays = GetDealsArraysFromCombo(combos[i]);
                allDealsArrays.AddRange(dealsArrays);
            }

            return FindMaxProfitFromAllDealsArrays(allDealsArrays);
        }

        private int FindMaxProfitFromAllDealsArrays(List<Deal[]> allDealsArrays)
        {
            int maxProfit = 0;
            Deal[] etalonDeals = new Deal[_dealCount];
            foreach (var dealsArray in allDealsArrays)
            {
                int profit = 0;

                for (int j = 0; j < dealsArray.Length; j++)
                {
                    profit += dealsArray[j].Profit;
                }
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    etalonDeals = dealsArray;
                }
            }


            Console.WriteLine("The max profit would be on next deals:\n");
            for (int i = 0; i < etalonDeals.Length; i++)
            {
                Console.WriteLine($"{etalonDeals[i]}");
            }

            return maxProfit;
        }

        private List<Deal[]> GetDealsArraysFromCombo(in List<Deal> inDeals)
        {
            DealTree dt = DealTree.CreateTreeFromList(inDeals);
            var lastChilds = dt.GetAllLastChilds(new List<DealTree>());
            return ConvertLastChildsToDealArrays(lastChilds);
        }

        private List<Deal[]> ConvertLastChildsToDealArrays(List<DealTree> lastChilds)
        {
            var list = new List<Deal[]>();
            for (int i = 0; i < lastChilds.Count; i++)
            {
                var child = lastChilds[i];
                var littleDeals = new Deal[_dealCount];
                for (int j = _dealCount - 1; j >= 0; j--)
                {
                    littleDeals[j] = child.Deal;
                    child = child.GetParent();
                }
                list.Add(littleDeals);
            }
            return list;
        }

        //return only combinations of deals that could be together
        //Every List<Deal> starts with "0" deal index and contains only one "0" index
        private List<List<Deal>> SplitAllDealsToRealCombinatios()
        {
            List<List<Deal>> Combos = new List<List<Deal>>();

            List<int> zeroIndexes = new List<int>();
            for (int i = 0; i < _allDeals.Count; i++)
            {
                if (_allDeals[i].DealIndex == 0)
                    zeroIndexes.Add(i);
            }
            zeroIndexes.Add(_allDeals.Count);//add last index for next for loop

            for (int i = 0; i < zeroIndexes.Count - 1; i++)
            {
                var combination = new List<Deal>();
                for (int j = zeroIndexes[i]; j < zeroIndexes[i + 1]; j++)
                {
                    combination.Add(_allDeals[j]);
                }
                Combos.Add(combination);
            }

            Console.WriteLine("-------------------");
            foreach (var combo in Combos)
            {
                foreach (var deal in combo)
                {
                    Console.WriteLine(deal);
                }
                Console.WriteLine("*****");
            }

            return Combos;


        }

        //Save deals to _allDeals
        //Recursive function. Fills _allDeals list. Should be intitally called with dealIndex = 0 and jIndex 1
        //returns a recursive tree which contains all variations for 1, 2, 3 and more _dealCount but without crossing
        private void GenerateListOfAllPossibleDeals(int dealIndex, int jIndex)
        {
            if (dealIndex >= _dealCount) return;

            _lockableSequence.UnlockAll();
            if (dealIndex != 0)
            {
                _lockableSequence.LockRangeFromStartToIndex(jIndex);
            }
            //need to be locked on every step because of UnlockAll method
            var endIndent = (_dealCount - (dealIndex + 1)) * 2;
            _lockableSequence.LockRangeFromIndexToTheEnd(_lockableSequence.Length - endIndent);

            if (!_lockableSequence.IsFullyLocked())
            {
                var lastIIndex = _lockableSequence.GetLastIndex();
                for (int i = _lockableSequence.GetStartIndex(); i <= lastIIndex - 1; i++)
                {
                    //need to be locked on every step because of UnlockAll method
                    var endIndent1 = (_dealCount - (dealIndex + 1)) * 2;
                    _lockableSequence.LockRangeFromIndexToTheEnd(_lockableSequence.Length - endIndent1);

                    var lastJIndex = _lockableSequence.GetLastIndex();
                    for (int j = i + 1; j <= lastJIndex; j++)
                    {
                        //need to be locked on every step because of UnlockAll method
                        var endIndent2 = (_dealCount - (dealIndex + 1)) * 2;
                        _lockableSequence.LockRangeFromIndexToTheEnd(_lockableSequence.Length - endIndent2);


                        var deal = new Deal(_lockableSequence.GetValue(i), _lockableSequence.GetValue(j), dealIndex, i, j);
                        _allDeals.Add(deal);

                        Console.WriteLine(deal);

                        GenerateListOfAllPossibleDeals(dealIndex + 1, j);
                        _lockableSequence.UnlockAll();
                    }

                }
            }

        }

    }
}
