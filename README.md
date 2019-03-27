# Eventos Teresina

O App para Windows Phone Eventos Teresina consiste em uma lista de eventos da cidade de Teresina-PI, fornecidos através de um WebService, onde é possível ver o nome, data, destaque e imagem dos eventos. Ao clicar no evento escolhido, serão mostradas mais informações sobre o mesmo e uma imagem de um banner publicitário. Onde é permitido você salvar seu evento favorito com um tile secundário.

Definimos como escopo o consumo pelo aplicativo de uma lista predefinida de evento no formato XML e disponibilizada através do link http://fernandovaller.com/eventos.xml, sendo facilmente mudado para um WebService real (que apresente os campos utilizados pela aplicação:  nome, data, destaque, imagem);

![telas](https://cloud.githubusercontent.com/assets/5489549/19152058/783fb03c-8ba6-11e6-82bc-193ffcfe4c02.jpg)

## Requisitos e Recursos escolhidos

1. Deve ter o tile principal e ícone personalizados. (Atendido)
2. Deve suportar os idiomas Inglês e Português. (Atendido)
3. Deve conter pelo menos 3 dos seguintes recursos:

    * a) Armazenamento em banco de dados; (Usado)    
    * b) Uso de tiles secundários; (Usado)    
    * c) Consumo de webservices; (Usado)    
    * d) Uso de algum sensor (GPS, Acelerômetro, Câmera)
    
4. Deve estar compilando e rodando sem problemas. Não deve ter bugs e rodar normalmente (testem bem). (Atendido)

## Funcionamento

Ao abrir o App é realizada a verificação de existência de um Banco de Dados – DB caso não exista o mesmo será criado. No construtor da classe principal é chamado um método para consulta do WebService com os eventos, caso os eventos ainda não existam no DB o evento é adicionado.

Após baixar os evento e atualizar do DB é chamado o método para carregar os eventos presente no DB, que será exibido para o usuário.

Ao selecionar um evento presente na lista, é aberta uma nova página onde é possível ver mais informações do evento, como a imagem, o nome e o texto de destaque.

## Demonstração

* https://www.youtube.com/watch?v=bSdHVhIJ7N4

* https://www.youtube.com/watch?v=u4Opk1TKEUg
