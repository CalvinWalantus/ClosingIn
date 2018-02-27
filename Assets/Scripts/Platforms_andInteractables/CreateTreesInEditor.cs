#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateTreesInEditor : MonoBehaviour {

	public bool create = false, destroy = false;

	public int number = 5;

	public GameObject tree;
	List<GameObject> trees;

	public GameObject sizePlane;
	float x, z;


	// Update is called once per frame
	void Update () {
		if (create) {

			x = sizePlane.transform.localScale.x/2;
			z = sizePlane.transform.localScale.z/2;

			for (int i = 0; i < number; i++) {
				Vector3 position = new Vector3(gameObject.transform.position.x + Random.Range(-x, x), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-z, z));
				GameObject newTree = Object.Instantiate(tree, position, Quaternion.identity);
				newTree.transform.parent = gameObject.transform;
				trees.Add(newTree);
			}

			create = false;
		}

		if (destroy) {
			foreach (GameObject treeObject in trees) {
				DestroyImmediate(treeObject);
			}
			destroy = false;
		}
	}
}
#endif