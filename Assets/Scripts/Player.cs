using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 vec;
    [Header("Player Configurations")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject Vfx;
    [SerializeField] Transform pos;
    [SerializeField] float xMoveSpeed = 1f;
    [SerializeField] float zMoveSpeed = 1f;
    [SerializeField] float slope;
    [SerializeField] float waitTime = 1f;
    AudioSource laser;

    [Header("Boundaries")]
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float zMin;
    [SerializeField] float zMax;
    float fireTiming = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laser = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > fireTiming)
        {
            fireTiming = Time.time + waitTime;
            Instantiate(projectile, pos.position, Quaternion.identity);
            laser.Play();
        }
    }

    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * xMoveSpeed;
        var deltaZ = Input.GetAxis("Vertical") * Time.deltaTime * zMoveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newZPos = Mathf.Clamp(transform.position.z + deltaZ, zMin, zMax);
        vec = new Vector3(deltaX, 0, deltaZ);

        rb.linearVelocity = vec;

        rb.position = new Vector3(newXPos, 0, newZPos);

        rb.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.x * -slope);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            Instantiate(Vfx, transform.position, transform.rotation);
            FindObjectOfType<Spawn>().GameOver();
        }
    }
}
