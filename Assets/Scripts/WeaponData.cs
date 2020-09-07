using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponData : ScriptableObject
{
    public new string name;
    public string desc;

    public Sprite img;
    public Color color;

    public int speed;
    public int dexterity;
    public int attack;
    public int maxAmmo;
    public float reloadTime;
}
