# Criando Bots com o Microsoft Bot Framework
Tutotial demonstrando a criação de Bots com o **Microsoft Bot Framework**.

O Microsoft Bot Framework foi criado para ajudar na criação de bots. Bots são robôs pré programados que podem interagir com os usuários naturalmente no mais diversos canais como Skype, Facebook e outros serviços de mensagens.

Este tutorial demonstra como você pode construir bots usando o template Bot Application e o Bot Builder SDK for .NET, e como testá-lo com o Bot Framework Emulator.

## Pré-Requisitos

1. Instale o [Visual Studio 2017](https://www.visualstudio.com/downloads/) – Pode ser o community;
2. Faça o download dos arquivos zip do **Bot Application**, **Bot Controller**, e **Bot Dialog**. Instale o projeto template copiando o arquivo **Bot Application.zip** para o diretório projects template do seu Visual Studio e copie os arquivos **Bot Controller.zip** e Bot Dialog.zip para o diretório item templates do seu Visual Studio.
3. Faça o download do [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator) - Necessário para teste do BOT;

***Dica***: Geralmente as pastas project templates e item templates do Visual Studio, ficam localizadas respectivamente nos caminhos: 
```html
%USERPROFILE%\Documents\Visual Studio XXXX\Templates\ProjectTemplates\Visual C#\
%USERPROFILE%\Documents\Visual Studio XXXX\Templates\ItemTemplates\Visual C#\
```
Sendo **XXXX** o valor corresponde a versão do Visual Studio instalada.

## Criando o seu Bot

Primeiro, abra o Visual Studio e crie um novo projeto C#. Escolha o template Bot Application para o seu novo projeto.

![Novo Projeto no VS](images/1.png)

Usando o template Bot Application, você cria o projeto já contendo todos os componentes que são requeridos para construir uma simples aplicação bot, incluindo a referência para a framework Bot Builder SDK for .NET, ```Microsoft.Bot.Builder```. Verifique se as referências do projeto possuem a última versão da SDK seguindos os passos abaixo:

1. Clique com botão direito no projeto e selecione Manage NuGet Packages.
2. Na aba Browse, digite "Microsoft.Bot.Builder".
3. Localize o package Microsoft.Bot.Builder na lista de resultados e clique no botão Update do package.
4. Clique em OK na caixa de dialogo e após aceite as mudanças para confirmar as atualização.

![Update Package](images/2.png)

## Explorando o código

Uma vez, tendo o projeto criado a seguinte estrutura de diretórios será definida para solution, conforme figura abaixo:

![Solution Project](images/3.png)

**A Classe MessageController**

Dentro da pasta **Controllers** é criada a classe MessagesController.cs. Esta classe possui o método chamado ```Post```, responsável por receber a mensagem do usuário e invocar o root dialog.

```C#
[BotAuthentication]
public class MessagesController : ApiController
{
    /// <summary>
    /// POST: api/Messages
    /// Receive a message from a user and reply to it
    /// </summary>
    public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
    {
        if (activity.Type == ActivityTypes.Message)
        {
            await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
        }
        else
        {
            HandleSystemMessage(activity);
        }
        var response = Request.CreateResponse(HttpStatusCode.OK);
        return response;
    }
    ...
}
```

The root dialog processes the message and generates a response. The MessageReceivedAsync method within Dialogs\RootDialog.cs sends a reply that echos back the user's message, prefixed with the text 'You sent' and ending in the text 'which was ## characters', where ## represents the number of characters in the user's message.


