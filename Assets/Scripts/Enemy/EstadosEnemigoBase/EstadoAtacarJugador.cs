using UnityEngine;
using UnityEngine.AI;

public class EstadoAtacarJugador : EstadoBase
{
    readonly NavMeshAgent agent;
    readonly Transform jugador;
    readonly float rangoDeAtaque;
    
    // Control de combo
    private int comboStep = 0;
    private bool golpeConectado = false;

    private Temporizador temporizadorCombo;

    public EstadoAtacarJugador(Enemigo enemigo, Animator animator, NavMeshAgent agent, Transform jugador, float rangoDeAtaque) 
        : base(enemigo, animator)
    {
        this.agent = agent;
        this.jugador = jugador;
        this.rangoDeAtaque = rangoDeAtaque;
        temporizadorCombo = new Temporizador(1.0f); // duración aprox de cada animación
    }

    public override void OnEnter()
    {
        comboStep = 0;
        EjecutarAtaque(comboStep);
        temporizadorCombo.Empezar();
    }

    public override void Update()
    {
        // Mantener la distancia deseada al jugador
        Vector3 direccionAlJugador = (jugador.position - enemigo.transform.position).normalized;
        float distanciaDeseada = rangoDeAtaque;
        Vector3 destino = jugador.position - direccionAlJugador * distanciaDeseada;
        agent.SetDestination(destino);
        
        Vector3 lookDir = jugador.position - enemigo.transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            enemigo.transform.rotation = Quaternion.Slerp(
                enemigo.transform.rotation,
                Quaternion.LookRotation(lookDir),
                Time.deltaTime * 10f
            );
        }

        enemigo.Atacar();
        
        temporizadorCombo.Tick(Time.deltaTime);

        // Encadenar ataque cuando termine la animación
        if (temporizadorCombo.HaFinalizado)
        {
            // Si hubo impacto -> continuar combo
            if (golpeConectado || true)
            {
                golpeConectado = false;
                comboStep++;
                
                if (comboStep > 2) 
                    comboStep = 0;

                EjecutarAtaque(comboStep);
                temporizadorCombo.Reiniciar();
                temporizadorCombo.Empezar();
            }
        }
    }

    private void EjecutarAtaque(int step)
    {
        if (step == 0)
        {
            Debug.Log("Ataque normal");
            animator.CrossFade(AttackHash, duracionTransicion);
        }
        else if (step == 1)
        {
            Debug.Log("Ataque encadenado -> Attack1");
            animator.CrossFade(Attack1Hash, duracionTransicion);
        }
        else if (step == 2)
        {
            Debug.Log("Ataque final -> HeavyAttack");
            animator.CrossFade(HeavyAttackHash, duracionTransicion);
        }
    }

    // Se sigue usando por si luego lo quieres con impactos reales
    public void RegistrarImpacto()
    {
        golpeConectado = true;
    }
}
