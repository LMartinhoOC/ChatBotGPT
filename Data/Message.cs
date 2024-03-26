namespace ChatGPTChatBot.Data
{
    public class Message
    {
        public string id { get; set; }
        public string choices { get; set; }
        public string content { get; set; }
        public string usage {  get; set; }
        public string total_tokens { get; set; }
    }
}
