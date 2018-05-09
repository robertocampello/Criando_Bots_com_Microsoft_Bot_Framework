# Criando Bots com o Microsoft Bot Framework
Tutotial demonstrando a criação de Bots com o **Microsoft Bot Framework**.

O Microsoft Bot Framework foi criado para ajudar na criação de bots. Bots são robôs pré programados que podem interagir com os usuários naturalmente nos mais diversos canais como Skype, Facebook e outros serviços de mensagens.

Usando a SDK, você pode construir bots utilizando as seguintes SDK features:

* Sistema de diálogos que podem ser utilizados de forma isolada ou composto
* Prompts para interações simples como Yes/No, strings, numbers, e enumerations
* Diálogos que utilize AI frameworks como o [LUIS](https://www.luis.ai/home)
* FormFlow para gerar automaticamente um bot (através de uma classe C#) que direciona o usuário através da conversação, fornecendo ajuda, navegação, entendimento e confirmação

Este tutorial demonstra como você pode construir bots usando o template **Bot Application** e o **Bot Builder SDK versão 3.x**, e como testá-lo com o **Bot Framework Emulator**.

Você pode escolher usar seu ambiente de desenvolvimento favorito para criar seu bot usando **C#** o **Node.js**. A framework fornece ferramentas para as várias etapas de desenvolvimento que você pode utilizar para construir, testar e publicar o seu bot.

![Flow Dev Bot](images/8.png)

## Pré-Requisitos

1. Instalar o [Visual Studio 2017](https://www.visualstudio.com/downloads/) – Pode ser o community
2. Fazer o [download](https://marketplace.visualstudio.com/items?itemName=BotBuilder.BotBuilderV3) dos arquivos zip do **Bot Application**, **Bot Controller**, e **Bot Dialog**. Instalar o projeto template copiando o arquivo **Bot Application.zip** para o diretório projects template do seu Visual Studio e copiar os arquivos **Bot Controller.zip** e **Bot Dialog.zip** para o diretório item templates do seu Visual Studio.
3. Fazer o download do  - Necessário para teste do BOT

***Dica***: Geralmente as pastas **project templates** e **item templates** do Visual Studio, ficam localizadas respectivamente nos caminhos: 
```html
%USERPROFILE%\Documents\Visual Studio XXXX\Templates\ProjectTemplates\Visual C#\
%USERPROFILE%\Documents\Visual Studio XXXX\Templates\ItemTemplates\Visual C#\
```
Sendo **XXXX** o valor corresponde a versão do Visual Studio instalada.

## Conceitos Chaves

#### Connector
The [Bot Framework Connector](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-connector) fornece uma simples API REST que habilita a comunicação de um bot através de diversos canais como Skype, Email, Slack, e muito mais. Facilitando a comunicação entre o bot e o usuário transmitindo mensagens do bot para o canal e do canal para o bot.

No Bot Builder SDK for .NET, a biblioteca [Connector](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector?view=botconnector-3.12.2.4) habilita acesso ao Connector.

#### Activity
O Connector usa um objeto [Activity](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.activity?view=botconnector-3.12.2.4) para passar informações entre o bot e o canal (usuário). O tipo mais comum de activity é o message, mas existem outros tipos que podem ser usados para comunicação de vários tipos de informação de um bot ou canal.

Para maiores detalhes sobre Activities no Bot Builder SDK for .NET, acesse [Activities overview](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-activities).

#### Dialog
Quando você cria um bot usando a framework Bot Builder SDK for .NET, você pode usar dialogs para modelar uma conversação e gerenciar o [fluxo de conversações](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-conversation-flow#dialog-stack). Um dialog pode ser composto de outros dialogs para aumentar o reuso, e o contexto do dialog que mantém a [pilha de dialogs](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-conversation-flow) que estão ativos na conversação em qualquer momento.

No Bot Builder SDK for .NET, a biblioteca [Builder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs?view=botbuilder-3.12.2.4) permite que você gerencie dialogs.

#### FormFlow
Você pode usar o [FormFlow](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow) do Bot Builder SDK for .NET para simplificar a construção de um bot que coleta informações do usuário. Por exemplo, um bot que recebe pedidos de pizza deve coletar diversas informações do usuário como por exemplo, o sabor, o tamanho, entre outras informações. Através de guidelines básicos, o FormFlow pode automaticamente gerar o diálogo necessário para gerenciar a conversação. Neste tutorial vamos demonstrar a criação de um bot utilizando FormFlow.

#### State
A framework Bot Builder Framework permite o seu bot armazenar e recuperar o state data que está associado com um usuário. State data pode ser usado para diversar finalidades, como determinar onde a conversação anterior foi interrompida ou simplesmente cumprimentar um usuário através do seu nome. Se você armazenar as preferências do usuário, você pode usar esta informação para customizar a coversação da próxima vez que o usuário acessar o chat. Por exemplo, você pode alertar o usuário sobre novos artigos ou uma notícia do interesse dele, ou alertá-lo quando um evento estiver disponível.

## Criando o seu primeiro Projeto Bot Application

Primeiro, abra o Visual Studio e crie um novo projeto C#. Escolha o template Bot Application para o seu novo projeto.

![Novo Projeto no VS](images/1.png)

Usando o template Bot Application, você cria o projeto já contendo todos os componentes que são requeridos para construir uma simples aplicação bot, incluindo a referência para a framework Bot Builder SDK for .NET, ```Microsoft.Bot.Builder```. Verifique se as referências do projeto possuem a última versão da SDK seguindos os passos abaixo:

1. Clique com botão direito no projeto e selecione **Manage NuGet Packages**.
2. Na aba Browse, digite ```Microsoft.Bot.Builder```.
3. Localize o package ```Microsoft.Bot.Builder``` na lista de resultados e clique no botão **Update do package**.
4. Clique em **OK** na caixa de dialogo e após aceite as mudanças para confirmar as atualização.

![Update Package](images/2.png)

## Explorando o código

Uma vez, tendo o projeto criado a seguinte estrutura de diretórios será definida para solution, conforme figura abaixo:

![Solution Project](images/3.png)

**A Classe MessagesController**

Dentro da pasta **Controllers** é criada a classe **MessagesController.cs**. Esta classe possui o método chamado ```Post```, responsável por receber a mensagem do usuário e invocar o root dialog (RootDialog.cs). A classe RootDialogs.cs é criada automáticamente para prover um diálogo padrão de exemplo. 

Entretanto, no nosso tutotial não iremos utilizar esta classe, pois vamos implementar o nosso bot utilizando [Bot Form Builder](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow) que irá tornar nossa vida super fácil ao criar fluxos de conversações entre um usuário e o Bot.

## Implementando o bot com Bot Form Builder 

Vamos criar um bot para recomendar lugares como (bares, parques, shoppings, supermercados), de acordo com a localização do usuário.

#### Criando o Form

A classe ```RecommendationPlace``` será responsável por definir o form e enumerations que serão as opções para definição da recomendação. A classe inclui também um método static ```BuildForm``` usado para criar o form e definir uma simples mensagem de boas vindas.

Para usar o FormFlow, você deve importar o namespace [Microsoft.Bot.Builder.FormFlow](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.formflow?view=botbuilder-3.12.2.4).

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace Bot_RecommendationPlaces.Model.Bots
{
    // A classe RecommendationPlace representa o form que será utilizado para definir a comversação com o usuário.
    // Deve ser serializable, então o bot pode ser stateless.
    // A ordem dos fields define a sequencia default na qual o usuário questionado.

    // Os enumns definem as opções para cada field do form RecommendationPlace e a ordem dos valores representa a sequência
    // na qual eles serão apresentados para o usuário na conversação.
    [Serializable]
    public class RecommendationPlace {
        /// <summary>
        /// Define os locais
        /// </summary>
        public enum PlaceOptions {
            [Describe("Rio de Janeiro")]
            Rio_de_Janeiro,
            [Describe("São Paulo")]
            SaoPaulo,
            [Describe("Miami")]
            Miami,
            [Describe("Orlando")]
            Orlando,
            [Describe("New York")]
            NewYork,
            [Describe("Paris")]
            Paris,
            [Describe("Londres")]
            Londres
        }

        /// <summary>
        /// Define os tipos de lugares que o usuário deseja recomendação
        /// </summary>
        public enum TypePlaces {
            Shoppings,
            Supermercados,
            Parques,
            Restaurantes,
            Cinemas
        }
        
        // & fieldname, || tipos desejados
        [Prompt("Em local você se encontra? {||} Digite a sua opção abaixo.")]
        public PlaceOptions? Place { get; set; }

        // & fieldname, || tipos desejados
        [Prompt("Que tipo de local você deseja ir? {||} Digite a sua opção abaixo.")]
        public TypePlaces? Type { get; set; }

        /// <summary>
        /// Método que permite que o nosso bot converse com nossos usuários.
        /// </summary>
        /// <returns></returns>
        public static IForm<RecommendationPlace> BuildForm() {
            return new FormBuilder<RecommendationPlace>()
                .Message("Seja bem vindo ao Recommendation Place! O local onde você encontra as melhores recomendações.")
                .AddRemainingFields()
                .OnCompletion(async (context, profileForm) =>
                {
                    string       message = null;
                    List<String> places  = new List<String>();

                    // Define recomendação de acordo com as escolhas do usuário
                    switch (profileForm.Place) {
                        case PlaceOptions.Orlando :
                            switch (profileForm.Type) {
                                case TypePlaces.Parques :
                                    // Adicona recomendações na lista
                                    places.Add("Disney's Magic Kingdom");
                                    places.Add("Disney's Hollywood Studios");
                                    places.Add("Epcot Center");
                                    places.Add("SeaWorld");
                                    places.Add("Universal Studios Florida");
                                    places.Add("Universal's Islands of Adventure");
                                    places.Add("Universal's Volcano Bay");
                                    places.Add("Lego Land");
                                    break;
                            }
                            break;
                    }

                    // Valida lista de recomendações
                    if (places.Count > 0) {
                        StringBuilder stringPlaces = new StringBuilder();
                        places.ForEach(delegate (String place) {
                            stringPlaces.Append("<br/>").Append(place);
                        });

                        message = String.Format("Segue a lista de recomendações: {0}", stringPlaces);
                    } else {
                        message = "Não temos recomendação de lugares no momento para a consulta solicitada.";
                    } // end else

                    // Retorna message
                    await context.PostAsync(message);
                })
                .Build();
        }
    }
}
````

***Dica***
Este artigo descreve como você pode definir form usando uma classe C#. Entretanto, é possível também definir o form utilizando um  schema JSON. Para maiores detalhes acesse [Definindo um form usando um schema JSON](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow-json-schema?view=azure-bot-service-3.0)

#### Conectando o form ao framework 
Para conectar o form ao framework, você deve adicioná-lo a classe controller. Para isto iremos alterar a nossa classe **MessagesController** conforme demonstrado abaixo. O método ```Conversation.SendAsync``` invoca o método static ```MakeRootDialog```, que por sua vez, invoca o método ```FormDialog.FromForm``` para criar o form ```RecommendationPlace```.

```C#
namespace Bot_RecommendationPlaces
{
    [BotAuthentication]
    public class MessagesController : ApiController {
        /// <summary>
        /// Conecta a classe RecommendationPlace ao bot. Esta função irá retornar uma interface IDialog. 
        /// Essa interface que será responsável por gerenciar o fluxo de conversão entre nosso bot e os usuários.
        /// </summary>
        /// <returns></returns>
        internal static IDialog<RecommendationPlace> MakeRootDialog() {
            return Chain.From(() => FormDialog.FromForm(RecommendationPlace.BuildForm));
        }

        /// <summary>
        /// POST: api/Messages
        /// Recebe uma mensagem do usuário e a responde
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity) {
            if (activity.Type == ActivityTypes.Message) {
                await Conversation.SendAsync(activity, MakeRootDialog);
            } else {
                HandleSystemMessage(activity);
            } // end else
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Método utilizado para capturar eventos relacionados a conversação
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Activity HandleSystemMessage(Activity message) {
            if (message.Type == ActivityTypes.DeleteUserData) {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            } else if (message.Type == ActivityTypes.ConversationUpdate) {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            } else if (message.Type == ActivityTypes.ContactRelationUpdate) {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            } else if (message.Type == ActivityTypes.Typing) {
                // Handle knowing tha the user is typing
            } else if (message.Type == ActivityTypes.Ping) {

            } // end else

            return null;
        }
    }
}
```
Pronto, o nosso bot está pronto para ser testado. Através da definição de um simples form conectado ao framework usando o FormFlow, todo o gerenciamento da conversação entre o bot e o usuário é gerado automaticamente, permitindo que o usuário possa realizar operações como: (*ajuda*, *voltar a pergunta anterior*, *verificar o status da conversação*, *finalizar a conversação*), entre outras operações.

## Testando o Bot Application
Para testar o Bot, devemos primeiro fazer download do Bot Emulator, essa ferramenta irá nos ajudar a testar o nosso fluxo de conversação. O Bot Framework Emulator é uma aplicação desktop que permite aos desenvolvedores testar e debugar seus bots em um ambiente localhost ou executá-lo remotamente através de um túnel.

Tendo o **Bot Emulator** instalado excute os passos definidos abaixo:

1. Executar o projeto no Visual Studio pressionado F5
2. Abrir o Bot Emulator e conectar na URL, conforme exemplo: ```https://your_bots_hostname/api/messages```

Veja o fluxo de teste do nosso projeto **POC Bot Application**:

**Conectando ao Bot**

![Bot Conection](images/4.png)

**Fluxo 1 da conversação**

![Bot Conection](images/5.png)

**Fluxo 2 da conversação**

![Bot Conection](images/6.png)

**Fluxo 3 da conversação**

![Bot Conection](images/7.png)

Neste tutorial nós demonstramos como você pode criar uma simples aplicação Bot para definir uma conversação com o usuário. Entretanto, a framework Bot é uma ferramenta poderosa que te permite customizar uma interação ainda mais rica com usuário, onde você pode por exemplo: *especificar o estado inicial do form, adicionar validações e regras de negócio, prover segurança e até mesmo criar bots que façam uso de IA*. Para maiores detalhes consulte a [documentação da framework](https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-3.0).

A Microsoft possui um [canal](https://bots.botframework.com/) onde você pode encontrar bots para diversas finalidades.
