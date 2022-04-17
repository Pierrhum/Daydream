using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [System.NonSerialized]
    public CircleCollider2D Collider;
    [System.NonSerialized]
    public CircleCollider2D PlayerCollider;
    [System.NonSerialized]
    public Rigidbody2D Rigidbody;
    [System.NonSerialized]
    public Player Player;

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

    public void Move(Vector2 Direction)
    {
        Rigidbody.velocity = Speed * Direction;
    }

    public void Stop()
    {
        Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
