using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AI
{
    // Serialized Fields
    public float RangeOfAggression = 1.5f;
    public EnemyAssets asset;

    private void Awake()
    {
        if(asset != null)
        {
            Speed = asset.Speed;
            GetComponent<SpriteRenderer>().sprite = asset.Sprite;
            RangeOfAggression = asset.RangeOfAggression;
        }
    }
    void Update()
    {
        Vector2 PlayerDirection = Player.transform.position - this.transform.position;

        float PlayerDistance = PlayerDirection.magnitude;

        if (PlayerDistance < this.RangeOfAggression)
        {

            if (this.Collider.IsTouching(this.PlayerCollider)) LoadFight();

            else Move(PlayerDirection);
        }

        else Stop();
    }

    void LoadFight()
    {
        Stop();
        Debug.Log("Fight");
    }

}
