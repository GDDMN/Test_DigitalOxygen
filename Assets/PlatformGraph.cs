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
    public Vertex start;
    public Vertex fin;

    private void Start()
    {
        Vertex[] path = GetPath(start, fin);

        Debug.LogWarning("path: ");
        foreach(var p in path)
        {
            Debug.LogWarning($"{p.name}");
        }
    }

    public Vertex[] GetPath(Vertex start, Vertex fin)
    {
        List<WeightedVertex> weightVerts = new List<WeightedVertex>();
        List<WeightedVertex> returnTrip = new List<WeightedVertex>();
        int weight = 0;
        Vertex[] path;
        WeightedVertex firstVert = new WeightedVertex();
        
        weightVerts.Add(firstVert);
        weightVerts[0].vertex = start;
        weightVerts[0].weight = weight;

        for (int i=0; weightVerts[i].vertex != fin; i++)
        {
            List<WeightedVertex> reachable = GetWeightVertex(weightVerts, weightVerts[i], ref weight);

            foreach(var wv in reachable)
                weightVerts.Add(wv);
        }

        returnTrip.Add(weightVerts.Last<WeightedVertex>());

        for(int i=weightVerts.Count-1;i >= 0; i--)
        {
            if (returnTrip.Last<WeightedVertex>().weight != weightVerts[i].weight)
                returnTrip.Add(weightVerts[i]);
        }

        path = new Vertex[returnTrip.Count];
        
        for(int i=0;i<path.Length;i++)
        {
            path[i] = returnTrip[i].vertex;
        }

        Array.Reverse(path);
        return path;
    }

    private List<WeightedVertex> GetWeightVertex(List<WeightedVertex> _allWeightVertexes, WeightedVertex weightVertex, ref int weight)
    {
        weight += 1;
        List<WeightedVertex> wv = new List<WeightedVertex>();

        foreach (var v in weightVertex.vertex.GetReachable())
        {
            if (_allWeightVertexes.Find(w => w.vertex == v) != null)
                continue;

            WeightedVertex vertex = new WeightedVertex();
            vertex.vertex = v;
            vertex.weight = weight;
            wv.Add(vertex);
        }

        return wv;
    }

}
