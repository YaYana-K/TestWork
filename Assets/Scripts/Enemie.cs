using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
    public static event Action<Enemie> OnDeath;
    public float Hp = 2;
    public float Damage = 1;
    public float AtackSpeed = 1;
    public float AttackRange = 2;
    [SerializeField] float recoveryHp = 2;


    public Animator AnimatorController;
    public NavMeshAgent Agent;

    private float lastAttackTime = 0;
    private bool isDead = false;


    private void Start()
    {
        SceneManager.Instance.AddEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);

    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Agent.isStopped = true;
            Die();
            return;
        }

        
        if(SceneManager.Instance.Player.isDead == false)
        {
            var distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);

            if (distance <= AttackRange)
            {
                Agent.isStopped = true;
                if (Time.time - lastAttackTime > AtackSpeed)
                {
                    lastAttackTime = Time.time;
                    SceneManager.Instance.Player.ChangeHp(-Damage);
                    AnimatorController.SetTrigger("Attack");
                }
            }
            else
            {
                Agent.isStopped = false;
                Agent.SetDestination(SceneManager.Instance.Player.transform.position);
            }
            AnimatorController.SetFloat("Speed", Agent.speed);
            Debug.Log(Agent.speed);
        }
        else
        {
            Agent.isStopped = true;
            AnimatorController.SetFloat("Speed", 0);
        }
    }

    public void Die()
    {
        SceneManager.Instance.RemoveEnemie(this);
        isDead = true;
        AnimatorController.SetTrigger("Die");
        SceneManager.Instance.Player.ChangeHp(recoveryHp);
        OnDeath?.Invoke(this);
        Invoke("DestroyObj", 2);
    }
    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
