using System.Collections;
using UnityEngine;

public class PabloRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private Transform currentSpawnPoint;
    [SerializeField] private float respawnDelay = 0f;

    private CharacterController characterController;
    private bool isRespawning;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (currentSpawnPoint == null)
        {
            GameObject startObj = new GameObject("InitialSpawnPoint");
            startObj.transform.position = transform.position;
            startObj.transform.rotation = transform.rotation;
            currentSpawnPoint = startObj.transform;
        }
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        if (respawnDelay > 0f)
        {
            yield return new WaitForSeconds(respawnDelay);
        }

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        transform.position = currentSpawnPoint.position;
        transform.rotation = currentSpawnPoint.rotation;

        if (characterController != null)
        {
            characterController.enabled = true;
        }

        isRespawning = false;
    }
    
    public void SetCheckpoint(Transform newSpawnPoint)
    {
        if (newSpawnPoint == null)
        {
            return;
        }

        currentSpawnPoint = newSpawnPoint;
    }

}