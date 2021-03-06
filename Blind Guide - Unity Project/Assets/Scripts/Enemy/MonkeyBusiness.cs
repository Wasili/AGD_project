﻿using UnityEngine;
using System.Collections;

public class MonkeyBusiness : MonoBehaviour {

    public Sprite[] sprites;

    public float frameTime, deathFrameTime;
    SpriteRenderer spriteRenderer;
    float timer, deathAnimTimer;
    public GameObject coconut;
    float throwTimer;
    public float throwCooldown, reactionDistance;
    public AudioClip chimpanzee;
    public Sprite[] deathAnimation;
    int curDeathFrame = 0;
    Transform blindGuyTransform;

    bool dead = false;

    public float speed = 5f, throwHeight = 5f;
    public Vector3 throwOffset;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start () {        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        throwTimer = throwCooldown;
        deathAnimTimer = deathFrameTime;
        dataMetric.obstacle = DataMetricObstacle.Obstacle.Monkey;
    }

    void Awake()
    {
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }
	
	void Throw()
    {
        GameObject myCoconut = (GameObject)Instantiate(coconut, transform.position + new Vector3(-1, 0, 0), Quaternion.Euler(0, 0, 0));
        myCoconut.GetComponent<Coconut>().SetVelocity(blindGuyTransform.position, throwOffset, speed, throwHeight);
        timer = frameTime;
        spriteRenderer.sprite = sprites[1];
        throwTimer = throwCooldown;
    }

    void Update()
    {
        if (dead)
        {
            deathAnimTimer -= Time.deltaTime;
            if (deathAnimTimer <= 0 && curDeathFrame + 1 < deathAnimation.Length)
            {
                curDeathFrame += 1;
                deathAnimTimer = deathFrameTime;
            }
            spriteRenderer.sprite = deathAnimation[curDeathFrame];
            return;
        }

        throwTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            spriteRenderer.sprite = sprites[0];
        }

        if (throwTimer <= 0)
        {
            if (Vector2.Distance(transform.position, blindGuyTransform.position) <= reactionDistance)
            {
                Throw();
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack" && !dead)
        {
            dataMetric.howItDied = "Fire";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad;
            DataCollector datacoll = DataCollector.getInstance();
            datacoll.createObstacle(dataMetric);
            Die();
        }

        if (col.gameObject.GetComponent<Coconut>() != null && !dead)
        {
            if(col.gameObject.GetComponent<Coconut>().kill == true)
            {
                dataMetric.howItDied = "Destruction";
                dataMetric.defeatedTime = Time.timeSinceLevelLoad;
                DataCollector datacoll = DataCollector.getInstance();
                datacoll.createObstacle(dataMetric);
                Die();
            }
        }
    }

    void Die()
    {
        dead = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(transform.position, reactionDistance);
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad;
    }
}
