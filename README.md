# Chat com comunicação via websocket

Este projeto possui 2 partes, Client e Server
- Server
  - Backend desenvolvido em ASP.NET sem utilizar nenhuma biblioteca externa
  - Utilizado .NET Core 3.1
  - Utilizado Visual Studio 2019
  - Recomenda-se utilizar as mesmas ferramentas e versões descritas acima

- Client
  - Utiliza React JS com o padrão de Hooks
  Utiliza a solução [Create React App](https://github.com/facebook/create-react-app)
  - Não utiliza nenhuma biblioteca externa para websocket, apenas Javascript convencional
  - Utilizado [React Bootstrap](https://github.com/react-bootstrap/react-bootstrap) para facilitar a estilização do chat

## Funcionalidades implementadas
- Registro de apelido
- Envio de mensagem pública para sala
- Envio de mensagem pública mencionando um usuário
- Envio de mensagem privada para um usuário na sala
- Tela de ajuda para utilizar a tela de chat
- Comandos todos abstraídos para botões
- Sair do chat

## Como iniciar o Server
- Abra a solution no Visual Studio 2019 e selecione Server para iniciar
- O servidor irá escutar na porta #8000

##### [http://localhost:8000](http://localhost:8000)

## Como iniciar o Client

- Necessário possuir o node instalado versão 6.14.13 ou superior
- Acesse a pasta cliente pelo prompt de comando e esxecute os seguintes comandos
- Na rota "/" estará o client com a tela de login


### `npm install`
### `npm start`

- A aplicação irá iniciar em:
##### [http://localhost:3000](http://localhost:3000)

# Testes
- Acesse o client na seguinte rota:
##### [http://localhost:3000/test](http://localhost:3000/test)
- Abra o Console do navegador 
- Clique no botão "Iniciar"
- Os teste que serão executados são:
    - Conexão de 3 clients (A,B e C)
    - Realização de Login dos 3 clients
    - Mensagem de A para todos da sala
    - Mensagem de B mencionando C
    - Mensagem provada de de C para A
- O roteiro de testes encontra-se no arquivo "client/views/test"

## Funcionalidades futuras
- Tratativa de Registro de apelido não permitindo registrar um apelido já existente na sala
- Melhoria nos testes
- Criação de nova sala

