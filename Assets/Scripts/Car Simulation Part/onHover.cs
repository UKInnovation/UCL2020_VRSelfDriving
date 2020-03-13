using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
     
     /// <summary>
     /// Start is called on the frame when a script is enabled just before
     /// any of the Update methods is called the first time.
     /// </summary>
     void Start()
     {
        Image[] images = transform.gameObject.GetComponentsInChildren<Image>();
        RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
        for(int i = 0; i < images.Length; i++){
            Color color = images[i].color;
            images[i].color = new Color(color.r,color.g,color.b,0.2f);
        }
        for(int i = 0; i < rawImages.Length; i++){
            Color color = images[i].color;
            rawImages[i].color = new Color(color.r,color.g,color.b,0.2f);
        }
     }
     public void OnPointerEnter(PointerEventData eventData)
     {
        Image[] images = transform.gameObject.GetComponentsInChildren<Image>();
        RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
        for(int i = 0; i < images.Length; i++){
            Color color = images[i].color;
            images[i].color = new Color(color.r,color.g,color.b,1f);
        }
        for(int i = 0; i < rawImages.Length; i++){
            Color color = images[i].color;
            rawImages[i].color = new Color(color.r,color.g,color.b,1f);
        }
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        Image[] images = transform.gameObject.GetComponentsInChildren<Image>();
        RawImage[] rawImages = transform.gameObject.GetComponentsInChildren<RawImage>();
        for(int i = 0; i < images.Length; i++){
            Color color = images[i].color;
            images[i].color = new Color(color.r,color.g,color.b,0.2f);
        }
        for(int i = 0; i < rawImages.Length; i++){
            Color color = images[i].color;
            rawImages[i].color = new Color(color.r,color.g,color.b,0.2f);
        }
     }

 }