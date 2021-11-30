using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public int cogs = 4;
    public GameObject projectilePrefab, damageFX, musicMan;
    public bool ded = false,ween = false;
    public Text dedTxt, winTxt;
    public Text robotLog, cogText;
    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip winSong, loseSong;
    public int robotsFixed = 0;

    public int health { get { return currentHealth; } }
    public int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        dedTxt.enabled = false;
        winTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && ded == false&&cogs >0)
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    
					if (ween)
					{
                        Debug.Log("Teleport");
                        winTxt.enabled = false;
                        ween = false;
                        SceneManager.LoadScene(1);
                    }
                    else
					{
                        character.DisplayDialog();
                    }
                }
            }
        }
		if (Input.GetKeyDown(KeyCode.R)&& ded || Input.GetKeyDown(KeyCode.R) && ween)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            Instantiate(damageFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5), damageFX.transform.rotation);//, fxTransform);
            animator.SetTrigger("Hit");


            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if(currentHealth <= 0 && !ween)
		{
            //Debug.Log("Ded");
            dedTxt.enabled = true;
            ded = true;
            speed = 0;
            musicMan.GetComponent<AudioSource>().clip = loseSong;
            musicMan.GetComponent<AudioSource>().Play();
        }
    }
    public void ChangeCogs(int amount)
	{
        cogs += amount;
        cogText.text = cogs.ToString();
	}

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);
        ChangeCogs(-1);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void FixBot()
    {
        robotsFixed += 1;
        robotLog.text = "Fixed Robots: " + (robotsFixed.ToString());
        if(robotsFixed == 4)
		{
            ween = true;
            winTxt.enabled = true;
            musicMan.GetComponent<AudioSource>().clip = winSong;
            musicMan.GetComponent<AudioSource>().Play();
		}
    }
}
