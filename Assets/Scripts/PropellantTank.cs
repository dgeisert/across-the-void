using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellantTank : tumble
{
    public override void Interact(bool pressed)
    {
        if (pressed)
        {
            InGameUI.Instance.AddPropellant(100);
        }
        base.Interact(pressed);
    }
}
