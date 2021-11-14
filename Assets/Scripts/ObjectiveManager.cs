using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;

    float maxFuel = 60, currentfuel = 0;
    Image fuelFillbar;

    public bool O_powerOn = false;
    public bool O_fuel = false;
    public bool O_getKeycard = false;
    public bool O_findHealth = false;
    public bool O_getWeapon = false;

    private void Awake()
    {
        instance = this;
        fuelFillbar = GameObject.Find("Fuelbar").GetComponent<Image>();
    }
    void Update()
    {
        if (O_fuel)
            currentfuel += Time.deltaTime;
        currentfuel = Mathf.Clamp(currentfuel, 0, maxFuel);
        fuelFillbar.fillAmount = currentfuel / maxFuel;
    }
}
