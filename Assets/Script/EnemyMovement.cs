using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // 定义三个变量：路点数组、速度、当前索引
    public Transform[] waypoints;
    public float speed = 3f;
    private int currentIndex = 0;

    // --- 生命值系统 ---
    public int maxHp = 100; //最大生命值
    private int currentHp ; //当前生命值

     void Start()
    {
        currentHp = maxHp; //开始时，当前生命值等于最大生命值  
    }

    void Update()
    {
        // 判断：如果当前索引 小于 路点数组的长度
        if (currentIndex < waypoints.Length)
        {
            // 移动：让物体的位置，移动到当前目标点，速度乘上 Time.deltaTime
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentIndex].position, speed * Time.deltaTime);

            // 判断是否到达：如果当前位置和当前目标点的距离 小于 0.1f
            if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 0.1f)
            {
                // 索引加 1，去下一个点
                currentIndex++;
            }
        }
        else
        {
            //单例Instance调用
            if(BaseManager.Instance != null)
            {
                BaseManager.Instance.BaseDamage();
            }
           
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) //受伤逻辑
    {
        currentHp -= damage;
        Debug.Log($"敌人受到 {damage} 点伤害，剩余血量：{currentHp}");
        if (currentHp <= 0)
        {
            Die();
        }
    }
    private void Die() //死亡逻辑
    {
        Debug.Log("敌人死亡");
        BaseManager.Instance.AddGold(160);
        Destroy(gameObject);
        
    }
}