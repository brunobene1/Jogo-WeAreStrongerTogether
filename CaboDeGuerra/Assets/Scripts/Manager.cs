using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { jogadorTurno, cpuTurno, gameOverP, gameOverC};
public class Manager : MonoBehaviour
{
    public void BotõesRetornarMenu()
    {
        Debug.Log("Indo para menu");
        SceneManager.LoadScene("Menu");
    }

    //Pegar os game objects da cena
    public GameObject Corda;
    public Text DinheiroDisplay;
    public Text DinheiroDisplayGOP;
    public Text DinheiroDisplayGOC;
    public GameObject LinhaTrigger;
    public Text AtaqueDoTime;
    public Text DefesaDoTime;
    public Text AtaqueDaCpu;
    public Text DefesaDaCpu;
    public Animator inGameAnimator;
    public GameObject gameOverEMPTYp;
    public GameObject gameOverEMPTYc;
    //Variaveis de cada um
    public float ForçaJogador;
    public float ForçaCpu;
    public float DefesaJogador;
    public float DefesaCpu;
    //Controlador de status
    public GameState cntrl;
    //variavel de tempo de espera
    private float TempoDeEspera;
    private float tempinho = 1.5f;
    //Variavel dinheiro
    private float dinero;
    private float dineroPegarTotal;
    private float cntrlDinero;
    private float BonusVitoria;
    // Controlador do GameOver
    public bool GameOverBoolP;
    public bool GameOverBoolC;
    // loop for da função logica dos turnos
    private int i;
    //AUDIOS gameObjects
    public GameObject AudioTime;
    public GameObject AudioInimigo1;
    public GameObject AudioInimigo2;
    public GameObject AudioInimigo3;
    public GameObject AudioInimigo4;
    public GameObject AudioInimigo5;
    public GameObject AudioBloqueio;
    private GameObject AudioDeCadaFase;
    //Variavel para desbloquear o prox nivel quando ganhar!
    public int ProxLvlIndice;
    // Divisor de ataque de cada lvl
    private int Divisor;
    // Particula Movimento
    public GameObject ParticulaMovimento;
    // objeto de bloqueio
    public GameObject youGotBlocked;
    //referencia das setas
    public GameObject SetaTime;
    public GameObject SetaCpu;
    //controlador de vezes que mostra a setacpu
    private int controlladorSetaCpu;
    //divisor de defesa para poder balancear
    private int DivisorDefesa;
    //tempo animação ReadyGo
    private float TempoReadyGo;
    // audio ready go
    public GameObject AUDIOReadyGO;
    //bollean pular
    public bool PularBotao;
    //audio inimigo  fase secreta
    public GameObject AudioInimigoFaseSecreta;
    private void Start()
    {
        //definir status atual 
        cntrl = GameState.jogadorTurno;
        //voltar os objetos para posição atual;
        Corda.transform.position = new Vector2(0, -4.3f);
        //definir tempo de espera
        TempoDeEspera = 1f;
        //definir variaveis de cada lado;
        ForçaJogador = PlayerPrefs.GetFloat ("AtaqueTOTALdoTime", 1);
        DefesaJogador = PlayerPrefs.GetFloat("DefesaTOTALdoTime", 1);
        //Controlador de Dinheiro
        dinero = 0f;
        DinheiroDisplay.text = dinero.ToString();
        dineroPegarTotal = PlayerPrefs.GetFloat("Moneyy", 0);
        //botar as variaveis game over para começarem falsas
        GameOverBoolP = false;
        GameOverBoolC = false;
        //Colocar o texto nos respectivos lugares
        AtaqueDoTime.text = ForçaJogador.ToString();
        DefesaDoTime.text = DefesaJogador.ToString();
        
        //definir valor do i do loop for
        i = 0;
        //pegar qual o index do prox lvl que ira ser desbloqueado
        ProxLvlIndice = SceneManager.GetActiveScene().buildIndex + 1;
        //Cada Fase Vai ter Variaveis Diferentes
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ForçaCpu = 10.3f;
            DefesaCpu = 9.8f;
            
            AudioDeCadaFase = AudioInimigo1;
            Divisor = 5;
            cntrlDinero = 2;
            BonusVitoria = 200;
            DivisorDefesa = 5;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            ForçaCpu = 25.9f;
            DefesaCpu = 23.9f;
            AudioDeCadaFase = AudioInimigo2;
            Divisor = 10;
            cntrlDinero = 5;
            BonusVitoria = 500;
            DivisorDefesa = 8;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            ForçaCpu = 61.3f;
            DefesaCpu = 55.5f;
            AudioDeCadaFase = AudioInimigo3;
            Divisor = 15;
            cntrlDinero = 20;
            BonusVitoria = 1000;
            DivisorDefesa = 35;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            ForçaCpu = 150.3f;
            DefesaCpu = 130.8f;
            AudioDeCadaFase = AudioInimigo4;
            Divisor = 30;
            cntrlDinero = 50;
            BonusVitoria = 2000;
            DivisorDefesa = 45;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            ForçaCpu = 250f;
            DefesaCpu = 250f;
            AudioDeCadaFase = AudioInimigo5;
            Divisor = 65;
            cntrlDinero = 100;
            BonusVitoria = 10000;
            DivisorDefesa = 150;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            ForçaCpu = 340f;
            DefesaCpu = 340f;
            // lembrar de pegar audio
            AudioDeCadaFase = AudioInimigoFaseSecreta;
            Divisor = 100;
            cntrlDinero = 1000;
            BonusVitoria = 100000;
            DivisorDefesa = 200;
        }
        AtaqueDaCpu.text = ForçaCpu.ToString();
        DefesaDaCpu.text = DefesaCpu.ToString();
        //controlador setacpu
        controlladorSetaCpu = 0;
        // tempo ReadyGO
        TempoReadyGo = 1.5f;
        Instantiate(AUDIOReadyGO, transform.position, Quaternion.identity);
        //pular botao
        PularBotao = false;
    }
    //PULAR BOTAOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
    public void BotaoPular()
    {
        PularBotao = true;
    }
    private void Update()
    {
        DinheiroDisplay.text = dinero.ToString();
        if(TempoReadyGo < 0)
        {
            if(PularBotao == true)
            {
                tempinho = 0f;
            }
            
            LogicaDosTurnos();
        }
        else
        {
            TempoReadyGo -= Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        // Debug.Log(TempoDeEspera);
    }

    private void LogicaDosTurnos()
    {
        if (GameOverBoolC == false && GameOverBoolP == false && Corda.transform.position.x > -6.3f && Corda.transform.position.x < 6.3f)
        {
            if (cntrl == GameState.jogadorTurno)
            {
                //inGameAnimator.SetTrigger("JOGADORTURN");
                Debug.Log("JogadorTurno");
                if (TempoDeEspera <= 0)
                {
                    controlladorSetaCpu++;
                    if ((DefesaCpu == ForçaJogador) && (DefesaJogador == ForçaCpu) && (DefesaJogador == DefesaCpu) && (ForçaJogador == ForçaCpu))
                    {
                        //Caso eles fiquem presos num loop de bloqueio!
                        Corda.transform.Translate(-(ForçaJogador * 2 / (Divisor * (DefesaCpu / DivisorDefesa))), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioTime, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x) + (ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    else if ((DefesaCpu >= ForçaJogador) && (DefesaJogador >= ForçaCpu))
                    {
                        //Caso eles fiquem presos num loop de bloqueio!
                        Corda.transform.Translate(-(ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioTime, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x ) + (ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    else if(ForçaJogador<= DefesaCpu)
                    {
                        Corda.transform.Translate(0, 0, 0);
                        //animação de bloqueio
                        Instantiate(youGotBlocked, transform.position, Quaternion.identity);
                        //som de escudo defendido
                        if (PularBotao == false)
                        {
                            Instantiate(AudioBloqueio, transform.position, Quaternion.identity);
                        }
                    }
                    else if (ForçaJogador > DefesaCpu)
                    {
                        Corda.transform.Translate(-(ForçaJogador/(Divisor*(DefesaCpu/DivisorDefesa))), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioTime, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x - 4.3f) + (ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    /*
                    if ((DefesaCpu >= ForçaJogador) && (DefesaJogador >= ForçaCpu))
                    {
                        //Caso eles fiquem presos num loop de bloqueio!
                        Corda.transform.Translate(-(ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioTime, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x - 4.3f) + (ForçaJogador / (Divisor * (DefesaCpu / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    */
                    //Corda.transform.Translate((-ForçaJogador+DefesaCpu), 0, 0);
                    TempoDeEspera = tempinho;
                    SetaCpu.SetActive(false);
                    cntrl = GameState.cpuTurno;
                    dinero += cntrlDinero;
                }
                else
                {
                    if (controlladorSetaCpu == 0)
                    {
                        SetaCpu.SetActive(false);
                    }
                    else
                    {
                        SetaCpu.SetActive(true);
                    }
                    
                    TempoDeEspera -= Time.deltaTime;
                }

            }
            else if (cntrl == GameState.cpuTurno)
            {
                //inGameAnimator.SetTrigger("CPUTURN");
                Debug.Log("CpuTurno");
                if (TempoDeEspera <= 0)
                {
                    if ((DefesaCpu == ForçaJogador) && (DefesaJogador == ForçaCpu) && (DefesaJogador == DefesaCpu) && (ForçaJogador == ForçaCpu))
                    {
                        //Caso eles fiquem presos num loop de bloqueio!
                        Corda.transform.Translate((ForçaCpu / 2) / (Divisor * (DefesaJogador / DivisorDefesa)), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioDeCadaFase, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x + 6f) - (ForçaCpu / (Divisor * (DefesaJogador / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    else if ((DefesaCpu >= ForçaJogador) && (DefesaJogador >= ForçaCpu))
                    {
                        //Caso eles fiquem presos num loop de bloqueio!
                        Corda.transform.Translate(ForçaCpu / (Divisor * (DefesaJogador / DivisorDefesa)), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioDeCadaFase, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x + 6f) - (ForçaCpu / (Divisor * (DefesaJogador / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    else if(ForçaCpu<= DefesaJogador)
                    {
                        Corda.transform.Translate(0, 0, 0);
                        Instantiate(youGotBlocked, transform.position, Quaternion.identity);
                        //som de escudo defendendo
                        if (PularBotao == false)
                        {
                            Instantiate(AudioBloqueio, transform.position, Quaternion.identity);
                        }
                    }
                    else if(ForçaCpu > DefesaJogador)
                    {
                        Corda.transform.Translate(ForçaCpu / (Divisor*(DefesaJogador/DivisorDefesa)), 0, 0);
                        if (PularBotao == false)
                        {
                            Instantiate(AudioDeCadaFase, transform.position, Quaternion.identity);
                            Instantiate(ParticulaMovimento, new Vector3((Corda.transform.position.x + 4.3f) - (ForçaCpu / (Divisor * (DefesaJogador / DivisorDefesa))), Corda.transform.position.y, Corda.transform.position.z), Quaternion.identity);
                        }
                    }
                    
                    //Corda.transform.Translate((ForçaCpu-DefesaJogador), 0, 0);
                    TempoDeEspera = tempinho;
                    SetaTime.SetActive(false);
                    cntrl = GameState.jogadorTurno;
                    dinero += cntrlDinero;
                }
                else
                {
                    SetaTime.SetActive(true);
                    TempoDeEspera -= Time.deltaTime;
                }
            }
        }
        else if (GameOverBoolC == true || Corda.transform.position.x <= -6.3f)
        {
            SetaTime.SetActive(true);
            cntrl = GameState.gameOverC;
            gameOverEMPTYc.SetActive(true);
            DinheiroDisplayGOC.text = dinero.ToString();
            if (i < 1)
            {
                Debug.Log("O dinero foi:" + dinero);
                Debug.Log("O moneyy total era:" + dineroPegarTotal);
                dinero = dinero + BonusVitoria;
                PlayerPrefs.SetFloat("Moneyy", dineroPegarTotal += dinero);
                Debug.Log("Agora o moneyy total é: " + dineroPegarTotal);
                i++;
            }
            inGameAnimator.SetTrigger("GOC");
            //Desbloquear a Próxima fase!
            if(ProxLvlIndice > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", ProxLvlIndice);
            }
        }
        else if(GameOverBoolP == true || Corda.transform.position.x >= 6.3f)
        {
            SetaCpu.SetActive(true);
            cntrl = GameState.gameOverP;
            gameOverEMPTYp.SetActive(true);
            DinheiroDisplayGOP.text = dinero.ToString();
            Debug.Log("o i era:" + i);
            if (i < 1)
            {
                //Debug.Log("O dinero foi:" + dinero);
                //Debug.Log("O moneyy total era:" + dineroPegarTotal);
                PlayerPrefs.SetFloat("Moneyy", dineroPegarTotal += dinero);
                //Debug.Log("Agora o moneyy total é: " + dineroPegarTotal);
                Debug.Log("o i entrou no if sendo: " + i);
                i++;
            }
            else
            {
                Debug.Log("i foi mandado para o else e agora é:" + i);
            }
            inGameAnimator.SetTrigger("GOP");
        }
    }
}
