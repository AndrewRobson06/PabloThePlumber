using UnityEngine;

public class KillZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KillZone triggered by: " + other.name + " | Tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            PabloRespawn respawn = other.GetComponent<PabloRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}