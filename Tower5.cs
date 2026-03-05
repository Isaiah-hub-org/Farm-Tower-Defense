using Godot;
using Godot.Collections;

public partial class Tower5 : Node2D
{
	[Export] public PackedScene ArrowScene;
	[Export] public float FireRate = 0.7f;
	private bool ReadyToFire = true;
	
	[Export] int damage = 3;
	[ExportCategory("Node Connections")]
	Timer timer;
	private Area2D detectionZone;
	
	public Array<Enemy> enemiesInRange;
	
 
	public override void _Ready()
	{
		enemiesInRange = new Array<Enemy>();
		detectionZone = GetNode<Area2D>("DetectionZone");
		timer = GetNode<Timer>("Timer");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (HasEnemiesInRange() && ReadyToFire)
		{
			Attack(enemiesInRange.PickRandom());
		}
	}

	public bool HasEnemiesInRange() {
		var enemies = detectionZone.GetOverlappingBodies();
		return enemies.Count > 0;
	}

	public void TimerExpired() {
		ReadyToFire = true;
	}

	public void Attack(Enemy enemy) {
		ReadyToFire = false;
		timer.Start();
	}


}
