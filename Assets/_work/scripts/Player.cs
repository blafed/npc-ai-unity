using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITarget
{
    public float speed = 5;


    Vector3 movement;

    public Vector3? point => null;

    public TargetType type => TargetType.person;

    Shooter shooter;
    private void Start()
    {
        this.shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        this.movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButton("Jump"))
        {
            this.shooter.shoot();
        }

    }
    private void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }
}
