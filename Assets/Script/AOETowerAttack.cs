using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETowerAttack : MonoBehaviour
{
    // --- 面板配置区 ---
    public float attackRange; // 攻击距离
    public int attackDamage = 15; // 攻击伤害
    public float attackCooldown = 1f; //攻击CD
    public string enemyTag = "Enemy";

    // --- 内部变量区 ---

    private float timer = 0;
    // --- 外部引用区 ---

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            DoAOEDamage();
            timer = 0;
        }
    }
    void DoAOEDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        bool hitEnemy = false;
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                EnemyMovement enemyScript = col.GetComponent<EnemyMovement>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(attackDamage);
                    hitEnemy = true;
                }
            }
        }
        if (hitEnemy)
        {
            Debug.Log("周瑜释放神火计");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);//测试vs自带的git
    }
}
