using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPower : Interactable
{
    public override void Interact()
    {
        base.Interact();
        ObjectiveManager.instance.O_powerOn = true;
    }
}
