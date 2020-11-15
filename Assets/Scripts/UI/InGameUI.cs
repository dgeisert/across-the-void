using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;
    public Image air, propellant;
    private float currentAir, currentPropellant;

    private void Start()
    {
        Instance = this;
        AddO2(100);
        AddPropellant(100);
    }

    private void Update()
    {
        AddO2(-Time.deltaTime);
    }

    public void AddO2(float add)
    {
        currentAir += add;
        currentAir = Mathf.Min(currentAir, 100);
        if(currentAir <=0)
        {
            Game.Instance.Defeat();
        }
        air.GetComponent<RectTransform>().sizeDelta = new Vector2 (currentAir * 2, 20);
    }
    public void AddPropellant(float add)
    {
        currentPropellant += add;
        currentPropellant = Mathf.Min(currentPropellant, 100);
        propellant.GetComponent<RectTransform>().sizeDelta = new Vector2 (currentPropellant * 2, 20);
    }

    public TextMeshProUGUI scoreText;

    public void UpdateScore(float val)
    {
        scoreText.text = "Score: " + val.ToString("#,#");
    }
    public void EndGame(bool victory)
    {
        gameObject.SetActive(false);
    }
}