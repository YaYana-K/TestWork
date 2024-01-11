using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;
    public float speed = 5f;

    private float lastAttackTime = 0;
    private bool isDead = false;
    private bool isMove = false;
    private bool isAttack = false;
    private Vector2 move;
    private Enemie closestEnemie = null;

    public Animator AnimatorController;

    private void Update()
    {

        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }

        if (isMove)
        {
            Move();
        }
        if (isAttack)
        {
            Attack();
        }

        var enemies = SceneManager.Instance.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }
        
    }

    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        isMove = true;
    }

    private void Move()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y) * speed * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.10f);
            AnimatorController.SetFloat("Speed", speed);
        }
        else
        {
            isMove = false;
            AnimatorController.SetFloat("Speed", 0);
        }
        
        transform.Translate(movement, Space.World);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttack = true;
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime > AtackSpeed)
        {
            lastAttackTime = Time.time;
            AnimatorController.SetTrigger("Attack");
            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
                if (distance <= AttackRange)
                {
                    transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);
                    closestEnemie.Hp -= Damage;
                }
            }
        }
        isAttack = false;
    }

    public void SuperAttack()
    {
        if(!isDead)
        {
            AnimatorController.SetTrigger("DoubleAttack");
            Debug.Log("super attack");
            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
                if (distance <= AttackRange)
                {
                    transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);
                    closestEnemie.Hp -= Damage * 2;
                }
            }
        }
    }
}
