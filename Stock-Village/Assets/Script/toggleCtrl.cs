using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggleCtrl : MonoBehaviour
{
    public StockList stockList;
    public portfolio myPortfolio;

    public Toggle mystockCk; //only my stocks ��� üũ
    public GameObject tog0; //��� on,off üũ ǥ��

    public Toggle weather; //weather �ɼ� üũ
    public GameObject tog1; //��� on,off üũ ǥ��
    public Toggle mktime; //market time �ɼ� üũ
    public GameObject tog2; //��� on,off üũ ǥ��
    public Toggle scale; //scaling �ɼ� üũ
    public GameObject tog3; //��� on,off üũ ǥ��

    public void toggle_mystock()
    {
        if (mystockCk.isOn)
        {
            tog0.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<InGame>().portfolioBtnClick();
        }
        else
        {
            tog0.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<InGame>().totalBtnClick();
        }
    }
    public void toggle_weather()
    {

        //weather �ɼ�
        if (weather.isOn)
        {
            tog1.SetActive(true);
        }
        else
        {
            tog1.SetActive(false);
        }
    }
    public void toggle_mktime()
    {
        //market time �ɼ�
        if (mktime.isOn)
        {
            tog2.SetActive(true);
        }
        else
        {
            tog2.SetActive(false);
        }
    }
    public void toggle_scale()
    {
        //scale �ɼ�
        if (scale.isOn)
        {
            tog3.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<InGame>().settingPortfolio();
        }
        else
        {
            tog3.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<InGame>().settingPortfolio();
        }
    }
}
