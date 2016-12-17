using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController Instance;

    public float SFX_Volume { get; private set; }

    private AudioSource cameraAS;

    private const float StandardCD = 0.5f;

    [SerializeField]
    private AudioClip countDownAC;
    [SerializeField]
    private AudioClip countDownDoneAC;

    [SerializeField]
    private AudioClip turretPlaced;

    [SerializeField]
    private AudioClip enemySpawned;

    private float enemySpawnedCD;

    [SerializeField]
    private AudioClip enemyDead;

    [SerializeField]
    private AudioClip coreDamaged;

    [SerializeField]
    private AudioClip turretSelected;

    // Use this for initialization
    void Awake () {
        Instance = this;

        this.SFX_Volume = 0.6f;        
	}

    void Start()
    {
        this.cameraAS = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        this.enemySpawnedCD -= deltaTime;
    }

    public void PlayCountDownSFX()
    {
        this.cameraAS.PlayOneShot(countDownAC, this.SFX_Volume);
    }

    public void PlayCountDownDone()
    {
        this.cameraAS.PlayOneShot(countDownDoneAC, this.SFX_Volume);
    }

    public void OnCoreDamaged()
    {
        this.cameraAS.PlayOneShot(coreDamaged, this.SFX_Volume);
    }

    public void OnEnemyDead()
    {
        this.cameraAS.PlayOneShot(enemyDead, this.SFX_Volume);
    }

    public void OnEnemySpawned()
    {
        if (this.enemySpawnedCD <= 0f)
        {
            this.cameraAS.PlayOneShot(enemySpawned, this.SFX_Volume);
        }

        this.enemySpawnedCD = StandardCD;
    }

    public void OnTurretPlaced()
    {
        this.cameraAS.PlayOneShot(turretPlaced, this.SFX_Volume);
    }

    public void OnNewTurret()
    {
        this.cameraAS.PlayOneShot(turretSelected, this.SFX_Volume);
    }

    public void PlaySFX(AudioClip ac)
    {
        this.cameraAS.PlayOneShot(ac, this.SFX_Volume);
    }

    public void OnGameOver()
    {
        StartCoroutine(this.FadeOutSound());
    }

    private IEnumerator FadeOutSound()
    {
        float t = 0;

        float startSound = this.SFX_Volume;

        while (t < 1f)
        {
            this.SFX_Volume = Mathf.Lerp(startSound, 0, t);

            t += Time.deltaTime * 1f;

            yield return null;
        }
    }

}
