using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dispatcher : MonoBehaviour {
    static public Dispatcher Instance = new Dispatcher();
    [SerializeField]
    private GameObject LoginUi;
    [SerializeField]
    private GameObject HomeUi;

    [SerializeField]
    private GameObject NewProjUi;

    [SerializeField]
    private GameObject DescProjUi;

    [SerializeField]
    private GameObject OverviewProjUi;

    [SerializeField]
    private GameObject ActiveUi;

    [SerializeField]
    private GameObject ProjectListUi;

    [SerializeField]
    private GameObject ModifyProjectUi;

    [SerializeField]
    private GameObject RessourceListUi;

    [SerializeField]
    private GameObject ModifyRessourceUi;

    [SerializeField]
    private GameObject CardListUi;

    [SerializeField]
    private GameObject ModifyCardUi;

    [SerializeField]
    private GameObject EditVisualCardUi;

    [SerializeField]
    private GameObject NewRessourceUi;
    private Dictionary<string, GameObject> UiMap;
	// Use this for initialization
	void Start () {
        UiMap = new Dictionary<string, GameObject>();
        UiMap["LoginUi"] = LoginUi;
        UiMap["HomeUi"] = HomeUi;
        UiMap["NewProjUi"] = NewProjUi;
        UiMap["DescProjUi"] = DescProjUi;
        UiMap["OverviewProjUi"] = OverviewProjUi;
        UiMap["ProjectListUi"] = ProjectListUi;
        UiMap["ModifyProjectUi"] = ModifyProjectUi;
        UiMap["RessourceListUi"] = RessourceListUi;
        UiMap["ModifyRessourceUi"] = ModifyRessourceUi;
        UiMap["CardListUi"] = CardListUi;
        UiMap["ModifyCardUi"] = ModifyCardUi;
        UiMap["EditVisualCardUi"] = EditVisualCardUi;
        UiMap["NewRessourceUi"] = NewRessourceUi;

       dispatch("LoginUi", "LoginController", new Dictionary<string, string>());
        //dispatch("NewProjUi", "NewProjController", new List<string>());

    }

    // Update is called once per frame
    void Update () {
	
	}

   
   

    public void dispatch(string UiName, string UiScript, Dictionary<string,string> param)
    {
        if (ActiveUi)
            Destroy(ActiveUi);
        /*ActiveUi = GameObject.FindGameObjectWithTag("active");
        Destroy(ActiveUi);*/
        ActiveUi = Instantiate(UiMap[UiName]) as GameObject;
        ActiveUi.tag = "active";
        BasicController UiController = ActiveUi.GetComponent(UiScript) as BasicController;
        UiController.setParams(param);
        UiController.apply();
        ActiveUi.transform.SetParent(gameObject.transform, false);
    }
}
