using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WeightedVertex
{
    public int weight;
    public Vertex vertex;
}

public class PlatformGraph : MonoBehaviour
{
    [SerializeField] private List<Vertex> _allVertexes = new List<Vertex>();

    private void Start()
    {
    }

    public List<Vertex> GetPath(Vertex start, Vertex fin)
    {
        return CalculatePath(start, fin);
    }

    private List<Vertex> CalculatePath(Vertex start, Vertex fin)
    {
        List<WeightedVertex> weightVerts = new List<WeightedVertex>();
        List<WeightedVertex> returnTrip = new List<WeightedVertex>();
        int weight = 0;
        List<Vertex> path;
        WeightedVertex firstVert = new WeightedVertex();
        
        weightVerts.Add(firstVert);
        weightVerts[0].vertex = start;
        weightVerts[0].weight = weight;

        for (int i=0; weightVerts[i].vertex != fin; i++)
        {
            List<WeightedVertex> reachable = GetWeightVertex(weightVerts, weightVerts[i], weightVerts[i].weight+1);

            foreach(var wv in reachable)
                weightVerts.Add(wv);
        }

        int finVertIndex = weightVerts.IndexOf(weightVerts.Find(w => w.vertex == fin));
        returnTrip.Add(weightVerts[finVertIndex]);
        
        for (int i=finVertIndex;i >= 0; i--)
        {
            if (returnTrip.Last<WeightedVertex>().weight > weightVerts[i].weight && 
                returnTrip.Last<WeightedVertex>().vertex.GetReachable().Contains(weightVerts[i].vertex))
            {
                returnTrip.Add(weightVerts[i]);
            }    
        }

        path = new List<Vertex>();

        foreach (var rt in returnTrip)
            path.Add(rt.vertex);

        path.Reverse();
        return path;
    }

    private List<WeightedVertex> GetWeightVertex(List<WeightedVertex> _allWeightVertexes, WeightedVertex weightVertex, int weight)
    {
        List<WeightedVertex> wv = new List<WeightedVertex>();

        List<Vertex> vertexes = new List<Vertex>();

        foreach(var v in _allWeightVertexes)
        {
            vertexes.Add(v.vertex);
        }

        foreach (var v in weightVertex.vertex.GetReachable())
        {
            if (vertexes.Find(w => w == v) != null)
                continue;

            WeightedVertex vertex = new WeightedVertex();
            vertex.vertex = v;
            vertex.weight = weight;
            wv.Add(vertex);
        }

        return wv;
    }

}
