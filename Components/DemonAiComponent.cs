using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using tinr;
class DemonAiComponent : Component
{
    protected static Random random = new();
    public enum State
    {
        Idle,
        Moving,
        Chasing,
        Attacking,
    }

    public State state;

    public float detectionRadius { get; set; }
    public float attackRadius { get; set; }
    TransformComponent transform;
    SpriteComponent sprite;
    public Entity player;

    Vector2 nextTile;
    private TimeSpan lastBulletTime;
    private int firerate;

    public DemonAiComponent()
    {
        state = State.Idle;
        detectionRadius = 300;
        attackRadius = 200;

        player = EntityManager.GetEntityOfType<Player>();

        DemonAiSystem.Register(this);
    }


    public override void Update(GameTime gameTime)
    {
        transform = entity.GetComponent<TransformComponent>();
        sprite = entity.GetComponent<SpriteComponent>();


        switch (state)
        {
            case State.Idle:
                //check if player is in detection radius
                if (Vector2.Distance(transform.position, player.GetComponent<TransformComponent>().position) < detectionRadius)
                {
                    state = State.Chasing;
                }
                else
                {
                    //move randomly
                    nextTile = new Vector2(transform.position.X + (random.Next(-1, 2) * 64), transform.position.Y + (random.Next(-1, 2) * 64));

                    state = State.Moving;
                }
                break;
            case State.Moving:
                //check if player is in detection radius
                if (Vector2.Distance(transform.position, player.GetComponent<TransformComponent>().position) < detectionRadius)
                {
                    state = State.Chasing;
                }
                else
                {
                    //move to next tile
                    if (Vector2.Distance(transform.position, nextTile) < 1)
                    {
                        state = State.Idle;
                    }
                    else
                    {
                        //move towards next tile
                        Vector2 direction = nextTile - transform.position;
                        direction.Normalize();
                        transform.position += direction * 2;
                    }
                }
                break;
            case State.Chasing:
                //check if player is in attack radius
                if (Vector2.Distance(transform.position, player.GetComponent<TransformComponent>().position) < attackRadius)
                {
                    state = State.Attacking;
                    //rotate towards player
                }
                else
                {
                    //move towards player
                    Vector2 direction = player.GetComponent<TransformComponent>().position - transform.position;
                    direction.Normalize();
                    transform.position += direction * 2;
                }
                break;
            case State.Attacking:
                //check if player is in detection radius
                if (Vector2.Distance(transform.position, player.GetComponent<TransformComponent>().position) > attackRadius)
                {
                    state = State.Chasing;
                }
                else
                {
                    //attack player
                    //rotate towards player
                    // float angle = (float)Math.Atan2(player.GetComponent<TransformComponent>().position.Y - transform.position.Y, player.GetComponent<TransformComponent>().position.X - transform.position.X);
                    Vector2 direction = player.GetComponent<TransformComponent>().position - transform.position;
                    // direction.Normalize();
                    float rotation = (float)Math.Atan2(direction.Y, direction.X) + (float)Math.PI / 2;
                    if (gameTime.TotalGameTime - lastBulletTime > TimeSpan.FromSeconds(1 / sprite.firerate))
                    {
                        sprite.AddBullet(rotation);
                        // Console.WriteLine("*bang*");
                        lastBulletTime = gameTime.TotalGameTime;
                    }
                    Console.WriteLine("Attacking");
                }
                break;
        }

    }




}