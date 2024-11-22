using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform spawnPoint; // Punto de aparición de la bala
    public float bulletSpeed = 1000f; // Velocidad de la bala
    public float fireRate = 1.0f; // Tasa de disparo (bala por segundo)

    private bool canShoot = true; // Control para el disparo
    private Transform markerTransform; // Transformación del marcador asociado a la botella

    private void Start()
    {
        // Encuentra el marcador asociado al cañón (búsqueda por etiqueta o nombre)
        GameObject markerObject = GameObject.FindGameObjectWithTag("CannonMarker");
        if (markerObject != null)
        {
            markerTransform = markerObject.transform;
            Debug.Log("Marcador de cañón encontrado.");
        }
        else
        {
            Debug.LogError("No se encontró el marcador para el cañón. Asegúrate de etiquetar el marcador con 'CannonMarker'.");
        }
    }

    private void Update()
    {
        // Si el marcador está activo, actualiza la posición del cañón
        if (markerTransform != null)
        {
            transform.position = markerTransform.position;
            transform.rotation = markerTransform.rotation;

            // Disparo automático (opcional, desactiva si no es necesario)
            if (canShoot)
            {
                StartCoroutine(ShootRoutine());
            }
        }
    }

    private IEnumerator ShootRoutine()
    {
        canShoot = false; // Evita múltiples disparos simultáneos
        Shoot(); // Realiza el disparo
        yield return new WaitForSeconds(1f / fireRate); // Espera el tiempo de recarga
        canShoot = true; // Permite disparar nuevamente
    }

    private void Shoot()
    {
        Debug.Log("Disparando...");

        // Instancia una nueva bala en el punto de aparición
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Asegúrate de que el Rigidbody no sea nulo antes de aplicar la fuerza
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * bulletSpeed); // Aplica fuerza a la bala
            Debug.Log("Bala instanciada en: " + spawnPoint.position);
        }
        else
        {
            Debug.LogError("El prefab de la bala no tiene un componente Rigidbody.");
        }

        // Destruir la bala después de 5 segundos para evitar la acumulación de objetos
        Destroy(bullet, 5f);
    }
}
