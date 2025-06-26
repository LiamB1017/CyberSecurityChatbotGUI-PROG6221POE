// ✅ ChatbotEngine.cs
// This contains the reusable chatbot logic from Part 1 & Part 2
using System;
using System.Collections.Generic;

public class ChatbotEngine
{
    public List<string> SentimentKeywords = new List<string> { "happy", "sad", "angry", "worried" };
    public List<string> CyberKeywords = new List<string> { "password", "privacy", "phishing", "scam", "2fa", "reminder", "task" };

    public string GetResponse(string userInput, List<string> activityLog)
    {
        string input = userInput.ToLower();
        string response = "I'm not sure how to help with that. Can you rephrase?";

        // NLP Simulation - detect intent
        if (input.Contains("add") && input.Contains("task"))
        {
            response = "Sure! Opening Task Assistant window...";
            activityLog.Add("User requested to add a task.");
        }
        else if (input.Contains("quiz") || input.Contains("game"))
        {
            response = "Let's test your knowledge! Opening Quiz window...";
            activityLog.Add("User started the cybersecurity quiz.");
        }
        else if (input.Contains("activity") || input.Contains("what have you done"))
        {
            response = "Opening your activity log...";
            activityLog.Add("User viewed the activity log.");
        }
        else if (CyberKeywords.Exists(k => input.Contains(k)))
        {
            response = "Good topic! Always protect your passwords, avoid phishing scams, and enable 2FA.";
            activityLog.Add($"Chatbot responded to keyword: {input}");
        }
        else if (SentimentKeywords.Exists(k => input.Contains(k)))
        {
            response = "I sense how you feel. Remember, staying informed helps you stay safe online.";
            activityLog.Add("Chatbot responded to a sentiment input.");
        }

        return response;
    }
}
