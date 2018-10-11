using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandom : AI {
	public override void Play(GridLogic gridLogic, out int x, out int y)
	{
		while (true)
		{
			x = Random.Range(0, 3);
			y = Random.Range(0, 3);
			if (gridLogic.Get(x, y) == GridLogic.TILE_CONTENT.BLANK)
			{
				break;
			}
		}
	}
}
