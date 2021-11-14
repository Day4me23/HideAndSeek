using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OFuel : Interactable
{
    public override void Interact()
    {
        if (!ObjectiveManager.instance.O_powerOn)
            return;
        base.Interact();
        ObjectiveManager.instance.O_fuel = true;
    }
}
