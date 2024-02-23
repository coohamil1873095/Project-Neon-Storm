using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    [SerializeField] private GameObject EnemyObj;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDistance = 5f;
    [SerializeField] private float spawnRate = 3f;
    private GameObject[] enemies;
    private int enemyIterator = 0;
    private GameObject newEnemy;
    
    void Awake() 
    {
        if (Instance != null && Instance != this) 
            Destroy(gameObject);
        else 
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject[maxEnemies];
        for (int i = 0; i < maxEnemies; i++)
        {
            enemies[i] = Instantiate(EnemyObj, transform.position, Quaternion.identity);
            enemies[i].transform.SetParent(GameObject.Find("Enemies").transform);
            enemies[i].SetActive(false);
        }

        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true) 
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

            newEnemy = enemies[enemyIterator];
            if (!newEnemy.activeSelf) {
                newEnemy.transform.position = spawnPoint;
                newEnemy.SetActive(true);
            }
            enemyIterator = (enemyIterator + 1) % maxEnemies;

            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void DestroyEnemy(Enemy enemy) 
    {
        enemy.gameObject.SetActive(false);
    }
}
