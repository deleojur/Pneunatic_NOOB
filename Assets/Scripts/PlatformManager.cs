using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {
	
	public Sequence sequence;
    public Material[] materials = new Material[4];
    private PanelColour[] panelColours = new PanelColour[4];
    public Vector3[] positionList;
    IEnumerator Start()
    {
        
        panelColours[0] = new PanelColour("Blue", materials[0]);
        panelColours[1] = new PanelColour("Green", materials[1]);
        panelColours[2] = new PanelColour("Red", materials[2]);
        panelColours[3] = new PanelColour("Yellow", materials[3]);

        //create the positionList for Jur
        positionList = new Vector3[gameObject.transform.childCount];
        FillPositionList();
        //ChangeColour(transform.GetChild(2).gameObject, panelColours[2]);
        //this renames the platforms to my conventions used in the code.
        RenamePlatforms();
        while (true)
        {
            yield return null;
        }
	}
	
	public void FillPositionList(){
		for (int i = 0; i < positionList.Length; i++)
        {
            positionList[i] = gameObject.transform.GetChild(i).position;
        }
	}

    public void RandomizePlatform()
    {
        int platformNumber = (int)(Random.value * transform.childCount);
        int platformInt = (int)(Random.value * 4);
        ChangeColour(transform.GetChild(platformNumber).gameObject, panelColours[platformInt]);
    }
    private void RenamePlatforms()
    {
        int i = 0;
        foreach (Transform platform in gameObject.transform)
        {
			string c = string.Empty;
			if(this.FindTransform(platform, "Cube") != null)
			{
            	 c = GetMaterialName(this.FindTransform(platform, "Cube").renderer.material);
			} else{
				c= GetMaterialName(platform.renderer.material);	
			}
            c += "Platform" + i;
            i++;
            platform.name = c;
        }
    }
    public string GetMaterialName(Material mat)
    {
        return mat.name.Split(' ')[0];
    }
    private void ChangeColour(GameObject go, PanelColour newColour)
    {
        Transform coloured = FindTransform(go.transform, "Cube");
        coloured.renderer.material = newColour.material;
        go.name = newColour.colourName + "Panel";
    }

    //I stole this code from the interwebs
    public Transform FindTransform(Transform parent, string name)
    {
        if (parent.name.Equals(name)) return parent;
        foreach (Transform child in parent)
        {
            Transform result = FindTransform(child, name);
            if (result != null) return result;
        }
        return null;
    }
    internal class PanelColour
    {
        public string colourName;
        public Material material;
        public static PanelColour YELLOW = new PanelColour("Yellow", null);
        public PanelColour(string name, Material mat){
            this.colourName = name;
            this.material = mat;
        }

    }
}
