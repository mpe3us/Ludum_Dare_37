using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public static GameUIController Instance;

    [SerializeField]
    private GameObject buyableTurretsPanel;

    [SerializeField]
    public Text LivesText;

    [SerializeField]
    public Text MoneyText;

    [SerializeField]
    public Text WaveText;

    [SerializeField]
    public Text EnemiesText;

    // Use this for initialization
    void Awake () {
        Instance = this;
	}

    void Start()
    {
        this.UpdateInfoPanel();
    }
	
    void FixedUpdate()
    {
        this.UpdateInfoPanel();
    }

	public void UpdateInfoPanel()
    {
        LivesText.text = "Core HP: " + GameController.Instance.GameInstance.HomeBaseHP;
        MoneyText.text = "Credits: " + GameController.Instance.GameInstance.Credits;
        EnemiesText.text =  "Enemies: " + GameController.Instance.GameInstance.EnemiesLeftInCurrentWave;
    }

}
