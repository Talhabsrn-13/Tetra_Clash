using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShapeInfo : MonoBehaviour 
{
	public PlayerType PlayerType { get; set; }

	public int ShapeID = 0;
	[HideInInspector] public ShapeBlock firstBlock;
	[HideInInspector] public Sprite blockImage = null;
	[HideInInspector] public int startOffsetX = 0;
	[HideInInspector] public int startOffsetY = 0;
	PlayerController player;
	
    public List<ShapeBlock> ShapeBlocks;
	void Start()
	{
		player = GetComponentInParent<PlayerController>();
		PlayerType = player.PlayerType;
		CreateBlockList ();

		firstBlock = ShapeBlocks [0];
		blockImage = firstBlock.block.GetComponent<Image>().sprite;
		startOffsetX = firstBlock.rowID;
		startOffsetY = firstBlock.columnID;
	}

	void CreateBlockList()
	{
		ShapeBlocks = new List<ShapeBlock> ();
		List<Transform> shapeAllBlocks = transform.GetComponentsInChildren<Transform> ().ToList ();

		if (shapeAllBlocks.Contains (transform)) {
			shapeAllBlocks.Remove (transform);
		}

		foreach (Transform block in shapeAllBlocks) {
			string[] blockNameSplit =  block.name.Split ('-');

			if (blockNameSplit.Length == 3) {
				int rowID = blockNameSplit [1].TryParseInt ();
				int columnID = blockNameSplit [2].TryParseInt ();
			
				ShapeBlock thisBlock = new ShapeBlock (block, rowID, columnID, player.PlayerType);
				if (!ShapeBlocks.Contains (thisBlock)) {
					ShapeBlocks.Add (thisBlock);
				}
			}
		}
	}
}

public class ShapeBlock
{
	public Transform block;
	public int rowID;
	public int columnID;
    public PlayerType PlayerType { get; set; }
    public ShapeBlock(Transform _block, int _rowID, int _columnID, PlayerType playerType)
	{
		this.block = _block;
		this.rowID = _rowID;
		this.columnID = _columnID;
		this.PlayerType = playerType;
	}
}


