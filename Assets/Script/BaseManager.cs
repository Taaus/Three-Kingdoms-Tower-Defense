using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseManager : MonoBehaviour
{
    // 定义静态实例（这是单例的核心）
    //大括号里的get; private set;是 c# 语法，意思是外部脚本可以读取Instance，但不能修改它
    public static BaseManager Instance{get; private set;}

    public int baseHp = 10;//基地血量
    public int gold = 0; // 金币
    public int startGold = 200; //初始金币
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        // 第一次初始化，如果Instance为空，把自己赋给它
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)//保证全局唯一
        {
            Destroy(Instance);
        }
        gold = startGold;
        goldText.text = $"金币: {gold}";
    }
    public void BaseDamage() //基本损坏，扣除血量
    {
        baseHp --;
        Debug.Log($"荆州受到攻击！ 当前血量:{baseHp}");
        if(hpText != null )
        {
            hpText.text = $"荆州生命值: {baseHp}";
        }
        if(baseHp <= 0)
        {
            Time.timeScale = 0f;
            Debug.Log("荆州城破,Game Over");
        }
    }
    public void AddGold(int amount) //增加金币
    {
        gold += amount;
        if (goldText != null)
        {
            goldText.text = $"金币: {gold}";
        }
    }
    public bool SpendGold(int amount) //消费金币
    {
        if(gold >= amount)
        {
            gold -= amount;
            if (goldText != null)
            {
                goldText.text = $"金币: {gold}";
            }
            return true;
        }
        else return false;
    }
}
