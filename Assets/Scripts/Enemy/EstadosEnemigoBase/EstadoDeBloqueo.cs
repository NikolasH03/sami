using UnityEngine;
using UnityEngine.AI;

public class EstadoDeBloqueo : EstadoBase
{
    private readonly NavMeshAgent agent;
    private readonly HealthComp vidaEnemigo;

    public EstadoDeBloqueo(Enemigo enemigo, Animator animator, NavMeshAgent agent, HealthComp vidaEnemigo) : base(enemigo, animator)
    {
        this.agent = agent;
        this.vidaEnemigo = vidaEnemigo;
    }

    public override void OnEnter()
    {
        Debug.Log("Bloqueo!!!");
        animator.CrossFade(BlockHash, duracionTransicion);
        vidaEnemigo.setBloqueado(true);
    }

    public override void Update()
    {
        agent.isStopped = true;
    }

    public override void OnExit()
    {
        agent.isStopped = false;
        vidaEnemigo.setBloqueado(false);
    }
}