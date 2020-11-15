using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Tank : tumble
{
    public override void Interact(bool pressed)
    {
        if (pressed)
        {
            InGameUI.Instance.AddO2(100);
        }
        base.Interact(pressed);
    }
}