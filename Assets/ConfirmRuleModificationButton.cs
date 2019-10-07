using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmRuleModificationButton : MonoBehaviour
{
    GameObject model;
    int projectId;
    int ruleId;
    string projectName;
    [SerializeField]
    GameObject inputName;
    [SerializeField]
    GameObject inputDesc;
    [SerializeField]
    GameObject signals;
    [SerializeField]
    GameObject types;
    [SerializeField]
    GameObject instructions;
    [SerializeField]
    GameObject ressources;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static T DeserializeJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public void setProjectId(int id)
    {
        projectId = id;
    }

    public void setIdToModify(int id)
    {
        ruleId = id;
    }

    public void setProjectName(string name)
    {
        projectName = name;
    }
    public void applyInServerResponse(string json)
    {
        Dictionary<string, object> resp = DeserializeJson<Dictionary<string, object>>(json);
        ButtonListener but = gameObject.GetComponent<ButtonListener>();
        but.addParam("id", projectId.ToString());
        but.addParam("project_id", projectId.ToString());
        but.addParam("project_name", projectName);
        but.SendToDispatch();
    }

    public void click()
    {
        model = GameObject.Find("ModelRules");
        ModelRules modelScr = model.GetComponent<ModelRules>();
        string name = inputName.GetComponent<TMP_InputField>().text;
        string desc = inputDesc.GetComponent<TMP_InputField>().text;
        int n;
        string[] descs = new string[1];
        descs[0] = "null";
        modelScr.updateField(ruleId.ToString(), projectName, name, desc,
            signals.GetComponent<ButtonSetArraySelection>().getSelectedList().ToArray(),
            types.GetComponent<ButtonSetArraySelection>().getSelectedList().ToArray(),
            instructions.GetComponent<ButtonSetArraySelection>().getSelectedList().ToArray(),
            ressources.GetComponent<ButtonGetAllRessourcesName>().getSelectedList().ToArray(), descs,
             "0", applyInServerResponse);

    }
}
