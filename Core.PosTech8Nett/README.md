# Core.PosTech8Nett

## Decis�es
    - **API**: � o projeto de inicializa��o.

## Frameworks
- Este projeto roda em .net8.0, voc� pode baixar [here](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- Para testar, este projeto usa `xUnit`, `Moq`, `NSubstitute` e `FluentAssertions`

## Depend�ncias
- SqlServer

## Rodar

Na raiz deste projeto, execute:
```
$ dotnet build
$ dotnet run
```
Este projeto usa imagens privadas em dockerfile. � pr�-requisito ter o Visual Studio.

### Rodar testes
Na raiz do projeto, execute:
```
$ dotnet test
```