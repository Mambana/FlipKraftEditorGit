using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PhaseItemUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI indexText;
    public TextMeshProUGUI phaseNameText;

    public PhasesHandler PhaseHandler { get; set; }
    public RectTransform RectTransform { get; private set; }

    void Start()
    {

        //listOfProj = GameObject.FindWithTag("Container");
        PhaseHandler = GameObject.Find("Phases").GetComponent<PhasesHandler>();
    }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
        PhaseHandler.phaseSelected = int.Parse(indexText.text);
    }
}
