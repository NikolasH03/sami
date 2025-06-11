using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    [SerializeField] int vidaMax;
    [SerializeField] float vidaActual;
    [SerializeField] int estaminaMax;
    [SerializeField] float estaminaActual;
    [SerializeField] bool recibeDano;
    [SerializeField] float danoBase;
    [SerializeField] float curacionEstamina;
    [SerializeField] Image imagenBarraVida;
    [SerializeField] Image imagenEstamina;
    [SerializeField] float tiempoUltimoRelleno;


    [SerializeField] bool jugadorEnAnimacionMuriendo = false;
    [SerializeField] bool jugadorMuerto = false;
    [SerializeField] TextMeshProUGUI canvasNumMuertes;
    [SerializeField] int contadorMuertes = 0;
    [SerializeField] GameObject menuMuerte;

    private ControladorMovimiento controladorMovimiento;
    private ControladorCombate controladorCombate;


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

        menuMuerte.SetActive(false);
        canvasNumMuertes.text = contadorMuertes.ToString();

        recibeDano = false;

    }
    private void Update()
    {
        controladorMovimiento = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorMovimiento>();
        controladorCombate = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorCombate>();

        float tiempoTranscurrido = Time.time - tiempoUltimoRelleno;
        if (estaminaActual < 0)
        {
            estaminaActual = 0;
        }

        if (tiempoTranscurrido >= 2f)
        {
            rellenarBarraEstamina(tiempoTranscurrido);
        }


        if (recibeDano)
        {
            controladorDano();
        }
        if (jugadorEnAnimacionMuriendo)
        {
            terminoAnimacionMuerte();
        }




    }


    public void controladorDano()
    {

        if (controladorCombate.getBloqueando())
        {

            if (estaminaActual > 0)
            {
                recibeDanoBloqueo();
            }


            if (estaminaActual <= 0)
            {
                rompeBloqueo();
            }
        }

        else
        {

            controladorMovimiento.setCanMove(false);
            vidaActual -= danoBase;
            imagenBarraVida.fillAmount = vidaActual / vidaMax;

            if (vidaActual <= 0)
            {
                jugadorMuere();
            }
            else
            {
                recibeDanoVida();
            }

        }
    }

    public void rellenarBarraEstamina(float tiempoTranscurrido)
    {
        float estaminaPorSegundo = curacionEstamina / 2f;
        float estaminaRelleno = estaminaPorSegundo * Mathf.Floor(tiempoTranscurrido / 2f);

        estaminaActual += estaminaRelleno;
        estaminaActual = Mathf.Min(estaminaActual, estaminaMax);

        imagenEstamina.fillAmount = estaminaActual / estaminaMax;

        tiempoUltimoRelleno = Time.time;
    }

    public void recibeDanoBloqueo()
    {
        estaminaActual -= danoBase;
        imagenEstamina.fillAmount = estaminaActual / estaminaMax;
        controladorMovimiento.getAnim().Play("daño_bloqueando");
        ControladorSonido.instance.playAudio(ControladorSonido.instance.block);
        controladorMovimiento.GetComponent<Collider>().enabled = false;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = true;
        recibeDano = false;
    }

    public void rompeBloqueo()
    {
        controladorMovimiento.getAnim().Play("daño");
        ControladorSonido.instance.playAudio(ControladorSonido.instance.damage);
        controladorMovimiento.GetComponent<Collider>().enabled = true;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = false;
        recibeDano = false;
    }

    public void recibeDanoVida()
    {
        controladorMovimiento.GetComponent<Collider>().enabled = false;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = true;
        controladorMovimiento.getAnim().SetBool("running", false);
        controladorMovimiento.getAnim().Play("daño");
        ControladorSonido.instance.playAudio(ControladorSonido.instance.damage);
        recibeDano = false;
        controladorMovimiento.getAnim().SetBool("RecibeDaño", true);
    }

    public void jugadorMuere()
    {
        jugadorMuerto = true;
        controladorMovimiento.GetComponent<Collider>().enabled = false;
        controladorMovimiento.GetComponent<Rigidbody>().isKinematic = true;
        controladorMovimiento.getAnim().SetBool("Muere", true);
        controladorMovimiento.getAnim().Play("morir");
        ControladorSonido.instance.playAudio(ControladorSonido.instance.death);
        recibeDano = false;
      
    }

    public void terminoAnimacionMuerte()
    {
        controladorMovimiento.getAnim().speed = 0;
        menuMuerte.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        contadorMuertes++;
        canvasNumMuertes.text = contadorMuertes.ToString();
        jugadorEnAnimacionMuriendo = false;
    }

    public void revivirJugador()
    {
        controladorMovimiento.getAnim().speed = 1;
        menuMuerte.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        vidaActual = vidaMax;
        estaminaActual = estaminaMax;
        tiempoUltimoRelleno = Time.time;
        imagenBarraVida.fillAmount = vidaMax;
        imagenEstamina.fillAmount = estaminaMax;

        controladorMovimiento.GetComponent<Collider>().enabled = true;
        jugadorMuerto=false;
    }

    // setters y getters

    public float getVidaActual()
    {
        return vidaActual;
    }
    public void setVidaActual(float vida)
    {
        vidaActual= vida;
    }

    public bool getRecibeDano()
    {
        return recibeDano;
    }
    public void setRecibeDano(bool boolDano)
    {
        recibeDano = boolDano;
    }
    public bool getJugadorMuriendo()
    {
        return jugadorEnAnimacionMuriendo;
    }
    public void setJugadorMuriendo(bool boolMuerto)
    {
        jugadorEnAnimacionMuriendo = boolMuerto;
    }
    public bool getJugadorMuerto()
    {
        return jugadorMuerto;
    }
    public void setJugadorMuerto(bool boolMuerto)
    {
        jugadorMuerto = boolMuerto;
    }




}
