using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh : MonoBehaviour {
	public Transform target;
	private NavMeshPath path;
	private float elapsed = 0.0f; 
	void Start () {
		path = new NavMeshPath();
		elapsed = 0.0f;
	}
	void Update () {

		Debug.Log(NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path));
		}
}
