using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public TMP_Text textField;
    public Button AIButton;
    private OpenAIAPI api;
    private List<ChatMessage> messages;
    public string BattleInfo;

    // Start is called before the first frame update
    void Start()
    {
        api = new OpenAIAPI("...");
        StartConversation();
    }

    private void StartConversation()
    {
        messages = new List<ChatMessage> {
            new ChatMessage(ChatMessageRole.System, BattleInfo)
        };

    }

    public async void attackFlavor(string FlavorText)
    {
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
        textField.text = string.Format("{0}", responseMessage.Content);
        AIButton.gameObject.SetActive(true);
        AIButton.onClick.AddListener(() => textField.text = "..");
    }
}
