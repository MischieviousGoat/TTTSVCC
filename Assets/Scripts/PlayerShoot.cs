using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{
    public KeyCode reloadKey = KeyCode.R;

    public static Action attackInput;
    public static Action reloadInput;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            attackInput?.Invoke();
        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}
