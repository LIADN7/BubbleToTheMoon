using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int level { get; set; }
    [SerializeField] float boundryL { get; set; }
    [SerializeField] float boundryR { get; set; }
    [SerializeField] int spawnCount { get; set; }
    [SerializeField] GameObject[] enemies; //Change to Enemy class


    public void Spawn(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);

    }

    private void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // GameObject enemy = enemies[Random.Range(0, enemies.Length)]
            // Spawn(, )
        }
    }


}
