----CREATE DATABASE visage;
----USE visage;
---- Tabela tipo_usuario

-- Tabela tipo_usuario
CREATE TABLE tipo_usuario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tipo NVARCHAR(50)
);

-- Tabela usuarios
CREATE TABLE usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tipo_usuario_id INT,
    nome NVARCHAR(255),
    data_nascimento DATE,
    cpf NVARCHAR(14),
    email NVARCHAR(255),
    base64_image NVARCHAR(MAX),
    embedding VARBINARY(MAX),
    FOREIGN KEY (tipo_usuario_id) REFERENCES tipo_usuario(id)
);

-- Tabela registro_autenticacao
CREATE TABLE registro_autenticacao (
    id INT IDENTITY(1,1) PRIMARY KEY,
    usuarios_id INT,
    data_hora DATETIME,
    status BIT,
    tipo_acesso NVARCHAR(50),
    FOREIGN KEY (usuarios_id) REFERENCES usuarios(id)
);

-- Tabela logs
CREATE TABLE logs (
    id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT,
    data_hora DATETIME,
    tipo_evento NVARCHAR(50),
    descricao NVARCHAR(255),
    nivel_gravidade NVARCHAR(50),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

-- Tabela permissoes
CREATE TABLE permissoes (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome_permissao NVARCHAR(50)
);

-- Tabela usuarios_permissoes
CREATE TABLE usuarios_permissoes (
    usuarios_id INT,
    permissoes_id INT,
    PRIMARY KEY (usuarios_id, permissoes_id),
    FOREIGN KEY (usuarios_id) REFERENCES usuarios(id),
    FOREIGN KEY (permissoes_id) REFERENCES permissoes(id)
);

ALTER TABLE usuarios
ADD senha NVARCHAR(255) NOT NULL;

