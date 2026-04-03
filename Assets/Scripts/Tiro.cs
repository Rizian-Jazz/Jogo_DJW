
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
        Vector2 bulletDirection = Vector2.right;
        public Transform firePoint; //arma
        Coroutine FireCoroutine;

        interpolateMovement movement;

        void Awake()
    {
        movement = GetComponent<interpolateMovement>();
    }

        public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireCoroutine = StartCoroutine(FireLoop());
        }
        else if (context.canceled)
        {
            if (FireCoroutine != null)
            {
                StopCoroutine(FireCoroutine);
            }
        }
    } 
    
    IEnumerator FireLoop()
    {
        while (true)
        {
            if (movement.canThrow)
            {
                GameObject bullet = Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    firePoint.rotation
                );

                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                bulletDirection = movement.lastInput;

                if (bulletDirection == Vector2.zero)
                {
                    bulletDirection = Vector2.right;
                }

                bulletVelocity = bulletDirection.normalized * bulletSpeed;
                bulletRb.linearVelocity = bulletVelocity;

                fireEvent.Invoke();

                yield return StartCoroutine(movement.Throw());
            }

            yield return null;
        }            
    }
}