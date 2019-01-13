using System;

public class Singleton<T> where T:class, new() 
{
	private static T instance = default(T);
	public static T GetInstance()
	{
		if (instance == null)
		{
			instance = new T ();
		}
		return instance;
	}
}
