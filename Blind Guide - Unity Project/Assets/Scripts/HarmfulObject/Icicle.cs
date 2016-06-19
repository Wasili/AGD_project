using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour {

    Transform blindGuyTransform;
    GameObject BlindMan;
    public GameObject IcicleShattered;
    public GameObject IcicleIceShattered;
    public float shakeDistance = 2;
    public float fellDistance = 0.5f;
    public float shakeSpeed;
    public GameObject radiusChecker;
    public bool canBeFrozen, canBeMelted;
    bool frozen, collided = false;
    Transform myShatters;
    public bool isSpecial = false;
    public AudioClip icicleImpact;
    DataMetricObstacle dataMetric = new DataMetricObstacle();
    bool dataSend = false;
    bool vallen = false;

    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        dataMetric.obstacle = DataMetricObstacle.Obstacle.Icicle;
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }

    void Update()
    {
        if (myShatters != null)
        {
            myShatters.localScale -= new Vector3(Time.deltaTime * 0.2f, Time.deltaTime * 0.2f, 0);
            if (myShatters.localScale.x <= 0)
            {
                Destroy(myShatters.gameObject);
                Destroy(gameObject);
            }
        }

        if (BlindMan == null)
            BlindMan = GameObject.FindWithTag("Blindguy");

        if (Time.timeScale <= 0)
            return;

        if (GetComponent<Rigidbody2D>().gravityScale > 0 || frozen)
            return;

        // Trillen
        if (Vector2.Distance(transform.position, BlindMan.transform.position) <= shakeDistance)
        {
            transform.position = new Vector3((transform.position.x + (Mathf.Sin(Time.time * shakeSpeed))* Time.deltaTime), transform.position.y, transform.position.z) ; 
            // 0.05 Zodat de sinus korter is. en dus op schudden lijkt
        }
        // Vallen
        if (Vector2.Distance(radiusChecker.transform.position, BlindMan.transform.position) <= fellDistance)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.tag = "PullableObject";
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            vallen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireAttack" && canBeMelted && !dataSend)
        {
            dataSend = true;
            dataMetric.howItDied = "Fire";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            //if (isSpecial)
            //{
            myShatters = ((GameObject)Instantiate(IcicleShattered, transform.position, transform.rotation)).transform;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
            //}
            //else 
            //    Destroy(gameObject);
        }

        if (col.gameObject.tag == "IceAttack" && canBeFrozen && !frozen && !dataSend)
        {
            gameObject.tag = "PullableObject";
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            dataSend = true;
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            if (isSpecial)
            {
                canBeMelted = true;
                canBeFrozen = false;
            }
            else
            {
                frozen = true;
                playSound(icicleImpact);
            }
            GetComponent<SpriteRenderer>().color = Color.white;
        }



    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 200, 200, 0.3f);
        Gizmos.DrawSphere(radiusChecker.transform.position, fellDistance);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!collided)
        {
            playSound(icicleImpact);

            /*
            dataMetric.howItDied = "Telekinesis";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            */
            if (frozen) myShatters = ((GameObject)Instantiate(IcicleIceShattered, transform.position, transform.rotation)).transform;

            else myShatters = ((GameObject)Instantiate(IcicleShattered, transform.position, transform.rotation)).transform;

            dataMetric.defeatedTime = Time.time.ToString();
            dataMetric.howItDied = "Telekinesis";
            dataMetric.saveLocalData();
            Destroy(gameObject);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            collided = true;
        }
    }

    public void playSound(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }

    void OnBecameInvisible()
    {
		try {
			GetComponent<AudioSource>().Stop();
		}
		catch(MissingComponentException e) {

		}

        //metric data set to thrown behind blind guy
        if (transform.position.x < blindGuyTransform.position.x)
        {
            dataMetric.defeatedTime = Time.time.ToString();
            dataMetric.howItDied = "Telekinesis";
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }
}
