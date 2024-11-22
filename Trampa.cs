using UnityEngine;

public class Trampa : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raton"))
        {
            other.GetComponent<UnityEngine.AI.NavMeshAgent>().speed *= 0.5f; // Reducir velocidad
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Raton"))
        {
            other.GetComponent<UnityEngine.AI.NavMeshAgent>().speed *= 2f; // Restaurar velocidad
        }
    }
}
