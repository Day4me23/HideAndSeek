using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ORocket : Interactable
{
    public override void Interact()
    {
        if (ObjectiveManager.instance.maxFuel == ObjectiveManager.instance.currentfuel)
        {
            SceneManager.LoadScene(0);
        }
            base.Interact();
    }
}
