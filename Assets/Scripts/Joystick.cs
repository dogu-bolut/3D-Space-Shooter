using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector3 pointA;
    private Vector3 pointB;

    public Transform circle;
    public Transform outerCircle;

    void Start()
    {
        circle.GetComponent<SpriteRenderer>().enabled = false;
        outerCircle.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
            pointA = Camera.main.ScreenToWorldPoint(pointA);

            circle.transform.position = pointA;
            outerCircle.transform.position = pointA;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
            pointB = Camera.main.ScreenToWorldPoint(pointB);
        }
        else
        {
            touchStart = false;
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector3 offset = pointB - pointA;
            Vector3 direction = Vector3.ClampMagnitude(offset, 1.0f);
            MoveCharacter(direction);

            circle.transform.position = new Vector3(pointA.x + direction.x, pointA.y + direction.y, pointA.z + direction.z);
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }
}