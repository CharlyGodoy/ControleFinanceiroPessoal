namespace WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaPagars", "GrupoID", "dbo.Grupoes");
            DropIndex("dbo.ContaPagars", new[] { "GrupoID" });
            AddColumn("dbo.ContaPagars", "SubGrupoID", c => c.Int(nullable: false));
            CreateIndex("dbo.ContaPagars", "SubGrupoID");
            AddForeignKey("dbo.ContaPagars", "SubGrupoID", "dbo.SubGrupoes", "SubGrupoID", cascadeDelete: true);
            DropColumn("dbo.ContaPagars", "GrupoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContaPagars", "GrupoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ContaPagars", "SubGrupoID", "dbo.SubGrupoes");
            DropIndex("dbo.ContaPagars", new[] { "SubGrupoID" });
            DropColumn("dbo.ContaPagars", "SubGrupoID");
            CreateIndex("dbo.ContaPagars", "GrupoID");
            AddForeignKey("dbo.ContaPagars", "GrupoID", "dbo.Grupoes", "GrupoID", cascadeDelete: true);
        }
    }
}
