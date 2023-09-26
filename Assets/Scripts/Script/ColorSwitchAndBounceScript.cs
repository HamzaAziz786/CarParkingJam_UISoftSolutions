using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class ModelMeshData
{
    [Header("Mesh Renderer")]
    public Renderer meshrenderer;
    [Header("Model Materials")]
    public Material[] modelMaterial;
    [Header("Other Temp Materials")]
    public Material[] OtherMaterial;
}

public class ColorSwitchAndBounceScript : MonoBehaviour
{
    #region Variables
    [Header("Scale Up Down Data")]
    public Vector3 ObjectNewScale = new Vector3(1.25f, 1.5f, 1.25f);
    public Vector3 ObjectDefaultScale = new Vector3(1, 1, 1);
    public float ScaleUpDownDuration = 0.15f;
    [Header("ModelObject")]
    public GameObject ObjectMesh;
    [Header("Particle Object")]
    public GameObject ParticleObject;
    [Header("Material Switch Duration")]
    public float SwitchDelay = 0.25f;
    [Header("Model Mesh Data")]
    public List<ModelMeshData> modelmeshdata = new List<ModelMeshData>();
    [Header("Effect Color")]
    public Color color1 = Color.white;
    [Header("Color Change Duration")]
    public float duration = 0.35f;
    [Header("Upgrade Particle Area")]
    public int TotalParticleSpawn = 5;
    public List<GameObject> UpgradeParticleList = new List<GameObject>();
    private int _particleCount;
    private bool _startWorking;


    #endregion

    #region Awake Function
    private void Awake()
    {
        for (var i = 0; i < TotalParticleSpawn; i++)
        {
            var obj = Instantiate(ParticleObject, ParticleObject.transform.position, ParticleObject.transform.rotation, transform);
            obj.SetActive(false);
            UpgradeParticleList.Add(obj);
        }
        StartCoroutine(TimeDelay());
    }

    private IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1f);
        _startWorking = true;
    }
    #endregion

    #region Color_Change_Function
    public void ColorChangeFunction()
    {
        if(!_startWorking)
        {
            return;
        }
        //ParticleObject.SetActive(true);
        foreach (var t in modelmeshdata)
        {
            t.meshrenderer.sharedMaterials = t.OtherMaterial;
        }
        StartCoroutine(ScaleTimeDelay());
        StartCoroutine(MaterialSwitchTimeDelay());
        StopCoroutine(nameof(ColorChangeTimeDelay));
        StartCoroutine(nameof(ColorChangeTimeDelay));
    }
    #endregion

    #region Color_Change_Coroutine

    private IEnumerator ColorChangeTimeDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            foreach (var t1 in modelmeshdata)
            {
                for (var i = 0; i < t1.OtherMaterial.Length; i++)
                {
                    var t = Mathf.PingPong(Time.time, duration) / duration;
                    t1.OtherMaterial[i].color = Color.Lerp(t1.modelMaterial[i].color, color1, t);
                }
            }
        }
    }
    #endregion

    #region Material_Switch_Coroutine

    private IEnumerator MaterialSwitchTimeDelay()
    {
        yield return new WaitForSeconds(SwitchDelay);
        if(UpgradeParticleList.Count > 0)
        {
            UpgradeParticleList[_particleCount].SetActive(true);
            _particleCount++;
            if (_particleCount >= UpgradeParticleList.Count)
            {
                _particleCount = 0;
            }
        }
        foreach (var t in modelmeshdata)
        {
            t.meshrenderer.sharedMaterials = t.modelMaterial;
        }
        StopCoroutine(nameof(ColorChangeTimeDelay));
        yield return new WaitForSeconds(SwitchDelay);
    }
    #endregion

    #region Scale Coroutine

    private IEnumerator ScaleTimeDelay()
    {
        ObjectMesh.transform.DOScale(ObjectNewScale, ScaleUpDownDuration);
        yield return new WaitForSeconds(ScaleUpDownDuration);
        ObjectMesh.transform.DOScale(ObjectDefaultScale, ScaleUpDownDuration);
    }
    #endregion


    public void StorageAreaParticlePositionSet(float value)
    {
        Debug.Log("Value :::: " + value);
        UpgradeParticleList[_particleCount].transform.localPosition = new Vector3(UpgradeParticleList[_particleCount].transform.localPosition.x, UpgradeParticleList[_particleCount].transform.localPosition.y, -12 + (0.6944f * value));
    }
}
