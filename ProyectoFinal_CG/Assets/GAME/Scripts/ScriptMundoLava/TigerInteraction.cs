using UnityEngine;
using ithappy.Animals_FREE;  // para usar CreatureMover

public class TigerInteraction : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player; // arrastra aquí el Player

    [Header("Persecución")]
    [SerializeField] private float catchDistance = 1.8f;  // distancia para "atrapar"
    [SerializeField] private bool runWhenChasing = true;  // que corra al perseguir

    private CreatureMover mover;

    // Spawn del tigre
    private Vector3 tigerSpawnPos;
    private Quaternion tigerSpawnRot;

    // Spawn del jugador (lo guardamos al inicio)
    private Vector3 playerSpawnPos;
    private Quaternion playerSpawnRot;

    private void Awake()
    {
        mover = GetComponent<CreatureMover>();

        tigerSpawnPos = transform.position;
        tigerSpawnRot = transform.rotation;

        if (player != null)
        {
            playerSpawnPos = player.position;
            playerSpawnRot = player.rotation;
        }
    }

    private void Update()
    {
        if (player == null || mover == null) return;

        // Distancia actual al jugador
        Vector3 toPlayer = player.position - transform.position;
        float dist = toPlayer.magnitude;

        if (dist > catchDistance)
        {
            // Perseguir al jugador
            ChasePlayer();
        }
        else
        {
            // Lo alcanzó
            CatchPlayer();
        }
    }

    private void ChasePlayer()
    {
        // Axis (0,1) = avanzar hacia adelante
        Vector2 axis = new Vector2(0f, 1f);
        Vector3 targetPos = player.position;

        bool isRun = runWhenChasing;
        bool isJump = false;

        mover.SetInput(in axis, in targetPos, in isRun, in isJump);
    }

    private void CatchPlayer()
    {

        if (player != null)
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            player.position = playerSpawnPos;
            player.rotation = playerSpawnRot;

            if (cc != null) cc.enabled = true;
        }


        var tigerCC = GetComponent<CharacterController>();
        if (tigerCC != null) tigerCC.enabled = false;

        transform.position = tigerSpawnPos;
        transform.rotation = tigerSpawnRot;

        if (tigerCC != null) tigerCC.enabled = true;


        Vector2 axis = Vector2.zero;
        Vector3 targetPos = transform.position;
        mover.SetInput(in axis, in targetPos, false, false);


    }
}

