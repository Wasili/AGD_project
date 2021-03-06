﻿using UnityEngine;
using System.Collections;

public class Lavabal : MonoBehaviour {
    public float speed, maxHeight = 2, downwardSpeed = 6f, upwardSpeed = 5f, minYVelocity = -2f;
    bool going;
    Transform blindTransform;
    DataMetricObstacle dataMetric = new DataMetricObstacle();

    void Start()
    {
		GetComponent<Rigidbody2D>().velocity = (blindTransform.position - transform.position).normalized * speed;
		GetComponent<Rigidbody2D>().velocity += new Vector2(0, upwardSpeed);
        dataMetric.obstacle = DataMetricObstacle.Obstacle.LavaBall;
        dataMetric.spawnTime = Time.timeSinceLevelLoad;
    }

    void Awake()
    {
        blindTransform = GameObject.FindWithTag("Blindguy").transform;
    }

    void Update()
    {
		if (GetComponent<Rigidbody2D>().velocity.y > minYVelocity)
			GetComponent<Rigidbody2D>().velocity -= new Vector2(0, downwardSpeed * 

Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D ice)
    {
        if (ice.gameObject.tag == "IceAttack")
        {
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad;
            DataCollector datacoll = DataCollector.getInstance();
            datacoll.createObstacle(dataMetric);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            dataMetric.howItDied = "Telekinesis";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad;
            DataCollector datacoll = DataCollector.getInstance();
            datacoll.createObstacle(dataMetric);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        dataMetric.howItDied = "Telekinesis";
        dataMetric.defeatedTime = Time.timeSinceLevelLoad;
        Destroy(gameObject);
    }
}