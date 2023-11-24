using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public List<int> enemiesPerWave;
    public float timeBetweenWaves;
    public float timeBetweenEnemySpawns;
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnPoints;
    public List<Sprite> waveStartImages; // Tableau d'images de d�but de manche
    public GameObject victoryUI;

    public Image waveStartImage; // R�f�rence � l'objet Image dans l'inspecteur

    public int currentWave = 0;

    public void Start()
    {
        if (victoryUI != null)
        {
            victoryUI.SetActive(false); // Assurez-vous que l'UI de victoire est d�sactiv�e au d�part
        }

        StartCoroutine(SpawnWaves());
    }

    public IEnumerator SpawnWaves()
    {
        while (currentWave < enemiesPerWave.Count)
        {
            // Assurez-vous que l'index de la vague est dans la plage du tableau
            if (currentWave < waveStartImages.Count)
            {
                // Afficher l'image de d�but de manche correspondante � la vague
                waveStartImage.sprite = waveStartImages[currentWave];
                waveStartImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f); // Vous pouvez ajuster la dur�e de l'affichage
                waveStartImage.gameObject.SetActive(false);
            }

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

        // Toutes les vagues sont termin�es, afficher l'UI de victoire
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            Time.timeScale = 0f; // Mettez en pause le jeu lorsque la victoire est affich�e
        }
    }

    public void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
