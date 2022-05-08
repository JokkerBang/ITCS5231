using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float countdown;
    public bool trigger;

    private void Start()
    {
        countdown = 0;
        trigger = false;
    }

    private void Update()
    {
        if (countdown > 0) countdown -= Time.deltaTime;
        else if (countdown < 0)
        {
            trigger = true;
            countdown = 0;
        }
    }
}
