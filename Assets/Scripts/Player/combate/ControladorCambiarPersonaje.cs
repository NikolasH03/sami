using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCambiarPersonaje : MonoBehaviour
{
    public static ControladorCambiarPersonaje instance;

    [SerializeField] GameObject muisca;
    [SerializeField] GameObject espanol;
    public CinemachineFreeLook freeLookCamera;
    [SerializeField] ControladorCombate controladorMuisca;
    [SerializeField] ControladorCombate controladorEspanol;


    public bool protagonistaUno;

    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }
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


        if (Input.GetKeyDown(KeyCode.G) && espanol.activeSelf && !controladorEspanol.atacando && !HealthBar.instance.recibeDaño)
        {
            muisca.transform.position = espanol.transform.position;
            muisca.transform.rotation = espanol.transform.rotation;            
            espanol.SetActive(false);
            muisca.SetActive(true);
            ControladorSonido.instance.playAudio(ControladorSonido.instance.changeProta);
            freeLookCamera.Follow = muisca.transform;
            freeLookCamera.LookAt = muisca.transform;

            protagonistaUno = true;

        }

        else if (Input.GetKeyDown(KeyCode.G) && muisca.activeSelf && !controladorMuisca.atacando && !HealthBar.instance.recibeDaño)
        {
            espanol.transform.position = muisca.transform.position;
            espanol.transform.rotation = muisca.transform.rotation;

            muisca.SetActive(false);
            espanol.SetActive(true);
            ControladorSonido.instance.playAudio(ControladorSonido.instance.changeProta);
            freeLookCamera.Follow = espanol.transform;
            freeLookCamera.LookAt = espanol.transform;
            
            protagonistaUno = false;

        }
    }

}
