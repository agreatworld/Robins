using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMapProperty : MonoBehaviour {

	public enum SubMapDifferentiation {
		Lake, Mountain, Forest, Bushveld
	}

	public List<SubMapDifferentiation> differentiation = new List<SubMapDifferentiation>();
}
