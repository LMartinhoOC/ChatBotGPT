using ChatGPTChatBot.Data;

namespace ChatGPTChatBot.Persistence.Interface
{
    public interface IMensagemPersistence
    {
        List<Message> ObterMensagens();
        void InserirMensagem(Message mensagem);
    }
}
