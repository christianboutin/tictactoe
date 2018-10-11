using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour {
	GridLogic gridLogic;
	AI aI;

	[SerializeField]
	GameObject victoryLine;

	[SerializeField]
	GridElement[] gridElements;

	void Start () {
		gridLogic = new GridLogic();
		foreach (GridElement gridElement in gridElements)
		{
			gridElement.OnClickAction = GridElementClick;
		}

		aI = new AIRandom();
		Reset();
	}

	public void Reset()
	{
		gridLogic.Reset();
		foreach (GridElement gridElement in gridElements)
		{
			gridElement.Clear();
		}
		victoryLine.SetActive(false);
	}

	void GridElementClick(GridElement gridElement)
	{
		gridLogic.Set(gridElement.X, gridElement.Y, GridLogic.TILE_CONTENT.X);
		gridElement.Set(GridLogic.TILE_CONTENT.X);
		PostMove(GridLogic.TILE_CONTENT.X);
	}

	void PostMove(GridLogic.TILE_CONTENT tc)
	{
		GridLogic.WIN_TYPE wt = gridLogic.IsWinner(tc);
		if (wt == GridLogic.WIN_TYPE.NONE)
		{
			if (!gridLogic.IsFull())
			{
				if (tc == GridLogic.TILE_CONTENT.X)
				{
					PlayAI();
				}
			}
		}
		else
		{
			foreach (GridElement gridElement in gridElements)
			{
				gridElement.Lock();
			}
			SetVictoryLine(wt);
		}
	}

	void SetVictoryLine(GridLogic.WIN_TYPE wt)
	{
		victoryLine.SetActive(true);
		if ((int)wt < 3)
		{
			victoryLine.GetComponent<RectTransform>().localPosition = new Vector2(0, 164 - (int)wt * 164);
			victoryLine.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.zero);
		}
		else if ((int)wt < 6)
		{
			victoryLine.GetComponent<RectTransform>().localPosition = new Vector2(-164 + ((int)wt-3) * 164,0);
			victoryLine.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.forward*90f);
		}
		else if (wt == GridLogic.WIN_TYPE.DIA1)
		{
			victoryLine.GetComponent<RectTransform>().localPosition = Vector3.zero;
			victoryLine.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.back * 45f);
		}
		else if (wt == GridLogic.WIN_TYPE.DIA2)
		{
			victoryLine.GetComponent<RectTransform>().localPosition = Vector3.zero;
			victoryLine.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.back * 135f);
		}
	}

	void PlayAI()
	{
		int x, y;
		aI.Play(gridLogic, out x, out y);
		gridLogic.Set(x, y, GridLogic.TILE_CONTENT.O);
		gridElements.Where(i => i.X == x && i.Y == y).FirstOrDefault().Set(GridLogic.TILE_CONTENT.O); // Protect against null
		PostMove(GridLogic.TILE_CONTENT.O);
	}

}
