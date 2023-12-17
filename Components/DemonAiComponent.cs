using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;

class DemonAiComponent : Component
{
    enum State
    {
        Idle,
        Moving,
        Chasing,
        Attacking,
    }

    State state;

    public float detectionRadius { get; set; }
    public float attackRadius { get; set; }

    TransformComponent transform;
    SpriteComponent sprite;
    private Entity player;
    public DemonAiComponent()
    {
        state = State.Idle;
        detectionRadius = 400;
        attackRadius = 100;

        player = EntityManager.GetEntityOfType<Player>();
    }

    public override void Update(GameTime gameTime)
    {
        transform = entity.GetComponent<TransformComponent>();
        sprite = entity.GetComponent<SpriteComponent>();

        if (state == State.Idle)
        {
            if (Vector2.Distance(player.GetComponent<TransformComponent>().position, entity.GetComponent<TransformComponent>().position) < detectionRadius)
            {
                state = State.Chasing;
            }
            else{
            }
        }
        else if (state == State.Chasing)
        {

        }
        else if (state == State.Attacking)
        {

        }
        else if (state == State.Moving)
        {

        }

    }

    private void detectPlayer()
    {
        if (Vector2.Distance(player.GetComponent<TransformComponent>().position, entity.GetComponent<TransformComponent>().position) < detectionRadius)
        {
            state = State.Chasing;
        }
    }


}