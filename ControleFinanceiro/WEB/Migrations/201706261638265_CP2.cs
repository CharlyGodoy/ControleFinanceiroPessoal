namespace WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CP2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaPagars", "SubGrupoID", "dbo.SubGrupoes");
            DropIndex("dbo.ContaPagars", new[] { "SubGrupoID" });
            AddColumn("dbo.ContaPagars", "GrupoID", c => c.Int(nullable: false));
            CreateIndex("dbo.ContaPagars", "GrupoID");
            AddForeignKey("dbo.ContaPagars", "GrupoID", "dbo.Grupoes", "GrupoID", cascadeDelete: true);
            DropColumn("dbo.ContaPagars", "SubGrupoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContaPagars", "SubGrupoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ContaPagars", "GrupoID", "dbo.Grupoes");
            DropIndex("dbo.ContaPagars", new[] { "GrupoID" });
            DropColumn("dbo.ContaPagars", "GrupoID");
            CreateIndex("dbo.ContaPagars", "SubGrupoID");
            AddForeignKey("dbo.ContaPagars", "SubGrupoID", "dbo.SubGrupoes", "SubGrupoID", cascadeDelete: true);
        }
    }
}
