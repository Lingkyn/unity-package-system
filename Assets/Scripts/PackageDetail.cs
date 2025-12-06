using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackageDetail : MonoBehaviour
{
    private Transform UITitle;
    private Transform UIIcon;
    private Transform UIDescription;
    private Transform UIItemType;

    private PackageLocalItem packageLocalData;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUIName();
    }

    private void Test()
    {
        Refresh(GameManager.Instance.GetPackageLocalData()[0], null);
    }

    private void InitUIName()
    {
        UITitle = transform.Find("Top/Title");
        UIIcon = transform.Find("Center/Icon");
        UIDescription = transform.Find("Bottom/Description");
        UIItemType = transform.Find("Bottom/Category/Tag");
    }

    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiParent)
    {
        // 数据初始化
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiParent = uiParent;

        // 显示物品名称
        if (UITitle != null && this.packageTableItem != null)
        {
            TextMeshProUGUI tmp = UITitle.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = this.packageTableItem.name;
                Debug.Log("Title set to: " + tmp.text);
            }
        }
        else
        {
            // one of UITitle or packageTableItem is null; nothing to set
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

        // 显示物品描述
        if (UIDescription != null && this.packageTableItem != null)
        {
            TextMeshProUGUI tmp = UIDescription.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = this.packageTableItem.description;
            }
        }

        // 显示物品类型
        if (UIItemType != null && this.packageTableItem != null)
        {
            TextMeshProUGUI tmp = UIItemType.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = this.packageTableItem.type.ToString();
            }
        }
    }
}