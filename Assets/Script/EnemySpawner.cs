using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // --- 配置参数区 ---
    [Header("配置参数")]
    public GameObject enemyPrefab; //敌人预制体
    public Transform[] waypoints; //路点数组

    public float spawnInterval = 2f; //刷怪间隔
    public float timeBetweenWaves = 3f; //波次间隔

    // --- 状态机 ---
    //三种状态；
    //spawning = 正在出怪
    //WaitingForClear = 这一波出完了，等待玩家把场上怪打死
    //Interval = 怪物死光，休息倒计时，准备下一波
    public enum SpawnerState { Spawning, WaitingForClear, Interval};
    private SpawnerState currentState = SpawnerState.Interval; //声明一个状态变量，用于存放当前状态，默认值为Interval

    // ---波次数据---
    public int totalWaves = 3; //总共有几波
    public int currentWave = 0; //当前是第几波

    private int enemiesToSpawn = 0; //记录当前之波还需生成多少敌人，为0时说明当前敌人已经全部生成

    // --- 计时器 ---
    private float spawnTimer = 0f; //刷怪间隔计时器
    private float intervalTimer = 0f; //波次间隔计时器

    private void Start()
    {
        intervalTimer = timeBetweenWaves;
        Debug.Log("游戏开始，准备迎接第一波曹军");
    }
    void Update()
    {
        if (Time.timeScale == 0) return; //判断游戏是否暂停
        switch(currentState)
        {
            case SpawnerState.Interval:
                intervalTimer -= Time.deltaTime;

                if(intervalTimer <= 0f)
                {
                    StartNextWave(); //调用方法，开启下一波
                }
                break;

            case SpawnerState.Spawning:
                spawnTimer -= Time.deltaTime;
                if(spawnTimer <= 0f)
                {
                    SpawnEnemy();
                    enemiesToSpawn--;
                    spawnTimer = spawnInterval;
                    if (enemiesToSpawn <= 0)
                    {
                        currentState = SpawnerState.WaitingForClear;
                        Debug.Log("第" + currentWave + "波出怪完毕，等玩家清场");
                    }
                }
                break;

            case SpawnerState.WaitingForClear:
                if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    if(currentWave >= totalWaves)
                    {
                        Debug.Log("大获全胜！所有曹军以击败！");
                        currentState = SpawnerState.Interval;
                        intervalTimer = 99999f;
                    }
                    else
                    {
                        currentState = SpawnerState.Interval;
                        intervalTimer = timeBetweenWaves;
                        Debug.Log("清场完毕，准备下一波...");
                    }
                }
                break;
        }
            void SpawnEnemy()
            {
                if (waypoints == null || waypoints.Length == 00) return; //防御性编程
                GameObject newEnemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity); //实例化
                EnemyMovement moveScript = newEnemy.GetComponent<EnemyMovement>();//获取组件
                if (moveScript != null)
                {
                    moveScript.waypoints = waypoints;
                }
            }
            void StartNextWave()
            {
            currentWave++;
            enemiesToSpawn = currentWave * 5;
            spawnTimer = 0f;
            currentState = SpawnerState.Spawning;
            Debug.Log("===== 第" + currentWave + "波曹军来袭！数量" + enemiesToSpawn + "=====");
            }
        }
    }
