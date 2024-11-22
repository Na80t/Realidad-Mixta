using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject ratPrefab;       // Prefab de la rata
    public int numberOfRats = 5;       // Cantidad de ratas a generar
    public float spawnInterval = 1.0f; // Intervalo de tiempo entre cada generación

    private Transform cheeseTarget;
    private float spawnTimer = 0.0f;
    private int ratsSpawned = 0;

    void Start()
    {
        FindCheeseTarget();
    }

    void Update()
    {
        // Si ya tenemos un objetivo "queso", generamos ratas
        if (cheeseTarget != null && ratsSpawned < numberOfRats)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnRat();
                spawnTimer = 0.0f;
            }
        }
        else if (cheeseTarget == null) // Sigue buscando el "queso" si no está asignado
        {
            FindCheeseTarget();
        }
    }

    void FindCheeseTarget()
    {
        GameObject cheeseObject = GameObject.FindGameObjectWithTag("queso");

        if (cheeseObject != null)
        {
            cheeseTarget = cheeseObject.transform;
        }
    }

    void SpawnRat()
    {
        GameObject newRat = Instantiate(ratPrefab, transform.position, Quaternion.identity);
        Rat ratScript = newRat.GetComponent<Rat>();
        ratScript.cheeseTarget = cheeseTarget;

        ratsSpawned++;
    }
}
