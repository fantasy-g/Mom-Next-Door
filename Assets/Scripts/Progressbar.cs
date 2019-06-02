using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
   private Slider ProgressBar;
    public float num = 0;
   
    public Text T;
   
   public Button B;
    public bool IsFinish = false;
    public bool CanFill = false;

   
    // Use this for initialization
    void Start ()
    {
        ProgressBar = this.GetComponent<Slider>();
     
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (CanFill)
	    {
	        DOTween.To(() => num, x => num = x, 101, 5);

	        ProgressBar.value = (int) num;
	        T.text = ProgressBar.value.ToString(CultureInfo.InvariantCulture) + "%";
	       
	        if (ProgressBar.value == 100)
	        {
	            CanFill = false;
	            Device.instance.IsFinish = true;
                Destroy(this.gameObject);
	        }
	    }
	}

    public void C()
    {
        CanFill = true;
    }

   
    
    
}
