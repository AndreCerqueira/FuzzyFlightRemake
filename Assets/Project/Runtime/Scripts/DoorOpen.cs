using UnityEngine;
using DG.Tweening;

public class DoorOpen : MonoBehaviour
{
    [Header("Portas")]
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    [Header("Efeitos das Portas")]
    [SerializeField] private Transform leftDoorEffect;
    [SerializeField] private Transform rightDoorEffect;
    private float effectMoveDistance = 20f; // dobrado de 5 → 10
    private float effectDuration = 0.2f;

    [Header("Configuração de Abertura")]
    [SerializeField] private float openSpeed = 20f;
    [SerializeField] private float openTime = 0.6f; // dobrado de 0.3 → 0.6
    [SerializeField] private bool disableAfterOpen = true;

    private SectionGenerator section;
    private float counter = 0f;
    private bool isOpening = false;
    private bool effectTriggered = false;

    void Awake()
    {
        // Apenas procura uma vez — otimização importante
        section = FindFirstObjectByType<SectionGenerator>();
    }

    void Update()
    {
        if (isOpening)
        {
            counter += Time.deltaTime;

            // Movimento suave das portas
            leftDoor.transform.Translate(Vector3.left * Time.deltaTime * openSpeed);
            rightDoor.transform.Translate(Vector3.right * Time.deltaTime * openSpeed);

            // Parar depois de um tempo definido
            if (counter >= openTime)
            {
                isOpening = false;

                if (!effectTriggered)
                {
                    effectTriggered = true;
                    TriggerDoorEffect();

                    if (disableAfterOpen)
                        GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpening && other.CompareTag("Ship"))
        {
            // Inicia o movimento de abertura
            isOpening = true;
            counter = 0f;
            effectTriggered = false;

            // Gera a próxima secção
            if (section != null)
                section.GenSection();
            else
                Debug.LogWarning("SectionGenerator não encontrado!");
        }
    }

    private void TriggerDoorEffect()
    {
        if (leftDoorEffect != null)
        {
            leftDoorEffect.DOMove(
                leftDoorEffect.position + Vector3.left * effectMoveDistance,
                effectDuration
            ).SetEase(Ease.OutQuad)
             .OnComplete(() => leftDoorEffect.gameObject.SetActive(false));
        }

        if (rightDoorEffect != null)
        {
            rightDoorEffect.DOMove(
                rightDoorEffect.position + Vector3.right * effectMoveDistance,
                effectDuration
            ).SetEase(Ease.OutQuad)
             .OnComplete(() => rightDoorEffect.gameObject.SetActive(false));
        }
    }
}
