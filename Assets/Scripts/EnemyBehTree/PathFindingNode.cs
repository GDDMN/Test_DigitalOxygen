using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode : ActionNode
{
    private List<Vertex> graph = new List<Vertex>();
    private PlatformGraph platformGraph;
    private PathFinding pathFinding;
    [SerializeField] private EnemyController enemy;

    public override void SetActor(ref Actor actor)
    {        
        enemy = actor as EnemyController;
    }

    public void FindGraph()
    {
        if (enemy.player.vertex == null || enemy.vertex == null)
            return;

        graph = platformGraph.GetPath(enemy.vertex, enemy.player.vertex);
    }

    protected override void OnStart()
    {
        platformGraph = FindObjectOfType<PlatformGraph>();
        pathFinding = new PathFinding(34, 20, enemy.tilemap);

        enemy.groundedOnPlatform += FindGraph;
        enemy.player.groundedOnPlatform += FindGraph;
    }

    protected override void OnStop()
    {
        enemy.groundedOnPlatform -= FindGraph;
        enemy.player.groundedOnPlatform -= FindGraph;
    }

    protected override State OnUpdate()
    {
        if (enemy.player == null)
            return State.FAILURE;

        pathFinding.GetGrid().GetXY(enemy.transform.position - new Vector3(0f, 1f, 0f), out int xStart, out int yStart);
        List<PathNode> path;

        if (graph.Contains(enemy.vertex))
            graph.Remove(enemy.vertex);


        if (graph.Count > 0)
        {
            pathFinding.GetGrid().GetXY(graph[0].transform.position - new Vector3(0f, 1f, 0f), out int xEnd, out int yEnd);
            path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        }
        else
        {
            pathFinding.GetGrid().GetXY(enemy.player.transform.position - new Vector3(0f, 1f, 0f), out int xEnd, out int yEnd);
            path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        }

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X - 11, path[i].Y),
                    new Vector3(path[i + 1].X - 11, path[i + 1].Y), Color.green);
            }

            float movementDirection = 0;

            if (path.Count > 3)
            {
                float direction = path[1].X - path[0].X;

                if (direction != 0)
                    movementDirection = direction / Mathf.Abs(direction);

                if (path[2].Y - path[0].Y > 1)
                    enemy.Jump();
            }

            enemy.Run(movementDirection);

            if (graph.Count > 0)
            {
                for (int i = 0; i < graph.Count - 1; i++)
                {
                    Debug.DrawLine(graph[i].transform.position,
                                   graph[1 + i].transform.position, Color.green);
                }
            }
        }
        return State.RUNNING;
    }


}