using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Agent_navigation : MonoBehaviour
{
    public List<Vector3> waypoints;

    public int waypointIndex = 0;

    public float speed = 4;

    public Graph_generation graphGeneration;

    public Transform target;
    public Transform player;
    public float rotationSpeed = 5f;

    private Animator animator;
    public bool Tiro = false;
    [SerializeField] private float Timer = 5;
    private float bulletTime;
    public GameObject enemyBullet;
    public Transform SpawnPoint;
    public float enemySpeed;

    private bool isSpeedModified = false;
    private float originalSpeed;

    private void Start()
    {
        waypoints = graphGeneration.wayPoints;
        graphGeneration.resetWaypoints();
        animator = GetComponent<Animator>();
        originalSpeed = speed;
    }

    private void Update()
    {
        waypoints = graphGeneration.wayPoints;
        move();
        ShootAtPlayer();
        animator.SetBool("Tiro", Tiro);

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void move()
    {
        if ((target.position - transform.position).magnitude > 1f && waypoints.Count == 0)
        {
            graphGeneration.resetWaypoints();
        }
        else if (waypoints.Count == 0)
        {
            moveFinalApproach();
        }
        else
        {
            followPath();
        }
    }

    public void followPath()
    {
        if (waypointIndex >= waypoints.Count)
        {
            return;
        }

        Vector3 movement = ((waypoints[waypointIndex] - transform.position).normalized / 100) * speed;
        transform.position = transform.position + movement;

        if ((waypoints[waypointIndex] - transform.position).magnitude < .3f)
        {
            waypointIndex++;
            graphGeneration.resetWaypoints();
        }
    }

    public void moveFinalApproach()
    {
        Vector3 movement = ((target.position - transform.position).normalized / 100) * speed;
        transform.position = transform.position + movement;
    }

    private void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;

        bulletTime = Timer;

        GameObject bulletObj = Instantiate(enemyBullet, SpawnPoint.transform.position, SpawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
        Destroy(bulletObj, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomba"))
        {
            speed = 0;
            if (!isSpeedModified)
            {
                isSpeedModified = true;
                originalSpeed = speed;
                speed = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bomba"))
        {
            speed = 4;
            if (isSpeedModified)
            {
                speed = originalSpeed;
                isSpeedModified = false;
            }
        }
    }
}

