using UnityEngine;

[CreateAssetMenu(menuName = "Inventario/Munition", fileName = "Munition Item")]
public class ItemMunition : ScriptableObject
{
    public Sprite spr_;
    public string name_;

    public int qtdMax;

    [Space]
    public typeMunition type;
}
