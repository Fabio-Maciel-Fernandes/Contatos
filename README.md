# Contatos

*************SE ERRO NO DOCKER   
*************remover o docker   
*************instalar novamente   
*************abrir o aplicativo docker desktop e logar nele   


baixar a imagem: docker pull postgres

docker run -d --name postgre-fiap -e POSTGRES_PASSWORD=102030 -p 5432:5432 postgres:latest

docker exec -it 707d70bf319b3525eb316de02dfde17e605f965e2c122f36caaeff9f0ba96f7a psql -U postgres -d postgres


create database ContatosDataBase;

CREATE TABLE regiao (
  ddd int4 PRIMARY KEY,
  nome varchar(50),
  estado varchar(50)
 );

CREATE TABLE contato (
  id serial PRIMARY KEY,
  nome varchar(255),
  telefone varchar(15),
  email varchar(100),
  ddd int4,
  FOREIGN KEY (ddd) REFERENCES regiao(ddd)
);
