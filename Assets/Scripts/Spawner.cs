using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] int spawnCount;
    [SerializeField] Enemy[] enemies;
    HashSet<Enemy> spawnedEnemies { get; set; } = new HashSet<Enemy>();
    BGController controller;


    private void Start()
    {
        controller = GetComponentInParent<BGController>();
        for (int i = 0; i < spawnCount; i++)
        {
            spawnedEnemies.Add(Spawn());
        }
    }
    public Enemy Spawn()
    {
        Enemy enemyToCreate = enemies[Random.Range(0, enemies.Length)];
        Enemy enemyCreated = Instantiate(enemyToCreate, controller.transform) as Enemy;
        UnityEngine.Vector3 pos = WhereToSpawn(enemyCreated);
        enemyCreated.transform.position = pos;

        return enemyCreated;
    }

    private UnityEngine.Vector3 WhereToSpawn(Enemy enemy)
    {
        Renderer currentBGrenderer = controller.GetComponent<Renderer>();
        if (currentBGrenderer == null)
            return new UnityEngine.Vector3(0, 0, 0);

        float boundryT = currentBGrenderer.bounds.max.y;
        float boundryB = currentBGrenderer.bounds.min.y;
        float boundryR = currentBGrenderer.bounds.max.x;
        float boundryL = currentBGrenderer.bounds.min.x;
        float randomY = Random.Range(boundryB, boundryT);
        float randomX = Random.Range(boundryL, boundryR);

        if (enemy.spawnPos == SpawnPos.Middle)
            return new UnityEngine.Vector3(randomX, randomY, 0);

        else if (enemy.spawnPos == SpawnPos.Side)
        {
            int chooseSide = Random.Range(0, 2);
            enemy.rightSide = chooseSide == 0 ? true : false;
            return new UnityEngine.Vector3(enemy.rightSide ? boundryR : boundryL, randomY, 0);
        }
        else
            return new UnityEngine.Vector3(0, 0, 0);
    }

}
