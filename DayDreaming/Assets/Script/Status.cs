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

    public Sprite GetSprite()
    {
        Texture2D tex = null;
        switch (type)
        {
            case Type.Heal:
                tex = Resources.Load<Texture2D>("Sprite/Status/Heal");
                break;
            case Type.Hurt:
                tex = Resources.Load<Texture2D>("Sprite/Status/poison");
                break;
            case Type.Skip:
                tex = Resources.Load<Texture2D>("Sprite/Status/debuff");
                break;
        }
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
    }
}
