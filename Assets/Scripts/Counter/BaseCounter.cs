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

    protected virtual void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerChangeSelected;
        Hide();
    }
    
    private void PlayerChangeSelected(object sender, Player.OnSelectedCounterChangedEventArgs e)
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

    public virtual void Interact(Player player)
    {
        //Debug.Log("Interact " + this.name);
    }

    public virtual void InteractAlternate(Player player)
    {
       // Debug.Log("Interact Alternate " + this.name);
    }
}
