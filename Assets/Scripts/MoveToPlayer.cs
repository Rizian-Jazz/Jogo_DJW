using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
   public Transform player;
   public float speed = 3f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);   
    }
}
