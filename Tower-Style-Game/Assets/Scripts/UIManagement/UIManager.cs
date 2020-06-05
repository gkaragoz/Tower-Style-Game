using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using GK;

namespace GY {

    public class UIManager : MonoBehaviour {
        // Global Gold is gonna change
        private int _totalGold = 0;
        private int _currentSceneGold = 0;
        Vector3 firstTouchPos;
        bool isGameStarted;

        public RectTransform fill;
        public RectTransform indicator;

        public GameObject grpGold;
        public MainMenuCloserTween pnlMainMenu;
        public GameObject pnlLevelPath;
        public GameObject pnlInsideGamePlay;
        public GameObject pnlGamePlay;
        public PanelInTween pnlPause;
        public PanelInTween pnlFail;
        public PanelInTween pnlWin;
        public PanelInTween pnlSure;
        public PanelInTween pnlMarket;
        public RectTransform imgArmor;
        public RectTransform imgDoubleJump;
        public GameObject gameSceneUIBlocker;

        public TextMeshProUGUI txtFailGold;
        public TextMeshProUGUI txtWinGold;
        public TextMeshProUGUI txtGlobalGold;
        public TextMeshProUGUI txtLevelHeader;
        public TextMeshProUGUI txtCurrentLevel;
        public TextMeshProUGUI txtNextLevel;

        private EndGamePlatform _endGamePlatform;

        [SerializeField]
        private InputManager _inputManager;
        private void Start() {
            //if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt("currentLevel"))
            //{
            //    SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel"));
            //}

            OpenMainMenu();
            _inputManager.OnInputDragging += OnDragging;
            _inputManager.OnInputBegin += OnInputBegin;

            _totalGold = PlayerPrefs.GetInt("totalGold");
            UpdateGoldUI();

            txtLevelHeader.text= "Level " + (SceneManager.GetActiveScene().buildIndex + 1 );
            txtCurrentLevel.text= "Level " + (SceneManager.GetActiveScene().buildIndex + 1 );
            txtNextLevel.text= "Level " + (SceneManager.GetActiveScene().buildIndex + 2 );
            _endGamePlatform = GameObject.FindObjectOfType<EndGamePlatform>();

        }
        private void OnInputBegin(Vector2 obj) {
            firstTouchPos = obj;
        }
        private void OnDragging(Vector2 arg1, Vector2 arg2) {
            if (Vector3.Distance(arg1,firstTouchPos) >.5 && !isGameStarted) {
                OpenGamePlayPanel();
                CloseMainMenu();
                isGameStarted = true;
            }
        }
        public void OpenMainMenu() {
            pnlMainMenu.Open();
            grpGold.SetActive(true);
        }
        public void CloseMainMenu() {
            pnlMainMenu.Close();
            grpGold.SetActive(false);
        }
        public void OpenGamePlayPanel() {
            pnlGamePlay.SetActive(true);
            pnlInsideGamePlay.SetActive(true);
            pnlLevelPath.SetActive(true);
        }
        public void OpenPausePanel() {
            pnlInsideGamePlay.SetActive(false);
            pnlPause.Open();
            grpGold.SetActive(true);
            Time.timeScale = 0;
            gameSceneUIBlocker.SetActive(true);
        }
        public void ClosePausePanel() {
            pnlInsideGamePlay.SetActive(true);
            pnlPause.Close();
            grpGold.SetActive(false);
            Time.timeScale = 1;
            gameSceneUIBlocker.SetActive(false);
        }
        public void OpenFailPanel() {
            LeanTween.delayedCall(0.5f, () => {
                pnlInsideGamePlay.SetActive(false);
                grpGold.SetActive(true);
                pnlFail.Open();
                gameSceneUIBlocker.SetActive(true);
            });
        }
        public void OpenSuccesPanel() {

            _endGamePlatform.EndGameVFX();
            LeanTween.delayedCall(1f, () => {
                pnlGamePlay.SetActive(false);
                pnlInsideGamePlay.SetActive(false);
                grpGold.SetActive(true);
                pnlWin.Open();
                gameSceneUIBlocker.SetActive(true);
            });
        }

