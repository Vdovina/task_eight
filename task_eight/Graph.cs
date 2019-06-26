using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_eight
{
    public class Graph
    {
        public Vertex head;
        public List<Vertex> Vertexes { get; set; }

        public Graph(int number, int[,] matrix)
        {
            Vertexes = new List<Vertex>();
            for (int i = 0; i < number; i++)
            {
                Vertex point = new Vertex(i + 1);
                Vertexes.Add(point);
            }
            head = Vertexes[0];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (i != j && matrix[i, j] == 1)
                    {
                        if (Vertexes[i] != null) Vertexes[i].MakeTie(Vertexes[j]);
                    }
            }

        }

        public override string ToString()
        {
            string s = "Вершины: ";
            foreach (Vertex peek in Vertexes)
                s += $"\n{peek.ToString()}";
            return s;
        }

        public void GoInDepth()
        {
            Vertex temp = head;
           
        }

        public bool DeapthSearch(int v, int f, HashSet<string> bridges, HashSet<string> ribs, bool cycle, out int from)
        {
            from = f;
            f = v;
            Vertexes[v].colour++;
            for (int i = 0; i < Vertexes.Count; i++)
            {
                bool k = ribs.Contains((v + 1).ToString() + (i + 1).ToString());
                if (i != v && ribs.Contains((v + 1).ToString() + (i + 1).ToString()) && Vertexes[i].colour == Colour.white)
                    cycle = DeapthSearch(i, f, bridges, ribs, cycle, out from);
                if (i != v && i != from && ribs.Contains((i + 1).ToString() + (v + 1).ToString()) && Vertexes[i].colour == Colour.grey)
                {
                    from = i;
                    return cycle = true;
                }
            }
            return cycle;
        }

        public HashSet<string> RemoveCycleRibs(int v, int f, HashSet<string> ribs, out int from)
        {
            from = f;
            f = v;
            ribs.Remove((from + 1).ToString() + (v + 1).ToString());
            ribs.Remove((v + 1).ToString() + (from + 1).ToString());
            for (int i = 0; i < Vertexes.Count; i++)
            {
                if (i != v && (ribs.Contains((v + 1).ToString() + (i + 1).ToString()) || ribs.Contains((i + 1).ToString() + (v + 1).ToString())))
                {
                    //ribs.Remove((v + 1).ToString() + (i + 1).ToString());
                    //ribs.Remove((i + 1).ToString() + (v + 1).ToString());
                    RemoveCycleRibs(i, f, ribs, out from);
                    if (v != from) return ribs;
                }
            }
            return ribs;
        }

        //public HashSet<string> Bridges (int v)
        //{

        //}

    }

    public class Vertex
    {
        public Colour colour;

        public List<Vertex> SubPoints { get; set; }

        public string Name { get; set; }

        public Vertex(int s)
        {
            colour = Colour.white;
            Name = s.ToString();
            SubPoints = new List<Vertex>();
        }

        public void MakeTie(Vertex point)
        {
            SubPoints.Add(point);
        }

        public int DepthSearch(string s)
        {
            int from = (int)this.colour, to = 1;
            if (SubPoints.Count != 0 && (int)colour < 3)
            {   
                for (int i = 0; i < this.SubPoints.Count; i++)
                {
                    to = SubPoints[i].DepthSearch(s);
                    colour++;
                    if (from == to && to == 3) s += $"{this.Name}";
                }
            }
            return to;
        }

        public override string ToString()
        {
            string s = Name.ToString() + ": связи с ";
            foreach (Vertex n in SubPoints)
                s += n.Name + " ";
            return s;
        }
    }

    public enum Colour { white  = 1, grey, black }
}
