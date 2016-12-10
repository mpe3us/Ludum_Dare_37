using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {

    public static MouseController Instance;

    private Ray mouseRay;
    private RaycastHit mouseRayHitInfo;

    public GameObject MouseOverObject { get; private set; }

    // Enum which defines the different kinds of target modes which we can be in
    public enum TargetMode
    {
        NONE,
        BUY,
        SELL
    }

    public TargetMode CurrentTargetMode { get; private set; }
    
    // Use this for initialization
    void Awake () {
        Instance = this;

        this.CurrentTargetMode = TargetMode.BUY;
	}
	
	// Update is called once per frame
	void Update () {

        this.mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // If we mouse over a UI object...
        }
        else if (Physics.Raycast(this.mouseRay, out this.mouseRayHitInfo))
        {
            this.MouseOverObject = this.mouseRayHitInfo.transform.parent.gameObject;
            this.HandleMouseEnterObject();
        }
        else
        {
            this.HandleMouseExitObject();
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.HandleLeftClickDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.HandleLeftClickUp();
        }

    }

    private void HandleMouseEnterObject()
    {
        //Debug.Log(this.MouseOverObject.name);
    }

    private void HandleMouseExitObject()
    {

    }

    private void HandleLeftClickDown()
    {

    }


    private void HandleLeftClickUp()
    {
        switch (this.CurrentTargetMode)
        {
            case TargetMode.NONE:
                break;
            case TargetMode.BUY:
                GameController.Instance.BuyTurretAt(this.MouseOverObject);
                break;
            case TargetMode.SELL:
                break;
            default:
                Debug.Log("Unsupported mode: " + this.CurrentTargetMode);
                break;
        }
    }

}
