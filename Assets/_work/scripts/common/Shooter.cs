using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Vector3 offset = Vector3.up;
    public float reload = 0.5f;
    public ITarget sender;
    public GameObject projectilePrefab;
    GameObject reloadedProjectile;



    float __timer;


    public void shoot()
    {
        if (Time.time > __timer + reload)
        {
            __timer = Time.time;
            this.reloadedProjectile = Instantiate(projectilePrefab, transform.position + offset, transform.rotation);
            this.reloadedProjectile.GetComponent<Rigidbody>().isKinematic = false;
            this.reloadedProjectile.GetComponent<Projectile>().sender = sender;
        }
    }
}