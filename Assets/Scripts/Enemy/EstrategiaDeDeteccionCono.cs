using UnityEngine;

public class EstrategiaDeDeteccionCono : IEstrategiaDeDeteccion
{
    private readonly float _angulo;
    private readonly float _radio;
    private readonly float _radioInternoDeDeteccion;

    public EstrategiaDeDeteccionCono(float angulo, float radioExterno, float radioInternoDeDeteccion)
    {
        this._angulo = angulo;
        this._radio = radioExterno;
        this._radioInternoDeDeteccion = radioInternoDeDeteccion;
    }

    public bool Ejecutar(Transform player, Transform detector, Temporizador temporizador)
    {
        if (temporizador.EstaCorriendo) return false;
        
        var direccionAJugador = player.position - detector.position;
        var anguloAJugador = Vector3.Angle(direccionAJugador, detector.forward);
        
        if (!(anguloAJugador <= _angulo / 2f) || !(direccionAJugador.magnitude < _radioInternoDeDeteccion) && !(direccionAJugador.magnitude < _radio))
            return false;
        
        temporizador.Empezar();
        return true;
    }
}