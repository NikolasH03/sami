using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [SerializeField] ControladorCombate controladorCombate;
    public Collider objeto;
    public GameObject mano;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "objeto")
        {
            if(Input.GetKey(KeyCode.E)) {

                objeto = other;
                controladorCombate.anim.Play("agarrar");
                
            }
        }

    }
    public void destruirObjeto()
    {
        objeto.GetComponent<ObjectLogic>().efecto();
        Destroy(objeto.gameObject);
    }
    public void objetoEsHijo ()
    {
        objeto.transform.SetParent(mano.transform);
    }
}
