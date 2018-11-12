IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Battles] (
    [Id] int NOT NULL IDENTITY,
    [EndDate] datetime2 NOT NULL,
    [Name] nvarchar(max),
    [StartDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Battles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Samurais] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max),
    CONSTRAINT [PK_Samurais] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Quotes] (
    [Id] int NOT NULL IDENTITY,
    [SamuraiId] int NOT NULL,
    [Text] nvarchar(max),
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Quotes_Samurais_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurais] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [SamuraiBattles] (
    [BattleId] int NOT NULL,
    [SamuraiId] int NOT NULL,
    CONSTRAINT [PK_SamuraiBattles] PRIMARY KEY ([BattleId], [SamuraiId]),
    CONSTRAINT [FK_SamuraiBattles_Battles_BattleId] FOREIGN KEY ([BattleId]) REFERENCES [Battles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SamuraiBattles_Samurais_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurais] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [SecretIdentity] (
    [Id] int NOT NULL IDENTITY,
    [RealName] nvarchar(max),
    [SamuraiId] int NOT NULL,
    CONSTRAINT [PK_SecretIdentity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SecretIdentity_Samurais_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurais] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Quotes_SamuraiId] ON [Quotes] ([SamuraiId]);

GO

CREATE INDEX [IX_SamuraiBattles_BattleId] ON [SamuraiBattles] ([BattleId]);

GO

CREATE INDEX [IX_SamuraiBattles_SamuraiId] ON [SamuraiBattles] ([SamuraiId]);

GO

CREATE UNIQUE INDEX [IX_SecretIdentity_SamuraiId] ON [SecretIdentity] ([SamuraiId]) WHERE [SamuraiId] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181112135247_init', N'1.0.3');

GO

