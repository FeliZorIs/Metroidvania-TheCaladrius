using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> EnemiesInTotal;
    public List<Enemy> enemiesOnMap;
    public static EnemyManager Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemiesOnMap.Add(enemy);
    }

    public void DeregisterEnemy(Enemy enemy)
    {
        enemiesOnMap.Remove(enemy);
    }
}
