using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Image = UnityEngine.UI.Image;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform icon;
    
    private List<Image> _children = new List<Image>(); 
    private float _ogScale = -1.0f;
    
    private const float TweenTime = 0.3f;

    private void Start()
    {
        _children = GetChildImages();
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        foreach (var child in _children)
        {   
            child.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (var child in _children.Where(child => child != null))
        {
            child.raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_ogScale < 0f) _ogScale = transform.localScale.x;
        icon.DOScale(_ogScale * 1.3f, TweenTime).SetEase(Ease.InExpo);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        icon.DOScale(_ogScale, TweenTime).SetEase(Ease.InExpo);
    }
    
    //UTILS
    private List<Image> GetChildImages()
    {
        var children = this.GetComponentsInChildren<Image>().Where(x => x.raycastTarget).ToList();
        children.Add(this.GetComponent<Image>());
        
        return children.Where(child => child != null).ToList();
    }

}
