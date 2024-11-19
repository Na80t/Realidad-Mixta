using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RataMovement : MonoBehaviour
{
    public NavMeshAgent agent; // Asegúrate de arrastrar aquí el componente NavMeshAgent del zombi
    public float moveRange = 10f;   // Rango de movimiento aleatorio
    public float updateInterval = 5f; // Intervalo de tiempo para actualizar el destino
    private float nextUpdateTime;

    public Transform cat; // Cambia 'player' a 'cat'
    public float detectionRange = 15f;  // Rango en el que detectará al gato

    void Start()
    {
        // Busca al gato en la escena
        GameObject catObject = GameObject.FindGameObjectWithTag("Cat"); // Asegúrate de que el gato tenga el tag "Cat"
        if (catObject != null)
        {
            cat = catObject.transform; // Asigna el transform del gato
        }
        else
        {
            Debug.LogError("No se encontró el gato en la escena.");
        }

        // Verifica si el zombi está en el NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // Coloca al zombi en una posición válida
        }
        else
        {
            Debug.LogError("No se encontró una posición válida para el zombi en el NavMesh");
        }

        // Inicializa el tiempo para la primera actualización
        nextUpdateTime = Time.time + updateInterval;
    }

    void Update()
    {
        // Verifica que el agente esté activo y sobre el NavMesh
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            // Verifica si el gato está dentro del rango de detección
            if (cat != null)
            {
                float distanceToCat = Vector3.Distance(cat.position, transform.position);

                if (distanceToCat <= detectionRange)
                {
                    agent.SetDestination(cat.position); // Mueve al zombi hacia el gato
                }
                else
                {
                    // Movimiento aleatorio si el gato no está en el rango
                    if (Time.time >= nextUpdateTime || (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance))
                    {
                        SetRandomDestination();
                        nextUpdateTime = Time.time + updateInterval;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("El NavMeshAgent no está activo o no está en el NavMesh.");
        }
    }

    void SetRandomDestination()
    {
        // Genera una posición aleatoria dentro del rango especificado
        Vector3 randomDirection = Random.insideUnitSphere * moveRange;
        randomDirection += transform.position;

        // Comprueba si la posición está dentro del NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // Establece el destino aleatorio
        }
    }
}
