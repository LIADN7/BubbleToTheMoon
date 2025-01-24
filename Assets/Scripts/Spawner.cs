using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int level { get; set; }
    [SerializeField] float boundryL { get; set; }
    [SerializeField] float boundryR { get; set; }
    [SerializeField] int spawnCount { get; set; }
    [SerializeField] Enemy[] enemies;
    HashSet<Enemy> spawnedEnemies { get; set; } = new HashSet<Enemy>();
    BGController controller;


    public Enemy Spawn()
    {
        Enemy enemy = enemies[Random.Range(0, enemies.Length)];
        UnityEngine.Vector3 pos = WhereToSpawn(enemy);
        return Instantiate(enemy, pos, UnityEngine.Quaternion.identity) as Enemy;

    }

    private void Start()
    {
        controller = GetComponentInParent<BGController>();
        for (int i = 0; i < spawnCount; i++)
        {
            spawnedEnemies.Add(Spawn());
        }
    }

    private void OnDestroy()
    {

    }

    private UnityEngine.Vector3 WhereToSpawn(Enemy enemy)
    {
        Transform controllerTransform = controller.transform;
        float boundryT = controllerTransform.position.y + controllerTransform.localScale.y / 2;
        float boundryB = controllerTransform.position.y - controllerTransform.localScale.y / 2;
        float randomY = Random.Range(boundryB, boundryT);
        float randomX = Random.Range(boundryL, boundryR);

        if (enemy.spawnPos == SpawnPos.Middle)
            return new UnityEngine.Vector3(randomX, randomY, 0);
        else if (enemy.spawnPos == SpawnPos.Side)
        {
            int chooseSide = Random.Range(0, 1);

            return new UnityEngine.Vector3(chooseSide == 0 ? boundryL : boundryR, randomY, 0);
        }
        else
            return new UnityEngine.Vector3(0, 0, 0);
    }

}
