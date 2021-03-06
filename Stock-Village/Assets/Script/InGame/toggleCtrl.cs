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

    public Toggle mystockCk; //only my stocks 모드 체크
    public GameObject tog0; //토글 on,off 체크 표시

    public Toggle weather; //weather 옵션 체크
    public GameObject tog1; //토글 on,off 체크 표시
    public Toggle mktime; //market time 옵션 체크
    public GameObject tog2; //토글 on,off 체크 표시
    public Toggle scale; //scaling 옵션 체크
    public GameObject tog3; //토글 on,off 체크 표시

    public Text timeTxt; //market time 텍스트

    public void FixedUpdate()
    {
        marketTimer();
    }
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

        //전날 마감가에 대한 현재 시장가의 평균 변화율 계산
        foreach (var tmp in stockList.apiInfo.Keys)
        {
            avgMood += (stockList.apiInfo[tmp].api_marketprice - stockList.apiInfo[tmp].api_preclose) / stockList.apiInfo[tmp].api_preclose;
        }

        if(avgMood < 0) { onf = 0; } //rainy
        else { onf = 1; } //no rain

        //weather 옵션
        if (weather.isOn)
        {
            tog1.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().weatherClick(onf); //avgMood에 따라 on/off
        }
        else
        {
            tog1.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().weatherClick(1); //옵션이 꺼져있으면 off (=default)
        }
    }
    public void toggle_mktime()
    {
        //market time 옵션
        if (mktime.isOn)
        {
            tog2.SetActive(true);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().mktimeClick(marketTimer()); //market time에 따라 on/off
        }
        else
        {
            tog2.SetActive(false);
            GameObject.Find("InGameControl").GetComponent<DemoScript>().mktimeClick(0); //옵션이 꺼져있으면 on (=default)
        }
    }
    public void toggle_scale()
    {
        //scale 옵션
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

    int marketTimer()
    {
        //미국 시장 기준으로 23:30 ~ 6:00까지 market open
        string ck = "";

        DateTime now = DateTime.Now; //현재 시간
        DateTime openTime = new DateTime(now.Year, now.Month, now.Day, 23, 30, 00); //개장 : 23:30
        DateTime closeTime = new DateTime(now.Year, now.Month, now.Day, 06, 01, 00); //마감 : 6:01
        TimeSpan timer; //남은 시간

        //시간 체크
        if (now.Hour == 6)
        {
            if (now.Minute == 0) { ck = "open"; }
            else { ck = "close"; }
        }
        else if (now.Hour == 23)
        {
            if (now.Minute >= 30)
            {
                closeTime = closeTime.AddDays(1);
                ck = "open";
            }
            else { ck = "close"; }
        }
        else
        {
            if ((now.Hour >= 7) && (now.Hour <= 22)) { ck = "close"; }
            else { ck = "open"; }
        }

        //text setting & return
        if (ck.Equals("open"))
        {
            timer = closeTime - now;
            //Debug.Log("남은 시간 : " + timer.ToString(@"hh\:mm\:ss"));
            timeTxt.text = "마감까지 " + timer.ToString(@"hh\:mm\:ss");

            return 0;
        }
        else
        {
            timer = openTime - now;
            //Debug.Log("남은 시간 : " + timer.ToString(@"hh\:mm\:ss"));
            timeTxt.text = "개장까지 " + timer.ToString(@"hh\:mm\:ss");

            return 1;
        }
    }
}
