using UnityEngine;
using CodeMonkey.Utils;
using System.Collections.Generic;

public class EnemyController : Actor
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private ActorMovements _actorMovements;
    private PathFinding pathFinding;
    private void Start()
    {
        pathFinding = new PathFinding(31, 17);
    }

    private void Update()
    {
        pathFinding.GetGrid().GetXY(transform.position, out int xStart, out int yStart);
        pathFinding.GetGrid().GetXY(_player.gameObject.transform.position, out int xEnd, out int yEnd);

        List<PathNode> path = pathFinding.FindPath(xStart, yStart, xEnd, yEnd);
        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X - 11, path[i].Y) + Vector3.one,
                    new Vector3(path[i + 1].X - 11, path[i + 1].Y) + Vector3.one, Color.green);
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
        onDeath.Invoke(this);
        Destroy(gameObject);
    }
}
