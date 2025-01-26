using System.Collections;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public Transform player;
    public Vector3 prevPlayerPosition;
    public Rigidbody rb;

    public Vector3 resetPos;
    public float speed = 2.0f;
    public float angle = 45.0f;

    public bool grounded = false;
    public bool shooting = false;

    public Vector3 mousePosScreen;
    public Vector3 mousePosWorld;
    public Vector3 aimPos;

    public int timer;

    public Ray mouseRay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get original position to reset it later
        resetPos = transform.position;
        prevPlayerPosition = player.position;
        shooting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // update grounded
        grounded = IsGrounded();

        // pixel coordinates of the mouse
        mousePosScreen = Input.mousePosition;
        // set the screen point into the scene
        mousePosScreen += Camera.main.transform.forward * 10f;
        // position of the mouse in the scene
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        // draw a line from camera to mousePosWorld
        Debug.DrawLine(transform.position, mousePosWorld, Color.green, 2.5f);
        Debug.DrawLine(Camera.main.transform.position, mousePosWorld, Color.blue, 1f);
        // accomodate gravity through arc of shot
        aimPos = new Vector3(mousePosWorld.x, mousePosWorld.y + 5, 5f);
        // space key or click
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !shooting)
            Toss();
        // reset after n time
        if (prevPlayerPosition != player.position && timer == 0)
        {
            StopAllCoroutines();
            StartCoroutine(ResetTimer(3));
            prevPlayerPosition = player.position;
        }
    }

    void Toss()
    {
        shooting = true;
        // Debug.Log("Toss");

        // add an "Impulse" force in the mousePosWorld direction
        rb.AddForce(aimPos * speed, ForceMode.Impulse);

        StopAllCoroutines();
        StartCoroutine(ResetTimer(5));
    }

    public IEnumerator ResetTimer(int max)
    {
        timer = max;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        Reset();
    }

    void Reset()
    {
        resetPos = player.transform.position;
        // stop ball
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();

        // position ball in front of player and up a little
        rb.transform.position =
            player.transform.position
            + (player.transform.forward * 3f)
            + (player.transform.up * 3f);

        shooting = false;
    }

    bool IsGrounded() =>
        Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y / 2 + 0.05f);
}
