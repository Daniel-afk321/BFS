using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    private Rigidbody tankRigidbody;
    private Animator animator; 

    public float Health;
    public Slider HealthBar;
    public GameObject GameOver;
    private bool isMoving = false;
    //pedro
    public AudioSource audioPlayer;
    

    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
    }

    void Start()
    {
        HealthBar.value = Health;
    }

    void Update()
    {
        if (Health == 0)
        {
            Time.timeScale = 0f;
            GameOver.SetActive(true);
            
            animator.SetBool("dead", true);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        MoveTank(moveInput);
        RotateTank(rotationInput);
    }

    private void MoveTank(float input)
    {
        Vector3 movement = transform.forward * input * moveSpeed * Time.deltaTime;
        tankRigidbody.MovePosition(tankRigidbody.position + movement);
        if (input != 0)
        {
            animator.SetBool("run", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("run", false);
            isMoving = false;
        }

    }

    private void RotateTank(float input)
    {
        float rotation = input * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);
        tankRigidbody.MoveRotation(tankRigidbody.rotation * deltaRotation);
        if (isMoving)
        {
            animator.SetBool("run", true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tiro")
        {
            audioPlayer.Play();
            Health -= 10;
            HealthBar.value = Health;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(8))
        {
            audioPlayer.Play();
            Health -= 10;
            HealthBar.value = Health;
        }
    }
}
