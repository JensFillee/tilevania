using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;

    float xSpeed;
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // Find first object that has a PlayerMovement-component (?)
        player = FindObjectOfType<PlayerMovement>();

        // get x-scale of player (1 or -1) => shoot projectile in correct direction
        xSpeed = player.transform.localScale.x * projectileSpeed;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        // Destroy me (projectile)
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Destroy me (projectile)
        Destroy(gameObject);
    }
}
