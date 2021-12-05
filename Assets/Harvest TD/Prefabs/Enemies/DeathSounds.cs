using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSounds : MonoBehaviour
{
    public AudioClip enemyDeath;
    public AudioSource audioSource;

    private void Start()
    {
        if (enemyDeath != null)
        {
            audioSource.PlayOneShot(enemyDeath, 1);
            Destroy(gameObject, enemyDeath.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
