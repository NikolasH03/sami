using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class logicaEnemigo : MonoBehaviour
{
    [SerializeField] float vidaActual;
    [SerializeField] float vidaMax;
    [SerializeField] float dañoArma;
    [SerializeField] Animator anim;
    [SerializeField] Image imagenBarraVida;
    [SerializeField] Money dinero;
    public void Start()
    {
        vidaActual = vidaMax;
    }
    public void OnTriggerEnter(Collider other)
    {
       
        
       if (other.gameObject.tag == "arma")
       {
            vidaActual -= dañoArma;
            imagenBarraVida.fillAmount = vidaActual / vidaMax;

            if (vidaActual <= 0)
            {
                GetComponent<Collider>().enabled = false;
                anim.Play("Falling Back Death");
                dinero.contador = dinero.contador + 50;
                float delayInSeconds = 2.0f; 
                Invoke("eliminarEnemigo", delayInSeconds);
            }
            else
            {
                anim.Play("damage");

            }


        }

        
    }
    public void eliminarEnemigo()
    {
        gameObject.SetActive(false);
    }
}
