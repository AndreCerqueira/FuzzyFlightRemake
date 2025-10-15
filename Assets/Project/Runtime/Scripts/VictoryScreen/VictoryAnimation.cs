using DG.Tweening;
using UnityEngine;

public class VictoryAnimation : MonoBehaviour
{
    [Header("Victory Animation Settings")]
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _jumpDuration = 0.4f;
    [SerializeField] private float _spinDuration = 1f;
    [SerializeField] private float _maxRandomDelay = 0.8f;

    private Sequence _victorySequence;

    public void Awake()
    {
        PlayVictoryAnimation();
    }
    
    public void PlayVictoryAnimation()
    {
        if (_victorySequence != null && _victorySequence.IsActive())
            _victorySequence.Kill();

        var startPos = transform.position;
        var randomDelay = Random.Range(0f, _maxRandomDelay);

        _victorySequence = DOTween.Sequence();

        // Pequeno delay aleatório antes de começar
        _victorySequence.AppendInterval(randomDelay);

        // Loop infinito: salta e roda
        _victorySequence.Append(
            transform
                .DOMoveY(startPos.y + _jumpHeight, _jumpDuration)
                .SetEase(Ease.OutQuad)
                .SetLoops(-1, LoopType.Yoyo) // -1 = infinito
        );

        // Junta a rotação contínua em simultâneo
        _victorySequence.Join(
            transform
                .DOLocalRotate(new Vector3(0, 360f, 0), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
        );
    }

    public void StopVictoryAnimation()
    {
        if (_victorySequence != null)
            _victorySequence.Kill();

        transform.DOLocalRotate(Vector3.zero, 0.3f);
    }
}
