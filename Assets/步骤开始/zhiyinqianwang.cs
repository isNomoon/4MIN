using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskGuide : MonoBehaviour
{
    [Header("目标设置")]
    public GameObject targetObject;      // 目标物体（有光圈的那个）
    public Transform playerTransform;    // 玩家的Transform

    [Header("UI设置")]
    public TextMeshProUGUI guideText;               // 屏幕上的指引文字
    public GameObject targetIndicator;   // 目标上的光圈/指示器

    [Header("触发设置")]
    public float reachDistance = 3f;     // 到达距离（3米内算到达）
    public string guideMessage = "请前往发光地点";
    public string completeMessage = "到达目的地！";

    private bool isTaskComplete = false;

    void Start()
    {
        // 显示指引文字
        if (guideText != null)
            guideText.text = guideMessage;

        // 确保目标和指示器可见
        if (targetIndicator != null)
            targetIndicator.SetActive(true);

        // 如果没有手动拖入玩家，自动查找
        if (playerTransform == null)
            playerTransform = Camera.main.transform;
    }

    void Update()
    {
        if (isTaskComplete) return;
        if (targetObject == null || playerTransform == null) return;

        // 计算玩家到目标的距离
        float distance = Vector3.Distance(playerTransform.position, targetObject.transform.position);

        // 判断是否到达
        if (distance <= reachDistance)
        {
            CompleteTask();
        }
    }

    void CompleteTask()
    {
        isTaskComplete = true;

        // 隐藏指引文字
        if (guideText != null)
        {
            guideText.text = completeMessage;
            // 延迟后完全隐藏
            Invoke("HideUI", 1.5f);
        }

        // 隐藏目标物体和光圈
        if (targetObject != null)
            targetObject.SetActive(false);

        if (targetIndicator != null)
            targetIndicator.SetActive(false);

        Debug.Log("✅ 任务完成！玩家到达目标地点");
    }

    void HideUI()
    {
        if (guideText != null)
            guideText.gameObject.SetActive(false);
    }

    // ===== 在Scene视图中画辅助线 =====
    void OnDrawGizmosSelected()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetObject.transform.position, reachDistance);
        }
    }
}