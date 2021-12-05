using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TheFarmer : MonoBehaviour
{
	/* -If no object in front of him, spawn gears
	 * -Talk to him to unlock "Play button"
	 * 
	 * 
	 * 
	 * 
	 */
	public GameObject speechBubble, gears;
	public Button playButton;
	public List<GameObject> farmerDialogue;
	public TMP_Text dialogueText;
	public Queue<string> dialogueQ;
	private bool talking = false;
	public bool spokenTo = false;
	public bool inPlay = false;
	public float spawnCooldown = 2, timeTilSpawn = 0;
	public Transform spawnPoint;
	public bool objInFront = false;
	private void Start()
	{
		dialogueQ = new Queue<string>();
	}
	public void FarmerTalk()
	{
		speechBubble.SetActive(false);
		talking = true;
		spokenTo = true;
		foreach (GameObject f in farmerDialogue)
		{
			f.SetActive(true);
		}
		dialogueText.text = "";
		Dialogue dialogue = FindObjectOfType<WizardLines>().wizardLines;
		foreach(string sentence in dialogue.sentences)
		{
			dialogueQ.Enqueue(sentence);
		}
		NextLineShow();
	}

	public void NextLineShow()
	{
		//Debug.Log("Trombone Noises");
		if (dialogueQ.Count == 0)
		{
			Debug.Log("Done");
			playButton.interactable = true;
			talking = false;
			foreach (GameObject f in farmerDialogue)
			{
				f.SetActive(false);
			}
			FindObjectOfType<RubyController>().speed = 5;
			return;
		}
		dialogueText.text = dialogueQ.Dequeue();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && talking)
		{
			NextLineShow();
		}
		timeTilSpawn -= Time.deltaTime;
		if(timeTilSpawn <=0)
		{
			if(inPlay && objInFront == false)
			{
				Instantiate(gears, spawnPoint);
				objInFront = true;
			}
			timeTilSpawn = spawnCooldown;
		}

	}
	public void SetinPlayTrue()
	{
		inPlay = true;
	}
}
