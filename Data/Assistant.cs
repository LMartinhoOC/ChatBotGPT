using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace ChatGPTChatBot.Data
{
    public class Assistant
    {
        public string bearer { get; set; }
        private string filepath = "Data/Treinamento/Dados.json";

        public async Task<Message> EnviaMensagem(Message mensagem, List<Message> contexto)
        {
            OpenAIAPI api = new(bearer);

            var chat = api.Chat.CreateConversation();

            foreach (var message in contexto)
            {
                if (message.Autor == null)
                {
                    chat.AppendSystemMessage(message.Content);
                }
                else
                {
                    chat.AppendUserInput(message.Content);
                }
            }
            chat.Model = Model.ChatGPTTurbo; //define o modelo do ChatGPT

            //Passa instruções ao bot
            chat.RequestParameters.Temperature = 0;

            chat.AppendUserInput(mensagem.Content);
            var response = await chat.GetResponseFromChatbotAsync();

            Message resposta = new();
            {
                resposta.Id = chat.MostRecentApiResult.Id;
                resposta.Created = chat.MostRecentApiResult.Created.Value;
                resposta.Content = response;
                resposta.Total_tokens = chat.MostRecentApiResult.Usage.TotalTokens;
                resposta.Autor = null;
            }
            return resposta;
        }

        public async Task<Message> EnviaMensagemAzure(Message mensagem, List<Message> contexto)
        {

        }
    }
}
