using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode : ActionNode
{
    private List<Vertex> graph = new List<Vertex>();
    private PlatformGraph platformGraph;
    private List<Vector3> path;
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
        {
            enemy.Run(0f);
            return State.FAILURE;
        }

        if (graph.Contains(enemy.vertex))
            graph.Remove(enemy.vertex);

        path = FindPathAStar();

        if (path != null)
        {
            float movementDirection = 0;

            if (path.Count > 2)
            {
                float direction = path[1].x - path[0].x;

                if (direction != 0)
                    movementDirection = direction / Mathf.Abs(direction);

                enemy.Run(movementDirection);

                float distance = 2.5f;
                Vector3 rayDirection = Vector3.down + (Vector3.right * direction);
                Ray ray = new Ray(enemy.transform.position, rayDirection);

                
                if(graph.Count > 0)
                {
                    if (((!Physics.Raycast(ray, distance)) && graph[0].transform.position.y > enemy.transform.position.y) || path[2].y - path[0].y > 1)
                        enemy.Jump();
                }else
                {
                    if (path[2].y - path[0].y > 1 || (!Physics.Raycast(ray, distance)))
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

    private List<Vector3> FindPathAStar()
    {
        if (graph.Count > 0)
            return PathFinding.Instance().FindPath(enemy.transform.position, graph[0].transform.position);
        else
            return PathFinding.Instance().FindPath(enemy.transform.position, enemy.player.transform.position);
    }
}
