using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    
    public static bool ScoreControl = false;
    
    public Ease cubeEase;
    
    public GameObject cubeUI;
    
    public Camera cam;
    
    private Vector3 _screenPos;
    
    private Transform _target;
    private Transform _cube;
    
    public Transform UiCubeTarget;
    
    private Collider _cubeCollider;
    private void Start()
    {
        _cube = GetComponent<Transform>();
        _cubeCollider = GetComponent<Collider>();
        CubeMovement();
    }
    private void Update()
    {
        _target = _cube;
        _screenPos = cam.WorldToScreenPoint(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        _cubeCollider.enabled = !_cubeCollider.enabled;
        if (other.gameObject.CompareTag("Player"))
        {
            var position = _cube.transform.position;
            _cube.DOMoveY(position.y + 2f, 0.3f).SetLoops(2,LoopType.Yoyo).SetEase(cubeEase);
            _cube.DOMoveX(position.x + 1f, 0.3f).SetLoops(2, LoopType.Yoyo);
            _cube.DOScale(new Vector3(_cube.localScale.x + 0.15f, _cube.localScale.y + 0.15f, _cube.localScale.z + 0.15f),
                0.3f).SetLoops(2, LoopType.Yoyo).OnComplete(() => CubeUIInstantiate());
            Destroy(gameObject,0.6f);
        }
    }

    private void CubeUIInstantiate()
    {
        GameObject newcubeUI = Instantiate(cubeUI,new Vector3(_screenPos.x,_screenPos.y,10),Quaternion.identity) as GameObject;
        newcubeUI.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        newcubeUI.transform.DOMove(UiCubeTarget.transform.position, 1f).SetEase(Ease.InBack)
            .OnComplete(() => ScoreControl = true);
    }
    private void CubeMovement()
    {
        _cube.DOMoveY(_cube.transform.position.y +1f, 3f).SetLoops(-1, LoopType.Yoyo);
        _cube.transform.DORotate(new Vector3(0, 360, 60), 5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
    }
    
}
