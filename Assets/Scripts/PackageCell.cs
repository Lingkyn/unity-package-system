using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PackageCell : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform UIIcon;
    private Transform UISelect;
    private Transform UIItemName;
    private Transform UIDeleteSelect;

    private PackageLocalItem packageLocalData;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUIName();
    }
    private void InitUIName()
    {
        UIIcon = transform.Find("Top/Icon");
        UIItemName = transform.Find("Bottom/itemName");
        UISelect = transform.Find("Select");
        UIDeleteSelect = transform.Find("DeleteSelect");

        UIDeleteSelect.gameObject.SetActive(false);
    }

    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    {
        // 数据初始化
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;
        // 显示物品名称（只支持 TextMeshProUGUI，项目使用 TMP 时更简洁可靠）
        if (UIItemName != null && this.packageTableItem != null)
        {
            TextMeshProUGUI tmp = UIItemName.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = this.packageTableItem.name;
            }
            else
            {
                Debug.LogWarning("PackageCell: 找不到 TextMeshProUGUI 组件，请检查 'Bottom/itemName' 节点。", this);
            }
        }
        // 显示物品图片
        if (this.packageTableItem != null && !string.IsNullOrEmpty(this.packageTableItem.imagePath))
        {
            Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
            if (t != null)
            {
                Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
                Image img = UIIcon != null ? UIIcon.GetComponent<Image>() : null;
                if (img != null)
                    img.sprite = temp;
            }
        }
        else
        {
            // 如果查不到表中数据，打印诊断信息
            if (this.packageTableItem == null)
            {
                Debug.LogWarning($"PackageCell.Refresh: 找不到 PackageTableItem for local id={packageLocalData?.id}. Check PackageTable DataList and PackageLocalData.", this);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PackageCell: 点击了物品格子 " + eventData.ToString(), this);
        if (uiParent != null)
        {
            uiParent.ShowDetail(this.packageLocalData);
        }
        else
        {
            Debug.LogWarning("PackageCell: uiParent is null, cannot show detail", this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("PackageCell: 鼠标进入物品格子 " + eventData.ToString(), this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("PackageCell: 鼠标离开物品格子 " + eventData.ToString(), this);
    }
}
