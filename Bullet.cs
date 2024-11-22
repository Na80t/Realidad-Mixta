using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;  // Daño que aplica la bala

    private void OnTriggerEnter(Collider other)
    {
        // Si la bala golpea a un ratón
        if (other.CompareTag("Rat"))
        {
            Rat rat = other.GetComponent<Rat>();
            if (rat != null)
            {
                rat.TakeDamage(damage);  // Aplica el daño al ratón
                Debug.Log("Ratón golpeado. Daño aplicado: " + damage);
            }

            // Destruir la bala después de causar daño
            Destroy(gameObject);
        }
        // Si la bala golpea el suelo
        else if (other.CompareTag("Ground"))
        {
            Debug.Log("La bala golpeó el suelo y fue destruida.");
            Destroy(gameObject);  // Destruir la bala al tocar el suelo
        }
        // Si golpea cualquier otra cosa (opcional)
        else
        {
            Debug.Log("La bala golpeó algo no especificado: " + other.name);
            Destroy(gameObject);  // Destruir la bala si no es un caso específico
        }
    }
}
