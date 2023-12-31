using Sirenix.OdinInspector;
using UnityEngine;
using GameExt;

public class ProductInfoScriptable : ScriptableObject
{
	[PreviewField(150, Alignment = ObjectFieldAlignment.Center)]
	[SerializeField] protected Sprite menuSprite;

	[SerializeField] protected PoolType poolType;
	
	[SerializeField] protected string productName;

	[SerializeField] protected Vector2Int size;

	[SerializeField] protected int baseHealth;


	public string ProductName { get => productName; }
	public PoolType PoolType { get => poolType; }
	public Sprite MenuSprite { get => menuSprite; }
	public Vector2Int Size { get => size; }
	public int BaseHealth { get => baseHealth; }
}
