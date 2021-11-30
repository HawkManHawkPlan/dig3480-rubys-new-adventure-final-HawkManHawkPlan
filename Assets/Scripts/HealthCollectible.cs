using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public GameObject healthFX;
    public int cogs = 0;
    public bool cogObj = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
			if (cogObj)
			{
                controller.ChangeCogs(cogs);
                Instantiate(healthFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5), healthFX.transform.rotation);//, fxTransform);

                Destroy(gameObject);

                controller.PlaySound(collectedClip);

            }
            if (cogObj == false && controller.health < controller.maxHealth)
            {
                //Transform fxTransform = gameObject.transform;
                //fxTransform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5);
                Instantiate(healthFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5), healthFX.transform.rotation);//, fxTransform);
                controller.ChangeHealth(1);
                
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }
        }

    }
}
