using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float lightDamage;
    public float heavyDamage;
    public float maxDistance;

    [Header("Reloading")]
    public float lightFireRate;
    public float heavyFireRate;
}
