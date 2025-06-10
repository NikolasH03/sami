using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ControladorCombate : MonoBehaviour
{

    public Animator anim;

    //ataque
    [SerializeField] int numeroArma;
    [SerializeField] bool atacando = false;
    public string tipoAtaque;

    //bloqueo y dash
    [SerializeField] bool bloqueando = false;

    //intanciar arma melee
    [SerializeField] private ArmaData armaActual;
    [SerializeField] private Transform puntoSujecion;
    private GameObject armaInstanciada;

    //Daño del arma a distancia
    [SerializeField] private ArmaDistanciaData armaDistancia;

    //variables para generar los combos en los estados
    [HideInInspector] public bool puedeHacerCombo = false;
    [HideInInspector] public TipoInputCombate inputBufferCombo = TipoInputCombate.Ninguno;
    public List<TipoInputCombate> secuenciaInputs = new List<TipoInputCombate>();
    public Dictionary<string, Combo> combos;

    //colliders necesarios para generar daño
    [SerializeField] Collider ColliderArma;
    [SerializeField] Collider ColliderPierna;
    [SerializeField]  int normalLayerIndex;
    [SerializeField] int dodgeLayerIndex;



    //referencias 
    [SerializeField] ControladorCambioArmas cambioArma;
    ControladorMovimiento controladorMovimiento;
    private CombatStateMachine fsm;
    //[SerializeField] HabilidadesJugador habilidadesJugador;
    private void Start()
    {
        EquiparArma(armaActual);
        ColliderArma = armaInstanciada.GetComponent<Collider>();
        
        ColliderArma.enabled = false;
        ColliderPierna.enabled = false;

        normalLayerIndex = LayerMask.NameToLayer("Default");
        dodgeLayerIndex = LayerMask.NameToLayer("Esquivar");

        anim = GetComponent<Animator>();
        controladorMovimiento = GetComponent<ControladorMovimiento>();

        fsm = new CombatStateMachine();
        fsm.ChangeState(new IdleState(fsm, this));
        combos = ComboDatabase.Combos;
    }
    public void Update()
    {
        if (!HealthBar.instance.getJugadorMuerto())
        {
            fsm.Update();
            //bloqueoCheck();
            //dashCheck();
        }

    }


    public int VerificarArmaEquipada()
    {
        return cambioArma.getterArma();
    }

    public void EquiparArma(ArmaData nuevaArma)
    {
        if (nuevaArma == null) return;


        armaInstanciada = Instantiate(nuevaArma.prefab, puntoSujecion);
        armaInstanciada.transform.localPosition = Vector3.zero;
        armaInstanciada.transform.localRotation = Quaternion.identity;
        armaInstanciada.transform.localScale = Vector3.one;

        armaActual = nuevaArma;
    }

    public int EntregarDañoArmaMelee()
    {
        if (tipoAtaque=="ligero")
        {
            return armaActual.dañoGolpeLigero;
        }
        else if (tipoAtaque == "fuerte")
        {
            return armaActual.dañoGolpeFuerte;
        }
        else
        {
            return armaActual.dañoGolpeLigero;
        }
       
    }

    public int EntregarDañoArmaDistancia()
    {
        return armaDistancia.dañoDisparo;
    }


    public void bloquearDespuesDeGolpe()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
    public void LimpiarSecuenciaInputs()
    {
        secuenciaInputs.Clear();
    }

    // funciones para los Animation Events
    public void VolverAIdle()
    {
        puedeHacerCombo = false;
        fsm.ChangeState(new IdleState(fsm, this));
        inputBufferCombo = TipoInputCombate.Ninguno;
        LimpiarSecuenciaInputs();
    }
    public void ActivarVentanaCombo()
    {
        puedeHacerCombo = true;
    }
    public void DesactivarVentanaCombo()
    {
        puedeHacerCombo = false;
        inputBufferCombo = TipoInputCombate.Ninguno;
        LimpiarSecuenciaInputs();
    }
    public void terminarDash()
    {
        anim.SetBool("dashing", false);
        anim.SetBool("RecibeDaño", false);
        gameObject.layer = normalLayerIndex;
        fsm.ChangeState(new IdleState(fsm, this));
    }
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



    //prueba para el arbol de habilidades
    public bool PuedeUsarCapoeira()
    {
        return HabilidadesJugador.instance.estaDesbloqueada(HabilidadesJugador.TipoHabilidad.Capoeira);
    }


    //setters y getters

    public bool getAtacando()
    {
        return atacando;
    }
    public void setAtacando(bool ataque)
    {
        atacando = ataque;
    }
    public bool getDashing()
    {
        return anim.GetBool("dashing");
    }
    public bool getBloqueando()
    {
        return bloqueando;
    }
    public void setBloqueando(bool block)
    {
        bloqueando = block;
    }
    public GameObject getArmaActual()
    {
        return armaInstanciada;
    }
    public int getLayerDodge()
    {
        return dodgeLayerIndex;
    }

}
