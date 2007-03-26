using System;
using System.Collections.Generic;
using System.Text;
using Tanis.Collections;
using AntHill.NET;

namespace astar
{
    class Astar
    {
        
        class AstarNode : IComparable
        {
            private AstarNode parent;

            public AstarNode Parent
            {
                get { return parent; }
                set { parent = value; }
            }


            private double cost;
            public double Cost
            {
                get { return cost; }
                set { cost = value; }
            }

            private double goalEstimate;

            public double GoalEstimate
            {
                get { return goalEstimate; }
                set { goalEstimate = value; }
            }

            public double TotalCost
            {
                get { return Cost + GoalEstimate; }

            }

            public int CompareTo(Object other)
            {
                if (!(other is AstarNode)) return -1;
                return -this.TotalCost.CompareTo(((AstarNode)other).TotalCost);
            }


            public AstarNode(KeyValuePair<int, int> loc, double weightCost)
            {
                this.loc = loc;
                cost = weightCost;
                if ((int)weightCost == int.MaxValue) isObstacle = true;
            }

            private bool isObstacle;

            public bool IsObstacle
            {
                get { return isObstacle; }
                set { isObstacle = value; }
            }

            private KeyValuePair<int, int> loc;

            public KeyValuePair<int, int> Loc
            {
                get { return loc; }
                set { loc = value; }
            }

            public void Calc(KeyValuePair<int, int> goal)
            {
                GoalEstimate = Math.Sqrt(Math.Pow(this.loc.Key - goal.Key, 2) + Math.Pow(this.loc.Value - goal.Value, 2));

            }

        }

        int[,] map;

        Heap OpenHeap = new Heap();
        List<AstarNode> Closed = new List<AstarNode>();
        System.Collections.Generic.Dictionary<KeyValuePair<int, int>, AstarNode> dict = new Dictionary<KeyValuePair<int, int>, AstarNode>();
        private int width;
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public Astar(int width, int height)
        {
            map = new int[width, height];
            this.width = width;
            this.height = height;
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    map[i, j] = 1;
                }
            }
        }

        int this[int x, int y]
        {
            get { return map[x, y]; }
            set { map[x, y] = value; }
        }

        public void Round(int x, int y, int r, int value)
        {
            for (int i = -r; i < r; i++)
            {
                for (int j = -r; j < r; j++)
                {
                    if ((i) * (i) + (j) * (j) < r * r)
                    {
                        map[x + i, y + j] = value;
                    }
                }
            }

        }

        List<KeyValuePair<int, int>> CreatePath(AstarNode last)
        {
            List<KeyValuePair<int, int>> path = new List<KeyValuePair<int, int>>();
            do
            {
                path.Add(last.Loc);
                last = (AstarNode)last.Parent;
            } while (last != null);

            path.Reverse();

            return path;
        }

        public void Point(int x, int y, int value)
        {
            map[x, y] = value;
        }

        public List<KeyValuePair<int, int>> Search(KeyValuePair<int, int> start, KeyValuePair<int, int> goal, IAstar ia )
        {
            //int counter = 0;
            OpenHeap.Clear();
            Closed.Clear();
            AstarNode StartNode = new AstarNode(start, map[start.Key, start.Value]);
            StartNode.Parent = null;
            StartNode.Calc(goal);
            dict.Clear();
            int idx;
            OpenHeap.Add(StartNode);
            dict.Add(StartNode.Loc, StartNode);
            bool inClosed;
            bool flag;
            while (OpenHeap.Count != 0)
            {
                AstarNode node = (AstarNode)OpenHeap.Pop();
                dict.Remove(node.Loc);
                if (node.Loc.Equals(goal))
                {
                    return CreatePath(node);
                }

                foreach (AstarNode other in NearNodes(node,ia))
                {
                    if (other.IsObstacle) { Closed.Add(other); continue; }
                    inClosed = false;
                    other.Calc(goal);
                    flag = false;
                    if (dict.ContainsKey(other.Loc))
                    {
                        if (dict[other.Loc].TotalCost < other.TotalCost) continue;
                        flag = true;
                    }
                    else if (Closed.Contains(other))
                    {
                        inClosed = true;
                        idx = Closed.IndexOf(other);
                        if (Closed[idx].TotalCost < other.TotalCost) continue;
                    }
                    other.Parent = node;
                    if (inClosed) Closed.Remove(other);
                    else
                    {
                        if (flag)
                        {
                            OpenHeap.Remove(dict[other.Loc]);
                            dict.Remove(other.Loc);
                        }

                        OpenHeap.Add(other);
                        dict.Add(other.Loc, other);
                    }

                }//foreach
                Closed.Add(node);
            }

            return null;
        }

        List<AstarNode> NearNodes(AstarNode center, IAstar ia)
        {
            List<AstarNode> list = new List<AstarNode>();
            KeyValuePair<int, int> loc;
            //if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key - 1, center.Loc.Value - 1)))
            //{
            //    list.Add(new AstarNode(loc, map[center.Loc.Key - 1, center.Loc.Value - 1]));
            //}
            if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key, center.Loc.Value - 1)))
            {
                list.Add(new AstarNode(loc,ia.GetWeight(center.Loc.Key, center.Loc.Value - 1)));
            }
            //if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key + 1, center.Loc.Value - 1)))
            //{
            //    list.Add(new AstarNode(loc, map[center.Loc.Key + 1, center.Loc.Value - 1]));
            //}
            if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key - 1, center.Loc.Value)))
            {
                list.Add(new AstarNode(loc, ia.GetWeight(center.Loc.Key - 1, center.Loc.Value)));
            }
            if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key + 1, center.Loc.Value)))
            {
                list.Add(new AstarNode(loc, ia.GetWeight(center.Loc.Key + 1, center.Loc.Value)));
            }
            //if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key - 1, center.Loc.Value + 1)))
            //{
            //    list.Add(new AstarNode(loc, map[center.Loc.Key - 1, center.Loc.Value + 1]));
            //}
            if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key, center.Loc.Value + 1)))
            {
                list.Add(new AstarNode(loc, ia.GetWeight(center.Loc.Key, center.Loc.Value + 1)));
            }
            //if (Inside(loc = new KeyValuePair<int, int>(center.Loc.Key + 1, center.Loc.Value + 1)))
            //{
            //    list.Add(new AstarNode(loc, map[center.Loc.Key + 1, center.Loc.Value + 1]));
            //}

            return list;
        }

        bool Inside(KeyValuePair<int, int> loc)
        {
            return loc.Key >= 0 && loc.Key < width && loc.Value >= 0 && loc.Value < height;
        }
    }
}
