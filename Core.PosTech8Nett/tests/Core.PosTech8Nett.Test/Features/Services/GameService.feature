Feature: Game Service

  Scenario: Buscar todos os jogos
    Given que existem jogos cadastrados
    When o serviço requisita todos os jogos
    Then o sistema deve retornar a lista com todos os jogos

  Scenario: Buscar jogo por ID existente
    Given que existe um jogo com ID válido
    When o serviço requisita o jogo por ID
    Then o sistema deve retornar o jogo correspondente

  Scenario: Adicionar um novo jogo
    Given que um jogo novo foi criado
    When o serviço adiciona o jogo
    Then o repositório deve ser chamado para adicionar

  Scenario: Atualizar um jogo existente
    Given que um jogo já existente foi alterado
    When o serviço atualiza o jogo
    Then o repositório deve ser chamado para atualizar

  Scenario: Remover um jogo por ID existente
    Given que existe um jogo com ID válido para exclusão
    When o serviço exclui o jogo
    Then o repositório deve ser chamado para excluir
