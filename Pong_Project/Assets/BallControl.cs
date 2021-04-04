using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody2D rb;
    public float xInitForce;
    public float yInitForce;
    public float ballSpeed = 10f;
    private Vector2 ballVelocity;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    void Start()
    {
        trajectoryOrigin = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        RestartGame();
    }

    private void Update()
    {
        if(ballVelocity.magnitude != 0 && rb.velocity.magnitude > ballVelocity.magnitude)
        {
            rb.velocity = (rb.velocity.normalized) * ballVelocity.magnitude;
        }
        ballVelocity = rb.velocity;
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    void PushBall()
    {
        Vector2 force;
        float yRandomInitForce = Random.Range(-yInitForce, yInitForce);
        float randomDirection = Random.Range(0, 2);
        //moves left if smaller than 1, move right if larger than equal to 1
        if (randomDirection < 1.0f)
        {
            force = new Vector2(-xInitForce, yRandomInitForce);
        }
        else
        {
            force = new Vector2(xInitForce, yRandomInitForce);
        }
        rb.AddForce(force.normalized * ballSpeed * 5f);
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = this.transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
