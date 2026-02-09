# Unity 背包系统（Mosaic Shorelines 示例）

背包/收集系统示例：场景拾取 → 分类浏览 → 详情展示，数据持久化到 PlayerPrefs。示例内容为 Mosaic Shorelines 海岸线生态物品。

## 目录

- [Unity 背包系统（Mosaic Shorelines 示例）](#unity-背包系统mosaic-shorelines-示例)
  - [目录](#目录)
  - [环境要求](#环境要求)
  - [快速开始](#快速开始)
  - [开发者复用](#开发者复用)
    - [1. 物品表（PackageTable）](#1-物品表packagetable)
    - [2. 场景拾取](#2-场景拾取)
    - [3. 打开背包](#3-打开背包)
    - [4. 代码 API](#4-代码-api)
  - [脚本说明](#脚本说明)
  - [素材与致谢](#素材与致谢)
  - [许可](#许可)

## 环境要求

- **Unity** 6000.x（或与项目一致）
- **TextMeshPro**、**Unity UI**
- 场景中需有 Canvas（或 `UICanvas` / `PackageCanvas`），否则 UIManager 会自动创建

## 快速开始

1. 克隆本仓库或下载 ZIP，用 Unity 打开工程根目录。
2. 打开场景 `Assets/Scenes/01Main.unity`，运行。
3. 点击场景中的可拾取物加入背包，再点击界面上的背包按钮打开背包查看。

## 开发者复用

### 1. 物品表（PackageTable）

- 路径：`Resources/Data/PackageTable.asset`，或在 Project 窗口右键 **Create > Custom > PackageTable** 新建。
- `PackageTableItem` 字段：`id`（唯一）、`categrory`、`type`、`name`、`description`、`imagePath`（相对 `Resources`，如 `Sprites/Vegetation/Spartina`）。
- 详情页类型标签在 `PackageDetail.GetTypeName` 中按 `type` 映射，可自行扩展。

### 2. 场景拾取

- 物体添加组件 **WorldPickup**（需带 Collider）。
- 设置 **Package Id**（对应表内 id）、**Num**、**Destroy On Pickup**。

### 3. 打开背包

- 在 Button 上添加 **OpenPanelButton**，Panel Type 选 `PackagePanel`。
- 背包预制体路径：`Resources/Prefabs/Panel/Package/PackagePanel.prefab`。若需修改路径，请编辑 `UIManager` 内 `InitDicts` 中的 pathDict。

### 4. 代码 API

| 操作         | 调用                                             |
| ------------ | ------------------------------------------------ |
| 添加物品     | `GameManager.Instance.AddItemToPackage(id, num)` |
| 获取背包列表 | `GameManager.Instance.GetPackageLocalData()`     |
| 按 id 查表   | `GameManager.Instance.GetPackageItemById(id)`    |
| 清空存档     | `GameManager.Instance.ClearGameData()`           |

背包条目为 `PackageLocalItem`（uid、id、num），同 id 累加 num；持久化键 `"PackageLocalData"`，由 `PackageLocalData` 单例读写。

## 脚本说明

| 脚本                                           | 职责                                |
| ---------------------------------------------- | ----------------------------------- |
| **GameManager**                                | 物品表加载、背包读写、AddItem、清档 |
| **PackageLocalData**                           | 单例，JSON 存读 PlayerPrefs         |
| **PackageTable**                               | ScriptableObject 物品表             |
| **PackagePanel / PackageCell / PackageDetail** | 背包 UI 与详情                      |
| **WorldPickup**                                | 场景拾取                            |
| **OpenPanelButton**                            | 打开指定面板（如背包）              |
| **UIManager**                                  | 面板加载与路径配置                  |

## 素材与致谢

本仓库示例中的图片素材（如 `Assets/Resources/Sprites`、`Assets/UI/Icon` 等）由 **Nano Banana AI** 生成。

## 许可

本项目采用 [MIT License](LICENSE)。
这意味着你可以自由使用、修改和分发这些翻译内容，但请务必保留原始的版权声明。