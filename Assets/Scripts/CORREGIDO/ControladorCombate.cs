using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCombate: MonoBehaviour
{

    public Animator anim;

    //ataque
    [SerializeField] int numeroArma;
    public bool atacando=false;
    public int numeroGolpesLigeros=0;
    public int numeroGolpesFuertes=0;
    [SerializeField] Collider ColliderArma;
    [SerializeField] Collider ColliderPierna;


  

    //herencias
    [SerializeField] ControladorCambioArmas cambioArma;
    ControladorMovimiento controladorMovimiento;
    private void Start()
    {
        

        ColliderArma.enabled=false;
        ColliderPierna.enabled = false;
    

        anim = GetComponent<Animator>();
        controladorMovimiento = GetComponent<ControladorMovimiento>();
    }
    public void Update()
    {
        golpeCheck();
        bloqueoCheck();
    }



    //verifica si el usuario oprimio el click y activa la animacion de golpe
    public void golpeCheck()
    {
       numeroArma=cambioArma.getterArma();

        if (numeroArma == 1)
        {
            anim.SetBool("distance", false);
            
            if (Input.GetMouseButton(0) && !atacando && !controladorMovimiento.estaCorriendo)
            {

                atacando = true;
                numeroGolpesLigeros = 1;

            }
            if (Input.GetMouseButton(1) && !atacando && !controladorMovimiento.estaCorriendo)
            {

                atacando = true;
                numeroGolpesFuertes = 1;

            }
            if (!atacando)
            {
                numeroGolpesLigeros = 0;
                numeroGolpesFuertes = 0;
            }

        }
        else if (numeroArma == 2)
        {
            anim.SetBool("distance", true);

    


        }

       

    }
   

    //verifica si el jugador esta manteniendo oprimida la tecla para bloquear
    public void bloqueoCheck()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
               
             anim.SetBool("blocking", true);
            


        }
        
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            
            GetComponent<Collider>().enabled = true;
            anim.SetBool("blocking", false);
            
        }

    }
    public void bloquearDespuesDeGolpe()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }


    //activar colliders de diferentes armas
    public void activarColliderArma()
    {
        ColliderArma.enabled = true;
    }
    public void desactivarColliderArma()
    {
        ColliderArma.enabled = false;
    }
    public void activarColliderPierna()
    {
        ColliderPierna.enabled = true;
    }
    public void desactivarColliderPierna()
    {
        ColliderPierna.enabled = false;
    }

  
}
