using UnityEngine;

public class EnemigoInteraction : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;  
    [SerializeField] private float detectionRange = 10f; 
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f; 

    private bool isPlayerInRange = false;

    // Referencia al sistema de salud del jugador
    public PlayerHealth playerHealth;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;  // Buscar al jugador por tag si no está asignado
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Distancia entre el golem y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            isPlayerInRange = false;
        }

        // Si está dentro del rango de ataque, atacar
        if (isPlayerInRange && distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
    }

    // Mover al golem hacia el jugador
    private void ChasePlayer(float distanceToPlayer)
    {
        if (isPlayerInRange)
        {
            // Dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);  // Moverse hacia el jugador
        }
    }

    // Hacer daño al jugador al alcanzar el rango de ataque
    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(20f);  // Puedes ajustar el daño
            Debug.Log("¡El golem ha atacado al jugador!");
        }
    }
}