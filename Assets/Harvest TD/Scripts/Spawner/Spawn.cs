using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Spawn : MonoBehaviour
{
	public GameObject nextEnemy;
	public float spawnCooldown = 1, timeTilSpawn = 0;
	public List<GameObject> enemies;
	public bool amEnabled = false;
	public Button playButton;
	public TMP_Text enemyCount;
	public int maxEnemies;

	private void OnEnable()
	{
		amEnabled = true;
		playButton.interactable = false;
		maxEnemies = enemies.Count-8;
		enemyCount.text = "Familiars Left: " + maxEnemies;

	}
	private void Update()
	{
		timeTilSpawn -= Time.deltaTime;
		if (timeTilSpawn <= 0 && amEnabled == true)
		{
			if (enemies.Count > 0)
			{
				nextEnemy = enemies[0];
				SpawnEnemy();
			}
			timeTilSpawn = spawnCooldown;
		}

	}
	void SpawnEnemy()
	{
		if (enemies.Count > 0)
		{
			Instantiate(nextEnemy, gameObject.transform);
			enemies.Remove(nextEnemy);
			if (enemies.Count != 0)
			{
				nextEnemy = enemies[0];
				//gameObject.GetComponent<EnemyMonitor>().Invoke("EnemyLog", 0);
			}
			if (enemies.Count == 0)
			{
				gameObject.GetComponent<EnemyMonitor>().Invoke("EnemyLog", 0);
			}
		}
	}
}

