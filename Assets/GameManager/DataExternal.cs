using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DataExternal {

	public static DataExternal Instance {
		get; private set;
	} = new DataExternal();



	private DataExternal() {

	}
}
