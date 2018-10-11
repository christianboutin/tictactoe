using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMinMax : AI
{
	struct Move
	{
		public int row, col;
	};

	char player = 'x', opponent = 'o';

	bool IsMovesLeft(char[,] board)
	{
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				if (board[i,j] == '_')
					return true;
		return false;
	}

	// This is the evaluation function as discussed 
	// in the previous article ( http://goo.gl/sJgv68 ) 
	int Evaluate(char[,] b)
	{
		// Checking for Rows for X or O victory. 
		for (int row = 0; row < 3; row++)
		{
			if (b[row,0] == b[row,1] &&
				b[row,1] == b[row,2])
			{
				if (b[row,0] == player)
					return +10;
				else if (b[row,0] == opponent)
					return -10;
			}
		}

		// Checking for Columns for X or O victory. 
		for (int col = 0; col < 3; col++)
		{
			if (b[0,col] == b[1,col] &&
				b[1,col] == b[2,col])
			{
				if (b[0,col] == player)
					return +10;

				else if (b[0,col] == opponent)
					return -10;
			}
		}

		// Checking for Diagonals for X or O victory. 
		if (b[0,0] == b[1,1] && b[1,1] == b[2,2])
		{
			if (b[0,0] == player)
				return +10;
			else if (b[0,0] == opponent)
				return -10;
		}

		if (b[0,2] == b[1,1] && b[1,1] == b[2,0])
		{
			if (b[0,2] == player)
				return +10;
			else if (b[0,2] == opponent)
				return -10;
		}

		// Else if none of them have won then return 0 
		return 0;
	}

	// This is the minimax function. It considers all 
	// the possible ways the game can go and returns 
	// the value of the board 
	int Minimax(char[,] board, int depth, bool isMax)
	{
		int score = Evaluate(board);

		// If Maximizer has won the game return his/her 
		// evaluated score 
		if (score == 10)
			return score;

		// If Minimizer has won the game return his/her 
		// evaluated score 
		if (score == -10)
			return score;

		// If there are no more moves and no winner then 
		// it is a tie 
		if (IsMovesLeft(board) == false)
			return 0;

		// If this maximizer's move 
		if (isMax)
		{
			int best = -1000;

			// Traverse all cells 
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					// Check if cell is empty 
					if (board[i,j] == '_')
					{
						// Make the move 
						board[i,j] = player;

						// Call minimax recursively and choose 
						// the maximum value 
						best = Mathf.Max(best,
							Minimax(board, depth + 1, !isMax));

						// Undo the move 
						board[i,j] = '_';
					}
				}
			}
			return best;
		}

		// If this minimizer's move 
		else
		{
			int best = 1000;

			// Traverse all cells 
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					// Check if cell is empty 
					if (board[i,j] == '_')
					{
						// Make the move 
						board[i,j] = opponent;

						// Call minimax recursively and choose 
						// the minimum value 
						best = Mathf.Min(best,
							   Minimax(board, depth + 1, !isMax));

						// Undo the move 
						board[i,j] = '_';
					}
				}
			}
			return best;
		}
	}

	// This will return the best possible move for the player 
	Move FindBestMove(char[,] board)
	{
		int bestVal = -1000;
		Move bestMove = new Move();
		bestMove.row = -1;
		bestMove.col = -1;

		// Traverse all cells, evalutae minimax function for 
		// all empty cells. And return the cell with optimal 
		// value. 
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				// Check if cell is empty 
				if (board[i,j] == '_')
				{
					// Make the move 
					board[i,j] = player;

					// compute evaluation function for this 
					// move. 
					int moveVal = Minimax(board, 0, false);

					// Undo the move 
					board[i,j] = '_';

					// If the value of the current move is 
					// more than the best value, then update 
					// best/ 
					if (moveVal > bestVal)
					{
						bestMove.row = i;
						bestMove.col = j;
						bestVal = moveVal;
					}
				}
			}
		}
		return bestMove;
	}
	public override void Play(GridLogic gridLogic, out int x, out int y)
	{
		char[,] board = new char[3, 3];
		for(int bx = 0; bx < 3; bx++)
		{
			for (int by = 0; by < 3; by++)
			{
				board[by, bx] = gridLogic.Get(bx, by) == GridLogic.TILE_CONTENT.BLANK ? '_' : gridLogic.Get(bx, by) == GridLogic.TILE_CONTENT.O ? player : opponent;
			}
		}
		Move bestMove = FindBestMove(board);
		x = bestMove.col;
		y = bestMove.row;
	}
}
