using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float attackRange = 10f; //攻击范围
    [Header("攻击间隔参数")]
    public float attackCooldown = 1f;//攻击冷却时间
    private float timer = 0f;//累加器
    public int attackDamage = 60;
    void Update()
    {
        timer += Time.deltaTime;//时间累加
       
        if (timer >= attackCooldown)
        {
            AttackEnemy();//执行攻击函数
            timer = 0f;//攻击后重置累加器
        }
        void AttackEnemy()//定义一个攻击函数
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");//定义一数组，存放游戏对象里有标签叫Enemy的游戏对象
            foreach (GameObject enemy in enemies)//遍历数组enemies
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);//计算当前脚本挂载的游戏对象到enemy的距离
                if (distance < attackRange)//判断敌人是否在攻击范围内
                {
                    EnemyMovement enemyScript = enemy.GetComponent<EnemyMovement>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(attackDamage);
                    }
                    break;
                }
            }
        }
    }
    
}
