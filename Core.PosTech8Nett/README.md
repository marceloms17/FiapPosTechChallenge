# Core.PosTech8Nett

## Decisões
    - **API**: É o projeto de inicialização.

## Frameworks
- Este projeto roda em .net8.0, você pode baixar [here](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- Para testar, este projeto usa `xUnit`, `Moq`, `NSubstitute` e `FluentAssertions`

## Dependências
- SqlServer

## Rodar

Na raiz deste projeto, execute:
```
$ dotnet build
$ dotnet run
```
Este projeto usa imagens privadas em dockerfile. É pré-requisito ter o Visual Studio.

### Rodar testes
Na raiz do projeto, execute:
```
$ dotnet test
```