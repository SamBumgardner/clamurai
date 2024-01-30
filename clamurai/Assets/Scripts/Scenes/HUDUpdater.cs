using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour
{
    private Image health;

    private float healthWidth;

    private TextMeshProUGUI CollectibleCountText;

    public Player player;
    public ObjectiveManager objectiveManager;

    // Start is called before the first frame update
    void Start()
    {
        health = this.gameObject.transform.Find("Health").transform.GetComponent<Image>();
        healthWidth = health.rectTransform.rect.width;
        CollectibleCountText = this.gameObject.transform.Find("CollectibleCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null && player != null)
        {
            health.rectTransform.sizeDelta = new Vector2(healthWidth * player.health / player.healthMax, health.rectTransform.rect.height);
        }

        if (CollectibleCountText != null && objectiveManager != null)
        {
            CollectibleCountText.text = "x " + objectiveManager.GetRemainingStatusCount();
        }
    }
}
