using System;
using DigitalRuby.RainMaker;
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
        double avgMood = 0f;
        int onf;

        //���� �������� ���� ���� ���尡�� ��� ��ȭ�� ���
        foreach (var tmp in stockList.apiInfo.Keys)
        {
            avgMood += (stockList.apiInfo[tmp].api_marketprice - stockList.apiInfo[tmp].api_preclose) / stockList.apiInfo[tmp].api_preclose;
        }

        if(avgMood < 0) { onf = 0; } //rainy
        else { onf = 1; } //no rain

        //weather �ɼ�
        if (weather.isOn)
        {
            tog1.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().weatherClick(onf); //avgMood�� ���� on/off
        }
        else
        {
            tog1.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().weatherClick(1); //�ɼ��� ���������� off (=default)
        }
    }
    public void toggle_mktime()
    {
        //�̱� ���� �������� 23:30 ~ 6:00���� market open
        string h = DateTime.Now.ToString(("HH"));
        int hour = Int32.Parse(h);

        int onf;
        if ((hour > 23) || (hour < 7)) { onf = 0; } //market open
        else { onf = 1; } //market close

        //market time �ɼ�
        if (mktime.isOn)
        {
            tog2.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().mktimeClick(onf); //market time�� ���� on/off
        }
        else
        {
            tog2.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().mktimeClick(0); //�ɼ��� ���������� on (=default)
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
