using UnityEngine;
using UnityEngine.AI;

public class EstadoAtacarJugador : EstadoBase
{
    readonly NavMeshAgent agent;
    readonly Transform jugador;
    
    
    public EstadoAtacarJugador(Enemigo enemigo, Animator animator, NavMeshAgent agent, Transform jugador) : base(enemigo, animator)
    {
        this.agent = agent;
        this.jugador = jugador;
    }

    public override void OnEnter()
    {
        Debug.Log("Atacando!");
        animator.CrossFade(AttackHash, duracionTransicion);
    }

    public override void Update()
    {
        agent.SetDestination(jugador.position);
        enemigo.Atacar();
    }
    
    
}