using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //primeiro os botões do menu
    public void SairDoJogo()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void Fase1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Fase2()
    {
        SceneManager.LoadScene("Fase2");
    }
    public void Fase3()
    {
        SceneManager.LoadScene("Fase3");
    }
    public void Fase4()
    {
        SceneManager.LoadScene("Fase4");
    }
    public void Fase5()
    {
        SceneManager.LoadScene("Fase5");
    }



    //AGORA O SISTEMA DE MONEY E SHOP
    public static float Moneyy;
    public Text MoneyMenuDisplay;
    public static float AtaqueTOTALdoTime;
    public static float DefesaTOTALdoTime;
    //---------------------------------------------------------------
    //referencia ataque
    public float DAJ;
    public float DAW;
    public float DAS;
    public float DAB;
    public Text DisplayDAJ;
    public Text DisplayDAW;
    public Text DisplayDAS;
    public Text DisplayDAB;
    //---------------------------------------------------------------
    //referencia defesa
    public float DDJ;
    public float DDW;
    public float DDS;
    public float DDB;
    public Text DisplayDDJ;
    public Text DisplayDDW;
    public Text DisplayDDS;
    public Text DisplayDDB;
    //-----------------------------------------------------------------
    //vezes ataque
    private static int VEZESBAJ;
    private int VEZESBAW;
    private int VEZESBAS;
    private int VEZESBAB;
    //vezes defesa
    private int VEZESBDJ;
    private int VEZESBDW;
    private int VEZESBDS;
    private int VEZESBDB;
    //Audio
    public GameObject AudioDinheiro;
    public GameObject AudioError;
    //Animação Sem dinheiro
    public Animator MenuAnimator;
    // variaveis bool para verificar se o atributo esta no maximo
    private int VerificarMaxBAJ;
    private int VerificarMaxBAW;
    private int VerificarMaxBAS;
    private int VerificarMaxBAB;
    private int VerificarMaxBDJ;
    private int VerificarMaxBDW;
    private int VerificarMaxBDS;
    private int VerificarMaxBDB;
    //sliders do menu
    public Slider sliderJA;
    public Slider sliderJD;
    public Slider sliderWA;
    public Slider sliderWD;
    public Slider sliderSA;
    public Slider sliderSD;
    public Slider sliderBA;
    public Slider sliderBD;
    //teste
    private int levelQueEsta;
    // botao reiniciar
    public Button BotaoReiniciarStatus;
    //Botao Fase secreta logica
    public float DinheiroFaseSecreta;
    public Text TextoDinheiroFaseSecreta;
    public GameObject AudioComprouFS;
    public GameObject ParticulaComprouFS;
    public GameObject ParticulaComprouFS2;


    private void Start()
    {
        levelQueEsta = PlayerPrefs.GetInt("levelAt");
        Debug.Log("o seu level é:"+levelQueEsta);
        //--------------------------------------------------
        Moneyy = PlayerPrefs.GetFloat("Moneyy", 50);
        MoneyMenuDisplay.text = Moneyy.ToString();
        AtaqueTOTALdoTime = PlayerPrefs.GetFloat("AtaqueTOTALdoTime", 1);
        DefesaTOTALdoTime = PlayerPrefs.GetFloat("DefesaTOTALdoTime", 1);
        //---------------------------------------------------------------
        //definindo os textos de CUSTO de ataque
        DAJ = PlayerPrefs.GetFloat("DAJ", 1);
        DAW = PlayerPrefs.GetFloat("DAW", 1);
        DAS = PlayerPrefs.GetFloat("DAS", 1);
        DAB = PlayerPrefs.GetFloat("DAB", 1);
        DisplayDAJ.text = DAJ.ToString();
        DisplayDAW.text = DAW.ToString();
        DisplayDAS.text = DAS.ToString();
        DisplayDAB.text = DAB.ToString();
        //definindo os textos de CUSTO de defesa
        DDJ = PlayerPrefs.GetFloat("DDJ", 1);
        DDW = PlayerPrefs.GetFloat("DDW", 1);
        DDS = PlayerPrefs.GetFloat("DDS", 1);
        DDB = PlayerPrefs.GetFloat("DDB", 1);
        DisplayDDJ.text = DDJ.ToString();
        DisplayDDW.text = DDW.ToString();
        DisplayDDS.text = DDS.ToString();
        DisplayDDB.text = DDB.ToString();
        //------------------Salvar as vezes que se aperta cada botao---------------------------
        VEZESBAJ = PlayerPrefs.GetInt("VEZESBAJ", 0);
        VEZESBAW = PlayerPrefs.GetInt("VEZESBAW", 0);
        VEZESBAS = PlayerPrefs.GetInt("VEZESBAS", 0);
        VEZESBAB = PlayerPrefs.GetInt("VEZESBAB", 0);
        VEZESBDJ = PlayerPrefs.GetInt("VEZESBDJ", 0);
        VEZESBDW = PlayerPrefs.GetInt("VEZESBDW", 0);
        VEZESBDS = PlayerPrefs.GetInt("VEZESBDS", 0);
        VEZESBDB = PlayerPrefs.GetInt("VEZESBDB", 0);
        //------------------ Booleanas disfarçadas para verificar compra max -----------------------
        VerificarMaxBAJ = PlayerPrefs.GetInt("VerificarMaxBAJ", 0);
        VerificarMaxBAW = PlayerPrefs.GetInt("VerificarMaxBAW", 0);
        VerificarMaxBAS = PlayerPrefs.GetInt("VerificarMaxBAS", 0);
        VerificarMaxBAB = PlayerPrefs.GetInt("VerificarMaxBAB", 0);
        VerificarMaxBDJ = PlayerPrefs.GetInt("VerificarMaxBDJ", 0);
        VerificarMaxBDW = PlayerPrefs.GetInt("VerificarMaxBDW", 0);
        VerificarMaxBDS = PlayerPrefs.GetInt("VerificarMaxBDS", 0);
        VerificarMaxBDB = PlayerPrefs.GetInt("VerificarMaxBDB", 0);
        // texto dinheiro fase secreta
        DinheiroFaseSecreta = 99999.9f;
        TextoDinheiroFaseSecreta.text = DinheiroFaseSecreta.ToString();

    }
    public void BotaoReiniciar()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    private void DeletarPlayerPrfs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            MoneyMenuDisplay.text = "0";
            Debug.Log("Deletados");
        }
    }
    // ------------------------ BOTOES DO SHOPPING LOGICA -----------------------------------
    //variaveis da logica dos botões

    //Multiplicador universal
    private int Multiplicador = 2;

    public void BotaoAtaqueJ()
    {
        if (VEZESBAJ < 88)
        {
            Debug.Log("Click");
            if (Moneyy >= DAJ)
            {

                PlayerPrefs.SetFloat("AtaqueTOTALdoTime", AtaqueTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBAJ", VEZESBAJ++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBAJ * Multiplicador));
                PlayerPrefs.SetFloat("DAJ", DAJ = (VEZESBAJ * Multiplicador));
                Debug.Log(VEZESBAJ);
                //tocar som de dinheiro saindo do caixa
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DAJ)
            {
                Debug.Log("Deu errado");
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                //animação no texto do dinheiro
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Debug.Log("Maximo Atingido");
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBAJ", 1);
            DisplayDAJ.text = ("MAX");
        }
    }
    public void BotaoAtaqueW()
    {
        if (VEZESBAW < 88)
        {
            if (Moneyy >= DAW)
            {
                PlayerPrefs.SetFloat("AtaqueTOTALdoTime", AtaqueTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBAW", VEZESBAW++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBAW * Multiplicador));
                PlayerPrefs.SetFloat("DAW", DAW = (VEZESBAW * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DAW)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar botao max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBAW", 1);
            DisplayDAW.text = ("MAX");
        }
    }
    public void BotaoAtaqueS()
    {
        if (VEZESBAS < 88)
        {
            if (Moneyy >= DAS)
            {
                PlayerPrefs.SetFloat("AtaqueTOTALdoTime", AtaqueTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBAS", VEZESBAS++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBAS * Multiplicador));
                PlayerPrefs.SetFloat("DAS", DAS = (VEZESBAS * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DAS)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBAS", 1);
            DisplayDAS.text = ("MAX");
        }
    }
    public void BotaoAtaqueB()
    {
        if (VEZESBAB < 88)
        {
            if (Moneyy >= DAB)
            {
                PlayerPrefs.SetFloat("AtaqueTOTALdoTime", AtaqueTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBAB", VEZESBAB++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBAB * Multiplicador));
                PlayerPrefs.SetFloat("DAB", DAB = (VEZESBAB * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DAB)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBAB", 1);
            DisplayDAB.text = ("MAX");
        }
    }
    public void BotaoDefesaJ()
    {
        if (VEZESBDJ < 88)
        {
            if (Moneyy >= DDJ)
            {
                PlayerPrefs.SetFloat("DefesaTOTALdoTime", DefesaTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBDJ", VEZESBDJ++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBDJ * Multiplicador));
                PlayerPrefs.SetFloat("DDJ", DDJ = (VEZESBDJ * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DDJ)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBDJ", 1);
            DisplayDDJ.text = ("MAX");
        }
    }
    public void BotaoDefesaW()
    {
        if (VEZESBDW < 88)
        {
            if (Moneyy >= DDW)
            {
                PlayerPrefs.SetFloat("DefesaTOTALdoTime", DefesaTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBDW", VEZESBDW++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBDW * Multiplicador));
                PlayerPrefs.SetFloat("DDW", DDW = (VEZESBDW * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DDW)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBDW", 1);
            DisplayDDW.text = ("MAX");
        }
    }
    public void BotaoDefesaS()
    {
        if (VEZESBDS < 88)
        {
            if (Moneyy >= DDS)
            {
                PlayerPrefs.SetFloat("DefesaTOTALdoTime", DefesaTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBDS", VEZESBDS++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBDS * Multiplicador));
                PlayerPrefs.SetFloat("DDS", DDS = (VEZESBDS * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DDS)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBDS", 1);
            DisplayDDS.text = ("MAX");
        }
    }
    public void BotaoDefesaB()
    {
        if (VEZESBDB < 88)
        {
            if (Moneyy >= DDB)
            {
                PlayerPrefs.SetFloat("DefesaTOTALdoTime", DefesaTOTALdoTime++);
                PlayerPrefs.SetInt("VEZESBDB", VEZESBDB++);
                PlayerPrefs.SetFloat("Moneyy", Moneyy -= (VEZESBDB * Multiplicador));
                PlayerPrefs.SetFloat("DDB", DDB = (VEZESBDB * Multiplicador));
                Instantiate(AudioDinheiro, transform.position, Quaternion.identity);
            }
            else if (Moneyy < DDB)
            {
                //tocar sonzinho de erro
                Instantiate(AudioError, transform.position, Quaternion.identity);
                MenuAnimator.SetTrigger("SDin");
            }
        }
        else
        {
            //colocar o botao de max atingido
            Instantiate(AudioError, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("VerificarMaxBDB", 1);
            DisplayDDB.text = ("MAX");
        }
    }
    // LOGICA DO FASE SECRETAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
    public Button BotaoComprarFS;
    private float tempoFS = 3.2f;
    private bool ConseguiComprarFS = false;
    public void BotaoComprarFaseSecreta()
    {
        if(Moneyy >= DinheiroFaseSecreta)
        {
            // mudar para poder entrar na fase
            Instantiate(AudioComprouFS, transform.position, Quaternion.identity);
            Instantiate(ParticulaComprouFS, ParticulaComprouFS.transform.position, Quaternion.identity);
            Instantiate(ParticulaComprouFS2, ParticulaComprouFS2.transform.position, Quaternion.identity);
            PlayerPrefs.SetFloat("Moneyy", Moneyy -= DinheiroFaseSecreta);
            ConseguiComprarFS = true;
        }
        else
        {
            MenuAnimator.SetTrigger("SemDinFS");
            Instantiate(AudioError, transform.position, Quaternion.identity);
           
        }
    }
    private int a;
    private int b;
    private int c;
    private int d;
    private int e;
    private int f;
    private int g;
    private int h;
    private void Update()
    {

        a = PlayerPrefs.GetInt("VerificarMaxBAJ");
        b = PlayerPrefs.GetInt("VerificarMaxBAW");
        c = PlayerPrefs.GetInt("VerificarMaxBAS");
        d = PlayerPrefs.GetInt("VerificarMaxBAB");
        e = PlayerPrefs.GetInt("VerificarMaxBDJ");
        f = PlayerPrefs.GetInt("VerificarMaxBDW");
        g = PlayerPrefs.GetInt("VerificarMaxBDS");
        h = PlayerPrefs.GetInt("VerificarMaxBDB");
        //--------------------------------------
        //DeletarPlayerPrfs();
        MoneyMenuDisplay.text = Moneyy.ToString();
        //---------------------------------------
        if (a == 0)
        {
            DisplayDAJ.text = DAJ.ToString();
        }
        if (b == 0)
        {
            DisplayDAW.text = DAW.ToString();
        }
        if (c == 0)
        {
            DisplayDAS.text = DAS.ToString();
        }
        if (d == 0)
        {
            DisplayDAB.text = DAB.ToString();
        }
        if (e == 0)
        {
            DisplayDDJ.text = DDJ.ToString();
        }
        if (f == 0)
        {
            DisplayDDW.text = DDW.ToString();
        }
        if (g == 0)
        {
            DisplayDDS.text = DDS.ToString();
        }
        if (h == 0)
        {
            DisplayDDB.text = DDB.ToString();
        }
        //------------------------------------------------------------------
        if(VEZESBAJ == 88)
        {
            DisplayDAJ.text = ("MAX");
        }
        if (VEZESBAW == 88)
        {
            DisplayDAW.text = ("MAX");
        }
        if (VEZESBAS == 88)
        {
            DisplayDAS.text = ("MAX");
        }
        if (VEZESBAB == 88)
        {
            DisplayDAB.text = ("MAX");
        }
        if (VEZESBDJ == 88)
        {
            DisplayDDJ.text = ("MAX");
        }
        if (VEZESBDW == 88)
        {
            DisplayDDW.text = ("MAX");
        }
        if (VEZESBDS == 88)
        {
            DisplayDDS.text = ("MAX");
        }
        if (VEZESBDB == 88)
        {
            DisplayDDB.text = ("MAX");
        }
        //-------------------------------- mudar os valores do preenchimento dos slider ----------------------
        sliderJA.value = PlayerPrefs.GetInt("VEZESBAJ");
        sliderWA.value = PlayerPrefs.GetInt("VEZESBAW");
        sliderSA.value = PlayerPrefs.GetInt("VEZESBAS");
        sliderBA.value = PlayerPrefs.GetInt("VEZESBAB");
        sliderJD.value = PlayerPrefs.GetInt("VEZESBDJ");
        sliderWD.value = PlayerPrefs.GetInt("VEZESBDW");
        sliderSD.value = PlayerPrefs.GetInt("VEZESBDS");
        sliderBD.value = PlayerPrefs.GetInt("VEZESBDB");
        // botao reset
        if(levelQueEsta == 6 || levelQueEsta == 7)
        {
            BotaoReiniciarStatus.interactable= true;
        }
        else
        {
            BotaoReiniciarStatus.interactable = false;
        }
        // logica para entrar na fase secreta
        if (ConseguiComprarFS)
        {
            if (tempoFS < 0)
            {
                SceneManager.LoadScene("FaseSecreta");
            }
            else
            {
                tempoFS -= Time.deltaTime;
            }
        }
    }
}
