using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
//using UnityEngine.UIElements;
//using static UnityEngine.Rendering.DebugUI;

public class NPC2 : MonoBehaviour
{
    public GameObject canvasUI1; // Reference to the Canvas UI GameObject
    public GameObject Panel1;
    public GameObject inputText1;
    public TMP_InputField inputField1; // Reference to the Input Field
    public TMP_Text textField1;      // Reference to the TextMesh PRO text display
    public Button AIButton1;

    public GameObject Player11;
    public GameObject Player21;
    public GameObject Player31;

    private OpenAIAPI api;
    private List<ChatMessage> messages;


    void Start()
    {
        api = new OpenAIAPI("...");
        StartConversation();
    }

    private void Update()
    {
        
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, "You are a wizard. Three heroes come up to talk to you. Respond in a way that is short, concise, and in the voice of a wizard. They ask: ")
        };

    }

    public async void PlayerInputOver()
    {
        string FlavorText = inputField1.text;
        inputField1.gameObject.SetActive(false);
        AIButton1.gameObject.SetActive(false);
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
            MaxTokens = 50,
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
        textField1.text = string.Format("{0}", responseMessage.Content);
    }

// Called when the collider enters another collider
void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        if (other.CompareTag("Player")) // Check if the collider belongs to the Player
        {
            canvasUI1.SetActive(true); // Activate the Canvas UI
            AIButton1.onClick.AddListener(() => PlayerInputOver());
        }
    }

    // Called when the collider exits another collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the Player
        {
            canvasUI1.SetActive(false); // Deactivate the Canvas UI
        }
    }
    void ButtonClicked1()
    {
        AIButton1.onClick.RemoveAllListeners(); // Remove previous listeners to avoid stacking
        textField1.text = "Hello!";
        inputField1.gameObject.SetActive(false);
    }
  }
