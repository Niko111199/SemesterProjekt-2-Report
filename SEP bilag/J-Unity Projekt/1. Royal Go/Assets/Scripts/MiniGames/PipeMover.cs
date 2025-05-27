//dette skript tage og laver en object pool af pipeprefabs og flytter dem hen over scenen

using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [Header("Pipe Settings")]
    public GameObject pipePrefab;
    public int poolSize = 5;
    public float spawnRate = 2f;
    public float pipeSpeed = 2f;
    public float minY = -1f;
    public float maxY = 2f;
    public float spawnXPosition = 1000f;

    public Queue<GameObject> pipePool;
    private float timer;

    void Start()
    {
        pipePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pipePrefab,transform);
            obj.SetActive(false);
            pipePool.Enqueue(obj);
        }

        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }

        MovePipes();
    }

    void SpawnPipe()
    {
        GameObject pipe = pipePool.Dequeue();

        float randomY = Random.Range(minY, maxY);
        pipe.transform.position = new Vector3(spawnXPosition, randomY, 0f);
        pipe.SetActive(true);

        pipePool.Enqueue(pipe);
    }

    void MovePipes()
    {
        foreach (GameObject pipe in pipePool)
        {
            if (pipe.activeInHierarchy)
            {
                pipe.transform.position += Vector3.left * pipeSpeed * Time.deltaTime;

                if (pipe.transform.position.x < -850f)
                {
                    pipe.SetActive(false);
                }
            }
        }
    }

    public void ReturnPipeToPool(GameObject pipe)
    {
        pipe.SetActive(false);
        pipePool.Enqueue(pipe);
    }
}

//skrevet af Nikolaj Bræmer Christensen
//Valideret af: Victor