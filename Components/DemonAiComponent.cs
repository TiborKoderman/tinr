using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using tinr;
class DemonAiComponent : AiComponent
{
    TransformComponent transform;
    SpriteComponent sprite;
    public Entity player;

    Vector2 nextTile;

    public DemonAiComponent()
    {
        state = State.Idle;
        detectionRadius = 400;
        attackRadius = 100;

        player = EntityManager.GetEntityOfType<Player>();

        AiSystem.Register(this);
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
                    sprite.firerate = 0.5f;
                    // sprite.AddBullet();
                    Console.WriteLine("Attack");
                }
                break;
        }

    }




}