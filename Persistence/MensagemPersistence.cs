using ChatGPTChatBot.Data;
using ChatGPTChatBot.Persistence.Interface;
using System.Data.SqlClient;

namespace ChatGPTChatBot.Persistence
{
    public class MensagemPersistence : IMensagemPersistence
    {
        private readonly IConfiguration _configuration;
        protected SqlConnection Con { get; set; }
        protected SqlCommand Cmd { get; set; }
        protected SqlDataReader Dr { get; set; }
        protected SqlTransaction Tr { get; set; }

        public MensagemPersistence(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        private void AbrirConexao()
        {
            if (Con == null)
            {
                Con = new SqlConnection(this._configuration.GetConnectionString("Conexao"));
                Con.Open();
            }
            else
            {
                Con.Open();
            }
        }

        private void FecharConexao()
        {
            if (Con != null)
            {
                Con.Close();
                Con = null;
            }
        }

        public void InserirMensagem(Message mensagem)
        {
            try
            {
                AbrirConexao();

                string query = "INSERT INTO [dbo].[TB_MENSAGENS]" + "(Id, CONTENT, TOTAL_TOKENS, CREATED)" +
                    "VALUES" + "(@ID, @CONTENT, @TOTAL_TOKENS, @CREATED)";

                this.Cmd = new SqlCommand(query, Con);

                this.Cmd.Parameters.AddWithValue("@ID", mensagem.Id);
                this.Cmd.Parameters.AddWithValue("@CONTENT", mensagem.Content);
                this.Cmd.Parameters.AddWithValue("@TOTAL_TOKENS", mensagem.Total_tokens);
                this.Cmd.Parameters.AddWithValue("@CREATED", mensagem.Created);
                this.Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<Message> ObterMensagens()
        {
            try
            {
                AbrirConexao();

                List<Message> lista = new List<Message>();

                string query = "SELECT * FROM TB_MENSAGENS";

                this.Cmd = new SqlCommand(query, Con);
                this.Dr = Cmd.ExecuteReader();

                while (Dr.Read())
                {
                    Message message = new Message();
                    message = new Message();
                    message.Id = (string)Dr["ID"];
                    message.Content = (string)Dr["CONTENT"];
                    message.Total_tokens = (int)Dr["TOTAL_TOKENS"];
                    message.Created = (DateTime)Dr["CREATED"];

                    lista.Add(message);
                }

                Dr.Close();
                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<Message> ObterMensagens(DateTime rangeStart, DateTime rangeEnd)
        {
            try
            {
                AbrirConexao();

                List<Message> lista = new List<Message>();

                string query = $"SELECT * FROM TB_MENSAGENS " +
                    $"WHERE CREATED BETWEEN {rangeStart} AND {rangeEnd}";


                this.Cmd = new SqlCommand(query, Con);
                this.Dr = Cmd.ExecuteReader();

                while (Dr.Read())
                {
                    Message message = new Message();
                    message = new Message();
                    message.Id = (string)Dr["ID"];
                    message.Content = (string)Dr["CONTENT"];
                    message.Total_tokens = (int)Dr["TOTAL_TOKENS"];
                    message.Created = (DateTime)Dr["CREATED"];

                    lista.Add(message);
                }

                Dr.Close();
                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
