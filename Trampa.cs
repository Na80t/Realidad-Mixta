using UnityEngine;

public class Trampa : MonoBehaviour
{
    public float slowMultiplier = 0.5f; // Factor de reducción de velocidad
    private float originalSpeed;       // Velocidad original del ratón

    public GameObject cajaTrampa;      // Referencia a la caja trampa (puede ser otro objeto)
    private bool isInCajaTrampa = false; // Indica si el ratón está en la caja trampa

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raton"))
        {
            // Obtén el componente NavMeshAgent
            var navAgent = other.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (navAgent != null)
            {
                // Si el ratón entra en la trampa que ralentiza
                if (this.CompareTag("TrampaRalentizadora"))
                {
                    // Guarda la velocidad original del ratón
                    originalSpeed = navAgent.speed;

                    // Reduce la velocidad del ratón
                    navAgent.speed *= slowMultiplier;
                    Debug.Log("Rata ralentizada.");
                }

                // Si el ratón entra en la caja trampa
                if (this.CompareTag("CajaTrampa"))
                {
                    isInCajaTrampa = true;
                    Debug.Log("Rata atrapada en la caja.");

                    // Destruye el ratón
                    Destroy(other.gameObject);
                    Debug.Log("Rata destruida.");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raton"))
        {
            // Obtén el componente NavMeshAgent
            var navAgent = other.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (navAgent != null)
            {
                // Si el ratón salió de la trampa que ralentiza
                if (this.CompareTag("TrampaRalentizadora"))
                {
                    // Restaura la velocidad original del ratón
                    navAgent.speed = originalSpeed;
                    Debug.Log("Rata recuperó su velocidad.");
                }

                // Si el ratón salió de la caja trampa
                if (this.CompareTag("CajaTrampa"))
                {
                    isInCajaTrampa = false;
                    Debug.Log("Rata salió de la caja.");
                }
            }
        }
    }
}
