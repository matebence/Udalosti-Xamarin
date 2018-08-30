package md521d56e4d5b4c28d87d8ab1f65fdc080f;


public class UdalostiAndroid
	extends md51558244f76c53b6aeda52c8a337f2c37.FormsAppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Udalosti.Droid.UdalostiAndroid, Udalosti.Android", UdalostiAndroid.class, __md_methods);
	}


	public UdalostiAndroid ()
	{
		super ();
		if (getClass () == UdalostiAndroid.class)
			mono.android.TypeManager.Activate ("Udalosti.Droid.UdalostiAndroid, Udalosti.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
