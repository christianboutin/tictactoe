using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An AI Necessarily plays for the O side
public abstract class AI 
{
	abstract public void Play(GridLogic gridLogic, out int x, out int y);
}
