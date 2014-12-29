namespace MaSitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePersonalSpace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonalSpaceModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonalSpaceModels");
        }
    }
}
