using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [Header("Player Configurations")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject Vfx;
    [SerializeField] Transform pos;
    [SerializeField] float xMoveSpeed = 1f;
    [SerializeField] float zMoveSpeed = 1f;
    [SerializeField] float slope;
    [SerializeField] float waitTime = 1f;
    AudioSource laser;
    float minX, maxX, minZ, maxZ;
    float fireTiming = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        laser = GetComponent<AudioSource>();
        float padding = 1.0f;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minZ = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).z + padding;
        maxZ = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).z - padding;
    }
    private void Update()
    {
        rb.position = new Vector3(rb.position.x, 0, rb.position.z);
        Move();
        if (Input.GetButton("Fire1") && Time.time > fireTiming)
        {
            fireTiming = Time.time + waitTime;
            Instantiate(projectile, pos.position, Quaternion.identity);
            laser.Play();
        }
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * xMoveSpeed;
        var deltaZ = Input.GetAxis("Vertical") * Time.deltaTime * zMoveSpeed;

        Vector3 newPosition = rb.position + new Vector3(deltaX, 0, deltaZ);

        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedZ = Mathf.Clamp(newPosition.z, minZ, maxZ);

        rb.velocity = new Vector3(clampedX - rb.position.x, 0, clampedZ - rb.position.z);

        rb.position = new Vector3(clampedX, 0, clampedZ);

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -slope);
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
