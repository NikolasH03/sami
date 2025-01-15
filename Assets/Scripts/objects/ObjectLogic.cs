using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour
{

    [SerializeField] ControladorMovimiento controladorMovimiento;
    [SerializeField] int tipo;

    //1. crece
    //2. aumenta velocidad base
    //3. desaparece el sprint

    void Start()
    {
        controladorMovimiento = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorMovimiento>();
    }
    public void efecto()
    {
        switch (tipo)
        {
            case 1:
                controladorMovimiento.gameObject.transform.localScale = new Vector3(3, 3, 3);
                break;

            default:
                Debug.Log("ningun efecto");
                break;

        }
    }
}
