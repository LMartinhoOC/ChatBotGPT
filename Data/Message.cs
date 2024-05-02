namespace ChatGPTChatBot.Data
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int Total_tokens { get; set; }
        public DateTime Created { get; set; }
        public string? Autor {  get; set; }

        public override string ToString()
        {
           return $"MENSAGEM RECEBIDA - {Created}\n" +
                  $"Id: {this.Id}\n" +
                  $"Mensagem: {this.Content}\n" +
                  $"Total de tokens gastos: {this.Total_tokens}\n";
        }
    }    
}
