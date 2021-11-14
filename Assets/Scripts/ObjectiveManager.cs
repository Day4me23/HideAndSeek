using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private void Awake() => instance = this;
    public static ObjectiveManager instance;

    public bool O_powerOn = false;
    public bool O_fuel = false;
    public bool O_getKeycard = false;
    public bool O_findHealth = false;
    public bool O_getWeapon = false;
}
