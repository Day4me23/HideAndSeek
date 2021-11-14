using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    private void Awake() => instance = this;
    public static ObjectiveManager instance;

    float maxFuel = 60, currentfuel = 0;
    public Image fuelFillbar;

    public bool O_powerOn = false;
    public bool O_fuel = false;
    public bool O_getKeycard = false;
    public bool O_findHealth = false;
    public bool O_getWeapon = false;

    private void Update()
    {
        if (O_fuel)
            currentfuel += Time.deltaTime;
        currentfuel = Mathf.Clamp(currentfuel, 0, maxFuel);
        fuelFillbar.fillAmount = currentfuel / maxFuel;
    }
}
