using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "portfolio")]

public class portfolio : ScriptableObject
{
    public List<GameObject> pfBuildings; // ��Ʈ������ �ǹ� ������Ʈ ����Ʈ
    public StockList stockList; //�ֽ� ���� ����
    public double Cash = 0; //����
    public double totalGain = 0; //��ü ���ͱ�
    public double totalStPrice = 0; //��ü �򰡱ݾ�
    public double totalStInvest = 0; //��ü ���ڱ�
    public bool renew = false; //��Ʈ������ ���� ���� Ȯ�� ����

    public Dictionary<string, StockStat> stockInfo = new Dictionary<string, StockStat>(); //���� ������
    public Dictionary<string, List<Trade>> tradeList = new Dictionary<string, List<Trade>>(); //���� �ŷ� ����

    public struct StockStat //���� ������(������, ��� �ż� �ݾ�)
    {
        public int shares;
        public double avgCostPerShare;
    };

    public class Trade //API���� �ҷ��� ������ return �ϱ� ���� Ŭ����
    {
        public string tradeDate; //�Ÿ� ��¥
        public int shares; //�Ÿ� ����
        public double costPerShare; //��� �Ÿ� �ݾ�
        public int state; //buy: 0, sell: 1
        public Trade(string tradeDate, int shares, double costPerShare, int state)
        {
            this.tradeDate = tradeDate;
            this.shares = shares;
            this.costPerShare = costPerShare;
            this.state = state;
        }
    }
    public void BUY(string symbol, double costPerShare, int shares, string tradeDate, int state)
    {
        //���� �ŷ� ������ �����ϴ� ������ ���
        if (tradeList.ContainsKey(symbol))
        {
            //�Է��� �ڱ��� �������� ���� ���
            if (Cash >= shares * costPerShare)
            {
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //���� �ŷ� ������ �߰�

                StockStat tmp = stockInfo[symbol];
                tmp.avgCostPerShare = (shares * costPerShare + tmp.avgCostPerShare * tmp.shares) / (tmp.shares + shares);
                tmp.shares += shares;
                stockInfo[symbol] = tmp;
                Cash -= shares * costPerShare;
                renew = true;
            }
            else
            {
                Debug.Log("�ż��� �� �ִ� �ڱ��� �����մϴ�.");
            }
        }
        //���ο� ������ ���
        else
        {
            //�Է��� �ڱ��� �������� ���� ���
            if (Cash >= shares * costPerShare)
            {
                tradeList.Add(symbol, new List<Trade>());
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //���� �ŷ� ������ �߰�

                stockInfo.Add(symbol, new StockStat());
                StockStat tmp = stockInfo[symbol];
                tmp.shares = shares;
                tmp.avgCostPerShare = costPerShare;
                stockInfo[symbol] = tmp;
                Cash -= shares * costPerShare;
                renew = true;
            }
            else
            {
                Debug.Log("�ż��� �� �ִ� �ڱ��� �����մϴ�.");
            }
        }
    }
    public void SELL(string symbol, double costPerShare, int shares, string tradeDate, int state)
    {
        //���� �ŷ� ������ �����ϴ� ������ ���
        if (tradeList.ContainsKey(symbol))
        {
            if (stockInfo[symbol].shares < shares)
            {
                Debug.Log("�������񺸴� ���� �ŵ��� �� �����ϴ�.");
            }
            else
            {
                tradeList[symbol].Add(new Trade(tradeDate, shares, costPerShare, state)); //���� �ŷ� ������ �߰�

                StockStat tmp = stockInfo[symbol];
                tmp.avgCostPerShare = (tmp.avgCostPerShare * tmp.shares - shares * costPerShare) / (tmp.shares - shares);
                tmp.shares -= shares;
                stockInfo[symbol] = tmp;
                Cash += shares * costPerShare;
                renew = true;
            }
        }
        else
        {
            Debug.Log("�������� ���� ������ �ŵ��� �� �����ϴ�");
        }
    }

    //���� �� �ݾ� (���尡�� X ����)
    public double updateStPrice(string symbol)
    {
        return stockList.apiInfo[symbol].api_marketprice * stockInfo[symbol].shares;
    }

    //���� ���ڱ� (��� �ż� �ݾ� * ���� ����)
    public double updateStInvest(string symbol)
    {
        return stockInfo[symbol].avgCostPerShare * stockInfo[symbol].shares;
    }

    //��ü �� �ݾ�
    public double update_Total_Stprice()
    {
        totalStPrice = 0;

        foreach (var key in stockInfo.Keys.ToList())
        {
            if (stockInfo[key].shares == 0) { continue; } //���������� 0�� ��� ����
            totalStPrice += updateStPrice(key);
        }

        return totalStPrice;
    }
    
    //��ü ���ڱ�
    public double update_Total_StInvest()
    {
        totalStInvest = 0;

        //��ü ���ڱ�
        foreach (var key in stockInfo.Keys.ToList())
        {
            totalStInvest += updateStInvest(key);
        }

        return totalStInvest;
    }
}