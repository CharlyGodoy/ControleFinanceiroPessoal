namespace WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cpf_Cnpj = c.String(),
                        Obs = c.String(),
                        Inativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.ContaPagars",
                c => new
                    {
                        ContaPagarID = c.Int(nullable: false, identity: true),
                        Valor = c.Double(nullable: false),
                        Descricao = c.String(),
                        Valor_Pago = c.Double(nullable: false),
                        Data_Inclusao = c.DateTime(nullable: false),
                        Data_Vencimento = c.DateTime(nullable: false),
                        Data_Pagamento = c.DateTime(nullable: false),
                        Baixado = c.Boolean(nullable: false),
                        Liquidado = c.Boolean(nullable: false),
                        GrupoID = c.Int(nullable: false),
                        Fornecedor_FornecedorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContaPagarID)
                .ForeignKey("dbo.Grupoes", t => t.GrupoID, cascadeDelete: true)
                .ForeignKey("dbo.Fornecedors", t => t.Fornecedor_FornecedorID, cascadeDelete: true)
                .Index(t => t.GrupoID)
                .Index(t => t.Fornecedor_FornecedorID);
            
            CreateTable(
                "dbo.Grupoes",
                c => new
                    {
                        GrupoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Inativo = c.Boolean(nullable: false),
                        Grupo_ID_Pai = c.String(),
                    })
                .PrimaryKey(t => t.GrupoID);
            
            CreateTable(
                "dbo.Fornecedors",
                c => new
                    {
                        FornecedorID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cpf_Cnpj = c.String(),
                        Obs = c.String(),
                        Inativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FornecedorID);
            
            CreateTable(
                "dbo.ContaRecebers",
                c => new
                    {
                        ContaReceberID = c.Int(nullable: false, identity: true),
                        Valor = c.Double(nullable: false),
                        Descricao = c.String(),
                        Valor_Recebido = c.Double(nullable: false),
                        Data_Inclusao = c.DateTime(nullable: false),
                        Data_PrevRecebimento = c.DateTime(nullable: false),
                        Data_Recebimento = c.DateTime(nullable: false),
                        Baixado = c.Boolean(nullable: false),
                        Liquidado = c.Boolean(nullable: false),
                        GrupoID = c.Int(nullable: false),
                        Cliente_ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContaReceberID)
                .ForeignKey("dbo.Grupoes", t => t.GrupoID, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ClienteID, cascadeDelete: true)
                .Index(t => t.GrupoID)
                .Index(t => t.Cliente_ClienteID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ContaRecebers", "Cliente_ClienteID", "dbo.Clientes");
            DropForeignKey("dbo.ContaRecebers", "GrupoID", "dbo.Grupoes");
            DropForeignKey("dbo.ContaPagars", "Fornecedor_FornecedorID", "dbo.Fornecedors");
            DropForeignKey("dbo.ContaPagars", "GrupoID", "dbo.Grupoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ContaRecebers", new[] { "Cliente_ClienteID" });
            DropIndex("dbo.ContaRecebers", new[] { "GrupoID" });
            DropIndex("dbo.ContaPagars", new[] { "Fornecedor_FornecedorID" });
            DropIndex("dbo.ContaPagars", new[] { "GrupoID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ContaRecebers");
            DropTable("dbo.Fornecedors");
            DropTable("dbo.Grupoes");
            DropTable("dbo.ContaPagars");
            DropTable("dbo.Clientes");
        }
    }
}
