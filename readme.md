# Apresentação

Esse programa é um desenvolvimento feito com relação ao TCC de Ciências da Computação, Turma EAD241222 do período 2024.1, sob a orientação do Professor Sergio Assunção Monteiro.

O intuito do Chat é ter um atendente com IA para responder apenas perguntas relacionadas à saúde, como em um consultório médico, onde o usuário pode consultar um atendente e este pode ajuda-lo a identificar seus sintomas.

Ao mesmo tempo, o Bot é instruído a ler a conversa, tentar constatar a causa, e retornar um diagnóstico prévio com uma possível causa. Esse retorno seria passado à um profissional de saúde mas, nesse caso, ele é exibido na tela, para demonstração.

## Como utilizar

O programa utiliza a API da OpenAI, por onde envia e recebe as mensagens passadas pelo usuário. Por isso, é necessário utilizar uma chave da API para poder realizar requisições ao bot.

Preencha a Chave no campo do lado esquerdo da página e então, ao clicar no botão de envio, a chave será validada.

Uma vez que a chave for validada, o texto inserido no campo de envio de mensagem do chat passa uma requisição POST e pede uma resposta. O bot é treinado para responder apenas perguntas relacionadas à saúde e se perguntado sobre outros assuntos, ele sempre tentará desviar o assunto de volta para a área da saúde.


## Técnico

O ChatBot desenvolvido utilizando Bootstrap e .NET 8 para a construção e validação dos formulários. Na parte do C#, utiliza a biblioteca [OpenAI-API-dotnet](https://github.com/OkGoDoIt/OpenAI-API-dotnet) criada por [OkGoDoIt](https://github.com/OkGoDoIt).
