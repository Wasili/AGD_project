﻿using UnityEngine;
using System.Collections;

public class SetDizzyDeath : MonoBehaviour {

    GameObject blindMan;
    public DataMetricObstacle.Obstacle obstacleType;

    void Awake()
    {
        blindMan = GameObject.FindWithTag("Blindguy");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Blindguy")
        {
            blindMan.GetComponent<BlindGuyAI>().SetDizzyDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Blindguy")
        {
            blindMan.GetComponent<BlindGuyAI>().SetDizzyDeath();
        }
    }
}
