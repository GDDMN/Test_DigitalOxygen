using UnityEngine.Tilemaps;

public class EnemyController : Actor
{
    public PlayerController player;
    public ActorMovements actorMovements;
    public ActorAttack actorAttack;
    public Tilemap tilemap;
    public BehaviourTree behaviourTree;
    
    private void Awake()
    {
        Actor actor = this as Actor;
        behaviourTree.SetActor(ref actor);
        behaviourTree = behaviourTree.Clone();
        

        player = FindObjectOfType<PlayerController>();
        tilemap = FindObjectOfType<Tilemap>();
    }

    private void Start()
    {
        Hurt = false;
    }

    private void Update()
    {
        if (Hurt)
            return;

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

    public void Attack()
    {
        actorAttack.StartAttack();
    }

    public override void Death()
    {
        Hurt = true;
        if (vertex != null)
            vertex.RemoveActorAction(this);

        Destroy(gameObject);
    }
}
