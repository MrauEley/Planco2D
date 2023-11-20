using UnityEngine;

public class BlackHole: MonoBehaviour
{
    public float attractionForce = 10f;
    public float attractionRadius = 5f;

    private bool isActive = false;
    private Transform ball=null;
    private Vector3 direction;
    private Rigidbody2D _ballRB;

    void Update()
    {
        if(isActive == true && ball!=null)
        {
            direction = (transform.position - ball.position).normalized;
            ball.position += direction * attractionForce * Time.deltaTime;
        }
    }

    void AttractObject(Rigidbody2D rb)
    {
        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - rb.position;
        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            float forceMagnitude = attractionForce / distance;
            Vector2 force = direction.normalized * forceMagnitude;

            rb.AddForce(force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ball = collision.transform;
        _ballRB = ball.GetComponent<Rigidbody2D>();
        _ballRB.isKinematic = true; //_ballRB.velocity = Vector2.zero;
        isActive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _ballRB.isKinematic = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ball = null;
        isActive = false;
        _ballRB.isKinematic = false;
    }
}

