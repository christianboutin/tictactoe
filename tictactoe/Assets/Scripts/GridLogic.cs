using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridLogic
{
	public enum TILE_CONTENT
	{
		BLANK,
		X,
		O
    }

	public enum WIN_TYPE
	{
		ROW1=0,
		ROW2=1,
		ROW3=2,
		COL1=3,
		COL2=4,
		COL3=5,
		DIA1=6,
		DIA2=7,
		NONE=8
	}

	TILE_CONTENT[,] grid = new TILE_CONTENT[3,3];

	int nbFilled;

	//// Use this to test some conditions.  Alternatively integrate in automated tests
	//public void Test () {
	//	Reset();
	//	Set(0, 0, TILE_CONTENT.X);
	//	Set(1, 1, TILE_CONTENT.X);
	//	Set(2, 2, TILE_CONTENT.X);
	//	Set(2, 0, TILE_CONTENT.O);
	//	Set(2, 1, TILE_CONTENT.O);
	//	Debug.Log(IsWinner(TILE_CONTENT.X));
	//	Reset();
	//	Set(0, 0, TILE_CONTENT.X);
	//	Set(1, 0, TILE_CONTENT.X);
	//	Set(2, 0, TILE_CONTENT.X);
	//	Set(2, 2, TILE_CONTENT.O);
	//	Set(2, 1, TILE_CONTENT.O);
	//	Debug.Log(IsWinner(TILE_CONTENT.X));
	//	Reset();
	//	Set(1, 1, TILE_CONTENT.O);
	//	Set(1, 2, TILE_CONTENT.O);
	//	Set(1, 0, TILE_CONTENT.O);
	//	Set(2, 2, TILE_CONTENT.X);
	//	Set(2, 1, TILE_CONTENT.X);
	//	Debug.Log(IsWinner(TILE_CONTENT.O));

	//}


	public void Set(int x, int y, TILE_CONTENT tc)
	{
		if (x < 0 || x > 2 || y < 0 || y > 2)
		{
			return;
		}

		if (grid[x,y] == TILE_CONTENT.BLANK && tc != TILE_CONTENT.BLANK)
		{
			nbFilled++;
		}
		grid[x, y] = tc;
	}

	public TILE_CONTENT Get(int x, int y)
	{
		return grid[x, y];
	}

	public void Reset()
	{
		for (int x = 0; x < 3; x++)
		{
			for(int y = 0; y < 3; y++)
			{
				grid.SetValue(TILE_CONTENT.BLANK, x, y);
			}
		}
		nbFilled = 0;
	}

	public WIN_TYPE IsWinner(TILE_CONTENT tc)
	{
		if (nbFilled < 5) return WIN_TYPE.NONE; // Unless there are 5 filled slots, victory is impossible.
		
		// Assume every winning type is true
		bool[] solutions = new bool[] { true, true, true, true, true, true, true, true };
		
		// Go through evey grid position.  Invalidate all winning conditions associated with the grid position if it doesn't contain the wanted tile content
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if (grid[x,y] != tc)
				{
					solutions[y] = false;
					solutions[x + 3] = false;
					if (x == y)
					{
						solutions[6] = false;
					}
					if (x == 2-y)
					{
						solutions[7] = false;
					}
				}
			}
		}

		// Return the first true win condition
		WIN_TYPE wt;
		for (wt = 0; wt < WIN_TYPE.NONE; wt++)
		{
			if (solutions[(int)wt] == true)
			{
				return wt;
			}

		}
		// Alternatively return WIN_TYPE.NONE
		return wt;
	}

	public bool IsFull()
	{
		return nbFilled == 9;
	}
}
