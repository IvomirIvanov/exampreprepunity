using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public bool isMoving;
    public bool isGrounded;
    public Rigidbody2D attachedRigidbody;
    public float charachterMovingSpeed = 2f;
    public AudioSource collectBananaSound;
    public AudioSource jumpSound;

    float speedMultiplier;

    Animator dumpyAnimator;

    public float jumpForce = 1000f;

    public int score;

    // Use this for initialization
    void Start()
    {
        dumpyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();

    }


    void UpdateMovement()
    {
        if (!isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            speedMultiplier = 1f;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (!dumpyAnimator.GetBool("Run"))
            {
                dumpyAnimator.SetBool("Run", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedMultiplier = -1.5f;            
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (!dumpyAnimator.GetBool("Run"))
            {
                dumpyAnimator.SetBool("Run", true);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            this.attachedRigidbody.AddForce(new Vector3(0, this.jumpForce, 0), ForceMode2D.Force);
            this.jumpSound.Play();
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            this.dumpyAnimator.SetTrigger("Attack");
        }
        else
        {
            speedMultiplier = -0.8f;

            if (dumpyAnimator.GetBool("Run"))
            {
                dumpyAnimator.SetBool("Run", false);
            }

        }

        attachedRigidbody.velocity = new Vector2(speedMultiplier * charachterMovingSpeed * Time.deltaTime, attachedRigidbody.velocity.y);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Land") && coll.transform.position.y < transform.position.y)
        {
            isGrounded = true;
        }
        else if (coll.collider.CompareTag("ResetCollider"))
        {
            score = 0;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Land") && coll.transform.position.y < transform.position.y && !isGrounded)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Land") && coll.transform.position.y < transform.position.y)
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Banana"))
        {
            score++;
            Destroy(coll.gameObject);
            collectBananaSound.Play();
        }
    }
}
