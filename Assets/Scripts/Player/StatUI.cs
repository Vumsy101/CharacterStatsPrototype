using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public GameObject player;
    
    public Text healthText;
    public Text strengthText;

    public PlayerStats playerstats;
    // Start is called before the first frame update
    void Start()
    {
        playerstats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerstats.Health.Value;
        strengthText.text = "Strength: " + playerstats.Strength.Value;
    }
}
