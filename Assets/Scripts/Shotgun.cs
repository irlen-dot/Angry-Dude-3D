using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [Header("The reload animation settings")]
    [SerializeField]
    [Tooltip("The num of frames for the whole animation will be as twice as this field")]
    private int numOfFramesForHalfReload = 2;

    [SerializeField]
    [Tooltip("The delay in seconds between the frames")]
    private float interFrameDelay = 0.7f;

    private List<float> reloadInitedBlendValues = new List<float>();
    private SkinnedMeshRenderer meshRenderer;

    private int numberOfRequestsToReload = 0;

    private void InitBlendValues()
    {
        List<float> blendValues = InterpolationBetween.GetEvenlyDistributedNumbersAsArray(0, 100, numOfFramesForHalfReload);
        List<float> revertBlendValues = new List<float>(blendValues);
        revertBlendValues.Reverse();
        reloadInitedBlendValues.AddRange(blendValues.AsEnumerable());
        reloadInitedBlendValues.AddRange(revertBlendValues.AsEnumerable());
    }



    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }


    public void Reload()
    {
        numberOfRequestsToReload++;
        Debug.Log($"Number of requests to reload: {numberOfRequestsToReload}.");
        StartCoroutine(AnimateReload());
    }

    private IEnumerator AnimateReload()
    {
        foreach (float blendVal in reloadInitedBlendValues)
        {
            Debug.Log($"Reload shape is set to: {blendVal}");
            meshRenderer.SetBlendShapeWeight(0, blendVal);
            yield return new WaitForSeconds(interFrameDelay);
        }
    }

    void Awake()
    {
        InitBlendValues();
    }
}
