using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarPersona : MonoBehaviour
{
    [SerializeField] GameObject muisca;
    [SerializeField] GameObject espanol;


    void Start()
    {
        muisca.SetActive(false);
        espanol.SetActive(true);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G) && espanol.activeSelf)
        {
            muisca.SetActive(true);
            espanol.SetActive(false);

        }

        else if (Input.GetKeyDown(KeyCode.G) && muisca.activeSelf)
        {
            muisca.SetActive(false);
            espanol.SetActive(true);

        }
    }

}
