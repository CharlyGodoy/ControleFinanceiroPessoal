namespace WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubGrupo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubGrupoes",
                c => new
                    {
                        SubGrupoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Inativo = c.Boolean(nullable: false),
                        GrupoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubGrupoID)
                .ForeignKey("dbo.Grupoes", t => t.GrupoID, cascadeDelete: true)
                .Index(t => t.GrupoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubGrupoes", "GrupoID", "dbo.Grupoes");
            DropIndex("dbo.SubGrupoes", new[] { "GrupoID" });
            DropTable("dbo.SubGrupoes");
        }
    }
}
