using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class clickBuilding : MonoBehaviour
{
    public portfolio myPortfolio; //��Ʈ������ ���� �����ڷ�
    public StockList stockList;//�ֽ� api������ �̿��ϱ� ���� stockList
    private RaycastHit hit; //���콺�� Ŭ���� ��ü

    //���� ���� UI
    public GameObject stockInfo; //���� ���� UI ������
    public Text _symbol; //���� �ڵ�
    public Text _mkPrice; //���� �ð�
    public Text _sector; //���� ������ �з�
    public Text _mkCap; //�ð��Ѿ�
    public Text _per; //PER
    public Text _52week; //�ð� ���� ��ȭ(52 week change)
    public Text _volume; //�ŷ���
    public Text _divDate; //�����

    //��Ʈ������ ���� UI
    public GameObject pfInfo; //��Ʈ������ ���� UI ������
    public Text _shares; //���� ����
    public Text _valueCost; //�򰡱ݾ�
    public Text _buy; //�ż� �ݾ�
    public Text _gain; //���ͱ�/��
    public Text _dividend; //����
    public Text _tradeDate; //�ֱ� �ŷ���

    private void FixedUpdate()
    {
        buildingClick();
    }
    void buildingClick()
    {
        Debug.Log("Click");
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; } //�����ִ� �˾�â�� �ִٸ� �������� ����

        //Ŭ���� ��ü �̸� ���
        if (Input.GetMouseButtonDown(0))
        {
            string symbol = "";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("stock"))//�ǹ� ������Ʈ�� Ŭ���� ���
                {
                    stockInfo.SetActive(true);
                    GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = true;
                    symbol = hit.collider.gameObject.name;
                    settingStockInfo(symbol);
                }

            }
        }
    }
    void settingStockInfo(string symbol)
    {
        if (!stockList.apiInfo.ContainsKey(symbol)) { return; } //������ ������ return

        _symbol.text = "[ " + symbol + " ]";
        _mkPrice.text = stockList.apiInfo[symbol].api_marketprice.ToString("F3") + "$";
        _sector.text = stockList.apiInfo[symbol].api_sector;
        _per.text = stockList.apiInfo[symbol].api_per.ToString("F3");
        _52week.text = stockList.apiInfo[symbol].api_52week.ToString("F3");
        _volume.text = stockList.apiInfo[symbol].api_volume.ToString();
        _divDate.text = GameObject.Find("InGameControl").GetComponent<dividendCtrl>().divDate(symbol);

        //_mkCap
        double tmp = stockList.apiInfo[symbol].api_marketcap;
        string marketcap = "";
        if (tmp >= 1000000000000) { marketcap = (tmp / 1000000000000).ToString("F3") + " T"; } //trillion
        else if (tmp >= 100000000000) { marketcap = (tmp / 100000000000).ToString("F3") + " B"; } //billion
        else { marketcap = tmp + ""; } //T, B ���� ���� �ִٸ� �߰��ϱ�
        _mkCap.text = marketcap;
    }
    
    public void closeBtnClick()
    {
        GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp = false;
        stockInfo.SetActive(false);
    }
    public void pfInfoBtnClick()
    {
        string symbol = _symbol.ToString();
        double tmp_gain;
        double tmp_gainRatio;

        stockInfo.SetActive(false);
        pfInfo.SetActive(true);

        _shares.text = myPortfolio.stockInfo[symbol].shares.ToString();
        _valueCost.text = myPortfolio.updateStPrice(symbol).ToString();
        _buy.text = myPortfolio.updateStInvest(symbol).ToString();

        tmp_gain = myPortfolio.updateStPrice(symbol) - myPortfolio.updateStInvest(symbol); //�򰡱ݾ� - ��� �ż� �ݾ�
        tmp_gainRatio = (tmp_gain / myPortfolio.updateStInvest(symbol)) * 100; // (�򰡱ݾ� - ��� �ż� �ݾ�) / ��� �ż� �ݾ� * 100
        _gain.text = tmp_gain + " / " + tmp_gainRatio;

        _dividend.text = GameObject.Find("InGameControl").GetComponent<dividendCtrl>().dividend(symbol).ToString();
        //_tradeDate
    }
    public void backBtnClick()
    {
        pfInfo.SetActive(false);
        stockInfo.SetActive(true);
    }

}
