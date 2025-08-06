using Unity.VisualScripting;
using UnityEngine;

public class AttackMenuAnimation : MonoBehaviour
{
    Vector3 targetScale;

    private void Start()
    {
        targetScale = new Vector3(0.6f, 0.6f, 0.6f);
        transform.localScale = targetScale;
        Debug.Log("bwa ha ha");
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 2f);
    }

    public void SetTargetScale()
    {
        targetScale = new Vector3(1f, 1f, 1f);
    }

    public void ResetTargetScale()
    {
        targetScale = new Vector3(0.6f, 0.6f, 0.6f);
        transform.localScale = targetScale;
    }
}
