using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasFade : MonoBehaviour
{
    [SerializeField] public Image fadeImage;
    [SerializeField] float  fadeOutDuration, fadeInDuration;
    const int fadeOutValue = 0, fadeInValue = 255;
  
    [SerializeField] GameObject player;


    private void Start()
    {
        FadeOut();
    }
    
    public void FadeOut()
    {
        fadeImage.DOFade(fadeOutValue, fadeOutDuration).SetEase(Ease.InSine);
        
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) { ;  FadeIn(); }
    }
    public void FadeIn()
    {
        fadeImage.DOFade(fadeInValue, fadeInDuration + 15);

        
    }
  

}
