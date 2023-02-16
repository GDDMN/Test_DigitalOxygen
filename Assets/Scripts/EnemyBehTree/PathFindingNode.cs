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
            float movementDirection = 0;

            if (path.Count > 2)
            {
                float direction = path[1].X - path[0].X;

                if (direction != 0)
                    movementDirection = direction / Mathf.Abs(direction);

                enemy.Run(movementDirection);

                float distance = 2.5f;
                Vector3 rayDirection = Vector3.down + (Vector3.right * direction);
                Ray ray = new Ray(enemy.transform.position, rayDirection);

                
                if(graph.Count > 0)
                {
                    if (((!Physics.Raycast(ray, distance)) && graph[0].transform.position.y > enemy.transform.position.y) || path[2].Y - path[0].Y > 1)
                        enemy.Jump();
                }else
                {
                    if (path[2].Y - path[0].Y > 1 || (!Physics.Raycast(ray, distance)))
                        enemy.Jump();
                }
            }
            else
            {
                enemy.Run(0f);
                return State.SUCCESS;
            }
        }
        return State.RUNNING;
    }
}
