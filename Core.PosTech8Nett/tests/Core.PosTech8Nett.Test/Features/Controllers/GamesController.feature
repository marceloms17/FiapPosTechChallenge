Feature: Games Controller

  Scenario: Listar todos os jogos cadastrados
    Given que existem jogos cadastrados no sistema
    When o usuário requisita todos os jogos
    Then o sistema deve retornar OK com a lista mapeada

  Scenario: Buscar jogo por ID inexistente
    Given que nenhum jogo corresponde ao ID informado
    When o usuário requisita o jogo por ID
    Then o sistema deve retornar NotFound

  Scenario: Buscar jogo por ID existente
    Given que existe um jogo com ID válido
    When o usuário requisita o jogo por ID
    Then o sistema deve retornar OK com o jogo mapeado

  Scenario: Criar um novo jogo
    Given que o usuário envia dados válidos de um novo jogo
    When o usuário solicita a criação do jogo
    Then o sistema deve retornar CreatedAtAction

  Scenario: Atualizar um jogo existente
    Given que o usuário envia dados atualizados de um jogo
    When o usuário solicita a atualização do jogo
    Then o sistema deve retornar NoContent

  Scenario: Deletar um jogo
    Given que existe um jogo com ID válido
    When o usuário solicita a exclusão do jogo
    Then o sistema deve retornar NoContent
