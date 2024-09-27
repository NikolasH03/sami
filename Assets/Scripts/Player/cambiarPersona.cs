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
        Pmuisca.atacando = false;
        Pmuisca.atacandoDebil = false;
        Pmuisca.atacandoFuerte = false;
        Pmuisca.isDashing = false;
        Pmuisca.numero_golpesDebiles = 0;
        Pmuisca.numero_golpesFuertes = 0;
        Pespanol.atacando = false;
        Pespanol.atacandoDebil = false;
        Pespanol.atacandoFuerte = false;
        Pespanol.isDashing = false;
        Pespanol.numero_golpesDebiles = 0;
        Pespanol.numero_golpesFuertes = 0;
        freeLookCamera.Follow = espanol.transform;  
        freeLookCamera.LookAt = espanol.transform;  
    
    protagonistaUno = false;

}

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G) && espanol.activeSelf)
        {
            muisca.SetActive(true);
            espanol.SetActive(false);
            Pespanol.atacando = false;
            Pespanol.atacandoDebil = false;
            Pespanol.atacandoFuerte = false;
            Pespanol.isDashing = false;
            Pespanol.numero_golpesDebiles = 0;
            Pespanol.numero_golpesFuertes = 0;
            Pmuisca.atacando = false;
            Pmuisca.atacandoDebil = false;
            Pmuisca.atacandoFuerte = false;
            Pmuisca.isDashing = false;
            Pmuisca.numero_golpesDebiles = 0;
            Pmuisca.numero_golpesFuertes = 0;
            freeLookCamera.Follow = muisca.transform;
            freeLookCamera.LookAt = muisca.transform;
            muisca.transform.position=espanol.transform.position;
            muisca.transform.rotation=espanol.transform.rotation;
            protagonistaUno = true;

        }

        else if (Input.GetKeyDown(KeyCode.G) && muisca.activeSelf)
        {
            muisca.SetActive(false);
            espanol.SetActive(true);
            Pmuisca.atacando = false;
            Pmuisca.atacandoDebil = false;
            Pmuisca.atacandoFuerte = false;
            Pmuisca.isDashing = false;
            Pmuisca.numero_golpesDebiles = 0;
            Pmuisca.numero_golpesFuertes = 0;
            Pespanol.atacando = false;
            Pespanol.atacandoDebil = false;
            Pespanol.atacandoFuerte = false;
            Pespanol.isDashing = false;
            Pespanol.numero_golpesDebiles = 0;
            Pespanol.numero_golpesFuertes = 0;
            freeLookCamera.Follow = espanol.transform;
            freeLookCamera.LookAt = espanol.transform;
            espanol.transform.position = muisca.transform.position;
            espanol.transform.rotation = muisca.transform.rotation;
            protagonistaUno = false;

        }
    }

}
