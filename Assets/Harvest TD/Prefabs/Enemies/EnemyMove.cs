using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Waypoints Wpoint;
	public int waypointIndex;
	public float speed;
    public GameObject deathRattle;
    public bool killed = false;
    [SerializeField] private GameObject spawnerDaddy;

    private void Start()
	{
        spawnerDaddy = GameObject.FindGameObjectWithTag("Spawner");
        Wpoint = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
	}
	private void Update()
	{
        transform.position = Vector2.MoveTowards(transform.position, Wpoint.waypoints[waypointIndex].position, speed * Time.deltaTime);
        if (waypointIndex == 1 || waypointIndex == 11)//Face down
        {
            //sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            //cotton.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);


        }
        if (waypointIndex == 0 || waypointIndex == 2 || waypointIndex == 6 || waypointIndex == 8 || waypointIndex == 10 || waypointIndex == 12)//Face right
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (waypointIndex == 4)//Face left
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (waypointIndex == 3 || waypointIndex == 5 || waypointIndex == 7 || waypointIndex == 9)//Face up
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            waypointIndex++;
        }
        if (collision.gameObject.layer == 13)
        {
            Debug.Log("Ded");
            RubyController player = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
            player.ChangeHealth(-999);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            RubyController player = collision.gameObject.GetComponent<RubyController>();
            if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }
    }
    public void GotJanked()
    {
        killed = true;
    }

    private void OnDestroy()
    {
        spawnerDaddy.GetComponent<EnemyMonitor>().CheckEnemies(1);
        if (killed)
		{
            Instantiate(deathRattle);
		}
    }
}
