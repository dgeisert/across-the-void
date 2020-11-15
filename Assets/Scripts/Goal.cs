using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Goal Instance;
    private void Start()
    {
        Instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        Part part = other.gameObject.GetComponent<Part>();
        if (part != null)
        {
            Game.Score += 1;
            part.Coast();
        }
    }
}