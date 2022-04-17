using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AI
{
    // Serialized Fields
    public float RangeOfAggression = 1.5f;
    public EnemyAssets asset;

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        if(asset != null)
        {
            Speed = asset.Speed;
            if (SpriteRenderer == null)
                GetComponent<SpriteRenderer>().sprite = asset.Sprite;
            else SpriteRenderer.sprite = asset.Sprite;
            RangeOfAggression = asset.RangeOfAggression;
        }
    }
    void Update()
    {
        if (!Player.isFighting)
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
    }

    void LoadFight()
    {
        Stop();
        Player.isFighting = true;
        GameManager.instance.uiManager.OpenFightMenu();
    }

}
