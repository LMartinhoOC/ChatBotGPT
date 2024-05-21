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
        private string filepath = "../Treinamento/teste.json";

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
            chat.AppendSystemMessage("Você é um assistente médico que trabalha em um consultório médico. Sua missão é questionar o usuário e gerar um relatório breve sobre a condição dele. Como uma versão breve de um diagnóstico. Não diga ao usuário para procurar um médico, pois ele já está em um consultório.");
            chat.AppendUserInput("Tenho tido tosse e espirros.");
            chat.AppendExampleChatbotOutput("Parece ser um resfriado, talvez, no máximo uma gripe.");

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
    }
}
