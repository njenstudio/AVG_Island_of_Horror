using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
public class UISpineControl : MonoBehaviour
{
    public enum ClothType
    {
        Body,
        Underwear,
        Cloth,
        Face,
    }

    public enum SlotCode
    {
        //MainBody
        [Description("素體")]
        M_P0,
        [Description("歐派")]
        M_P1,
        [Description("紅暈")]
        Blush,
        [Description("手")]
        Hand,
        [Description("影子")]
        Shadow,
        //Underwear
        [Description("歐派(無衣服)")]
        M_U1,
        [Description("白色樸素")]
        M_U2,
        [Description("繃帶短")]
        M_U3,
        [Description("繃帶長")]
        M_U4,
        [Description("女僕款內衣")]
        M_U5,
        [Description("女僕款內衣差分")]
        M_U6,
        [Description("OK绷")]
        M_U7,
        [Description("魔法少女內衣(長)")]
        M_U8,
        [Description("魔法少女內衣(短)")]
        M_U9,
        //Cloth
        [Description("怪獸裝")]
        M_C2,
        [Description("巫女裝")]
        M_C3,
        [Description("女僕裝")]
        M_C4,
        [Description("死庫水")]
        M_C5,
        [Description("魔法少女服裝")]
        M_C6,
        [Description("綁繩比基尼")]
        M_C7,
        //Face
        [Description("微笑眼")]
        M_F2,
        [Description("閉眼")]
        M_F3,
        [Description("愛心眼")]
        M_F4,
        [Description("忍不住的表情(><)")]
        M_F5,
        [Description("心不在焉(看旁邊)")]
        M_F6,
        //Other
        [Description("光線")]
        Light,
        [Description("背景")]
        BG,
    }

    [SpineSkin]
    public string templateSkinName;
    [SpineSlot]
    public string testSlot;

    public List<ClothData> equippables = new List<ClothData>();

    public Material sourceMaterial;
    public bool applyPMA = true;
    public Dictionary<ClothAsset, Attachment> cachedAttachments = new Dictionary<ClothAsset, Attachment>();
    Spine.Skin equipsSkin;
    Spine.Skin collectedSkin;
    SkeletonGraphic skeletonGraphic;
    void Start()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        equipsSkin = new Skin("Equips");

        // OPTIONAL: Add all the attachments from the template skin.
        var templateSkin = skeletonGraphic.Skeleton.Data.FindSkin(templateSkinName);
        if (templateSkin != null)
            equipsSkin.AddAttachments(templateSkin);

        skeletonGraphic.Skeleton.Skin = equipsSkin;
        RefreshSkeletonAttachments();

    }

    public void ChangeCloth(ClothAsset asset)
    {
        var equipType = asset.equipType;
        ClothData howToEquip = equippables.Find(x => x.type == equipType);

        var skeletonData = skeletonGraphic.skeletonDataAsset.GetSkeletonData(true);
        int slotIndex = skeletonData.FindSlotIndex(testSlot);
        var attachment = GenerateAttachmentFromEquipAsset(asset, slotIndex, howToEquip.templateSkin, howToEquip.templateAttachment);
        Equip(slotIndex, howToEquip.templateAttachment, attachment);
    }
    Attachment GenerateAttachmentFromEquipAsset(ClothAsset asset, int slotIndex, string templateSkinName, string templateAttachmentName)
    {
        Attachment attachment;
        cachedAttachments.TryGetValue(asset, out attachment);

        if (attachment == null)
        {
            var skeletonData = skeletonGraphic.skeletonDataAsset.GetSkeletonData(true);
            var templateSkin = skeletonData.FindSkin(templateSkinName);
            Attachment templateAttachment = templateSkin.GetAttachment(slotIndex, templateAttachmentName);
            attachment = templateAttachment.GetRemappedClone(asset.sprite, sourceMaterial, premultiplyAlpha: this.applyPMA);

            cachedAttachments.Add(asset, attachment); // Cache this value for next time this asset is used.
        }

        return attachment;
    }


    public void Equip(int slotIndex, string attachmentName, Attachment attachment)
    {
        equipsSkin.SetAttachment(slotIndex, attachmentName, attachment);
        skeletonGraphic.Skeleton.SetSkin(equipsSkin);
        RefreshSkeletonAttachments();
    }

    void RefreshSkeletonAttachments()
    {
        skeletonGraphic.Skeleton.SetSlotsToSetupPose();
        skeletonGraphic.AnimationState.Apply(skeletonGraphic.Skeleton); //skeletonAnimation.Update(0);
    }

    [System.Serializable]
    public class ClothData
    {
        public ClothType type;
        [SpineSlot]
        public string slot;
        [SpineSkin]
        public string templateSkin;
        [SpineAttachment(skinField: "templateSkin")]
        public string templateAttachment;
    }
#if UNITY_EDITOR
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
    [MenuItem("Assets/ClothAsset/CreateClothAsset")]
    public static void CreateAsset()
    {
       CreateAsset<ClothAsset>();
    }
#endif
}
