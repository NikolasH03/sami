using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour
{

    
    public bool destruirAutomatico;
    [SerializeField] Player playerMove1;
    [SerializeField] int tipo;

    //1. crece
    //2. aumenta velocidad base
    //3. desaparece el sprint

    void Start()
    {
        playerMove1=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
   public void efecto()
    {
        switch (tipo)
        {
            case 1:
                playerMove1.gameObject.transform.localScale = new Vector3(3,3,3);
                break;

            case 2:
               float speed1= playerMove1.returnSpeed();
                speed1 += 5;
                playerMove1.setSpeed(speed1);
                break;

            case 3:
                float nMultiplier = 1;
                playerMove1.setMultiplier(nMultiplier);
                break;

            default:
                Debug.Log("ningun efecto");
                break;

        }
    }
}
