using System.Collections;
using UnityEngine;

public class RataSpawner : MonoBehaviour
{
    public GameObject RataPrefab; // Asigna el prefab del raton en el Inspector
    public float spawnInterval = 3f; // Intervalo entre apariciones de raton
    public Transform[] spawnPoints; // Puntos de aparición de raton

    private void Start()
    {
        // Verificar si se ha asignado el prefab de Rata
        if (RataPrefab == null)
        {
            Debug.LogError("Rata prefab no asignado. Por favor, asigna el prefab del Rata en el Inspector.");
            return; // Detener si no hay prefab
        }

        // Verificar si hay puntos de spawn asignados
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados. Asigna al menos un punto de spawn en el Inspector.");
            return; // Detener si no hay puntos de spawn
        }

        // Iniciar la corutina de generación de zombis
        StartCoroutine(SpawnRatas());
    }

    // Corutina para generar zombis en intervalos de tiempo
    private IEnumerator SpawnRatas()
    {
        while (true)
        {
            SpawnRata();
            yield return new WaitForSeconds(spawnInterval); // Espera antes de generar el siguiente zombi
        }
    }

    // Método para generar un zombi en un punto aleatorio
    private void SpawnRata()
    {
        if (spawnPoints.Length > 0)
        {
            // Elegir un punto de aparición aleatorio
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Debug.Log("Generando Rata en: " + spawnPoints[spawnIndex].position); // Log para verificar el spawn

            // Instanciar el Rata en el punto de aparición seleccionado
            Instantiate(RataPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
