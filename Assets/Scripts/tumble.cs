using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tumble : Interactible
{
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddTorque(new Vector3(Random.value, Random.value, Random.value) * 20);
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public override void Interact(bool pressed)
    {
        //rb.angularVelocity = Vector3.zero;
    }
}
