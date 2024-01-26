using Godot;
using System;

public partial class Main : Node2D
{
	CollisionPolygon2D CollisionPolygon;
	Polygon2D Level;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionPolygon = GetNode<CollisionPolygon2D>("StaticBody2D/CollisionPolygon2D");
		Level = GetNode<Polygon2D>("StaticBody2D/CollisionPolygon2D/Polygon2D");
		Level.Polygon = CollisionPolygon.Polygon;
		RenderingServer.SetDefaultClearColor(new Color(0,0,0,1));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
