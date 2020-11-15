using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] Transform ship;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.localPosition = 0.1f * ship.position;
    }
}
