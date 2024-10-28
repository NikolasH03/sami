using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAtaque : MonoBehaviour
{

    [SerializeField] Collider ColliderAtaque1;


    public void desactivarCollider()
    {
        ColliderAtaque1.enabled = false;
    }
    public void activarCollider()
    {
        ColliderAtaque1.enabled = true;
    }
}
