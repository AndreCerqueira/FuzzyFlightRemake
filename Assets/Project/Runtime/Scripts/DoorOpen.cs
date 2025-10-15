using System.Collections;
using DG.Tweening;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    private SectionGenerator section;
    
    [Header("Door Effects")]
    [SerializeField] private Transform leftDoorEffect; 
    [SerializeField] private Transform rightDoorEffect; 
    private float effectMoveDistance = 5f; 
    private float effectDuration = 0.2f; 

    float speed = 0f;
    private float counter = 0f;
    private bool effectTriggered = false;

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

            // Movimenta as portas
            leftDoor.transform.Translate(Vector3.left * Time.deltaTime * speed);
            rightDoor.transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (counter > 0.3f)
            {
                speed = 0f;

                if (!effectTriggered)
                {
                    effectTriggered = true;
                    TriggerDoorEffect();
                }
            }
        }
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
    
    private void TriggerDoorEffect()
    {
        // Move o efeito na mesma direção da porta usando DOTween
        if (leftDoorEffect != null)
        {
            leftDoorEffect.DOMove(leftDoorEffect.position + Vector3.left * effectMoveDistance, effectDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => leftDoorEffect.gameObject.SetActive(false)); // opcional: desativar efeito depois
        }

        if (rightDoorEffect != null)
        {
            rightDoorEffect.DOMove(rightDoorEffect.position + Vector3.right * effectMoveDistance, effectDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => rightDoorEffect.gameObject.SetActive(false));
        }
    }
}