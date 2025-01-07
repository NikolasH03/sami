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
    public bool recibeDaño;
    public float dañoBase;
    public float curacionEstamina;
    public Image imagenBarraVida;
    public Image imagenEstamina;
    public float tiempoUltimoRelleno; 
    public bool dañoEstamina;
    public static HealthBar instance;

    //variables para salida forzosa del bloqueo
    public float tiempoDeBloqueo = 10f; // Tiempo en segundos para bloquear la entrada


    //Variables para despues de morir
    public bool playerDied;


    private ControladorMovimiento protagonista;

    
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

    

        vidaActual = vidaMax;
        estaminaActual = estaminaMax;


        tiempoUltimoRelleno = Time.time;

        playerDied = false;

    }
    private void Update()
    {
        protagonista = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorMovimiento>();

 
        float tiempoTranscurrido = Time.time - tiempoUltimoRelleno;
        if(estaminaActual < 0)
        {
            estaminaActual = 0;
        }

        if (tiempoTranscurrido >= 4f)
        {
     
            float estaminaPorSegundo = curacionEstamina / 2f;

         
            float estaminaRelleno = estaminaPorSegundo * Mathf.Floor(tiempoTranscurrido / 4f);

       
            estaminaActual += estaminaRelleno;
            estaminaActual = Mathf.Min(estaminaActual, estaminaMax);

            
            imagenEstamina.fillAmount = estaminaActual / estaminaMax;

           
            tiempoUltimoRelleno = Time.time;
        }


        if (recibeDaño)
        {
            controladorDaño();
        }
        
       
        
       
    }


    public void controladorDaño()
    {
        
                if (protagonista.anim.GetBool("blocking"))
                {

                  
                    if (estaminaActual > 0)
                        {

                            estaminaActual -= dañoBase;
                            imagenEstamina.fillAmount = estaminaActual / estaminaMax;
                            protagonista.anim.Play("daño_bloqueando");
                            ControladorSonido.instance.playAudio(ControladorSonido.instance.block);
                            protagonista.GetComponent<Collider>().enabled = false;
                            protagonista.GetComponent<Rigidbody>().isKinematic = true;
                            recibeDaño = false;


                    }


                        if (estaminaActual <= 0)
                        {
                            protagonista.anim.Play("daño");
                    ControladorSonido.instance.playAudio(ControladorSonido.instance.damage);
                        protagonista.GetComponent<Collider>().enabled = true;
                    protagonista.GetComponent<Rigidbody>().isKinematic = false;
                    recibeDaño = false;
                          
                    }
                    }
 
                else
                {
               
               
                    protagonista.canMove = false;
                    vidaActual -= dañoBase;
                    imagenBarraVida.fillAmount = vidaActual / vidaMax;

                    if (vidaActual <= 0)
                    {

                        protagonista.GetComponent<Collider>().enabled = false;
                protagonista.GetComponent<Rigidbody>().isKinematic = true;
                protagonista.anim.Play("morir");
                ControladorSonido.instance.playAudio(ControladorSonido.instance.death);
                    recibeDaño = false;


                }
                    else
                    {
                        protagonista.GetComponent<Collider>().enabled = false;
                protagonista.GetComponent<Rigidbody>().isKinematic = true;
                protagonista.anim.SetBool("running", false);
                protagonista.anim.Play("daño");
                    ControladorSonido.instance.playAudio(ControladorSonido.instance.damage);
                    recibeDaño = false;

                }

                }
            
                    
      
    }
   
   



}
