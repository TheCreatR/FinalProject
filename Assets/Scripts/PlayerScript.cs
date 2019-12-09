using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text winText;

    public int scoreValue = 0;

    public int winScore = 0;

    Animator anim;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
            anim.transform.Rotate(0, 180, 0);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
            anim.transform.Rotate(0, 180, 0);
        }

         if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            musicSource.Stop();
        }
       
        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        SetWinText ();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            winScore += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        if (collision.collider.tag == "Enemy")
        {
            scoreValue -= 1;
            winScore -= 1;
            score.text = scoreValue.ToString();
        }

        if (winScore == 4) 
		{
    		transform.position = new Vector2(27.7f, 0.6f); 
		}

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetWinText ()
    {
        if (winScore == 8)
        {
            winText.text = "You Win! Game created by Michael Martinez!";
        }

        if (winScore == -1)
        {
            winText.text = "You Lost! Game created by Michael Martinez!";
        }
    }
}