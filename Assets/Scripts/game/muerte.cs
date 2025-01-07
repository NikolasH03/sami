using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class muerte : MonoBehaviour
{


    [SerializeField] ControladorCombate player;
    [SerializeField] HealthBar healthBar;
    [SerializeField] TextMeshProUGUI numMuertes;
    [SerializeField] int contador = 0;
    [SerializeField] bool jugadorMuerto = false;
    [SerializeField] GameObject menuMuerte;
    private void Start()
    {
        menuMuerte.SetActive(false);
        numMuertes.text = contador.ToString();
    }

    private void Update()
    {

        if (healthBar.playerDied == true)
        {
            
            destroyPlayer();
        }
    }
    public void destroyPlayer()
    {
       
        player.anim.speed = 0;
        menuMuerte.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(!jugadorMuerto) {

            contador++;
            numMuertes.text = contador.ToString();

            jugadorMuerto=true;

        }
        

    }
    public void revivePlayer()
    {
        healthBar.playerDied = false;
        player.anim.speed = 1;
        menuMuerte.SetActive(false);
        jugadorMuerto = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        healthBar.vidaActual = healthBar.vidaMax;
        healthBar.estaminaActual = healthBar.estaminaMax;
        healthBar.tiempoUltimoRelleno = Time.time;
        healthBar.imagenBarraVida.fillAmount = healthBar.vidaMax;
        healthBar.imagenEstamina.fillAmount = healthBar.estaminaMax;

        player.GetComponent<Collider>().enabled = true;
    }
}
