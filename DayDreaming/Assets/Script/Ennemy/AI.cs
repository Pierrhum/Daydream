using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent Agent;
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
        Collider = GetComponentInChildren<CircleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody.freezeRotation = true;
    }

    public void Move(Vector3 target)
    {
        Agent.SetDestination(target);
    }

    public void Stop()
    {
        Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

}
