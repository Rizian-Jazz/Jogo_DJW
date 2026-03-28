
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Tiro : MonoBehaviour
{
    public GameObject bulletPrefab;
    public UnityEvent fireEvent;
    public float bulletSpeed = 10f;
    public Rigidbody2D rb;
     Vector2 bulletVelocity;
     Vector2 bulletDirection;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collider"))
        {
            Destroy(gameObject);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletDirection = context.ReadValue<Vector2>();
            bulletVelocity = transform.right * bulletSpeed;
            bulletRb.linearVelocity = bulletVelocity;
            fireEvent.Invoke();
        }
    }
}
