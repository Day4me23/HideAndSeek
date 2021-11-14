using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject ping;
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + name);
    }
}
