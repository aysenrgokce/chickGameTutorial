using UnityEngine;

public enum PlayerState 
{ 
    // 5 aşamam var 
    Idle,//duran aşama
    Move, //hareket eden 
    Jump,//zıplayan 
    SlideIdle, //kayma modunda duran
    Slide, //kayma modunda aktif hareket eden 
    Roll 
}
