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
                if (ThrowCoroutine != null)
                {
                    StopCoroutine(ThrowCoroutine);
                    ThrowCoroutine = null;
                }
                canThrow = true;
               
            }
        } 
    
    IEnumerator FireLoop()
    {
        while (true)
        {
            if (canThrow)
            {
                float angle = 0f;

                if (bulletDirection.x > 0) angle = 180f;      
                else if (bulletDirection.x < 0) angle = 0f;  
                else if (bulletDirection.y > 0) angle = 270f;  
                else if (bulletDirection.y < 0) angle = 90f;    

                transform.rotation = Quaternion.Euler(0f, 0f, angle);

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