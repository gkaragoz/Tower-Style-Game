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
        public GameObject imgArmor;
        public GameObject imgDoubleJump;
        public GameObject gameSceneUIBlocker;

        public TextMeshProUGUI txtFailGold;
        public TextMeshProUGUI txtWinGold;
        public TextMeshProUGUI txtGlobalGold;
        public TextMeshProUGUI txtCurrentLevelIndex;
        public TextMeshProUGUI txtNextLevelIndex;
        public TextMeshProUGUI txtLevelHeader;

        public ParticleSystem VFXEndStar01;
        public ParticleSystem VFXEndStar02;
        [SerializeField]
        private InputManager _inputManager;
        private void Start() {
            
            OpenMainMenu();
            _inputManager.OnInputDragging += OnDragging;
            _inputManager.OnInputBegin += OnInputBegin;

            _totalGold = PlayerPrefs.GetInt("totalGold");
            UpdateGoldUI();

            txtLevelHeader.text= "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
            txtCurrentLevelIndex.text = "Level "+ SceneManager.GetActiveScene().buildIndex.ToString();
            txtNextLevelIndex.text = "Level "+ (SceneManager.GetActiveScene().buildIndex+1).ToString();
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
            LeanTween.delayedCall(1f, () => {
                pnlGamePlay.SetActive(false);
                pnlInsideGamePlay.SetActive(false);
                grpGold.SetActive(true);
                pnlWin.Open();
                gameSceneUIBlocker.SetActive(true);

                var seq = LeanTween.sequence();
                seq.append(LeanTween.delayedCall(0f, () => {
                    if (VFXEndStar01 != null) {
                        VFXEndStar01.Play();
                    }
                }));
                seq.append(LeanTween.delayedCall(2f, () => {
                    if (VFXEndStar02 != null) {
                        VFXEndStar02.Play();
                    }
                }));
            });
        }

        public void OpenMarketPanel() {
            grpGold.SetActive(true);
            pnlMainMenu.Close();
            pnlMarket.Open();
            gameSceneUIBlocker.SetActive(true);
        }
        public void CloseMarketPanel() {
            grpGold.SetActive(true);
            pnlMainMenu.Open();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        public void OpenNextScene(int sceneIndex) {
            Debug.Log(sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }
        public void ShowDoubleJump() {
            imgDoubleJump.SetActive(true);
        }
        public void ShowArmor() {
            imgArmor.SetActive(true);
        }
        public void CloseDoubleJump() {
            imgDoubleJump.SetActive(false);
        }
        public void CloseArmor() {
            imgArmor.SetActive(false);
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