using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab; // Assign your NPC prefab in the inspector
    public Transform[] spawnPoints; // Assign your spawn points in the inspector
    public int maxNPCs = 5; // Max number of NPCs to spawn

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        // Example rule: spawn a random number of NPCs up to maxNPCs
        int numToSpawn = Random.Range(1, maxNPCs + 1);

        for (int i = 0; i < numToSpawn; i++)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the NPC prefab at the spawn point
            Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("NPC instantiated at: " + npcPrefab.transform.position);
        }
    }

    // Additional methods or rules for spawning can be added here
}