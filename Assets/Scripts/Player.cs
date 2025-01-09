using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Singleton
    public static Player instance;

    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerRight;
    [SerializeField]
    private float playerLeft;

    [SerializeField]
    GameObject projectilePrefab;
    public bool projectile;
    private bool projectileCount;
    private float projectileTimer;
    [SerializeField]
    private float projectileCooldown;
    public float speedProjectile;


    private void Awake()
    {
        //Singleton to use this script's variables and functions from other scripts
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
        //Can shoot at start
        projectile = true;
        //Cooldown at 0
        projectileTimer = 0f;
    }

    void Update()
    {
        //Can start to count
        if (projectileCount == true)
        {
            //Count time
            projectileTimer = projectileTimer + Time.deltaTime;
            //If timer is equal to cooldown
            if (projectileTimer >= projectileCooldown)
            {
                //Set to false to stop counting and set to 0
                projectile = true;
                projectileCount = false;
                projectileTimer = 0f;
            }
        }

        //Get position
        Vector3 gameObjectPosition = transform.position;
        //Calculate new X
        gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + (Input.GetAxis("Horizontal")) * playerSpeed * Time.deltaTime, playerLeft, playerRight);
        //Update position
        transform.position = gameObjectPosition;

        //If Jump is pressed and projectile is true
        if (Input.GetButton("Jump") && projectile == true)
        {
            //Set to can't shoot
            projectile = false;
            //Set to start counting
            projectileCount = true;
            //Set creating point of projectile with X, Y and Z separation
            Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //Create projectile
            Instantiate(projectilePrefab, playerPosition, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy projectile collides, destroy it
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            LivesandPoints.instance.DamageTaken();
        }
    }
}
