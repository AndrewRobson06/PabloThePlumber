using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Optional custom respawn transform; defaults to this checkpoint's transform.")]
    [SerializeField] private Transform spawnPointOverride;

    [SerializeField] private bool oneTimeActivation = false;

    private bool isActivated;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (oneTimeActivation && isActivated)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        PabloRespawn respawn = other.GetComponentInParent<PabloRespawn>();
        if (respawn == null)
        {
            return;
        }

        Transform targetSpawn = spawnPointOverride != null ? spawnPointOverride : transform;
        respawn.SetCheckpoint(targetSpawn);
        isActivated = true;
    }
}