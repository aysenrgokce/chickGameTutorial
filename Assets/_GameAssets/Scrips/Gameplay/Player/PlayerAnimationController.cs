using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    //
    [SerializeField] private Animator _playerAnimator;
    private PlayerController _playerController;
    private StateController _stateController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }
    
    private void Start(){
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    private void Update()
    {
        SetPlayerAnimations();
    }
    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    // oyuncunun animasyonlarını bulunduğu duruma göre ayarlayan bir fonksiyon 
    private void SetPlayerAnimations()
    {
        //oyuncunun şu anki durumu alınır
        var currentState = _stateController.GetCurrentState();

        //currentState durumuna göre işlem yapıcaz 
        switch (currentState)
        {
            case PlayerState.Idle://Oyuncu boşta duruyorsa:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);//Kayma animasyonu devre dışı (false)
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);//Hareket animasyonu devre dışı (false)
                break;
            case PlayerState.Move://Oyuncu yürüyorsa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);//Kayma animasyonu kapalı
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);//Hareket animasyonu aktif
                break;
            case PlayerState.SlideIdle://Oyuncu kayma pozisyonunda ama hareket etmiyorsa:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);//Kayma animasyonu açık (pozisyonda)
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);//Aktif kayma animasyonu kapalı (kaymıyor)
                break;
            case PlayerState.Slide://Oyuncu gerçekten kayıyorsa:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);//Kayma animasyonu açık
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);//Kayma aktif animasyonu da açık
                break;

        }
    }


}
