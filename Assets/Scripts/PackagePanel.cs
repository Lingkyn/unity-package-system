using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UIMenu;
    private Transform UIMenuVegetation;
    private Transform UIMenuSand;
    private Transform UIMenuRocks;
    private Transform UIMenuShellfish;
    private Transform UITabName;
    private Transform UICloseBtn;
    private Transform UICenter;
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UILeftBtn;
    private Transform UIRightBtn;
    private Transform UIDeletePanel;
    private Transform UIDeleteBackBtn;
    private Transform UIDeleteConfirmBtn;
    private Transform UIBottomMenus;
    private Transform UIDeleteBtn;
    private Transform UIDetailBtn;

    public GameObject PackageUIItemPrefab;


    override protected void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void Start()
    {
        RefreshUI();
    }

    private void InitUI()
    {
        InitUIName();
        InitClick();
    }

    private void RefreshUI()
    {
        RefreshScroll();
    }


    private void RefreshScroll()
    {
        // 清理滚动容器中原本的物品
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = scrollContent.childCount - 1; i >= 0; i--)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        PackageLocalItem firstItem = null;
        int count = 0;
        foreach (PackageLocalItem localData in GameManager.Instance.GetSortPackageLocalData())
        {
            Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
            PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
            // 关键：把本地数据和父面板传给格子，否则格子不会填充名称和图片
            packageCell.Refresh(localData, this);
            if (firstItem == null) firstItem = localData;
            count++;
        }

        // 自动显示第一个物品（若存在），保证右侧 DetailPanel 在打开时有内容
        if (firstItem != null)
        {
            ShowDetail(firstItem);
        }

    }

    private void InitUIName()
    {
        //Top Center
        UIMenu = transform.Find("TopCenter/Menu");
        UIMenuVegetation = transform.Find("TopCenter/Menus/Vegetation");
        UIMenuSand = transform.Find("TopCenter/Menus/Sand");
        UIMenuRocks = transform.Find("TopCenter/Menus/Rocks");
        UIMenuShellfish = transform.Find("TopCenter/Menus/Shellfish");

        //Left Top
        UITabName = transform.Find("LeftTop/TabName");


        //Right Top
        UILeftBtn = transform.Find("RightTop/NumText");
        UICloseBtn = transform.Find("RightTop/CloseBtn");

        //Center
        UICenter = transform.Find("Center");
        UIScrollView = transform.Find("Center/Scroll View");
        UIDetailPanel = transform.Find("Center/DetailPanel");

        //Left and Right Btn
        UIRightBtn = transform.Find("Right/Button");
        UILeftBtn = transform.Find("Left/Button");

        //Bottom
        UIDeletePanel = transform.Find("Bottom/DeletePanel");
        UIDeleteBackBtn = transform.Find("Bottom/DeletePanel/Back");
        UIDeleteConfirmBtn = transform.Find("Bottom/DeletePanel/ConfirmBtn");
        UIBottomMenus = transform.Find("Bottom/BottomMenus");
        UIDeleteBtn = transform.Find("Bottom/BottomMenus/DeleteBtn");
        UIDetailBtn = transform.Find("Bottom/BottomMenus/DetailBtn");

        UIDeletePanel.gameObject.SetActive(false);
        UIBottomMenus.gameObject.SetActive(true);
    }

    private void InitClick()
    {
        UIMenuVegetation.GetComponent<Button>().onClick.AddListener(OnClickVegetation);
        UIMenuSand.GetComponent<Button>().onClick.AddListener(OnClickSand);
        UIMenuRocks.GetComponent<Button>().onClick.AddListener(OnClickRocks);
        UIMenuShellfish.GetComponent<Button>().onClick.AddListener(OnClickShellfish);

        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UILeftBtn.GetComponent<Button>().onClick.AddListener(OnClickLeft);
        UIRightBtn.GetComponent<Button>().onClick.AddListener(OnClickRight);

        UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnDeleteBack);
        UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(OnDeleteConfirm);
        UIDeleteBtn.GetComponent<Button>().onClick.AddListener(OnDelete);
        UIDetailBtn.GetComponent<Button>().onClick.AddListener(OnDetail);

    }
    private void OnClickVegetation()
    {
        print(">>>>> OnClickVegetation");
    }

    private void OnClickSand()
    {
        print(">>>>> OnClickSand");
    }
    private void OnClickRocks()
    {
        print(">>>>> OnClickRocks");
    }
    private void OnClickShellfish()
    {
        print(">>>>> OnClickShellfish");
    }

    private void OnClickClose()
    {
        print(">>>>> OnClickClose");
        ClosePanel();
        // UIManager.Instance.ClosePanel(UIConst.PackagePanel);
    }

    private void OnClickLeft()
    {
        print(">>>>> OnClickLeft");
    }

    private void OnClickRight()
    {
        print(">>>>> OnClickRight");
    }

    private void OnDeleteBack()
    {
        print(">>>>> onDeleteBack");
    }

    private void OnDeleteConfirm()
    {
        print(">>>>> OnDeleteConfirm");
    }

    private void OnDelete()
    {
        print(">>>>> OnDelete");
    }

    private void OnDetail()
    {
        print(">>>>> OnDetail");
    }

    // Called by PackageCell when a cell is clicked to display detail info
    public void ShowDetail(PackageLocalItem item)
    {
        if (UIDetailPanel == null)
        {
            Debug.LogWarning("PackagePanel.ShowDetail: UIDetailPanel is null", this);
            return;
        }

        PackageDetail detail = UIDetailPanel.GetComponent<PackageDetail>();
        if (detail == null)
        {
            Debug.LogWarning("PackagePanel.ShowDetail: PackageDetail component not found on UIDetailPanel", UIDetailPanel);
            return;
        }

        detail.Refresh(item, this);
    }
}
