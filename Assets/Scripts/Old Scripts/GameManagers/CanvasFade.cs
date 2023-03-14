using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasFade : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float  fadeOutDuration, fadeInDuration;
    const int fadeOutValue = 0, fadeInValue = 255;
    bool isFading = false;
    [SerializeField] GameObject player;


    private void Start()
    {
        FadeOut();
    }
    public void FadeOut()
    {
        fadeImage.DOFade(fadeOutValue, fadeOutDuration).SetEase(Ease.InSine);
        
    }

  
  public  IEnumerator FadeIn()
    {
        fadeImage.DOFade(fadeInValue, fadeInDuration);
        yield return new WaitForSeconds(fadeInDuration);
        FadeOut();
    }
}
