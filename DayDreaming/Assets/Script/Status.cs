using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private Fighter Fighter;
    private Type type;
    private int amount;
    public enum Type { Heal, Hurt, Skip}

    public Status(Fighter Fighter, Type type, int amount=0)
    {
        this.Fighter = Fighter;
        this.type = type;
        this.amount = amount;
    }

    public void ApplyStatus()
    {
        switch(type)
        {
            case Type.Heal:
                Fighter.Heal(amount);
                break;
            case Type.Hurt:
                Fighter.Hurt(amount);
                break;
            case Type.Skip:
                Fighter.canPlay = false;
                break;
        }
    }
}
