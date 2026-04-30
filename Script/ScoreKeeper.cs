using Godot;
using System;

public partial class ScoreKeeper : Node
{
	
	public int Score = 0;
	
	public int AddPoints(int points) {
		Score += points;
		return Score;
		
		
	}
}
