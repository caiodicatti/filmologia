CREATE DATABASE db_filmologia;
USE db_filmologia;


CREATE TABLE Usuario(
idUsuario INTEGER AUTO_INCREMENT NOT NULL,
Nome VARCHAR(150) NOT NULL,
Email VARCHAR(150) NOT NULL,
Senha TEXT NOT NULL,
Sexo VARCHAR(1) NOT NULL, -- (M/F)
DtaNascimento DATE NOT NULL,
DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
PRIMARY KEY(idUsuario));

CREATE TABLE UsuarioFilme(
IdFilme INTEGER AUTO_INCREMENT NOT NULL,
IdUsuario INTEGER NOT NULL,
IdFilmeAPI INTEGER NOT NULL,
Nome VARCHAR(250) NOT NULL,
Sinopse TEXT,
Poster VARCHAR(250),
Lancamento DATE,
DataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
CONSTRAINT FK_USUARIOFILME_USUARIO FOREIGN KEY(IdUsuario) REFERENCES Usuario(idUsuario),
PRIMARY KEY(IdFilme));

-- ------------------- Testes e Inserções ---------------- 

/*Insert into Usuario (Nome, Email, Senha, Sexo, DtaNascimento)
VALUES ('Caio Dicatti', 'caio@email.com', '123456789', 'M', '1991-09-25'); */

select * from Usuario;