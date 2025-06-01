using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStateIU : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedtranform;
    [SerializeField] private RectTransform _boosterJumptranform;
    [SerializeField] private RectTransform _boosterSlowtranform;

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _holyBoosterWheatImage;
    [SerializeField] private Image _rottenBoosterWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;
    [Header("Setting")]
    [SerializeField] private float _moveDuraction;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedtranform;
    public RectTransform GetBoosterJumpTransform => _boosterJumptranform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowtranform;
    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetRottenBoosterWheatImage => _rottenBoosterWheatImage;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImage;

    private Image _PlayerWalkingImage;
    private Image _PlayerSlidingImage;
    private void Awake()
    {
        _PlayerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _PlayerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }


    private void Start()
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;
            // üsteki kart açılacak 
            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterfaces(_playerSlidingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;  //alttaki kart açılacak
        }
    }


    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
            RectTransform activeTransform, RectTransform passiveTransform)
    {
        _PlayerWalkingImage.sprite = playerWalkingSprite;
        _PlayerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuraction).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuraction).SetEase(_moveEase);
    }
    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImages,
     Image wheatImages, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImages.sprite = activeSprite;
        wheatImages.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuraction).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImages.sprite = passiveSprite;
        wheatImages.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuraction).SetEase(_moveEase);
    }

    public void PlayBoosterAnimations(RectTransform activeTransform, Image boosterImages,
     Image wheatImages, Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImages, wheatImages, activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }
}