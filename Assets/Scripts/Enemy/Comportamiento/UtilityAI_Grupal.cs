using UnityEngine;

public enum AccionGrupal { Atacar, Flanquear, DefenderArquero, Rodear, Retirarse }

public class UtilityAI_Grupal
{
    private Enemigo enemigo;
    private EnemyManager manager;
    private Transform jugador;
    private HealthComp salud;

    public UtilityAI_Grupal(Enemigo enemigo, EnemyManager manager)
    {
        this.enemigo = enemigo;
        this.manager = manager;
        this.salud = enemigo.GetComponent<HealthComp>();
        this.jugador = enemigo.JugadorActual;
    }

    public AccionGrupal DecidirAccion()
    {
        float uAtacar = CalcularUtilidadAtacar();
        float uFlanquear = CalcularUtilidadFlanquear();
        float uRodear = CalcularUtilidadRodear();
        float uRetirada = CalcularUtilidadRetirarse();

        float max = Mathf.Max(uAtacar, uFlanquear, uRodear, uRetirada);

        if (max == uAtacar) return AccionGrupal.Atacar;
        if (max == uFlanquear) return AccionGrupal.Flanquear;
        if (max == uRodear) return AccionGrupal.Rodear;
        return AccionGrupal.Retirarse;
    }

    public float CalcularUtilidadAtacar()
    {
        float utilidad = 0.5f;

        // Verificar que tiene permiso
        if (!enemigo.EstaDisponibleParaAtacar())
        {
            return 0f; // No puede atacar sin permiso
        }

        // Penalizar si no hay slots
        if (!manager.HaySlotsDisponibles())
        {
            utilidad *= 0.3f;
        }
        else
        {
            utilidad += 0.2f;
        }

        float distancia = Vector3.Distance(enemigo.transform.position, jugador.position);

        // Recompensar distancia óptima (3-5m)
        if (distancia >= 3f && distancia <= 5f)
            utilidad += 0.3f;

        // Recompensar vida alta
        if (salud != null && !salud.EstaMuerto)
        {
            float vidaRatio = salud.GetVidaNormalizada();
            if (vidaRatio > 0.5f) utilidad += 0.2f;
        }

        return Mathf.Clamp01(utilidad);
    }

    private float CalcularUtilidadFlanquear()
    {
        float utilidad = 0.3f;

        if (!JugadorMirandoEnemigo())
            utilidad += 0.3f;

        Vector3 dirJugador = jugador.forward;
        Vector3 dirEnemigo = (enemigo.transform.position - jugador.position).normalized;
        float dot = Vector3.Dot(dirJugador, dirEnemigo);

        if (dot > 0.5f) utilidad += 0.2f;

        return Mathf.Clamp01(utilidad);
    }

    private float CalcularUtilidadRodear()
    {
        float utilidad = 0.3f;

        // Si no tiene permiso de ataque, rodear
        if (!enemigo.EstaDisponibleParaAtacar())
            utilidad += 0.3f;

        float distancia = Vector3.Distance(enemigo.transform.position, jugador.position);

        // Distancia ideal para rodear (5-8m)
        if (distancia >= 5f && distancia <= 8f)
            utilidad += 0.2f;

        return Mathf.Clamp01(utilidad);
    }

    private float CalcularUtilidadRetirarse()
    {
        float utilidad = 0.1f;

        // Retirarse si acaba de atacar
        if (!enemigo.EstaDisponibleParaAtacar())
            utilidad += 0.4f;

        // Retirarse si hay muchos atacando
        if (manager.ContarEnemigosAtacando() >= 2)
            utilidad += 0.2f;

        return Mathf.Clamp01(utilidad);
    }

    private bool JugadorMirandoEnemigo()
    {
        Transform player = enemigo.JugadorActual;
        if (player == null) return false;

        Vector3 dirJugador = player.forward;
        Vector3 dirHaciaEnemigo = (enemigo.transform.position - player.position).normalized;
        float dot = Vector3.Dot(dirJugador, dirHaciaEnemigo);

        return dot > 0.7f;
    }
}
