using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private GameObject colliderUp;

    void Update()
    {
        //Get projectile position
        Vector3 projectilePosition = transform.position;
        //Calculate new Y position
        projectilePosition.y = projectilePosition.y + Player.instance.speedProjectile * Time.deltaTime;
        //Update projectile position
        transform.position = projectilePosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collider"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            LivesandPoints.instance.Score();
            AllEnemies.instance.NumberOfEnemies();
            Destroy(gameObject);
        }
    }
}
