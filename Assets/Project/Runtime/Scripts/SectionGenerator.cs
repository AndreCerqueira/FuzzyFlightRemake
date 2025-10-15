using MoreMountains.Feedbacks;
using Project.Runtime.Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] GameObject initialPrefab;

    [SerializeField] MMF_Player enterFinalSceneFeedback;
    
    [SerializeField] GameObject[] Area1Size6Prefab;
    [SerializeField] GameObject[] Area1Size15Prefab;


    [SerializeField] GameObject[] Area2Size10Prefab;
    [SerializeField] GameObject[] Area2Size12Prefab;
    [SerializeField] GameObject[] Area2Size8Prefab;


    [SerializeField] GameObject[] Area3Size4Prefab;
    [SerializeField] GameObject[] Area3Size3Prefab;
    [SerializeField] GameObject[] Area3Size2Prefab;
    [SerializeField] GameObject[] Area3Size6Prefab;


    [SerializeField] GameObject finalPrefab;
    private int sectionCount = 0;
    private int sectionLimit = 13;

    void Start()
    {
        GenStart();
    }

    public void GenSection()
    {
        if (sectionCount < sectionLimit)
        {
            if (sectionCount < 5)
            {
                GenArea1();
            }
            else if (sectionCount < 8)
            {
                GenArea2();
            }
            else if (sectionCount < 13)
            {
                GenArea3();
            }
        }
        else if (sectionCount == sectionLimit)
        {
            GenFinal();
            sectionCount++;
        }
        else
        {
            print("Section limit reached");
            enterFinalSceneFeedback?.PlayFeedbacks();
        }
    }

    private void GenArea1()
    {
        if (sectionCount < 4)
        {
            int index = Random.Range(0, Area1Size6Prefab.Length);

            print(index);


            float chunkSize = Area1Size6Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area1Size6Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else
        {
            int index = Random.Range(0, Area1Size15Prefab.Length);

            print(index);


            float chunkSize = Area1Size15Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area1Size15Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
    }

    private void GenArea2()
    {
        if (sectionCount == 5)
        {
            int index = Random.Range(0, Area2Size10Prefab.Length);

            print(index);


            float chunkSize = Area2Size10Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area2Size10Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else if (sectionCount == 6)
        {
            int index = Random.Range(0, Area2Size12Prefab.Length);

            print(index);


            float chunkSize = Area2Size12Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area2Size12Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else
        {
            int index = Random.Range(0, Area2Size8Prefab.Length);

            print(index);


            float chunkSize = Area2Size8Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area2Size8Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
    }
    
    private void GenArea3()
    {
        if (sectionCount == 8)
        {
            int index = Random.Range(0, Area3Size4Prefab.Length);

            print(index);


            float chunkSize = Area3Size4Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area3Size4Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else if (sectionCount == 9)
        {
            int index = Random.Range(0, Area3Size3Prefab.Length);

            print(index);


            float chunkSize = Area3Size3Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, 0);

            Instantiate(Area3Size3Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else if (sectionCount == 10)
        {
            int index = Random.Range(0, Area3Size2Prefab.Length);

            print(index);


            float chunkSize = Area3Size2Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area3Size2Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else if (sectionCount == 11)
        {
            int index = Random.Range(0, Area3Size3Prefab.Length);

            print(index);


            float chunkSize = Area3Size3Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, 0);

            Instantiate(Area3Size3Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else
        {
            int index = Random.Range(0, Area3Size6Prefab.Length);

            print(index);


            float chunkSize = Area3Size6Prefab[index].GetComponent<Chunk>().offset;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(Area3Size6Prefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
    }

    public void GenStart()
    {
        Instantiate(initialPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GenFinal()
    {
        Instantiate(finalPrefab, transform.position + new Vector3(0, 0, (5.481222f * 5)), Quaternion.identity);
    }
}
