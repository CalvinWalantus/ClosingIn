using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class which all complex compressable 

[RequireComponent(typeof(ComplexCompressableController))]
public abstract class ComplexCompressable : MonoBehaviour {

	ComplexCompressableController controller;

	public virtual void Awake () {
		controller = GetComponent<ComplexCompressableController> ();
		controller.compressEvent += ComplexCompress;
		controller.decompressEvent += ComplexDecompress;
	}

	public abstract void ComplexCompress (int two_shot, Vector3 player_position);

	public abstract void ComplexDecompress (Vector3 original);

}
