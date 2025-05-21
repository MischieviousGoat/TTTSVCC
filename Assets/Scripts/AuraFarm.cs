using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraFarm : MonoBehaviour
{
    [SerializeField] private Target player;
    [SerializeField] private PlayerMovement pm;

    public KeyCode auraKey = KeyCode.E;

    public void Update()
    {
        if (Input.GetKeyDown(auraKey))
        {
            player.animator.Play("Female Standing Pose");

            player.IncreaseHealth(5);
        }
    }
}
