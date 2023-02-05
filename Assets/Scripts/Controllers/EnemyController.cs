using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;
using System.Collections.Generic;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ActorMovements _actorMovements;
    [SerializeField] private Tilemap tilemap;

    private PathFinding pathFinding;
    
    private void Start()
    {
        pathFinding = new PathFinding(34, 20, tilemap);
    }

    private void Update()
    {
        if (_player == null)
            return; 

        pathFinding.GetGrid().GetXY(transform.position - new Vector3(0f, 1f, 0f), out int xStart, out int yStart);
        pathFinding.GetGrid().GetXY(_player.gameObject.transform.position - new Vector3(0f, 1f, 0f), out int xEnd, out int yEnd);

        List<PathNode> path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        if (path != null)
        {

            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X - 11, path[i].Y),
                    new Vector3(path[i + 1].X - 11, path[i + 1].Y), Color.green);
            }
        }
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
        //onDeath.Invoke(this);
        Destroy(gameObject);
    }
}
