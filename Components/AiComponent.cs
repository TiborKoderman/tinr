using System;
using Microsoft.Xna.Framework;

public abstract class AiComponent : Component
{
    protected static Random random = new Random();
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


}