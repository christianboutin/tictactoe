using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour {
	[SerializeField]
	int x, y;

	public int X { get { return x; } }
	public int Y { get { return y; } }

	[SerializeField]
	Sprite[] contentSprites;

	[SerializeField]
	Image image;

	System.Action<GridElement> onClickAction;

	public System.Action<GridElement> OnClickAction { set { onClickAction = value; } }

	public void Clear()
	{
		image.enabled = false;
		GetComponent<Button>().interactable = true;
	}

	public void Lock()
	{
		GetComponent<Button>().interactable = false;
	}

	public void Set(GridLogic.TILE_CONTENT tc)
	{
		GetComponent<Button>().interactable = false;
		image.enabled = true;
		image.sprite = contentSprites[(int)tc];
	}

	public void OnClick()
	{
		onClickAction(this);
	}
}
