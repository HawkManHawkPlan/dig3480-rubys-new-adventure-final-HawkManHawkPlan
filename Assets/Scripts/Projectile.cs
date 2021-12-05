using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int speed;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force* speed);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }


        Destroy(gameObject);
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Enemy")//Add crowenemy tag
        {
            MoveCrow crowScript = collision.GetComponent<MoveCrow>();

            if(crowScript != null)
			{
                crowScript.Invoke("GotJanked", 0);
			}
			else
			{
                EnemyMove enemyScript = collision.GetComponent<EnemyMove>();
                enemyScript.Invoke("GotJanked", 0);
			}
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
