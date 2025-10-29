using UnityEngine;

public class EstadoRebirDano : EstadoBase
{
    private readonly Temporizador temporizadorDano;
    private readonly HealthComp vidaEnemigo;

    public EstadoRebirDano(Enemigo enemigo, Animator animator, HealthComp vidaEnemigo, float duracionDano)
        : base(enemigo, animator)
    {
        this.vidaEnemigo = vidaEnemigo;
        this.temporizadorDano = new Temporizador(duracionDano);
    }

    public override void OnEnter()
    {
        enemigo.desactivarCollider();
        animator.CrossFade(DamageHash, duracionTransicion);
        vidaEnemigo.setRecibiendoDano(true);
        temporizadorDano.Empezar();
    }

    public override void Update()
    {
        temporizadorDano.Tick(Time.deltaTime);

        if (temporizadorDano.HaFinalizado)
        {
            vidaEnemigo.setRecibiendoDano(false);

            if (enemigo.detectarJugador.SePuedeDetectarAlJugador())
            {
                enemigo.CambiarAEstado<EstadoSeguirJugador>();
            }
            else
            {
                enemigo.CambiarAEstado<EstadoPatrullaEnemigo>();
            }
        }
    }

    public bool TerminoTiempoDano => temporizadorDano.HaFinalizado;
}