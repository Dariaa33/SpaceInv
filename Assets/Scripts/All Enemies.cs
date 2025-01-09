using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AllEnemies : MonoBehaviour
{
    public static AllEnemies instance;

    public List<List<GameObject>> enemies = new List<List<GameObject>>();

    [SerializeField]
    private float columns;
    [SerializeField]
    private float rows;
    [SerializeField]
    private float enemiesPositionX;
    [SerializeField]
    private float enemiesPositionY;
    [SerializeField]
    private float enemiesPositionZ;
    public float speedWhenDestroy;
    [SerializeField]
    private float distanceChangeRow;
    [SerializeField]
    private float xBetweenEnemies;
    [SerializeField]
    private float yBetweenEnemies;
    public bool moving;
    public float speed;
    [SerializeField]
    GameObject[] prefabs;
    [SerializeField]
    GameObject parent;
    private float direction;
    private bool collision;
    private int enemiesTotal;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        for (int i = 0; i < columns; i++)
        {
            enemies.Add(new List<GameObject>());
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = new Vector3(enemiesPositionX + i * xBetweenEnemies, enemiesPositionY - j * yBetweenEnemies, enemiesPositionZ);
                GameObject enemy = Instantiate(prefabs[0], position, Quaternion.identity);
                enemy.name = $"({i},{j})";
                enemy.gameObject.transform.SetParent(parent.transform);
                enemies[i].Add(enemy);
            }
        }
        direction = 1;
        collision = false;
        moving = true;
        enemiesTotal = 0;
    }

    void Update()
    {
        collision = false;
        foreach (var column in enemies)
        {
            foreach (var enemy in column)
            {
                if (enemy.activeSelf == true)
                {
                    enemy.transform.position = enemy.transform.position + new Vector3 (1, 0, 0) * direction * speed * Time.deltaTime;
                }
            }
        }
    }

    //Change enemies row and direction
    public void InvertDirection()
    {
        if (collision == false)
        {
            collision = true;
            direction = direction * -1;
        }
        foreach (var column in enemies)
        {
            foreach (var enemy in column)
            {
                if (enemy.activeSelf == true)
                {
                    enemy.transform.position = enemy.transform.position + new Vector3 (0, -1, 0) * distanceChangeRow;
                }
            }
        }
    }

    //How many enemies are left
    public void NumberOfEnemies()
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                enemiesTotal = enemiesTotal + 1;
            }
        }
        if (enemiesTotal == 0)
        {
            Menus.instance.NextLevel();
        }
        enemiesTotal = 0;
    }

    public void RestartGame()
    {
        foreach (var column in enemies)
        {
            foreach (var enemy in column)
            {
                Destroy(enemy.gameObject);
            }
        }
        foreach (var enemyList in enemies)
        {
            enemyList.Clear();
        }
        for (int i = 0; i < columns; i++)
        {
            enemies.Add(new List<GameObject>());
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = new Vector3(enemiesPositionX + i * xBetweenEnemies, enemiesPositionY - j * yBetweenEnemies, enemiesPositionZ);
                GameObject enemy = Instantiate(prefabs[0], position, Quaternion.identity);
                enemy.name = $"({i},{j})";
                enemy.gameObject.transform.SetParent(parent.transform);
                enemies[i].Add(enemy);
            }
        }
    }
}
