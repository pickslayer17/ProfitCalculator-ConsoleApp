using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class DealTree
    {
        public Deal Deal { get; }
        private List<DealTree> _nodes;
        private DealTree _parent;
        public int Index { get; private set; } = 0;

        public DealTree(Deal deal)
        {
            Deal = deal;
            _nodes = new List<DealTree>();
        }

        public List<Deal[]> GetAllParentToChildDeals()
        {

            return null;
        }

        public static DealTree CreateTreeFromList(List<Deal> deals)
        {
            //Console.WriteLine("[TREE AT WORK]");
            DealTree dt = new DealTree(deals[0]);
            for (int i = 1; i < deals.Count; i++)
            {
                if (deals[i].DealIndex > deals[i - 1].DealIndex)
                {
                    if (deals[i].DealIndex - deals[i - 1].DealIndex > 1) throw new Exception("I hope never see it");

                    dt = dt.AddNode(deals[i]);
                }
                if (deals[i].DealIndex == deals[i - 1].DealIndex)
                {
                    dt = dt.GetParent();
                    dt = dt.AddNode(deals[i]);
                }
                if (deals[i].DealIndex < deals[i - 1].DealIndex)
                {
                    for (int j = 0; j < deals[i - 1].DealIndex - deals[i].DealIndex; j++)
                    {
                        dt = dt.GetParent();
                    }
                    dt = dt.GetParent();
                    dt = dt.AddNode(deals[i]);
                }
                //Console.WriteLine($"dt index [{dt.Index}] {dt.Deal}");

            }

            //Console.WriteLine("Childs count - " + dt.GetSuperParent().ChildsCount());
            //Console.WriteLine("[TREE ends work]");
            return dt.GetSuperParent();
        }

        public List<DealTree> GetAllLastChilds(List<DealTree> list)
        {

            foreach (var node in _nodes)
            {
                node.GetAllLastChilds(list);
            }

            if (!_nodes.Any())
            {
                list.Add(this);
            }
            return list;
        }
        public int ChildsCount()
        {

            int count = 0;
            foreach (var node in _nodes)
            {
                count += node.ChildsCount();
            }
            return _nodes.Count + count;
        }
        public DealTree GetSuperParent()
        {
            if (GetParent() == this) return this;
            return GetParent().GetSuperParent();
        }
        public DealTree GetParent()
        {
            return _parent ?? this;
        }
        public List<DealTree> GetChildren()
        {
            return _nodes;
        }

        public DealTree AddNode(Deal deal)
        {
            var node = new DealTree(deal);
            node.AddParent(this);
            _nodes.Add(node);
            node.SetIndex(Index + 1);
            return node;
        }
        private void SetIndex(int index)
        {
            Index = index;
        }
        private void AddParent(DealTree parent)
        {
            _parent = parent;
        }
    }
}