        public void OpenMarketPanel() {
            gameSceneUIBlocker.SetActive(true);
            grpGold.SetActive(true);
            pnlMarket.Open();
        }
        public void CloseMarketPanel() {
            grpGold.SetActive(true);
            pnlMarket.Close();
            gameSceneUIBlocker.SetActive(false);
        }
        public void WatchAds() {
            Debug.Log("Reklam İzle");
        }
        public void ShowRanking() {
            Debug.Log("Sıralamayı Göster");
        }
        public void ShowSurePanel() {
            pnlSure.Open();
        }
        public void RestartLevel() {
            LeanTween.cancelAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;

        }
        public void OpenNextScene() {
            LeanTween.cancelAll();
            if (SceneManager.GetActiveScene().buildIndex+1>20)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel + 1);
            PlayerPrefs.SetInt("currentLevel",currentLevel+1);
        }

        public void ShowDoubleJump() {
            imgDoubleJump.anchoredPosition = new Vector3(Screen.width / 2, 0, 0);

            var seq = LeanTween.sequence();

            seq.append(LeanTween.scale(imgDoubleJump.gameObject, Vector3.one * 1.4f, 0.25f).setFrom(0).setEase(LeanTweenType.easeOutBack));
            seq.insert(LeanTween.scale(imgDoubleJump.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutQuart));
            seq.append(
                LeanTween.value(imgDoubleJump.anchoredPosition.x, 109, 1f).setEase(LeanTweenType.easeOutCubic).setDelay(0.3f).setOnUpdate((float newValue) => {
                    Vector3 newPos = new Vector3(newValue, imgDoubleJump.anchoredPosition.y, 0);
                    imgDoubleJump.anchoredPosition = newPos;
                })
            );

            imgDoubleJump.gameObject.SetActive(true);
        }

        public void ShowArmor() {
            imgArmor.anchoredPosition = new Vector3(Screen.width / 2, 0, 0);

            var seq = LeanTween.sequence();

            seq.append(LeanTween.scale(imgArmor.gameObject, Vector3.one * 1.4f, 0.25f).setFrom(0).setEase(LeanTweenType.easeOutBack));
            seq.insert(LeanTween.scale(imgArmor.gameObject, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutQuart));
            seq.insert(LeanTween.value(imgArmor.anchoredPosition.y, 175, 1f).setEase(LeanTweenType.easeOutCubic).setDelay(0.3f).setOnUpdate((float newValue) => {
                Vector3 newPos = new Vector3(imgArmor.anchoredPosition.x, newValue, 0);
                imgArmor.anchoredPosition = newPos;
            }));
            seq.append(
                LeanTween.value(imgArmor.anchoredPosition.x, 109, 1f).setEase(LeanTweenType.easeOutCubic).setDelay(0.3f).setOnUpdate((float newValue) => {
                    Vector3 newPos = new Vector3(newValue, imgArmor.anchoredPosition.y, 0);
                    imgArmor.anchoredPosition = newPos;
                })
            );

            imgArmor.gameObject.SetActive(true);
        }

        public void CloseDoubleJump() {
            LeanTween.scale(imgDoubleJump.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
                imgDoubleJump.gameObject.SetActive(false);
            });
        }

        public void CloseArmor() {
            LeanTween.scale(imgArmor.gameObject, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
                imgArmor.gameObject.SetActive(false);
            });
        }

        ///Must Change Area
        ///
        public void AddGold() {
            _totalGold += 1;
            _currentSceneGold += 1;
            PlayerPrefs.SetInt("totalGold",_totalGold);
            UpdateGoldUI();
        }
       
        public void UpdateGoldUI() {
            txtGlobalGold.text = _totalGold.ToString();
            txtWinGold.text = _currentSceneGold.ToString();
            txtFailGold.text = _currentSceneGold.ToString();
        }

        public void UpdateIndicator(float percentage) {

            if (percentage<=1) {
            fill.sizeDelta = new Vector2(percentage * 600, fill.sizeDelta.y);

            indicator.anchoredPosition = new Vector3((percentage * 589)-(24), indicator.anchoredPosition.y);
            } else {
                fill.sizeDelta = new Vector2( 600, fill.sizeDelta.y);

                indicator.anchoredPosition = new Vector3( 589 - (24), indicator.anchoredPosition.y);
            }
        }



    }
}