using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static string nextSpawnPointID = ""; // Static so it persists across scenes

    private void Start()
    {
        if (!string.IsNullOrEmpty(nextSpawnPointID))
        {
            SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

            foreach (SpawnPoint point in spawnPoints)
            {
                if (point.spawnID == nextSpawnPointID)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        player.transform.position = point.transform.position;
                    }
                    break;
                }
            }
        }
    }
}

