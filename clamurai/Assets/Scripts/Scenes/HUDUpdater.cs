using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour
{
    private TextMeshProUGUI LivesText;

    private TextMeshProUGUI CollectibleCountText;

    public Player player;
    public ObjectiveManager objectiveManager;

    // Start is called before the first frame update
    void Start()
    {
        LivesText = this.gameObject.transform.Find("Lives").GetComponent<TextMeshProUGUI>();
        CollectibleCountText = this.gameObject.transform.Find("CollectibleCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LivesText != null && player != null)
        {
            LivesText.text = "x " + player.health;
        }

        if (CollectibleCountText != null && objectiveManager != null)
        {
            CollectibleCountText.text = "x " + objectiveManager.GetRemainingStatusCount();
        }
    }
}
