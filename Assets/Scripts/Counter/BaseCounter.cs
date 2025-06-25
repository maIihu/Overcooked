using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    private GameObject _visualGameObject;

    protected Transform CounterTopPoint;

    protected virtual void Awake()
    {
        _visualGameObject = transform.Find("Selected").gameObject;
        CounterTopPoint = transform.Find("CounterTopPoint").transform;
    }

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerChangeSelected;
        Hide();
    }
    
    private void PlayerChangeSelected(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == this)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        _visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        _visualGameObject.SetActive(false);
    }

    public virtual void Interact(PlayerController player)
    {
        //Debug.Log("Interact " + this.name);
    }

    public virtual void InteractAlternate(PlayerController player)
    {
       // Debug.Log("Interact Alternate " + this.name);
    }
}
