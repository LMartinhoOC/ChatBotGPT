using Azure;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ChatGPTChatBot.Data
{
    public class AssistantService
    {
        public string Bearer { get; set; }

        public async Task<Message> EnviaMensagem(Message mensagem, List<Message> contexto)
        {
            OpenAIAPI api = new(Bearer);

            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("Você é um assistente médico que só sabe falar português brasileiro. Sua missão é conversar com um paciente e entender o que ele está sentindo. Ao longo da conversa com o paciente, indague sobre seus sintomas e históricos, tanto familiares quanto da ocorrência atual. Receba o paciente como tal assistente. Não diga que sente pena do paciente.");

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
            chat.RequestParameters.Temperature = 0.8;

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

        public async Task<Message> GeraDiagnostico(List<Message> contexto)
        {
            OpenAIAPI api = new(Bearer);
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("Observe o conteúdo da conversa. Identifique os sintomas informados pelo paciente e liste-os junto com o palpite de uma doença relacionada. Tente dar uma resposta curta. Só responda se o assunto for relacionado à saúde do usuário. Não recomende-o a buscar ajuda profissional.");
            chat.AppendUserInput("Estou sentindo dificuldade de respirar!");
            chat.AppendExampleChatbotOutput("O paciente reporta estar sentindo falta de ar. Uma possibilidade seria uma crise de asma ou crise alergica.");

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

            var response = await chat.GetResponseFromChatbotAsync();

            Message diagnostico = new Message();
            {
                diagnostico.Id = chat.MostRecentApiResult.Id;
                diagnostico.Created = chat.MostRecentApiResult.Created.Value;
                diagnostico.Content = response;
                diagnostico.Total_tokens = chat.MostRecentApiResult.Usage.TotalTokens;
                diagnostico.Autor = null;
            }           

            return diagnostico;
        }
    }
}
