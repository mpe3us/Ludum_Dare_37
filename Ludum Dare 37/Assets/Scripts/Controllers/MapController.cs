using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [SerializeField]
    public Color TurretBaseOrgColor;

    public const float ChangeColorDistThreshold = 6f;
    public const float ChangeBackTreshold = 5f;

    public static MapController Instance;

	// Use this for initialization
	void Awake () {
        Instance = this;	
	}
	
    public void ChangeColorOfTurretBase(Collider hitCol, EnemyController turController, float hitDist, bool isDead)
    {
        Material curMaterial = hitCol.transform.GetComponent<Renderer>().material;
        Color curColor = curMaterial.color;
        Color enemyColor = turController.EnemyData.enemyColor;
        float curFactor = hitDist / ChangeColorDistThreshold;
        float oldR = TurretBaseOrgColor.r * curFactor;
        float oldG = TurretBaseOrgColor.g * curFactor;
        float oldB = TurretBaseOrgColor.b * curFactor;
        Color newColor = new Color(enemyColor.r + oldR, enemyColor.g + oldG, enemyColor.b + oldB, curColor.a);
        
        if (hitDist < ChangeBackTreshold && !isDead)
        {
            curMaterial.color = newColor;
            //StartCoroutine(this.ChangeTo(curMaterial, 0, newColor));
        }
        else
        {
            StartCoroutine(ChangeBack(curMaterial, 0));
        }

        //curMaterial.color = newColor;
        //StartCoroutine(ChangeBack(curMaterial, 0));

    }

    private IEnumerator ChangeTo(Material mat, float t, Color targetColor)
    {
        float deltaTime;

        while (t < 1f)
        {

            deltaTime = Time.deltaTime * GameController.GlobalSpeedFactor;

            t += deltaTime / 10f;

            targetColor.a = mat.color.a;
            mat.color = Color.Lerp(mat.color, targetColor, t);

            yield return null;

        }
    }

    private IEnumerator ChangeBack(Material mat, float t)
    {
        float deltaTime;
        
        while(t < 1f)
        {

            deltaTime = Time.deltaTime * GameController.GlobalSpeedFactor;

            t += deltaTime / 0.5f;

            Color targetColor = TurretBaseOrgColor;
            targetColor.a = mat.color.a;
            mat.color = Color.Lerp(mat.color, targetColor, t);

            yield return null;

        }
    }

}
