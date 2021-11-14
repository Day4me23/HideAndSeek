using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKey : Interactable
{
    public override void Interact()
    {
        base.Interact();
        ObjectiveManager.instance.O_getKeycard = true;
    }
}
