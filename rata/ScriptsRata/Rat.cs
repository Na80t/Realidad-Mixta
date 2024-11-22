using UnityEngine;

public class Rat : MonoBehaviour
{
    public int health = 50; // Salud del Rata
    public Transform cheeseTarget;
    public float speed = 2.0f;

    void Update()
    {
        if (cheeseTarget != null)
        {
            // Mueve la rata hacia el queso
            Vector3 direction = cheeseTarget.position - transform.position;
            transform.right = -direction.normalized;
            transform.position += Vector3.Normalize(cheeseTarget.position - transform.position) * speed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Reducir la salud

        if (health <= 0)
        {
            Debug.Log("El zombi ha sido destruido.");
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Rata muerto, destruyendo objeto.");
        Destroy(gameObject); // Destruye la rata
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name); // Verifica qué está colisionando

        if (collision.gameObject.CompareTag("Cat"))
        {
            Debug.Log("El zombi ha tocado al gato.");
            //gameManager.EndGame();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("El zombi ha sido golpeado por una bala.");
            TakeDamage(10); // Asumimos que la bala hace 10 de daño
        }
    }
}
