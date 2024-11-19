using UnityEngine;

public class Rata : MonoBehaviour
{
    public int health = 50; // Salud del Rata
    //private GameManager gameManager;

    void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
        //Debug.Log("GameManager encontrado: " + gameManager);
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
        Destroy(gameObject); // Destruye el zombi
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
