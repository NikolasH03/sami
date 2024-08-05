using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GrabObject : MonoBehaviour
{


    private void OnTriggerStay(Collider other)
    {
        

        if(other.tag=="objeto" && other.GetComponent<ObjectLogic>().destruirAutomatico==true) {

            other.GetComponent<ObjectLogic>().efecto();
            Destroy(other.gameObject);
        }
        if (other.tag == "objeto")
        {
            if(Input.GetKeyDown(KeyCode.E)) {
            

                other.GetComponent <ObjectLogic>().efecto();
                Destroy(other.gameObject);
            }
        }

    }
}
