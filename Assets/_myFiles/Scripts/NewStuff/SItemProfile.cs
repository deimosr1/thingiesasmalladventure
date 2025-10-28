using UnityEngine;

[CreateAssetMenu(fileName = "NewItemProfile", menuName = "Thingies/ItemProfile")]
public class SItemProfile : ScriptableObject
{
    public EItemType ItemType;
    public EToolType ToolType;
    public Sprite ItemIcon;
    public Texture2D MouseIcon;
    public string ItemID = string.Empty;
    public string ItemName = string.Empty;


    public virtual void Use()
    {

    }
}

public enum EItemType
{
    Item,
    Tool
}

public enum EToolType
{
    None,
    Sword,
    Stick,
    Spyglass
}
