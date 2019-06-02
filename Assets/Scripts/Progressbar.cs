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
    

   
    // Use this for initialization
    void Start ()
    {
        ProgressBar = this.GetComponent<Slider>();
     
    }
	
	// Update is called once per frame
	void Update ()
	{
	   
	    {
	        DOTween.To(() => num, x => num = x, 101, 5);

	        ProgressBar.value = (int) num;
	        T.text = ProgressBar.value.ToString(CultureInfo.InvariantCulture) + "%";
	       
	        if (num >100.3)
	        {
	            Destroy(this.gameObject);
	        }
	    }
	}

    
   
    
    
}
