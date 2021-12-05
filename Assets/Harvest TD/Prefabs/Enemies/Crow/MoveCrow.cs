using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrow : MonoBehaviour
{
    public Vector3 directionOfTarget;
    public Transform target, wPoint1;
	public GameObject endPoint, crowPoint, deathRattle;
    private int waypointIndex;
    public Waypoints Wpoint;
	public int speed;
    public float rotationZ;
    public bool killed = false;
    [SerializeField] private GameObject spawnerDaddy;

    private void Start()
	{
        spawnerDaddy = GameObject.FindGameObjectWithTag("Spawner");
        Wpoint = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
		endPoint = GameObject.FindGameObjectWithTag("EndPoint");
		crowPoint = GameObject.FindGameObjectWithTag("CrowPoint");
		target = endPoint.transform;
		wPoint1 = crowPoint.transform;
		gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
	}
	private void Update()
	{
		transform.position = Vector2.MoveTowards(transform.position, Wpoint.crowWaypoints[waypointIndex].position, speed * Time.deltaTime);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            waypointIndex++;
            directionOfTarget = (target.position - wPoint1.position).normalized;
            rotationZ = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 90);
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
