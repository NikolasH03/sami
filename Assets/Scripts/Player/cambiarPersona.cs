using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarPersona : MonoBehaviour
{
    [SerializeField] GameObject muisca;
    [SerializeField] GameObject espanol;
    public CinemachineFreeLook freeLookCamera;
    [SerializeField] Player Pmuisca;
    [SerializeField] Player Pespanol;

    public bool protagonistaUno;
    void Start()
    {
        

        muisca.SetActive(false);
        espanol.SetActive(true);
        freeLookCamera.Follow = espanol.transform;  
        freeLookCamera.LookAt = espanol.transform;  
    
        protagonistaUno = false;

}

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.G) && espanol.activeSelf)
        {
            muisca.transform.position = espanol.transform.position;
            muisca.transform.rotation = espanol.transform.rotation;            
            espanol.SetActive(false);
            muisca.SetActive(true);
            freeLookCamera.Follow = muisca.transform;
            freeLookCamera.LookAt = muisca.transform;
            Pespanol.atacando = false;
            Pespanol.atacandoDebil = false;
            Pespanol.atacandoFuerte = false;
            Pespanol.isDashing = false;
            Pmuisca.atacando = false;
            Pmuisca.atacandoDebil = false;
            Pmuisca.atacandoFuerte = false;
            Pmuisca.isDashing = false;


            
            //Pmuisca.GetComponent<Animator>().Rebind();
            //Pespanol.GetComponent<Animator>().Rebind();
            protagonistaUno = true;

        }

        else if (Input.GetKeyDown(KeyCode.G) && muisca.activeSelf)
        {
            espanol.transform.position = muisca.transform.position;
            espanol.transform.rotation = muisca.transform.rotation;

            muisca.SetActive(false);
            espanol.SetActive(true);
            freeLookCamera.Follow = espanol.transform;
            freeLookCamera.LookAt = espanol.transform;
            Pmuisca.atacando = false;
            Pmuisca.atacandoDebil = false;
            Pmuisca.atacandoFuerte = false;
            Pmuisca.isDashing = false;

            Pespanol.atacando = false;
            Pespanol.atacandoDebil = false;
            Pespanol.atacandoFuerte = false;
            Pespanol.isDashing = false;

            freeLookCamera.Follow = espanol.transform;
            freeLookCamera.LookAt = espanol.transform;
            
            //Pmuisca.GetComponent<Animator>().Rebind();
            //Pespanol.GetComponent<Animator>().Rebind();
            protagonistaUno = false;

        }
    }

}
