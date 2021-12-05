using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EnemyMonitor : MonoBehaviour
{
    public List<GameObject> enemiesInPlay;
    public bool levelOn = false;
    public GameObject manager;
    public TMP_Text enemyCount;
    public Text winTxt;
    public GameObject musicMan;
    public AudioClip winSong;
    int currentNumber;
	private void Awake()
	{
        currentNumber = FindObjectOfType<Spawn>().enemies.Count;
    }
    public void EnemyLog() //Get Spawner to invoke this after spawning last enemy in list
    {
        levelOn = true;
        Debug.Log("EnemyLog");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesInPlay.Add(enemy);
        }
    }
    private void Update()
    {
        enemiesInPlay.RemoveAll(GameObject => GameObject == null);
    }

    void FinishedLevel() //invoked by Move when destroyed if it's the last one 
    {
        //manager.GetComponent<ManagerFunctions>().Invoke("LevelDone", 0);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player.GetComponent<RubyController>().ded != true)
		{
            player.GetComponent<RubyController>().ween = true;
            winTxt.enabled = true;
            musicMan.GetComponent<AudioSource>().clip = winSong;
            musicMan.GetComponent<AudioSource>().Play();
            levelOn = false;
		}
    }

    public void CheckEnemies(int subtractor)
    {
        currentNumber -= subtractor;
        enemyCount.text = "Familiars Left: " + currentNumber;
        if (levelOn && currentNumber <= 0)
        {
            Debug.Log("LevelWin?");
            FinishedLevel();
        }
    }
}
