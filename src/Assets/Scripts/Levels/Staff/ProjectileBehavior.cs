using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 120f;

    void FixedUpdate()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        Destroy(gameObject);
    }
}