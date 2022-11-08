# ChamadoFacil

## Análise de Código
[![CodeFactor](https://www.codefactor.io/repository/github/cpsilva/chamadofacil/badge)](https://www.codefactor.io/repository/github/cpsilva/chamadofacil)

## Integração Contínua
[![Build status](https://ci.appveyor.com/api/projects/status/k3jawa7ghc88fp74?svg=true)](https://ci.appveyor.com/project/cpsilva/chamadofacil)

[![Build Status](https://dev.azure.com/caiopirees/ChamadoFacil/_apis/build/status/cpsilva.ChamadoFacil?branchName=master)](https://dev.azure.com/caiopirees/ChamadoFacil/_build/latest?definitionId=1&branchName=main)

## Tecnologias Utilizadas:

* Visual Studio 2017
* .NET Core 2.0
* C#
* ASP.NET Core 2.0
* EntityFrameworkCore 2.0
* Angular 8.0
* Typescript 3.5.3
* HTML5
* CSS3
* Admin LTE 2.3.11
* Font Awesome 4.7.0

## Camadas:

![](https://github.com/cpsilva/ChamadoFacil/blob/master/Screenshots/layers.PNG)

## Configuração do Banco de dados:

Para configurar o acesso a dados da aplicação deve-se alterar o arquivo appsettings.json que se encontra no projeto ChamadoFacil.ApplicationService como na imagem a baixo e lá alterar o DatabaseContext e lá utilizar uma connection string que aponte para o banco local ou passando o endereço de um banco em nuvem.

![](https://github.com/cpsilva/ChamadoFacil/blob/master/Screenshots/appsettings.PNG)

## Execução do Projeto Angular e IIS:

1. Executar Npm Install na pasta ChamadoFacil.UserInterface para instalar as dependencias.

2. Apos obter as depedencias executar o comando 'ng serve -o' este comando já ira abrir a interface do sistema no browser.

3. No Visual Studio selecionar o projeto ChamadoFacil.ApplicationService como Startup Project.

4. Executar o projeto com o IIS Express.

5. Neste momento a API da aplicação já está disponivel para acesso e consulta, assim basta ir para guia de interface e preencher o formulario para utilizar a aplicação.
