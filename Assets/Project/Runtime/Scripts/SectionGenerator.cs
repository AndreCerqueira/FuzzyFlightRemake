using Project.Runtime.Scripts;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] sectionPrefab;
    [SerializeField] GameObject finalPrefab;
    private int sectionCount = 0;
    [SerializeField] private int sectionLimit;

    public void GenSection()
    {
        if (sectionCount <= sectionLimit)
        {
            int index = Random.Range(0, 3);

            print(index);


            float chunkSize = sectionPrefab[index].GetComponent<Chunk>().size;
            Vector3 finalPosition = transform.position + new Vector3(0, 0, chunkSize * 5);

            Instantiate(sectionPrefab[index], finalPosition, Quaternion.identity);
            sectionCount++;
        }
        else if (sectionCount == sectionLimit + 1)
        {
            GenFinal();
            sectionCount++;
        }
        else
        {
            print("Section limit reached");
        }
    }

    public void GenFinal()
    {
        Instantiate(finalPrefab, transform.position + new Vector3(0, 0, (5.481222f * 5)), Quaternion.identity);
    }
}
