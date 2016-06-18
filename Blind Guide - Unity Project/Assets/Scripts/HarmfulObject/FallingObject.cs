using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {
    Transform blindGuyTransform;
    GameObject blindGuy;
    bool falling = false;
    public float reactionDistance, rotationSpeed, maxAngle;
    public GameObject baseObject;
    public Sprite frozenBase, frozenRock;
    float rotation;
    float timer;
    bool frozen = false;
    DataMetricObstacle dataMetric = new DataMetricObstacle();
    bool dataSend = false;

    void Start () 
    {
        blindGuy = GameObject.FindWithTag("Blindguy");
        rotation = -rotationSpeed;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        dataMetric.obstacle = DataMetricObstacle.Obstacle.FallingRock;
        blindGuyTransform = GameObject.FindWithTag("Blindguy").transform;
    }
	
	void Update () 
    {
        if (frozen)
            return;

        if (blindGuy == null)
            blindGuy = GameObject.FindWithTag("Blindguy");

        if (Mathf.Abs(transform.position.x - blindGuy.transform.position.x) < reactionDistance || GetComponent<Rigidbody2D>().isKinematic)
        {
            baseObject.SetActive(false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }

        timer += rotation * Time.deltaTime;

        if (timer > maxAngle)
        {
            rotation = -rotationSpeed;
            timer = maxAngle;
        }

        if (timer < -maxAngle)
        {
            rotation = rotationSpeed;
            timer = -maxAngle;
        }

        if (GetComponent<Rigidbody2D>().gravityScale <= 0)
        {
            transform.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
        }

        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "IceAttack" && GetComponent<Rigidbody2D>().gravityScale <= 0 && !dataSend)
        {
            dataSend = true;
            dataMetric.howItDied = "Ice";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            frozen = true;
            baseObject.GetComponent<SpriteRenderer>().sprite = frozenBase;
            GetComponent<SpriteRenderer>().sprite = frozenRock;
            gameObject.tag = "Untagged";
        }

        if (col.gameObject.tag == "FireAttack" && GetComponent<Rigidbody2D>().gravityScale <= 0)
        {
            dataSend = true;
            dataMetric.howItDied = "Fire";
            dataMetric.defeatedTime = Time.timeSinceLevelLoad.ToString();
            dataMetric.saveLocalData();
            baseObject.SetActive(false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		}

    }

    void OnBecameVisible()
    {
        dataMetric.spawnTime = Time.timeSinceLevelLoad.ToString();
    }

    void OnBecameInvisible()
    {
        //metric data set to thrown behind blind guy
        if (transform.position.x < blindGuyTransform.position.x)
        {
            dataMetric.defeatedTime = Time.time.ToString();
            dataMetric.howItDied = "Telekinesis";
            dataMetric.saveLocalData();
            Destroy(gameObject);
        }
    }
}
