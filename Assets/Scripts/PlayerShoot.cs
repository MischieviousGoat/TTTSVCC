using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{
    public KeyCode reloadKey = KeyCode.R;

    public static Action lightAttackInput;
    public static Action heavyAttackInput;
    public static Action reloadInput;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            lightAttackInput?.Invoke();
        if (Input.GetMouseButton(1))
            heavyAttackInput?.Invoke();
        if (Input.GetKeyDown(reloadKey))
            reloadInput?.Invoke();
    }
}
