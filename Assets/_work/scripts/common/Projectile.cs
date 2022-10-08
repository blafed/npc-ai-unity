using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public ITarget sender;

    public Rigidbody rb { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        Destroy(gameObject, 3f);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        collider.enabled = false;
        Destroy(gameObject, 0.5f);
    }
}