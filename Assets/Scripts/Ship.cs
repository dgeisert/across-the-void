using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance;
    GameObject focus;
    public Vector2 drag;
    public Vector2 accMult, speedMult;
    Vector2 acceleration, speed;
    [SerializeField] ParticleSystem main, leftSeconday, rightSecondary, leftReverse, rigthReverse;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Controls.forward.AddListener(Forward);
        Controls.left.AddListener(Left);
        Controls.right.AddListener(Right);
        Controls.reverse.AddListener(Reverse);
        Controls.interact.AddListener(Interact);
    }

    void Forward(bool press)
    {
        acceleration += (press ? 1 : -1) * new Vector2(0, accMult.y);
    }

    void Left(bool press)
    {
        acceleration += (press ? 1 : -1) * new Vector2(accMult.x, 0);
    }

    void Right(bool press)
    {
        acceleration -= (press ? 1 : -1) * new Vector2(accMult.x, 0);
    }

    void Reverse(bool press)
    {
        acceleration -= (press ? 1 : -1) * new Vector2(0, accMult.y);
    }

    private void Update()
    {
        speed += acceleration * Time.deltaTime;
        transform.position += transform.up * Time.deltaTime * speed.y * speedMult.y;
        transform.localEulerAngles += Vector3.forward * Time.deltaTime * speed.x * speedMult.x;
        speed = Vector3.Scale(speed, drag);

        InGameUI.Instance.AddPropellant(- Mathf.Abs(acceleration.x / 4 + acceleration.y) * Time.deltaTime * 20);

        AnimateEngines();
    }

    void AnimateEngines()
    {
        if (acceleration.y > 0)
        {
            if (rigthReverse.isPlaying)
            {
                rigthReverse.Stop();
            }
            if (leftReverse.isPlaying)
            {
                leftReverse.Stop();
            }
            if (!main.isPlaying)
            {
                main.Play();
            }
            if (acceleration.x > 0)
            {
                if (!rightSecondary.isPlaying)
                {
                    rightSecondary.Play();
                }
            }
            else
            {
                if (rightSecondary.isPlaying)
                {
                    rightSecondary.Stop();
                }
                if (acceleration.x < 0)
                {
                    if (!leftSeconday.isPlaying)
                    {
                        leftSeconday.Play();
                    }
                }
                else
                {
                    if (leftSeconday.isPlaying)
                    {
                        leftSeconday.Stop();
                    }
                }
            }
        }
        else
        {
            if (main.isPlaying)
            {
                main.Stop();
            }
            if (acceleration.y < 0)
            {
                if (!leftReverse.isPlaying)
                {
                    leftReverse.Play();
                }
                if (!rigthReverse.isPlaying)
                {
                    rigthReverse.Play();
                }
                if (leftSeconday.isPlaying)
                {
                    leftSeconday.Stop();
                }
                if (rightSecondary.isPlaying)
                {
                    rightSecondary.Stop();
                }
            }
            else
            {
                if (acceleration.x > 0)
                {
                    if (!leftReverse.isPlaying)
                    {
                        leftReverse.Play();
                    }
                    if (rigthReverse.isPlaying)
                    {
                        rigthReverse.Stop();
                    }
                    if (!rightSecondary.isPlaying)
                    {
                        rightSecondary.Play();
                    }
                }
                else
                {
                    if (acceleration.x < 0)
                    {
                        if (!rigthReverse.isPlaying)
                        {
                            rigthReverse.Play();
                        }
                        if (leftReverse.isPlaying)
                        {
                            leftReverse.Stop();
                        }
                        if (!leftSeconday.isPlaying)
                        {
                            leftSeconday.Play();
                        }
                    }
                    else
                    {
                        if (rigthReverse.isPlaying)
                        {
                            rigthReverse.Stop();
                        }
                        if (leftReverse.isPlaying)
                        {
                            leftReverse.Stop();
                        }
                        if (leftSeconday.isPlaying)
                        {
                            leftSeconday.Stop();
                        }
                        if (rightSecondary.isPlaying)
                        {
                            rightSecondary.Stop();
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        focus = other.gameObject;
    }

    private void Interact(bool pressed)
    {
        if (focus?.GetComponent<Interactible>() != null)
        {
            focus.GetComponent<Interactible>().Interact(pressed);
        }
    }
}