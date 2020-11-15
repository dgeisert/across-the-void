using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEvent : UnityEvent<bool> { }

public class Controls : MonoBehaviour
{
    public static BoolEvent
    forward,
    left,
    right,
    reverse,
    interact;

    private void Awake()
    {
        if (forward == null)
        {
            forward = new BoolEvent();
        }
        if (left == null)
        {
            left = new BoolEvent();
        }
        if (right == null)
        {
            right = new BoolEvent();
        }
        if (reverse == null)
        {
            reverse = new BoolEvent();
        }
        if (interact == null)
        {
            interact = new BoolEvent();
        }
    }

    private void Update()
    {
        if (!Game.active)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            forward.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            forward.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            right.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            right.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            reverse.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            reverse.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            interact.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Space))
        {
            interact.Invoke(false);
        }
    }
}