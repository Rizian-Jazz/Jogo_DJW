using UnityEngine;
using System.Collections;

public class Bobbe : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public float Health = 100f;
    public bool isDead = false;
    public float chargeSpeed = 8f, timeOffScreen = 0.6f, stunnedTime = 1.5f;

    public Transform[] spawnPoints;

    private Vector2 targetSpot;
    private bool foundSpot = false;

    private enum State {Start, LeavingScreen, FindingSpot, ReturnToScreen, Charging, Stunned, AttackVertical, AttackSides, Dying}
    private State currentState;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        ChangeState(State.Start);
    }

    void ChangeState(State newState)
    {
        currentState = newState;
        StopAllCoroutines();
        switch (newState)
        {
            case State.Start: StartCoroutine(FaseStart()); break;
            case State.LeavingScreen: StartCoroutine(FaseLeavingScreen()); break;
            case State.FindingSpot: StartCoroutine(FaseFindingSpot()); break;
            case State.ReturnToScreen: StartCoroutine(FaseReturnToScreen()); break;
            case State.Charging: StartCoroutine(FaseCharging()); break;
            case State.Stunned: StartCoroutine(FaseStunned()); break;
            case State.AttackVertical: StartCoroutine(FaseAtackVertical()); break;
            case State.AttackSides: StartCoroutine(FaseAtackSides()); break;
            case State.Dying: StartCoroutine(FaseDying()); break;
        }
        

    }

    IEnumerator FaseStart()
    {
        anim.Play("Start");
        yield return new WaitForSeconds(1.5f);
        ChangeState(State.LeavingScreen);
    }

    IEnumerator FaseLeavingScreen()
    {
        anim.Play("LeavingScreen");       
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector2 exitPoint = spawnPoints[randomIndex].position;
       
        while(Vector2.Distance(transform.position, exitPoint) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, exitPoint, chargeSpeed * Time.deltaTime);
            yield return null;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        anim.SetBool("foundSpot", false);
        ChangeState(State.FindingSpot);       
    }

    IEnumerator FaseFindingSpot()
    {
        anim.Play("FindingSpot");

        int randomIndex;
        Vector2 CurrentSpot = transform.position;
        do //primeira vez da riza usando essa palavra - riza 🤭
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
            targetSpot = spawnPoints[randomIndex].position;
        }
        while (Vector2.Distance(CurrentSpot, targetSpot) < 0.5f);

        yield return new WaitForSeconds(0.7f);

        foundSpot = true;
        anim.SetBool("foundSpot", true);

        ChangeState(State.ReturnToScreen);
    }

    IEnumerator FaseReturnToScreen()
    {  
        transform.position = targetSpot;

        GetComponent<SpriteRenderer>().enabled = true;
        anim.Play("ReturnToScreen");

        yield return new WaitForSeconds(0.5f); 

        if(player != null)
        {
            float dx = Mathf.Abs(player.position.x - transform.position.x);
            float dy = Mathf.Abs(player.position.y - transform.position.y);

            if(dy > dx) ChangeState(State.AttackVertical);
            else ChangeState(State.AttackSides);
        }
        else ChangeState(State.Charging);

        yield break;
    }
    IEnumerator FaseAtackVertical()
    {
        anim.Play("Attack player (up n down) ");
        yield return new WaitForSeconds(1f);
        ChangeState(State.Charging);
    }

    IEnumerator FaseAtackSides()
    {
        anim.Play("Attack player from the sides ");
        yield return new WaitForSeconds(1f);
        ChangeState(State.Charging);
    }

    IEnumerator FaseCharging()
    {
        anim.Play("Charging");

        if (player == null) {ChangeState(State.ReturnToScreen); yield break;}

        Vector2 chargeDirection = (player.position - transform.position).normalized;
        float timeMax = 1.5f;
        float time = 0f;
        while(time < timeMax)
        {
            time += Time.deltaTime;
            transform.position = (Vector3)(chargeDirection * chargeSpeed * Time.deltaTime) + transform.position;
            yield return null;
        }

        ChangeState(State.LeavingScreen);

    }

    IEnumerator FaseStunned()
    {
        anim.Play("Stunned");
        yield return new WaitForSeconds(stunnedTime);
        ChangeState(State.LeavingScreen);
    }

    IEnumerator FaseDying()
    {
        anim.Play("Dying");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
       
    public void ReceberDano(int dano)
    {
        if (isDead) return;

        Health -= dano;
        anim.SetFloat("Health", Health);

        if (Health <= 0)
        {
           ChangeState(State.Dying);
            return;
        }

       ChangeState(State.Stunned);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(isDead) return;

        if(other.CompareTag("Player"))
        {
            anim.SetTrigger("hitPlayer");
            ChangeState(State.LeavingScreen);
        }

        else if (other.CompareTag("Wall"))
        {
            anim.SetTrigger("hitWall");
            ChangeState(State.Stunned);
        }
    }
}
    
    


  

