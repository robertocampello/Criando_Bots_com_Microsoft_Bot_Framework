# Criando Bots com o Microsoft Bot Framework
Tutotial demonstrando a criação de Bots com o **Microsoft Bot Framework**.

O Microsoft Bot Framework foi criado para ajudar na criação de bots. Bots são robôs pré programados que podem interagir com os usuários naturalmente no mais diversos canais como Skype, Facebook e outros serviços de mensagens.

Este tutorial demonstra como você pode construir bots usando o template Bot Application e o Bot Builder SDK for .NET, e como testá-lo com o Bot Framework Emulator.

## Pré-Requisitos

1. Instale o [Visual Studio 2017](https://www.visualstudio.com/downloads/) – Pode ser o community;
2. Faça o download dos arquivos zip do **Bot Application**, **Bot Controller**, e **Bot Dialog**. Instale o projeto template copiando o arquivo **Bot Application.zip** para o diretório projects template do seu Visual Studio e copie os arquivos **Bot Controller.zip** e Bot Dialog.zip para o diretório item templates do seu Visual Studio.
3. Faça o download do [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator) - Necessário para teste do BOT;

***Dica***: Geralmente as pastas project templates e item templates do Visual Studio, ficam localizadas respectivamente nos caminhos: %USERPROFILE%\Documents\Visual Studio XXXX\Templates\ProjectTemplates\Visual C#\ e %USERPROFILE%\Documents\Visual Studio XXXX\Templates\ItemTemplates\Visual C#\, sendo XXXX o valor corresponde a versão instalada.

## Criando o seu Bot

Primeiro, abra o Visual Studio e crie um novo projeto C#. Escolha o template Bot Application para o seu novo projeto.
![Novo Projeto no VS](images/g1.png)
