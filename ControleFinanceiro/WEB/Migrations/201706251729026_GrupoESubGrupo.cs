namespace WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GrupoESubGrupo : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ContaRecebers", name: "Cliente_ClienteID", newName: "ClienteID");
            RenameColumn(table: "dbo.ContaPagars", name: "Fornecedor_FornecedorID", newName: "FornecedorID");
            RenameIndex(table: "dbo.ContaPagars", name: "IX_Fornecedor_FornecedorID", newName: "IX_FornecedorID");
            RenameIndex(table: "dbo.ContaRecebers", name: "IX_Cliente_ClienteID", newName: "IX_ClienteID");
            DropColumn("dbo.Grupoes", "Grupo_ID_Pai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grupoes", "Grupo_ID_Pai", c => c.String());
            RenameIndex(table: "dbo.ContaRecebers", name: "IX_ClienteID", newName: "IX_Cliente_ClienteID");
            RenameIndex(table: "dbo.ContaPagars", name: "IX_FornecedorID", newName: "IX_Fornecedor_FornecedorID");
            RenameColumn(table: "dbo.ContaPagars", name: "FornecedorID", newName: "Fornecedor_FornecedorID");
            RenameColumn(table: "dbo.ContaRecebers", name: "ClienteID", newName: "Cliente_ClienteID");
        }
    }
}
