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




# Migração do Cluster AKS
Baixar o Azure CLI
abrir o cmd
testar: az --version
logar: az login


abrir o portal: https://portal.azure.com/ 
Criar o cluster com o nome clusterfiap;
Ir para o recurso;
abrir o cloud shell
e digitar o comando: az aks get-credentials --resource-group rgFiap --name clusterfiap --file kubeconfig.yaml
apos criar o arquivo de configuração: cat kubeconfig.yaml
copiar configuração para secret no GitHub action

kubectl exec -it postgresregiao-648c64b675-lg95r -- psql -h localhost -U postgres --password -p 5432 regioesdatabase
CREATE TABLE regiao ( ddd int4 PRIMARY KEY, nome varchar(50), estado varchar(50) );
insert into regiao (ddd, nome, estado) values (48, 'Sul', 'Santa Catarina');



kubectl exec -it postgrescontato-755d7c5bb4-fxts9 -- psql -h localhost -U postgres --password -p 5432 contatosdatabase
CREATE TABLE contato ( id serial PRIMARY KEY, nome varchar(255), telefone varchar(15), email varchar(100), ddd int4 );
insert into contato (ddd, nome, email, telefone) values (48, 'Fabio Maciel Fernandes', 'fabbao@gmail.com', '4898468392');


CREATE TABLE compilacao (id serial PRIMARY KEY, data timestamp);


insert into compilacao (data) values (@data)


kubectl logs mscontatoapipod-758c54b747-fvlhg