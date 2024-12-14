using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class TurnManagerThree : MonoBehaviour
{

    // Sceme
    public string SceneToLoad;

    //AI
    public GameObject objectWithScriptAI; // Assign this in the Inspector
    private AIController scriptAI;

    // Particle effects
    public GameObject Indicator;

    public GameObject BloodAttack;
    public GameObject SpiritStrike;
    public GameObject HealingCircle;
    public GameObject ElectroSlash;
    public GameObject StarBuff;
    public GameObject SoulEruption;
    public GameObject VenomBolt;
    public GameObject DrainingBolt;
    public GameObject DisarmingAura;
    public GameObject DrainingAura;
    public GameObject IceStrike;
    public GameObject RoarOfPower;

    // UI
    public Button AIButton;
    public GameObject AIPanel;
    public TextMeshProUGUI AIText;


    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    public TextMeshProUGUI attack_description_1;
    public TextMeshProUGUI attack_description_2;
    public TextMeshProUGUI attack_description_3;
    public TextMeshProUGUI attack_description_4;

    public TextMeshProUGUI wizard_armor_text;
    public TextMeshProUGUI rogue_armor_text;
    public TextMeshProUGUI fighter_armor_text;
    public TextMeshProUGUI enemy_armor_text;

    public Image EnemyHealthBar;
    public Image EnemyStaminaBar;
    public Image WizardHealthBar;
    public Image WizardStaminaBar;
    public Image RogueHealthBar;
    public Image RogueStaminaBar;
    public Image FighterHealthBar;
    public Image FighterStaminaBar;

    public GameObject StatsCanvas;


    // Attack Descriptions
    public string WizardAttack1;
    public string WizardAttack2;
    public string WizardAttack3;
    public string WizardAttack4;

    public string RogueAttack1;
    public string RogueAttack2;
    public string RogueAttack3;
    public string RogueAttack4;

    public string FighterAttack1;
    public string FighterAttack2;
    public string FighterAttack3;
    public string FighterAttack4;


    // Stats For Characters
    public float WizardHealth = 30;
    public float WizardMaxHealth = 30;
    public float WizardStamina = 20;
    public float WizardMaxStamina = 20;
    public float WizardArmor = 1;

    public float RogueHealth = 30;
    public float RogueMaxHealth = 30;
    public float RogueStamina = 20;
    public float RogueMaxStamina = 20;
    public float RogueArmor = 1;

    public float FighterHealth = 30;
    public float FighterMaxHealth = 30;
    public float FighterStamina = 20;
    public float FighterMaxStamina = 20;
    public float FighterArmor = 2;


    public float EnemyHealth = 70;
    public float EnemyMaxHealth = 70;
    public float EnemyStamina = 25;
    public float EnemyMaxStamina = 25;
    public float EnemyArmor = 2;

    // Other Combat Variables
    public float Buff = 0;
    public float Poison = 0;
    public float HealthStolen = 0;
    private readonly System.Random random = new System.Random();


    // Animator
    public Animator WizardAnimator; 
    public Animator RogueAnimator;
    public Animator FighterAnimator;
    public Animator EnemyAnimator;

    void Start()
    {
        scriptAI = objectWithScriptAI.GetComponent<AIController>();
        StartPlayerTurn();

    }

    IEnumerator WaitSomeTime()
    { 
        yield return new WaitForSeconds(5f);
   
    }

    void StartPlayerTurn()
    {
        Debug.Log("StartPlayerTurn");
        WizardChooseAttack();
    }



    public void WizardChooseAttack()
    {
        
        StatsCanvas.SetActive(true);
        Indicator.gameObject.SetActive(true);
        Indicator.gameObject.transform.position = new Vector3(-3.0f, 0.1f, -1.3f);
        AIPanel.gameObject.SetActive(false);
        UpdateStatsUI();
        if (WizardHealth <= 0)
        {
            WizardHealth = -100;
            RogueChooseAttack();
        }
        else
        {

            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);
            Button3.gameObject.SetActive(true);
            Button4.gameObject.SetActive(true);

            if (WizardStamina < 4)
            {
                Button1.gameObject.SetActive(false);
            }

            if (WizardStamina < 6)
            {
                Button2.gameObject.SetActive(false);
            }

            if (WizardStamina < 4)
            {
                Button4.gameObject.SetActive(false);
            }


            Button1.GetComponentInChildren<TextMeshProUGUI>().text = "Spirit Strike (4)";
            Button2.GetComponentInChildren<TextMeshProUGUI>().text = "Electro Wave (6)";
            Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Cosmic Gift (0)";
            Button4.GetComponentInChildren<TextMeshProUGUI>().text = "Soul Eruption (4)";

            attack_description_1.text = WizardAttack1;
            attack_description_2.text = WizardAttack2;
            attack_description_3.text = WizardAttack3;
            attack_description_4.text = WizardAttack4;

            Button1.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button1.onClick.AddListener(() => StartCoroutine(WizardAttackOne())); // Start the coroutine directly
            Button2.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button2.onClick.AddListener(() => StartCoroutine(WizardAttackTwo())); // Start the coroutine directly
            Button3.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button3.onClick.AddListener(() => StartCoroutine(WizardAttackThree())); // Start the coroutine directly
            Button4.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button4.onClick.AddListener(() => StartCoroutine(WizardAttackFour())); // Start the coroutine directly
            AIButton.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            AIButton.onClick.AddListener(() => RogueChooseAttack()); // Start the coroutine directly
        }

    }

    public IEnumerator WizardAttackOne()

    {
        StatsCanvas.SetActive(false);
        AIPanel.gameObject.SetActive(true);
        scriptAI.attackFlavor("Describe the wizard hero attack with Spirit Strike: ");

        Buff = 0;
        WizardStamina -= 4;
        EnemyHealth -= 6 - EnemyArmor;
        RogueHealth += 2;

        if (RogueHealth > RogueMaxHealth) {
            RogueMaxHealth = RogueHealth;
        }

        FighterHealth += 2;
        if (FighterHealth > FighterMaxHealth)
        {
            FighterMaxHealth = FighterHealth;
        }



        WizardAnimator.Play("WizardAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(SpiritStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        GameObject effectInstance1 = Instantiate(HealingCircle, new Vector3((float)-2.0, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance1, effectInstance1.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time

        
    }

    public IEnumerator WizardAttackTwo()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Wizard hero attack Azazel with Electro Wave: ");
        WizardStamina -= 6;
        Buff = 2;
        EnemyHealth -= 7 -EnemyArmor;

        WizardAnimator.Play("WizardAttackTwo");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(ElectroSlash, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator WizardAttackThree()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Wizard Hero casting Cosmic Gift: ");
        WizardHealth += 4;
        RogueHealth += 4;
        FighterHealth += 4;
        WizardStamina += 7;

        WizardAnimator.Play("WizardAttackThree");
        yield return new WaitForSeconds(3f);
        GameObject effectInstance0 = Instantiate(StarBuff, new Vector3((float)-1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator WizardAttackFour()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Wizard hero cast Soul Eurption, dealing damage against Azazel and giving stamina to the Rogue and the Fighter: ");
        WizardStamina -= 4;
        EnemyHealth -= 5 - EnemyArmor;
        RogueStamina += 3;
        FighterStamina += 3;

        WizardAnimator.Play("WizardAttackOne");
        yield return new WaitForSeconds(2f);
        GameObject effectInstance0 = Instantiate(SoulEruption, new Vector3((float)1.0, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }







    public void RogueChooseAttack()
    {
        Indicator.gameObject.transform.position = new Vector3(-1.5f, 0.1f, -2.5f);
        StatsCanvas.SetActive(true);
        AIPanel.gameObject.SetActive(false);
        UpdateStatsUI();

        if (RogueHealth <= 0)
        {
            RogueHealth = -100;
            FighterChooseAttack();
        }

        else
        {



            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);
            Button3.gameObject.SetActive(true);
            Button4.gameObject.SetActive(true);

            if (RogueStamina < 5)
            {
                Button1.gameObject.SetActive(false);
            }

            if (RogueStamina < 8)
            {
                Button2.gameObject.SetActive(false);
            }

            if (RogueStamina < 4)
            {
                Button4.gameObject.SetActive(false);
            }



            Button1.GetComponentInChildren<TextMeshProUGUI>().text = "Venom Bolt (5)";
            Button2.GetComponentInChildren<TextMeshProUGUI>().text = "Draining Bolt (8)";
            Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Disarming Aura (0)";
            Button4.GetComponentInChildren<TextMeshProUGUI>().text = "Draining Aura (4)";

            attack_description_1.text = RogueAttack1;
            attack_description_2.text = RogueAttack2;
            attack_description_3.text = RogueAttack3;
            attack_description_4.text = RogueAttack4;

            Button1.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button1.onClick.AddListener(() => StartCoroutine(RogueAttackOne())); // Start the coroutine directly
            Button2.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button2.onClick.AddListener(() => StartCoroutine(RogueAttackTwo())); // Start the coroutine directly
            Button3.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button3.onClick.AddListener(() => StartCoroutine(RogueAttackThree())); // Start the coroutine directly
            Button4.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button4.onClick.AddListener(() => StartCoroutine(RogueAttackFour())); // Start the coroutine directly
            AIButton.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            AIButton.onClick.AddListener(() => FighterChooseAttack()); // Start the coroutine directly
        }
    }

    public IEnumerator RogueAttackOne()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe he Rogue hero attacks with Venom Bolt: ");
        RogueStamina -= 5;
        EnemyHealth -= 6 + Buff - EnemyArmor;
        Poison = 2;
        RogueAnimator.Play("RogueAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(DrainingBolt, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time

    }

    public IEnumerator RogueAttackTwo()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Rogue Hero attack with Draining Bolt: ");
        RogueStamina -= 8;
        RogueStamina += 4;
        EnemyStamina -= 4;
        EnemyHealth -= 7 + Buff - EnemyArmor;
        RogueAnimator.Play("RogueAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(VenomBolt, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator RogueAttackThree()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Rogue hero attack with Disarming Aura, giving the Rogue Stamina and removing Azazel's armor: ");
        RogueStamina += 6;
        EnemyArmor = 0;
        RogueAnimator.Play("RogueAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(DisarmingAura, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator RogueAttackFour()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Rogue hero attacks with Draining Aura: ");
        RogueStamina -= 4;
        HealthStolen = 4 + Buff - EnemyArmor;
        RogueHealth += HealthStolen;
        EnemyHealth -= HealthStolen;
        HealthStolen = 0;
        RogueAnimator.Play("RogueAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(DrainingAura, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }





    public void FighterChooseAttack()
    {
        AIPanel.gameObject.SetActive(false);
        Indicator.gameObject.transform.position = new Vector3(-1.0f, 0.1f, -1.0f);
        StatsCanvas.SetActive(true);
        UpdateStatsUI();


        if (FighterHealth <= 0)
        {
            FighterHealth = -100;
            WizardChooseAttack();
        }
        else
        {


            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(true);
            Button3.gameObject.SetActive(true);
            Button4.gameObject.SetActive(true);

            if (FighterStamina < 6)
            {
                Button1.gameObject.SetActive(false);
            }

            if (FighterStamina < 7)
            {
                Button3.gameObject.SetActive(false);
            }

            Button1.GetComponentInChildren<TextMeshProUGUI>().text = "Ice Pierce (6)";
            Button2.GetComponentInChildren<TextMeshProUGUI>().text = "Roar of Power (0)";
            Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Death Strike (7)";
            Button4.GetComponentInChildren<TextMeshProUGUI>().text = "Spirit Swing (0)";

            attack_description_1.text = FighterAttack1;
            attack_description_2.text = FighterAttack2;
            attack_description_3.text = FighterAttack3;
            attack_description_4.text = FighterAttack4;


            Button1.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button1.onClick.AddListener(() => StartCoroutine(FighterAttackOne())); // Start the coroutine directly
            Button2.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button2.onClick.AddListener(() => StartCoroutine(FighterAttackTwo())); // Start the coroutine directly
            Button3.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button3.onClick.AddListener(() => StartCoroutine(FighterAttackThree())); // Start the coroutine directly
            Button4.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            Button4.onClick.AddListener(() => StartCoroutine(FighterAttackFour())); // Start the coroutine directly
            AIButton.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
            AIButton.onClick.AddListener(() => EnemyChooseAttack()); // Start the coroutine directly
        }
    }

    public IEnumerator FighterAttackOne()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Fighter hero attack with Ice Pierce: ");
        FighterStamina -= 6;
        FighterArmor += 1;
        EnemyHealth -= 7 + Buff - EnemyArmor;
        FighterAnimator.Play("FighterAttackOne");
        yield return new WaitForSeconds(1f);
        EnemyAnimator.Play("Hit");
        GameObject effectInstance0 = Instantiate(IceStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator FighterAttackTwo()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe The Fighter hero use Roar of Power to regain stamina: ");
        FighterHealth -= 1;
        FighterStamina += 10;
        FighterAnimator.Play("Battlecry");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(IceStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
        
    }

    public IEnumerator FighterAttackThree()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Fighter hero attack with Death Strike: ");
        FighterStamina -= 7;
        FighterHealth -= 2;
        EnemyHealth -= 10 + Buff - EnemyArmor;
        FighterAnimator.Play("FighterAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(IceStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public IEnumerator FighterAttackFour()
    {
        AIPanel.gameObject.SetActive(true);
        StatsCanvas.SetActive(false);
        scriptAI.attackFlavor("Describe the Fighter hero attack with Spirit Swing: ");
        FighterStamina += 4;
        EnemyHealth -= 3 + Buff - EnemyArmor;
        FighterAnimator.Play("FighterAttackOne");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance0 = Instantiate(IceStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
        EnemyAnimator.Play("Hit");
        UpdateStatsUI();
        yield return new WaitForSeconds(2f); // Wait for animation and additional time
    }

    public void EnemyChooseAttack()
    {
        AIButton.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
        AIButton.onClick.AddListener(() => StartPlayerTurn()); // Start the coroutine directly
        if (Poison > 0.0)
        {
            EnemyHealth -= 2;
            Poison -= 1;
            GameObject effectInstance0 = Instantiate(SpiritStrike, new Vector3((float)1.5, (float)0.0, (float)-1.0), Quaternion.identity);
            Destroy(effectInstance0, effectInstance0.GetComponent<ParticleSystem>().main.duration);
            UpdateStatsUI();

        }
        
        StatsCanvas.SetActive(false);
        Indicator.gameObject.SetActive(false);

        int randomNumber = random.Next(1, 4);
        if (EnemyStamina < 0)
        {
            randomNumber = 5;
        }

        if (randomNumber == 1) {
            StartCoroutine(EnemyAttackOne());

        }
        if (randomNumber == 2)
        {
            if (WizardHealth > 0) { StartCoroutine(EnemyAttackTwoOnWizard()); }
            else if (RogueHealth > 0) { StartCoroutine(EnemyAttackTwoOnRogue()); }
            else if (FighterHealth > 0) { StartCoroutine(EnemyAttackTwoOnFighter()); }

        }
        if (randomNumber == 3)
        {
            if (RogueHealth > 0) { StartCoroutine(EnemyAttackTwoOnRogue()); }
            else if (WizardHealth > 0) { StartCoroutine(EnemyAttackTwoOnWizard()); }
            else if (FighterHealth > 0) { StartCoroutine(EnemyAttackTwoOnFighter()); }

        }
        if (randomNumber == 4)
        {
            if (FighterHealth > 0) { StartCoroutine(EnemyAttackTwoOnFighter()); }
            else if (RogueHealth > 0) { StartCoroutine(EnemyAttackTwoOnRogue()); }
            else if (WizardHealth > 0) { StartCoroutine(EnemyAttackTwoOnWizard()); }

        }
        if (randomNumber == 5)
        {
            StartCoroutine(EnemyRecover());

        }




    }

    public IEnumerator EnemyDie()
    {
        scriptAI.attackFlavor("The heros killed the demon Azazel!");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneToLoad);
    }

    public IEnumerator EnemyAttackOne()

    {
        scriptAI.attackFlavor("Describe Azazel as he rains down a laser upon the heros, hurting them and lowering their defenses!");
        WizardHealth -= 11 - WizardArmor;
        RogueHealth -= 11 - RogueArmor;
        FighterHealth -= 11 - FighterArmor;

        WizardArmor -= 1;
        RogueArmor -= 1;
        FighterArmor -= 1;
        EnemyStamina -= 8;
        EnemyAnimator.Play("Spell");
        GameObject effectInstance = Instantiate(BloodAttack, new Vector3((float)-1.5, (float)0.0, (float)1.0), Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        yield return new WaitForSeconds(1f);
        if (WizardHealth > 0) { WizardAnimator.Play("GetHit"); }
        if (RogueHealth > 0) { RogueAnimator.Play("GetHit"); }
        if (FighterHealth > 0) { FighterAnimator.Play("GetHit"); }
        UpdateStatsUI();
        yield return new WaitForSeconds(1f);
        if (WizardHealth > 0) { WizardAnimator.Play("GetHit"); }
        if (RogueHealth > 0) { RogueAnimator.Play("GetHit"); }
        if (FighterHealth > 0) { FighterAnimator.Play("GetHit"); }
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
    }


    public IEnumerator EnemyAttackTwoOnFighter()

    {
        scriptAI.attackFlavor("Make a 1 sentence Taunt against the heros as Azazel, the fallen angel from Biblical lore. Try to be scary: ");

        FighterHealth -= 14 + FighterArmor;
        EnemyStamina -= 8;
        EnemyHealth += 2;





        EnemyAnimator.Play("Spell");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance = Instantiate(SpiritStrike, new Vector3((float)-1.0, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        FighterAnimator.Play("GetHit");
        UpdateStatsUI();
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator EnemyAttackTwoOnWizard()

    {
        scriptAI.attackFlavor("Make a 1 sentence Taunt against the heros as Azazel, the fallen angel from Biblical lore. Try to be scary: ");
        WizardHealth -= 14 - WizardArmor;
        EnemyHealth += 2;

        EnemyStamina -= 8;
        EnemyAnimator.Play("Attack_Right");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance = Instantiate(SpiritStrike, new Vector3((float)-1.0, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        WizardAnimator.Play("GetHit");
        UpdateStatsUI();
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator EnemyAttackTwoOnRogue()

    {
        scriptAI.attackFlavor("Make a 1 sentence Taunt against the heros as Azazel, the fallen angel from Biblical lore. Try to be scary: ");
        RogueHealth -= 14 - RogueArmor;
        EnemyHealth += 2;

        EnemyStamina -= 8;
        EnemyAnimator.Play("Attack_Right");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance = Instantiate(SpiritStrike, new Vector3((float)-1.0, (float)0.0, (float)-1.0), Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        RogueAnimator.Play("GetHit");
        UpdateStatsUI();
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator EnemyRecover()

    {
        scriptAI.attackFlavor("Make a 1 sentence Taunt against the heros as Azazel, the fallen angel from Biblical lore. Try to be scary: ");
        EnemyStamina += 16;
        EnemyHealth += 4;
        EnemyArmor += 2;
        EnemyAnimator.Play("Spell");
        yield return new WaitForSeconds(1f);
        GameObject effectInstance = Instantiate(HealingCircle, new Vector3((float)2.0, (float)0.0, (float)-1.5), Quaternion.identity);
        Destroy(effectInstance, effectInstance.GetComponent<ParticleSystem>().main.duration);
        UpdateStatsUI();
        yield return new WaitForSeconds(1f);
    }









    public void UpdateStatsUI()
    {
        if (WizardHealth > WizardMaxHealth)
        {
            WizardHealth = WizardMaxHealth;
        }

        if (RogueHealth > RogueMaxHealth)
        {
            RogueHealth = RogueMaxHealth;
        }

        if (FighterHealth > FighterMaxHealth)
        {
            FighterHealth = FighterMaxHealth;
        }

        if (EnemyHealth > EnemyMaxHealth)
        {
            EnemyHealth = EnemyMaxHealth;
        }

        if (WizardStamina > WizardMaxStamina)
        {
            WizardStamina = WizardMaxStamina;
        }

        if (RogueStamina > RogueMaxStamina)
        {
            RogueStamina = RogueMaxStamina;
        }

        if (FighterStamina > FighterMaxStamina)
        {
            FighterStamina = FighterMaxStamina;
        }

        if (EnemyStamina > EnemyMaxStamina)
        {
            EnemyStamina = EnemyMaxStamina;
        }

        if (WizardHealth <= 0)
        {
            WizardAnimator.Play("Die");
        }

        if (RogueHealth <= 0)
        {
            RogueAnimator.Play("Die");
        }


        if (FighterHealth <= 0)
        {
            FighterAnimator.Play("Die");
        }

        if (EnemyHealth <= 0)
        {
            EnemyAnimator.Play("Death");
            StartCoroutine(EnemyDie());
        }




        WizardHealthBar.fillAmount = WizardHealth / WizardMaxHealth;
        RogueHealthBar.fillAmount = RogueHealth / RogueMaxHealth;
        FighterHealthBar.fillAmount = FighterHealth / FighterMaxHealth;
        EnemyHealthBar.fillAmount = EnemyHealth / EnemyMaxHealth;

        WizardStaminaBar.fillAmount = WizardStamina / WizardMaxStamina;
        RogueStaminaBar.fillAmount = RogueStamina / RogueMaxStamina;
        FighterStaminaBar.fillAmount = FighterStamina / FighterMaxStamina;
        EnemyStaminaBar.fillAmount = EnemyStamina / EnemyMaxStamina;

        wizard_armor_text.text = WizardArmor.ToString();
        rogue_armor_text.text = RogueArmor.ToString();
        fighter_armor_text.text = FighterArmor.ToString();
        enemy_armor_text.text = EnemyArmor.ToString();

    }
    

}