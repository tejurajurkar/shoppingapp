package md58841fd88da0d6782f15a79bc072d986f;


public class AddEditAddressBookActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("shoppingApp.Resources.AddEditAddressBookActivity, shoppingApp", AddEditAddressBookActivity.class, __md_methods);
	}


	public AddEditAddressBookActivity ()
	{
		super ();
		if (getClass () == AddEditAddressBookActivity.class)
			mono.android.TypeManager.Activate ("shoppingApp.Resources.AddEditAddressBookActivity, shoppingApp", "", this, new java.lang.Object[] {  });
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
