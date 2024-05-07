using System;
using System.Net.Http.Headers;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace ChatGPTChatBot.Data
{
    public class Assistant
    {
        public string bearer { get; set; }

        public async Task<Message> EnviaMensagem(Message mensagem)
        {
            OpenAIAPI api = new(bearer);

            var chat = api.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;
            chat.RequestParameters.Temperature = 0;

            //Passa instruções ao bot
            chat.AppendSystemMessage("Você responde tudo como se fosse o chewbacca");

            // give a few examples as user and assistant
            chat.AppendUserInput("Estou tossindo muito e morrendo de frio.");
            chat.AppendExampleChatbotOutput("É possível... Têm sentido mais algum sintoma? Precisamos investigar mais.");

            chat.AppendUserInput(mensagem.Content);
            var response = await chat.GetResponseFromChatbotAsync();

            Message resposta = new();
            {
                resposta.Id             = chat.MostRecentApiResult.Id;
                resposta.Created        = chat.MostRecentApiResult.Created.Value;
                resposta.Content        = response;
                resposta.Total_tokens   = chat.MostRecentApiResult.Usage.TotalTokens;
                resposta.Autor          = null;
            }

            return resposta;            
        }

        public async Task<Message> StreamMensagens(Message mensagem)
        {
            OpenAIAPI api = new(bearer);
            var chat = api.Chat.CreateConversation();
            chat.AppendUserInput(mensagem.Content);

            await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
            { 
                Console.WriteLine(res);
            }

            return null;
        }

        public async void FineTuning()
        {
            OpenAIAPI api = new(bearer);

            api.Files.GetFilesAsync();
        }
    }
}
