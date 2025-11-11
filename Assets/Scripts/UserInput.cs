using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private int _triggerKey = 0;

    public event Action KeyTriggered;

    public void Update()
    {
        if (Input.GetMouseButtonDown(_triggerKey))
            KeyTriggered.Invoke();
    }
}