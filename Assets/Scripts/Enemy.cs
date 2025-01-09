using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float shootTimer;
    [SerializeField] GameObject projectile;

    private void Update()
    {
        shootTimer = shootTimer + Time.deltaTime;

        if (shootTimer >= Random.Range(2, 5))
        {
            shootTimer = 0f;
            if (Bottom())
            {
                int randomChance = Random.Range(1, 4);
                if (randomChance == 1)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private bool Bottom()
    {
        Vector3 position = transform.position;
        foreach (var column in AllEnemies.instance.enemies)
        {
            if (column.Contains(gameObject) == true)
            {
                foreach (var enemy in column)
                {
                    if (enemy.transform.position.y < position.y && enemy.activeSelf ==true)
                    {
                        return false;
                    }
                }
                break;
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Collider"))
        {
            AllEnemies.instance.InvertDirection();
        }
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Menus.instance.GameOver();
        }
    }
}
