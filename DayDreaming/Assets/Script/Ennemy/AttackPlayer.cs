using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackPlayer : MonoBehaviour {

	private Rigidbody2D SelfRigidbody;
	private CircleCollider2D SelfCollider;
	private GameObject PlayerGameObject;
	private CircleCollider2D PlayerCollider;
	private Vector2 PlayerDirection;
	private float PlayerDistance;
	private float RangeOfAggression;
	private float Speed;

    // Start is called before the first frame update
    void Start() {
        
        this.SelfRigidbody = this.GetComponent<Rigidbody2D>();
        this.SelfCollider = this.GetComponent<CircleCollider2D>();
        this.PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        this.PlayerCollider = this.PlayerGameObject.GetComponent<CircleCollider2D>();
        this.PlayerDirection = new Vector2(0.0f,0.0f);
        this.PlayerDistance = Mathf.Infinity;
        this.RangeOfAggression = 1.5f;
        this.Speed = 1.0f;

        this.SelfRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        
        this.PlayerDirection = this.PlayerGameObject.transform.position - this.transform.position;
        this.PlayerDistance = this.PlayerDirection.magnitude;

        if(this.PlayerDistance < this.RangeOfAggression) {

        	if(this.SelfCollider.IsTouching(this.PlayerCollider)) LoadFight();

        	else RunAtPlayer();
        }

        else SetStill();
    }
	
	void LoadFight() {

		SceneManager.LoadScene("FightMode");
	}

	void RunAtPlayer() {

		this.SelfRigidbody.velocity = this.Speed * this.PlayerDirection;
	}

	void SetStill() {

		this.SelfRigidbody.velocity = new Vector3(0.0f,0.0f,0.0f);
	}
}
