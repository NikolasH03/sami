using UnityEngine;

public class DetectorImpactoMelee : MonoBehaviour
{
    [SerializeField] private string tagEnemigo = "enemy";
    HealthbarEnemigo enemigo;
    ControladorCombate player;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorCombate>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagEnemigo))
        {
            Vector3 puntoImpacto = other.ClosestPoint(this.transform.position);
            ControladorVFX.instance.GenerarEfecto(puntoImpacto);
            ControladorSonido.instance.playAudio(ControladorSonido.instance.slash);

            enemigo = other.GetComponent<HealthbarEnemigo>();
            enemigo.recibeDaño(player.TipoDeDaño());
            enemigo.setRecibiendoDaño(true);

        }
    }
}

