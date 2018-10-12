using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies;
    public static EnemyManager Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void DeregisterEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
