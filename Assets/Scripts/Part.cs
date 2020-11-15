using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : tumble
{
    public Follow follow;
    bool inGoal = false;

    public override void Interact(bool pressed)
    {
        if (!inGoal)
        {
            base.Interact(pressed);
            rb.angularVelocity = Vector3.zero;
            follow.enabled = pressed;
        }
    }

    public void Coast()
    {
        follow.follow = Goal.Instance.transform;
        inGoal = true;
    }

}