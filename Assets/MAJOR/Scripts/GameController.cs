using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] animeGirls;
    public UnityEvent onGameComplete;
    private int _currentStage = 0;
    private LabData _labData;

    private void Start()
    {
        _labData = FindObjectOfType<LabData>();
    }

    public void CompleteStage()
    {
        _currentStage++;
        OnStageComplete(_currentStage);
        if (_currentStage == 3)
        {
            onGameComplete.Invoke();
            // Запускаем асинхронный метод через корутину
            StartCoroutine(SendPostRequest());
        }
    }

    private void OnStageComplete(int objIndex)
    {
        foreach (var obj in animeGirls)
        {
            obj.SetActive(false);
        }
        animeGirls[objIndex].SetActive(true);
    }

    private IEnumerator SendPostRequest()
    {
        // Запускаем асинхронный метод и ждем его завершения
        yield return PostRequestExample.Main(_labData.GetWalletAddress());
    }

    public static class PostRequestExample
    {
        public static async Task Main(string wallet_address_new)
        {
            wallet_address_new = "0QAFmjUoZUqKFEBGYFEMbv-m61sFStgAfUR8J6hJDwUU0z7c";
            string baseUrl = "http://ipu.moe:8000/reward";
            int points = 100;

            // Создаем URL с параметрами запроса
            string urlWithParams = $"{baseUrl}?wallet_addr={wallet_address_new}&points={points}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(urlWithParams, null); // Пустое содержимое

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log("Response: " + responseBody);
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Debug.LogError("Error: " + response.StatusCode);
                    Debug.LogError("Error details: " + errorResponse);
                }
            }
        }
    }
}
