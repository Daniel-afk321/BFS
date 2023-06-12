using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private bool RotateTowardMouse;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    public float Health;
    public Slider HealthBar;
    public GameObject GameOver;

    private Animator animator;
    private Quaternion targetRotation;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        HealthBar.value = Health;
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);

        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        else
        {
            if (movementVector.magnitude == 0)
            {
                RotateInPlace();
            }
            else
            {
                RotateFromMouseVector();
            }
        }

        if (Health == 0)
        {
            GameOver.SetActive(true);
            animator.SetBool("dead", true);
        }
    }

    private void RotateInPlace()
    {
        if (targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        else
        {
            targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90f, transform.eulerAngles.z);
        }
    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tiro")
        {
            Health -= 10;
            HealthBar.value = Health;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(8))
        {
            Health -= 10;
            HealthBar.value = Health;
        }
    }
}
