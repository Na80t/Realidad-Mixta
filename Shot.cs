using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint; // Punto de aparición de la bala
    public GameObject bulletPrefab; // Prefab de la bala
    public float shotForce = 1500f; // Fuerza del disparo
    public float shotRate = 0.5f; // Intervalo entre disparos

    private float nextShotTime = 0.0f; // Controla el tiempo del próximo disparo

    private PlayerInput playerInput; // Componente PlayerInput
    private InputAction shootAction; // Acción de disparo

    private void Awake()
    {
        // Obtener el componente PlayerInput y la acción de disparo
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        // Subscribirse al evento de disparo
        shootAction.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        // Desubscribirse del evento de disparo
        shootAction.performed -= OnShootPerformed;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shotRate; // Actualiza el tiempo del próximo disparo
        }
    }

    void Shoot()
    {
        Debug.Log("Disparando desde el cañón...");

        // Instancia una nueva bala en el punto de aparición
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Asegúrate de que el prefab de la bala tenga un Rigidbody
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            // Aplica fuerza a la bala
            bulletRb.AddForce(spawnPoint.forward * shotForce, ForceMode.Impulse);
            Debug.Log("Bala disparada desde: " + spawnPoint.position);
            Destroy(bullet, 5f); // Destruye la bala después de 5 segundos para optimizar el juego
        }
        else
        {
            Debug.LogError("El prefab de la bala no tiene un componente Rigidbody.");
        }
    }
}
