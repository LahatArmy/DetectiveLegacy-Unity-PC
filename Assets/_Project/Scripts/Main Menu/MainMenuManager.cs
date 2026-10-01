using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Management")]
    [SerializeField] private int gameSceneIndex = 1;

    [Header("UI References (Assign in Inspector)")]
    [Tooltip("RectTransform dari kertas main menu utama")]
    [SerializeField] private RectTransform mainMenuPaper; 
    
    [Tooltip("RectTransform dari kertas credits")]
    [SerializeField] private RectTransform creditsPaper; 
    
    [Tooltip("CanvasGroup dari panel hitam untuk fade")]
    [SerializeField] private CanvasGroup fadePanel; 
    
    [Tooltip("GameObject yang berisi UI Loading (Teks/Icon)")]
    [SerializeField] private GameObject loadingUI; 

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.8f;
    [SerializeField] private float fadeDuration = 1.0f;

    // Variabel untuk menyimpan posisi luar layar
    private Vector2 offScreenBottom;
    private Vector2 offScreenRight;
    private Vector2 centerScreen = Vector2.zero;

    private void Start()
    {
        // Menentukan posisi di luar layar berdasarkan tinggi dan lebar layar saat ini
        offScreenBottom = new Vector2(0, -Screen.height * 0.5f);
        offScreenRight = new Vector2(Screen.width * 0.5f, 0);

        // Set posisi awal UI sebelum animasi dimulai
        mainMenuPaper.anchoredPosition = offScreenBottom;
        creditsPaper.anchoredPosition = offScreenRight;

        // Pastikan layar tidak tertutup fade screen & UI loading mati
        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;
        loadingUI.SetActive(false);

        // Mulai animasi kertas menu utama muncul dari bawah
        StartCoroutine(SlideUI(mainMenuPaper, offScreenBottom, centerScreen, slideDuration));
    }

    // Dipanggil saat menekan tombol "Start The Game"
    public void StartTheGame()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    // Dipanggil saat menekan tombol "Credits"
    public void OpenCredits()
    {
        StartCoroutine(SlideUI(creditsPaper, creditsPaper.anchoredPosition, centerScreen, slideDuration));
    }

    // Dipanggil saat menekan tombol "Back" di menu Credits
    public void CloseCredits()
    {
        StartCoroutine(SlideUI(creditsPaper, creditsPaper.anchoredPosition, offScreenRight, slideDuration));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Coroutine untuk menggerakkan UI (Smooth)
    private IEnumerator SlideUI(RectTransform panel, Vector2 startPos, Vector2 endPos, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            // Mathf.SmoothStep memberikan efek animasi yang lebih natural (ease-in & ease-out)
            float t = Mathf.SmoothStep(0, 1, time / duration);
            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            
            time += Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = endPos; // Pastikan posisi akhir akurat
    }

    // Coroutine untuk fade out hitam lalu load scene asinkron
    private IEnumerator FadeAndLoadScene()
    {
        // Cegah klik tombol lain saat proses loading dimulai
        fadePanel.blocksRaycasts = true;

        // 1. Fase Fade Out ke hitam
        float time = 0;
        while (time < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        fadePanel.alpha = 1;

        // 2. Munculkan UI Loading setelah layar hitam total
        loadingUI.SetActive(true);

        // (Opsional) Beri sedikit jeda agar pemain bisa melihat UI Loading sebelum scene benar-benar diproses
        yield return new WaitForSeconds(0.5f);

        // 3. Mulai Load Scene berikutnya secara background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneIndex);

        // Tunggu sampai loading selesai
        while (!asyncLoad.isDone)
        {
            // Kamu juga bisa menghubungkan progress bar loading di sini (menggunakan asyncLoad.progress)
            yield return null; 
        }
    }
}