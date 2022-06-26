using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public UnityEvent onMark = new UnityEvent();

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        onMark.Invoke();
    }
}
