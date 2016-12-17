using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurretButton : MonoBehaviour {

    [SerializeField]
    private Turret.TurretTypes turretType;

    [SerializeField]
    private Text turretNameText;

    [SerializeField]
    private Text turretPriceText;

    [SerializeField]
    private Image backGroundImage;

    [SerializeField]
    private Color affordableColor;

    [SerializeField]
    private Color notAffordableColor;

    public Turret TurretData { get; private set; }

    void Awake()
    {
        switch (turretType)
        {
            case Turret.TurretTypes.BASIC:
                this.TurretData = new BasicTurret();
                break;
            case Turret.TurretTypes.ROCKET:
                this.TurretData = new RocketTurret();
                break;
            case Turret.TurretTypes.SNIPER:
                this.TurretData = new SniperTurret();
                break;
            default:
                Debug.Log("Unsupported turret type");
                break;
        }

        this.turretNameText.text = this.TurretData.Name;
        this.turretPriceText.text = this.TurretData.Price.ToString() + " $";

    }
    
    void FixedUpdate()
    {
        if (this.TurretData.Price <= GameController.Instance.GameInstance.Credits)
        {
            this.backGroundImage.color = this.affordableColor;
        }   
        else
        {
            this.backGroundImage.color = this.notAffordableColor;
        }
    }

}
