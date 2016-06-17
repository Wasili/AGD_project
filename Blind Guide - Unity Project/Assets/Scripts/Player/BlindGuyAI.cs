using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlindGuyAI : MonoBehaviour {
    public float stopTimer = 0;
    public float speed = 2;

    float regularSpeed;

    Vector3 positionOffset, blindGuySize;

    DataMetricGame dataMetric = new DataMetricGame();
    GameObject level;

    public Sprite[] burned;
    public Sprite[] dizzy;
    public Sprite[] frozen;
    public Sprite[] stunned;
    public Sprite[] walking;
    int curSprite;
    public float animationTime = 1;
    float frameTimer;
    public AudioClip freezeDeath, flameDeath, dazedDeath;

    Sprite[] triggeredAnimation;

    bool dying = false;

	void Start ()
    {
        // dataMetric.level = GameObject.FindWithTag("level");
        DataCollector inst = DataCollector.getInstance();
        inst.startLevel((DataMetricLevel.Level) Application.loadedLevel);
        frameTimer = animationTime;
        regularSpeed = speed;
    }
	
	void Update () 
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (dying)
        {
            AnimateBlindGuy();
        }

        else if (stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;
            speed = 0;
            triggeredAnimation = stunned;
            AnimateBlindGuy();
        }
        else
        {
            speed = regularSpeed;
            triggeredAnimation = walking;
            AnimateBlindGuy();
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Finish")
        {
            DataCollector inst = DataCollector.getInstance();
            inst.endLevel(false);

            Debug.Log(SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == "Level10")
            {
                SceneManager.LoadScene("PostGameScene");
                return;
            }
            MenuBehaviour menuScript = GameObject.FindWithTag("UIBehaviour").GetComponent<MenuBehaviour>();
            menuScript.SaveProgress();
            SceneManager.LoadScene("WinState");
        }

        if (col.tag == "FallingTrigger")
        {
            col.GetComponentInParent<Rigidbody2D>().isKinematic = false;
        }

    }

    void LoadLevel()
    {
        SceneManager.LoadScene("LossState");
    }

   
    public void SetDizzyDeath() 
    {
        DataCollector inst = DataCollector.getInstance();
        inst.endLevel(true, "Dizzy");
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = dizzy;
        dying = true;
        AudioSource.PlayClipAtPoint(dazedDeath, transform.position);
    }

    public void SetFlameDeath()
    {
        DataCollector inst = DataCollector.getInstance();
        inst.endLevel(true, "Flames");
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = burned;
        dying = true;
        AudioSource.PlayClipAtPoint(flameDeath, transform.position);
    }

    public void SetFrozenDeath()
    {
        DataCollector inst = DataCollector.getInstance();
        inst.endLevel(true, "Frozen");
        Invoke("LoadLevel", 3f);
        regularSpeed = 0;
        speed = 0;
        triggeredAnimation = frozen;
        dying = true;
        AudioSource.PlayClipAtPoint(freezeDeath, transform.position);
    }

    void AnimateBlindGuy()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            curSprite++;
            if (curSprite >= triggeredAnimation.Length)
            {
                curSprite = 0;
            }
            frameTimer = animationTime;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = triggeredAnimation[curSprite];
    }
}
