namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsApplicationFAQAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        applicationId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        emailId = c.String(),
                        coverLetter = c.String(),
                        resume = c.String(),
                        JobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.applicationId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.FrequentlyAskedQuestions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        questions = c.String(),
                        answers = c.String(),
                        category = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "JobId", "dbo.Jobs");
            DropIndex("dbo.Applications", new[] { "JobId" });
            DropTable("dbo.FrequentlyAskedQuestions");
            DropTable("dbo.Applications");
        }
    }
}
