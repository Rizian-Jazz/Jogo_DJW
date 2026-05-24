using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class Shot : MonoBehaviour
{   
        public GameObject bulletPrefab;
        public UnityEvent fireEvent;
        public float bulletSpeed = 10f, bulletInterval = 0.7f;
        Vector2 bulletDirection;
        public Transform firePoint; 
        Coroutine FireCoroutine, ThrowCoroutine;
        public bool canThrow = true;


        public void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("Fire called: " + context.phase);

            if (context.performed)
            {
                if (FireCoroutine != null) return;
                bulletDirection = context.ReadValue<Vector2>();
                FireCoroutine = StartCoroutine(FireLoop());
            }
            if (context.canceled)
            {
                if (FireCoroutine != null)
                {
                    StopCoroutine(FireCoroutine);
                    FireCoroutine = null;
                }
            }
        } 
    
    IEnumerator FireLoop()
    {
        while (true)
        {
            if (canThrow)
            {
                GameObject bullet = Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    firePoint.rotation  
                );

                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                if (bulletDirection == Vector2.zero)
                {
                    bulletDirection = Vector2.right;
                }

                Vector2 bulletVelocity = bulletDirection.normalized * bulletSpeed;
                bulletRb.linearVelocity = bulletVelocity;

                fireEvent.Invoke();
                ThrowCoroutine = StartCoroutine(Throw());
                yield return ThrowCoroutine;
            }

            yield return null;
        }            
    }
    public IEnumerator Throw()
    {
        canThrow = false;
        yield return new WaitForSeconds(bulletInterval);
        canThrow = true;
    }
}