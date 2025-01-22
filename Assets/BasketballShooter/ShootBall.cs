using System.Collections;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 originalPosition;
    public float speed = 2.0f;
    public float angle = 45.0f;
    public Vector3 mousePos;
    public Vector3 aim;

    public bool canShoot = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        mousePos += Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        aim = Camera.main.ScreenToWorldPoint(mousePos);
        aim = new Vector3(aim.x, aim.y+5, 5f);

        if (Input.GetKey(KeyCode.Space))
        {
            // // reset
            // if (timer < 100)
            // {
            //
            //     return;
            // }
            if (canShoot)
                StartCoroutine(Toss());
        }
    }

    public IEnumerator Toss()
    {
        canShoot = false;
        Debug.Log("Toss");

        //instantiates the bullet
        // gunEnd.transform.LookAt(aim);
        // bullet = Instantiate(bulletPrefab, gunEnd.transform.position, Quaternion.identity);
        // bullet.transform.LookAt(aim);
        // Rigidbody b = bullet.GetComponent<Rigidbody>();
        // b.AddRelativeForce(Vector3.forward * force);


        // Quaternion rotation = Quaternion.Euler(0, 0, angle);
        // Vector3 velocity = rotation * (aim * speed);
        // rb.linearVelocity = velocity;

        // Vector2 direction = Input.mousePosition - transform.position;
        // direction.Normalize();
        // Vector2 aim = direction * speed; // assume _speed can be tuned


        rb.AddForce(aim * speed, ForceMode.Impulse);

        yield return new WaitForSeconds(3);
        Reset();
    }

    void Reset()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        rb.transform.position = originalPosition;
        canShoot = true;
    }
}
