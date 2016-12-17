using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    public GameObject SelectedTurretIndicator;

    [SerializeField]
    public GameObject BuyTurretInfoPanel;

    [SerializeField]
    public Text BuyTurretInfoText;

    [SerializeField]
    public GameObject NewWavePanel;

    [SerializeField]
    public Text TimeLeftText;

    [SerializeField]
    public GameObject GameOverPanel;

    [SerializeField]
    public Text GameOverStatus;

    [SerializeField]
    public Text WavesClearedStatus;

    // Use this for initialization
    void Awake () {
        Instance = this;
        this.BuyTurretInfoPanel.SetActive(false);
        this.NewWavePanel.SetActive(false);
	}

    void Start()
    {
        this.UpdateInfoPanel();
    }
	
    void FixedUpdate()
    {
        this.UpdateInfoPanel();
    }

    public void MouseEnterTurretToBuy(GameObject buyTurretButtonGO)
    {
        this.BuyTurretInfoText.text = buyTurretButtonGO.GetComponent<BuyTurretButton>().TurretData.Description;
        this.BuyTurretInfoPanel.SetActive(true);
    }

    public void UpdateTimeToNextWave(float curTime)
    {
        string oldText = this.TimeLeftText.text;
        string newText = Mathf.CeilToInt(curTime).ToString();
        if (oldText != newText) {
            this.TimeLeftText.text = newText;
            SoundController.Instance.PlayCountDownSFX();
        }
        this.NewWavePanel.SetActive(true);
    }

    public void ResetTimeToNextWave()
    {
        SoundController.Instance.PlayCountDownDone();
        this.NewWavePanel.SetActive(false);
    }

    public void MouseExitTurretToBuy()
    {
        this.BuyTurretInfoPanel.SetActive(false);
    }

    public void UpdateInfoPanel()
    {
        string hpColor = "lime";
        if (GameController.Instance.GameInstance.HomeBaseHP <= 3)
        {
            hpColor = "red";
        }
        LivesText.text = "Core HP:  " + "<color=" + hpColor + ">" + GameController.Instance.GameInstance.HomeBaseHP + "</color>";
        MoneyText.text = "Credits:  " +  "<color=yellow>" + GameController.Instance.GameInstance.Credits + " $ </color>";
        EnemiesText.text =  "Enemies:  " + "<color=red>" + GameController.Instance.GameInstance.EnemiesLeftInCurrentWave + "</color>";
        int curWaveSet = GameController.Instance.GameInstance.CurrentWaveSet;
        if (GameController.Instance.GameOver)
        {
            curWaveSet = GameController.Instance.GameInstance.TotalWaveSets;
        }
        WaveText.text = "Wave:  " + curWaveSet + "/" + GameController.Instance.GameInstance.TotalWaveSets;
    }

    public void ChangePositionOfSelectionIndicator(GameObject go)
    {
        Vector3 newPos = new Vector3(go.transform.position.x, this.SelectedTurretIndicator.transform.position.y, this.SelectedTurretIndicator.transform.position.z);
        this.SelectedTurretIndicator.transform.position = newPos;
        SoundController.Instance.OnNewTurret();
    }

    public void OnGameOver(bool gameWon)
    {
        if (gameWon)
        {
            this.GameOverStatus.text = "Game Won!";
        }
        else
        {
            this.GameOverStatus.text = "Game Over!";
        }

        int curWaveSet = GameController.Instance.GameInstance.CurrentWaveSet - 1;
        this.WavesClearedStatus.text = "Waves Cleared: " + curWaveSet + "/" + GameController.Instance.GameInstance.TotalWaveSets;

        StartCoroutine(this.FadeInGameOverScreen());
    }

    private IEnumerator FadeInGameOverScreen()
    {
        float t = 0;

        Vector3 startPos = this.GameOverPanel.transform.localPosition;
        Vector3 targetPos = Vector3.zero;

        while (t < 1f)
        {

            this.GameOverPanel.transform.localPosition = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, t));

            t += Time.deltaTime * 1f;

            yield return null;
        }
    }

    public void OnExitToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
