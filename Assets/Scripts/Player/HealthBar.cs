using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public int vidaMax;
    public float vidaActual;
    public int estaminaMax;
    public float estaminaActual;
    public float dañoBase;
    public float curacionEstamina;
    public Image imagenBarraVida;
    public Image imagenEstamina;
    public float tiempoUltimoRelleno; // Variable para rastrear el tiempo del último relleno
    public bool dañoEstamina;
    public static HealthBar instance;
    public float tiempoParry;

    //variables para salida forzosa del bloqueo
    public float tiempoDeBloqueo = 10f; // Tiempo en segundos para bloquear la entrada


    //Variables para despues de morir
    public bool playerDied;

    // otros codigos necesarios
    [SerializeField] cambiarPersona protagonista;
    [SerializeField] Player protagonista1;
    [SerializeField] Player protagonista2;

    AudioManager audioManager;

    public bool detectaAtaque;
    private void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    void Start()
    {

    

        vidaActual = vidaMax;
        estaminaActual = estaminaMax;

        // Establecer el tiempo del último relleno al inicio
        tiempoUltimoRelleno = Time.time;

        playerDied = false;

    }
    private void Update()
    {

        // Calcular el tiempo transcurrido desde el último relleno
        float tiempoTranscurrido = Time.time - tiempoUltimoRelleno;
        if(estaminaActual < 0)
        {
            estaminaActual = 0;
        }
        // Si ha pasado al menos 4 segundos desde el último relleno
        if (tiempoTranscurrido >= 4f)
        {
            // Calcular la cantidad de estamina que se debe rellenar cada segundo
            float estaminaPorSegundo = curacionEstamina / 2f;

            // Calcular cuánta estamina debería haberse rellenado desde el último relleno
            float estaminaRelleno = estaminaPorSegundo * Mathf.Floor(tiempoTranscurrido / 4f);

            // Incrementar la estamina actual y asegurarse de que no supere el máximo
            estaminaActual += estaminaRelleno;
            estaminaActual = Mathf.Min(estaminaActual, estaminaMax);

            // Actualizar la barra de estamina
            imagenEstamina.fillAmount = estaminaActual / estaminaMax;

            // Actualizar el tiempo del último relleno
            tiempoUltimoRelleno = Time.time;
        }


        if (detectaAtaque)
        {
            controladorDaño();
        }
        
       
        
       
    }


    public void controladorDaño()
    {
        
            if (protagonista.protagonistaUno)
            {
                if (protagonista1.anim.GetBool("blocking"))
                {
                    Debug.Log("Tiempo: " + Mathf.FloorToInt(protagonista1.tiempoTranscurrido * 1000) + " milisegundos");
                    tiempoParry = protagonista1.tiempoTranscurrido * 1000;

                    if (tiempoParry <= 4)
                    {
                        protagonista1.anim.Play("parry");
                        protagonista1.ResetTimer();
                        detectaAtaque = false;
                    }
                    else
                    {
                    
                  
                    if (estaminaActual > 0)
                        {

                            estaminaActual -= dañoBase;
                            imagenEstamina.fillAmount = estaminaActual / estaminaMax;
                            protagonista1.anim.Play("daño_bloqueando");
                            audioManager.playAudio(audioManager.block);
                            protagonista1.GetComponent<Collider>().enabled = false;
                            detectaAtaque = false;


                    }


                        if (estaminaActual <= 0)
                        {
                            protagonista1.anim.Play("daño");
                        audioManager.playAudio(audioManager.damage);
                        protagonista1.GetComponent<Collider>().enabled = true;
                            detectaAtaque = false;
                          
                    }
                    }




                }
                else
                {
               
               
                    protagonista1.canMove = false;
                    vidaActual -= dañoBase;
                    imagenBarraVida.fillAmount = vidaActual / vidaMax;

                    if (vidaActual <= 0)
                    {

                        protagonista1.GetComponent<Collider>().enabled = false;
                        protagonista1.anim.Play("morir");
                    audioManager.playAudio(audioManager.death);
                    detectaAtaque = false;


                }
                    else
                    {
                        protagonista1.GetComponent<Collider>().enabled = false;
                        protagonista1.anim.Play("daño");
                    audioManager.playAudio(audioManager.damage);
                    detectaAtaque = false;

                }

                }
            }
            else
            {
                if (protagonista2.anim.GetBool("blocking"))
                {
                    Debug.Log("Tiempo: " + Mathf.FloorToInt(protagonista2.tiempoTranscurrido * 1000) + " milisegundos");
                    tiempoParry = protagonista2.tiempoTranscurrido * 1000;

                    if (tiempoParry <= 4)
                    {
                        protagonista2.anim.Play("parry");
                        protagonista2.ResetTimer();
                    detectaAtaque = false;
                }
                    else
                    {
                    
                    
                    if (estaminaActual > 0)
                        {

                            estaminaActual -= dañoBase;
                            imagenEstamina.fillAmount = estaminaActual / estaminaMax;
                            protagonista2.anim.Play("daño_bloqueando");
                        audioManager.playAudio(audioManager.block);
                        protagonista2.GetComponent<Collider>().enabled = false;
                        detectaAtaque = false;


                    }


                        if (estaminaActual <= 0)
                        {
                      
                        protagonista2.anim.Play("daño");
                        audioManager.playAudio(audioManager.damage);
                        protagonista2.GetComponent<Collider>().enabled = true;
                        detectaAtaque = false;
                    }
                    }




                }
                else
                {
              
                

                protagonista2.canMove = false;
                    vidaActual -= dañoBase;
                    imagenBarraVida.fillAmount = vidaActual / vidaMax;

                    if (vidaActual <= 0)
                    {

                        protagonista2.GetComponent<Collider>().enabled = false;
                        protagonista2.anim.Play("morir");
                    audioManager.playAudio(audioManager.death);
                    detectaAtaque = false;


                }
                    else
                    {
                        protagonista2.GetComponent<Collider>().enabled = false;
                        protagonista2.anim.Play("daño");
                    audioManager.playAudio(audioManager.damage);
                    detectaAtaque = false;
                }

                }
            }        
      
    }
   
   



}
