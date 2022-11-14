using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextController : MonoBehaviour
{
    [Header("===Score Text===")]
    public Text scoreText;
    [Header("===Cube Image===")]
    public Image cubeImage;
    
    public  int score;
    public float duration,strength, randomnes;
    public int vibrato;
    void Update()
    {
        scoreText.text = "" + score;
        TextShake();
    }

    public void TextShake()
    {
        if (CollectObject.ScoreControl)
        { 
            score += 1;
            cubeImage.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.5f).From();
            scoreText.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.5f).From();
            CollectObject.ScoreControl = false;
        }
    }
}
