using System.Collections;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    private SectionGenerator section;

    float speed = 0f;
    private float counter = 0f;

    void Awake()
    {
        section = FindFirstObjectByType<SectionGenerator>();
    }

    void Update()
    {
        section = FindFirstObjectByType<SectionGenerator>();
        
        if (speed > 0f)
        {
            counter += Time.deltaTime;

            if (counter > 0.3f)
            {
                speed = 0f;
            }
        }

        leftDoor.transform.Translate(Vector3.left * Time.deltaTime * speed);
        rightDoor.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            speed = 20f;

            section.GenSection();

            GetComponent<BoxCollider>().enabled = false;
        }
    }
}