# delivery
ASP .NET 5, Angular 11, MySQL e Dapper. Sistema de controle de entregas para deposito de bebida, lanchonetes e outros. 

## Script para criação das tabelas do banco de dados



```
CREATE TABLE DLV_CLIENTE (
	ID INT NOT NULL AUTO_INCREMENT, 
	NOME VARCHAR(200) NOT NULL, 
	PRIMARY KEY(ID)
);

CREATE TABLE DLV_PRODUTO (
	ID INT NOT NULL AUTO_INCREMENT, 
	DESCRICAO VARCHAR(200) NOT NULL, 
	PRIMARY KEY(ID)
);

CREATE TABLE DLV_PEDIDO (
	ID INT NOT NULL AUTO_INCREMENT, 
	DATAHORAPEDIDO TIMESTAMP DEFAULT NOW(),
	DATAHORAPEDIDOENTREGUE TIMESTAMP,
	IDCLIENTE INT NOT NULL , 
	IDPRODUTO INT NOT NULL,
	PRIMARY KEY(ID), 
	FOREIGN KEY (IDCLIENTE) REFERENCES DLV_CLIENTE(ID), 
	FOREIGN KEY (IDPRODUTO) REFERENCES DLV_PRODUTO(ID)
);

INSERT INTO DLV_CLIENTE (NOME) VALUES ('CRISTIANO');
INSERT INTO DLV_PRODUTO (DESCRICAO) VALUES ('FARDO DE CERVEJA');
INSERT INTO DLV_PEDIDO (IDCLIENTE, IDPRODUTO) VALUES (1,1);
```
## Tela de Requisições (requests)


![alt text](https://github.com/consule/delivery-api/blob/main/api/assets/swagger.png)

## Tela de Pedidos 

![alt text](https://github.com/consule/delivery-api/blob/main/api/assets/pedidos.png)

## Tela para inserir novo pedido

![alt text](https://github.com/consule/delivery-api/blob/main/api/assets/novo.png)

## Tela para pesquisar pedidos entregues

![alt text](https://github.com/consule/delivery-api/blob/main/api/assets/entregues.png)

## Registra uma entrega de pedido

![alt text](https://github.com/consule/delivery-api/blob/main/api/assets/registraentrega.png)
