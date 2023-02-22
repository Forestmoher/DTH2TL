using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] private GameObject _solidBody;
    [SerializeField] private GameObject _transparentBody;
    
    private void Awake()
    {
        ShowSolid();
    }

    public void ShowSolid()
    {
        _solidBody.SetActive(true);
        _transparentBody.SetActive(false);
    }

    public void ShowTransparent()
    {
        _solidBody.SetActive(false);
        _transparentBody.SetActive(true);
    }
}
