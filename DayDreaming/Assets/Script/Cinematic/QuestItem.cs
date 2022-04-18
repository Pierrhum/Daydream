using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{

    private Button Button;
    private Image Image;
    private bool Hidden = true;

    private void Awake()
    {
        Button = GetComponent<Button>();
        Image = GetComponent<Image>();
        Image.gameObject.SetActive(false);
    }

    public bool isHidden()
    {
        return this.Hidden;
    }

    public IEnumerator Show()
    {
        Image.gameObject.SetActive(true);
        Hidden = false;
        Image.transform.localScale = Vector3.zero;
        Vector3 visible = new Vector3(1, 1, 1);
        float s = 0;
        while(Image.transform.localScale != visible)
        {
            Image.transform.localScale = new Vector3(s, s, s);
            s += Time.deltaTime;
            if (s > 1) s = 1;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Button.interactable = true;
    }

    public IEnumerator Hide()
    {
        Button.interactable = false;
        Vector3 visible = new Vector3(0, 0, 0);
        float s = 1;
        while (Image.transform.localScale != visible)
        {
            Image.transform.localScale = new Vector3(s, s, s);
            s -= Time.deltaTime;
            if (s < 0) s = 0;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Hidden = true;
        Image.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        StartCoroutine(Hide());
    }
}
