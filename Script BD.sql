IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'LigaEstoqueDB'
)
BEGIN
    CREATE DATABASE LigaEstoqueDB;
END
GO

USE LigaEstoqueDB;
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name = 'Usuarios'
)
BEGIN
    CREATE TABLE Usuarios (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nome VARCHAR(150) NOT NULL,
        cpf CHAR(11) NOT NULL UNIQUE,
        senha_hash VARCHAR(255) NOT NULL,
        ativo BIT DEFAULT 1
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name = 'Setores'
)
BEGIN
    CREATE TABLE Setores (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nome VARCHAR(100) NOT NULL
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name = 'Produtos'
)
BEGIN
    CREATE TABLE Produtos (
        id INT IDENTITY(1,1) PRIMARY KEY,
        sku VARCHAR(50) NOT NULL UNIQUE,
        nome VARCHAR(150) NOT NULL,
        descricao VARCHAR(255),
        preco DECIMAL(10,2) NOT NULL
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name = 'Estoque'
)
BEGIN
    CREATE TABLE Estoque (
        produto_id INT NOT NULL,
        setor_id INT NOT NULL,
        quantidade INT NOT NULL DEFAULT 0,
        PRIMARY KEY (produto_id, setor_id),
        CONSTRAINT FK_estoque_produto FOREIGN KEY (produto_id) REFERENCES Produtos(id),
        CONSTRAINT FK_estoque_setor FOREIGN KEY (setor_id) REFERENCES Setores(id)
    );
END
GO

IF NOT EXISTS (
    SELECT * FROM sys.tables WHERE name = 'MovimentacaoEstoque'
)
BEGIN
    CREATE TABLE MovimentacaoEstoque (
        id INT IDENTITY(1,1) PRIMARY KEY,
        tipo VARCHAR(20) NOT NULL,
        quantidade INT NOT NULL CHECK (quantidade > 0),
        produto_id INT NOT NULL,
        usuario_id INT NOT NULL,
        setor_origem_id INT NULL,
        setor_destino_id INT NULL,
        data_movimentacao DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_mov_produto FOREIGN KEY (produto_id) REFERENCES produtos(id),
        CONSTRAINT FK_mov_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
        CONSTRAINT FK_mov_setor_origem FOREIGN KEY (setor_origem_id) REFERENCES setores(id),
        CONSTRAINT FK_mov_setor_destino FOREIGN KEY (setor_destino_id) REFERENCES setores(id),
        CONSTRAINT CK_tipo_movimentacao CHECK (tipo IN ('ENTRADA', 'CONSUMO', 'TRANSFERENCIA'))
    );
END
GO

INSERT INTO setores (nome) VALUES
('Almoxarifado'),
('Oncologia'),
('UTI'),
('Farmácia'),
('Emergência');

INSERT INTO usuarios (nome, cpf, senha_hash, ativo) VALUES
('Ana Silva', '11111111111', 'hash1', 1),
('Bruno Souza', '22222222222', 'hash2', 1),
('Carlos Lima', '33333333333', 'hash3', 1),
('Daniela Rocha', '44444444444', 'hash4', 1),
('Eduardo Alves', '55555555555', 'hash5', 1);

INSERT INTO produtos (sku, nome, descricao, preco) VALUES
('SKU001', 'Seringa 10ml', 'Seringa descartável', 1.50),
('SKU002', 'Luva descartável', 'Luva de procedimento', 0.80),
('SKU003', 'Máscara cirúrgica', 'Proteção facial', 0.60),
('SKU004', 'Soro fisiológico', 'Solução salina 500ml', 5.00),
('SKU005', 'Agulha 25mm', 'Agulha estéril', 0.30);

INSERT INTO estoque (produto_id, setor_id, quantidade) VALUES
(1, 1, 100), 
(2, 1, 200), 
(3, 2, 150), 
(4, 3, 80), 
(5, 4, 120);

INSERT INTO MovimentacaoEstoque 
(tipo, quantidade, produto_id, usuario_id, setor_origem_id, setor_destino_id)
VALUES
('ENTRADA', 100, 1, 1, NULL, 1),
('CONSUMO', 20, 2, 2, 1, NULL),
('TRANSFERENCIA', 30, 3, 3, 2, 3),
('ENTRADA', 80, 4, 4, NULL, 3),
('TRANSFERENCIA', 50, 5, 5, 4, 5);