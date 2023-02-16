using UnityEngine.Tilemaps;

public class EnemyController : Actor
{
    public PlayerController player;
    public ActorMovements actorMovements;
    public Tilemap tilemap;
    public BehaviourTree behaviourTree;
    public UnityEngine.Transform nextStepPont;

    private void Awake()
    {
        Actor actor = this as Actor;
        behaviourTree.SetActor(ref actor);
        behaviourTree = behaviourTree.Clone();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        behaviourTree.Update();
    }

    private void FixedUpdate()
    {
        if (actorMovements.IsJumping)
            actorMovements.JumpAnimation();
    }

    public void Run(float direction)
    {
        actorMovements.Run(direction);
    }

    public void Jump()
    {
        actorMovements.Jump();
    }
    public override void Death()
    {
        if(vertex != null)
            vertex.RemoveActorAction(this);
        
        Destroy(gameObject);
    }
}
