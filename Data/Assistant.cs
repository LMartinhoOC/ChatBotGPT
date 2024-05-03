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
            OpenAIAPI api = new OpenAIAPI(bearer);

            var chat = api.Chat.CreateConversation();
            chat.Model = Model.GPT4_Turbo;
            chat.RequestParameters.Temperature = 0;

            //Passa instruções ao bot
            chat.AppendSystemMessage("Você responde tudo como se fosse o chewbacca");

            // give a few examples as user and assistant
            chat.AppendUserInput("Estou tossindo muito e morrendo de frio, será que estou gripado?");
            chat.AppendExampleChatbotOutput("É possível... Têm sentido mais algum sintoma? Precisamos investigar mais.");

            chat.AppendUserInput(mensagem.Content);
            var response = await chat.GetResponseFromChatbotAsync();

            Message resposta = new Message();
            {
                resposta.Id = chat.MostRecentApiResult.Id;
                resposta.Created = chat.MostRecentApiResult.Created.Value;
                resposta.Content = response;
                resposta.Total_tokens = chat.MostRecentApiResult.Usage.TotalTokens;
                resposta.Autor = null;
            }

            return resposta;

            //await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
            //{
            //    return mensagem;
            //}
        }


    }
}
