
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;


public class Tiro : MonoBehaviour
{   
        public GameObject bulletPrefab;
        public UnityEvent fireEvent;
        public float bulletSpeed = 10f;
        Vector2 bulletVelocity;
        Vector2 bulletDirection;
        public Transform firePoint; //arma

        interpolateMovement movement;

        void Awake()
    {
        movement = GetComponent<interpolateMovement>();
    }

        public void Fire(InputAction.CallbackContext context)
        {
            if (context.performed && movement.canThrow)
            {
                GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                bulletDirection = movement.lastInput.normalized;
                bulletVelocity =  bulletSpeed * bulletDirection;
                bulletRb.linearVelocity = bulletVelocity;
                fireEvent.Invoke();

                StartCoroutine(movement.Throw());
            }

        } 
    
}
