using UnityEngine;
using GameExt;

using Grid = GameExt.Grid;

public abstract class GridObjectBase : MonoBehaviour, IGridObject, IDamageAble, IPooledObject
{
	[SerializeField] protected SpriteRenderer spriteRend;
	[SerializeField] protected BoxCollider2D boxCollider;

	[SerializeField] protected ProductInfoScriptable productInfo;

	protected Grid[] currentGrids = new Grid[0];

	public Vector3 Pivot { get => transform.position + PivotOffset; }
	public Vector3 PivotOffset { get; set; }
	public Vector2Int ObjectSize { get; set; }
	public bool IsFit { get; set; }

	public HealthSystem HealthSystem { get; set; }
	public Transform Transform { get => transform; }


	protected virtual void Awake()
	{
		HealthSystem = new HealthSystem(productInfo.BaseHealth, this);
	}

	protected virtual void Start()
	{
		AdjustSize();
	}

	/// <summary>
	/// Objects that will use the grid system should adjust themselves to the current cell size.
	/// This helps prevent potential issues that may arise when the cell size changes in the future.
	/// </summary>
	/// 
	protected virtual void AdjustSize()
	{
		float currentCellSize = ActionManager.CellSize();
		Vector2 size = new Vector2(currentCellSize, currentCellSize);

		spriteRend.size = size;
		//boxCollider.size = size;

		ObjectSize = productInfo.Size;

		//It was necessary to align the Pivot of the object to the bottom-left corner in order to facilitate grid calculations.
		PivotOffset = new Vector3(ObjectSize.x * currentCellSize, ObjectSize.y * currentCellSize, 0) * -0.5f;

		transform.localScale = new Vector3(ObjectSize.x, ObjectSize.y, 1);
	}

	public void SetFitState(bool isFit)
	{
		this.IsFit = isFit;

		Color col = isFit ? Color.green : Color.red;
		spriteRend.color = col;
	}

	public void DragComplete()
	{
		if (IsFit)
			Insert();
		else
			ActionManager.ReturnItemToPool(gameObject, productInfo.PoolType);
	}

	protected virtual void Insert()
	{
		spriteRend.color = Color.white;
	}

	public void Snap(Vector3 targetPos)
	{
		transform.position = targetPos - PivotOffset;
	}

	public void SetCurrentGrids(Grid[] relevantGrids)
	{
		currentGrids = relevantGrids;
	}

	protected abstract void OnMouseDown();

	public virtual void OnDeath()
	{
		ActionManager.OnObjectDeath?.Invoke(this);
		ActionManager.ReturnItemToPool(gameObject, productInfo.PoolType);
		
		ReleaseGrids();
	}

	private void ReleaseGrids()
	{
		for (int i = 0; i < currentGrids.Length; i++)
		{
			currentGrids[i].currentObj = null;
		}
	}

	public void OnObjectGetFromPool()
	{
		HealthSystem.ReSpawn();
	}

	public void OnObjectInstantiate()
	{
	}


	public void OnObjectReturnToPool()
	{

	}
}
