using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
//using UnityEngine.UIElements;
//using static UnityEngine.Rendering.DebugUI;

public class NPC1 : MonoBehaviour
{
    public GameObject canvasUI; // Reference to the Canvas UI GameObject
    public GameObject Panel;
    public GameObject inputText;
    public TMP_InputField inputField; // Reference to the Input Field
    public TMP_Text textField;      // Reference to the TextMesh PRO text display
    public Button AIButton;

    private OpenAIAPI api;
    private List<ChatMessage> messages;
    public string PromptToUse;
    public string PromptToUse2;
    public string Lore;


    void Start()
    {
        api = new OpenAIAPI("API Key goes here");
    }



    public async void PlayerInputOver()
    {
        string FlavorText = inputField.text;
        inputField.gameObject.SetActive(false);
        AIButton.gameObject.SetActive(false);
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = FlavorText;
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        // Add the message to the list
        messages.Add(userMessage);


        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.9,
            MaxTokens = 80,
            Messages = messages
        });

        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.rawRole, responseMessage.Content));

        // Add the response to the list of messages
        messages.Add(responseMessage);

        // Update the text field with the response
        textField.text = string.Format("{0}", responseMessage.Content);
        AIButton.onClick.RemoveAllListeners();
        AIButton.onClick.AddListener(() => AskAnotherQuestion());
        AIButton.gameObject.SetActive(true);
        
    }

    void AskAnotherQuestion()
    {
        textField.text = "";
        inputField.gameObject.SetActive(true);
        AIButton.onClick.RemoveAllListeners();
        AIButton.onClick.AddListener(() => PlayerInputOver());

    }

// Called when the collider enters another collider
void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the Player
        {
            messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, PromptToUse)
        };

            textField.text = "";
            inputField.gameObject.SetActive(true);
            inputField.text = "";
            canvasUI.SetActive(true); // Activate the Canvas UI
            AIButton.onClick.AddListener(() => PlayerInputOver());
        }
    }

    // Called when the collider exits another collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the Player
        {
            canvasUI.SetActive(false); // Deactivate the Canvas UI
            inputField.text = "";
            textField.text = "";
            AIButton.onClick.RemoveAllListeners();

        }
    }
  }
