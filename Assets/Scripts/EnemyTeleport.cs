using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    public Transform[] teleportPositions; 
    public float detectionRadius = 5f; 

    private void Update()
    {
        CheckTeleport();
    }

    private void CheckTeleport()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Bomba"))
            {
                TeleportEnemy();
                break;
            }
        }
    }

    private void TeleportEnemy()
    {
        
        int randomIndex = Random.Range(0, teleportPositions.Length);
        Transform randomPosition = teleportPositions[randomIndex];

        
        transform.position = randomPosition.position;
    }

    private void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
