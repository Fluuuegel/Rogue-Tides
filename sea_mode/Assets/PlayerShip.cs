using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public float hp = 100f;
    public float maxSpeed = 15f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float rotationSpeed = 100f;
    public float attackPower = 10f;
    public float shieldPower = 20f;
     public Image steeringWheelUI; 
     public Slider throttleBar;
    public Slider playerHpBar;
    private float currentSpeed = 0f;
    private Rigidbody2D rb;
    private bool attackMode = false;

    public GameObject bombPrefab;
public Transform bombSpawnPoint;
public LineRenderer bombTrajectory;
public int trajectoryPoints = 30;
public float launchForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();

        if (playerHpBar != null)
{
    playerHpBar.value = hp / 100f;
}

         if (Input.GetKeyDown(KeyCode.E))
    {
        attackMode = !attackMode; // Toggle attack mode
         bombTrajectory.enabled = attackMode;
    }

         if (steeringWheelUI != null)
        {
            steeringWheelUI.rectTransform.rotation = Quaternion.Euler(0f, 0f, rb.rotation);
        }

        if (throttleBar != null)
{
    throttleBar.value = currentSpeed / maxSpeed;
}

 if (attackMode)
    {
        DrawMouseTrajectory();

       if (Input.GetMouseButtonDown(0)) // 0 = Left mouse button
{
    DropBombTowardMouse();
}
    }
    
    }



void DrawMouseTrajectory()
{
    if (bombPrefab == null)
    {
        Debug.LogWarning("Bomb prefab is not assigned!");
        return;
    }
    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorld.z = 0f;

    Vector3 start = transform.position;
    Vector3 end = mouseWorld;

    bombTrajectory.positionCount = trajectoryPoints;

    float arcHeight = 2f;

    for (int i = 0; i < trajectoryPoints; i++)
    {
        float t = i / (float)(trajectoryPoints - 1);
        Vector3 point = Vector3.Lerp(start, end, t);

        // Fake the arc by adding height offset (like a parabola)
        float arc = Mathf.Sin(t * Mathf.PI) * arcHeight;
        point.y += arc;

        bombTrajectory.SetPosition(i, point);
    }
}


void DropBombTowardMouse()
{
    if (bombPrefab == null)
    {
        Debug.LogWarning("bombPrefab is null in DropBombTowardMouse!");
        return;
    }

    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorld.z = 0f;

    GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

    TopDownBomb bombScript = bomb.GetComponent<TopDownBomb>();
    if (bombScript != null)
    {
        bombScript.Init(mouseWorld);
    }
    else
    {
        Debug.LogWarning("TopDownBomb script missing on bombPrefab!");
    }
}


    void HandleMovement()
    {
        float rotateInput = Input.GetAxisRaw("Horizontal"); // Left / Right
        float throttleInput = Input.GetAxisRaw("Vertical"); // Up / Down

        // Rotate
        rb.MoveRotation(rb.rotation - rotateInput * rotationSpeed * Time.deltaTime);

        // Acceleration / deceleration logic
        if (throttleInput > 0 && currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (throttleInput < 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            // No input: slow down naturally
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Clamp the speed
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Move forward based on currentSpeed
        Vector2 movement = transform.up * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Enemy"))
    {
        hp -= 20f; // damage amount
        Debug.Log("Player hit! HP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("Player destroyed!");
            // Optional: Destroy(gameObject);
        }
    }
    }



Vector2 CalculateLaunchVelocity(Vector2 start, Vector2 end, float height)
{
    float gravity = Mathf.Abs(Physics2D.gravity.y);

    float displacementY = end.y - start.y;
    float displacementX = end.x - start.x;

    float timeUp = Mathf.Sqrt(2 * height / gravity);
    float timeDown = Mathf.Sqrt(2 * (height - displacementY) / gravity);
    float totalTime = timeUp + timeDown;

    float velocityY = Mathf.Sqrt(2 * gravity * height);
    float velocityX = displacementX / totalTime;

    return new Vector2(velocityX, velocityY);
}

float EstimateFlightTime(Vector2 start, Vector2 end, float height)
{
    float gravity = Mathf.Abs(Physics2D.gravity.y);
    float displacementY = end.y - start.y;

    float timeUp = Mathf.Sqrt(2 * height / gravity);
    float timeDown = Mathf.Sqrt(2 * (height - displacementY) / gravity);
    return timeUp + timeDown;
}
}
