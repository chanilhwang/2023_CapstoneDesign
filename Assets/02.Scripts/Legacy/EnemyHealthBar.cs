using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] public Image healthBarImage;
    [SerializeField] public Transform enemyTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        if(canvas == null)
        {
            canvas = FindFirstObjectByType<Canvas>();
        }
        if(healthBarImage == null)
        {
            healthBarImage= GetComponent<Image>();
        }
    }

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemyTransform.position + offset);

        if (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            healthBarImage.enabled = true;

            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, null, out anchoredPos);
            healthBarImage.transform.position = healthBarImage.canvas.transform.TransformPoint(anchoredPos);

            Vector3 enemyScale = transform.lossyScale;
            float enemyWidth = enemyScale.x;
            float enemyHeight = enemyScale.y;
            float hpBarwidth = healthBarImage.rectTransform.rect.width;
            float hpBarheight = healthBarImage.rectTransform.rect.height;
            healthBarImage.rectTransform.sizeDelta = new Vector2(hpBarwidth, hpBarheight);
        }
        else
        {
            healthBarImage.enabled = false;
        }
    }

    public void SetHealth(int health, int maxHealth)
    {
        healthBarImage.fillAmount = (float)health / maxHealth;
    }
}
