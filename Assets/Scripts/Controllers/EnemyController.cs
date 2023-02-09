using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;
using System.Collections.Generic;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ActorMovements _actorMovements;
    [SerializeField] private Tilemap tilemap;

    private List<Vertex> graph = new List<Vertex>();
    private PlatformGraph _platformGraph;
    private PathFinding pathFinding;
    
    private void Start()
    {
        _platformGraph = FindObjectOfType<PlatformGraph>();
        pathFinding = new PathFinding(34, 20, tilemap);

        _player.groundedOnPlatform += FindGraph;
        this.groundedOnPlatform += FindGraph;
    }

    private void Update()
    {
        if (_player == null || graph.Count == 0)
            return;

        if (graph.Contains(this.vertex))
            graph.Remove(this.vertex);

        pathFinding.GetGrid().GetXY(transform.position - new Vector3(0f, 1f, 0f), out int xStart, out int yStart);
        List<PathNode> path;

        if (graph.Count > 0)
        {
            pathFinding.GetGrid().GetXY(graph[0].transform.position - new Vector3(0f, 1f, 0f), out int xEnd, out int yEnd);
            path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        }
        else
        {
            pathFinding.GetGrid().GetXY(_player.transform.position - new Vector3(0f, 1f, 0f), out int xEnd, out int yEnd);
            path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        }

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X - 11, path[i].Y),
                    new Vector3(path[i + 1].X - 11, path[i + 1].Y), Color.green);
            }
        }
    }

    private void FindGraph()
    {
        if (_player.vertex == null || this.vertex == null)
            return;

        graph = _platformGraph.GetPath(this.vertex, _player.vertex);
    }

    private void FixedUpdate()
    {
        if (_actorMovements.IsJumping)
            _actorMovements.JumpAnimation();
    }

    private void Run(float direction)
    {
        _actorMovements.Run(direction);
    }

    private void Jump()
    {
        _actorMovements.Jump();
    }
    public override void Death()
    {
        if(vertex != null)
            vertex.RemoveActorAction(this);
        
        Destroy(gameObject);
    }
}
