
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Tiro : MonoBehaviour
{
    public GameObject bulletPrefab;
    public UnityEvent fireEvent;
    public float bulletSpeed = 10f;
     Vector2 bulletVelocity;
     Vector2 bulletDirection;
     public Transform firePoint; //arma

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletDirection = firePoint.right;
            bulletVelocity =  bulletSpeed * bulletDirection;
            bulletRb.linearVelocity = bulletVelocity;
            fireEvent.Invoke();
        }

    }


    
}
