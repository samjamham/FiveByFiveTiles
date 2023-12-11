using UnityEngine;

public class WalkerObject
{
    public Vector2 Position;
    public Vector2 Direction;
    public float ChanceToChange;

    public WalkerObject() : this(Vector2.zero, Vector2.zero, 0)
    {

    }

    public WalkerObject(Vector2 pos, Vector2 dir, float chanceToChange){
        Position = pos;
        Direction = dir;
        ChanceToChange = chanceToChange;
    }
}
