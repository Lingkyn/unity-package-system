using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GMCmd
{
    [MenuItem("GMCmd/ReadTable")]
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("Data/PackageTable");
        foreach (PackageTableItem packageItem in packageTable.DataList)
        {
            Debug.Log(string.Format("[id]:{0},[type]:{1},[name]:{2},[imagePath]:{3}", packageItem.id, packageItem.type, packageItem.name,packageItem.imagePath));
        }
    }

    [MenuItem("GMCmd/CreateTestData")]
    public static void CreateLocalPackageData()
    {
        //保存数据
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 0; i < 10; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,

            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavdPackage();

    }
    [MenuItem("GMCmd/ReadTestData")]
    public static void ReadLocalPackageData()
    {
        //读取数据
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }

[MenuItem("GMCmd/OpenPackagePanel")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

}

