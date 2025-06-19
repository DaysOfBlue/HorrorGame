using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject itemPrefab;
    public GameObject monsterPrefab;
    public Transform[] itemSpawnPoints;
    int itemCnt = 0;

    private int preIdx = 0;
    // Change progress by calling this method.

    void SpawnItem()
    {
        int idx = Random.Range(0, itemSpawnPoints.Length);
        if (preIdx == idx)
        {
            idx += 1;
            idx = idx % itemSpawnPoints.Length;
        }
        GameObject item = Instantiate(itemPrefab, itemSpawnPoints[idx].position, Quaternion.identity);
        preIdx = idx;
        
        if (itemCnt == 2 || itemCnt == 4 || itemCnt == 6)
        {
            idx = Random.Range(0, itemSpawnPoints.Length);
            if (preIdx == idx)
            {
                idx += 1;
                idx = idx % itemSpawnPoints.Length;
            }
            GameObject monster = Instantiate(monsterPrefab, itemSpawnPoints[idx].position, Quaternion.identity);
            monster.GetComponent<Monster>().SD();
        }
    }

    public void AddItemCount(int input)
    {
        itemCnt += input;
        Debug.Log("Item Counts : " + itemCnt);
        SpawnItem();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnItem();
        }
    }

}
