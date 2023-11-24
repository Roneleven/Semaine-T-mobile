using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<int> enemiesPerWave;
    public float timeBetweenWaves;
    public float timeBetweenEnemySpawns;
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnPoints;
    public GameObject victoryUI;

    private int currentWave = 0;

    private void Start()
    {
        if (victoryUI != null)
        {
            victoryUI.SetActive(false); // Assurez-vous que l'UI de victoire est désactivée au départ
        }

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < enemiesPerWave.Count)
        {
            // Afficher l'image de début de manche ici pour chaque vague si nécessaire

            int numberOfEnemies = enemiesPerWave[currentWave];

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemySpawns);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            // Move to the next wave
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/NewWave");
            currentWave++;
        }

        // Toutes les vagues sont terminées, afficher l'UI de victoire
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            Time.timeScale = 0f; // Mettez en pause le jeu lorsque la victoire est affichée
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
