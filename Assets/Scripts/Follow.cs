using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float rate = 0.5f;
    public Transform follow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, follow.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, follow.position, rate * Time.deltaTime * 100);
        }
    }
}