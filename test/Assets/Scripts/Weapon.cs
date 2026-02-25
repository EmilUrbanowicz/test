using UnityEngine;
using System;
using System.Xml.Serialization;
using Unity.VisualScripting.FullSerializer;
[Serializable]




public struct Weapon
{

    public string Name;
    public int Damage;

    public Weapon(string _weaponName, int _weaponPower)
    {
        Name = _weaponName;
        Damage = _weaponPower;
    }

}
