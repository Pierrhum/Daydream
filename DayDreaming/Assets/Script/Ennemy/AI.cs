using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    protected CircleCollider2D Collider;
    protected CircleCollider2D PlayerCollider;
    protected Rigidbody2D Rigidbody;
    protected Player Player;

    // Serialized Fields
    public float Speed = 1.0f;

    void Start()
    {
        Player = GameManager.instance.player;
        PlayerCollider = Player.GetComponentInChildren<CircleCollider2D>();
        Collider = GetComponent<CircleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody.freezeRotation = true;
    }

    protected void Move(Vector2 Direction)
    {
        Rigidbody.velocity = Speed * Direction;
    }

    protected void Stop()
    {
        Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
