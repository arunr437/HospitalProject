namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.FeedbackTypes",
                c => new
                    {
                        typeId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.typeId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        DesignationId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        City = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Initials = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Feedback = c.String(),
                        typeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.FeedbackTypes", t => t.typeId, cascadeDelete: true)
                .Index(t => t.typeId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        jobId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        skill = c.String(),
                        description = c.String(),
                        type = c.String(),
                        salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.jobId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        headline = c.String(),
                        content = c.String(),
                        active = c.Int(nullable: false),
                        typeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.NotificationTypes", t => t.typeId, cascadeDelete: true)
                .Index(t => t.typeId);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.FeedbackTypesDepartments",
                c => new
                    {
                        FeedbackTypes_typeId = c.Int(nullable: false),
                        Departments_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FeedbackTypes_typeId, t.Departments_id })
                .ForeignKey("dbo.FeedbackTypes", t => t.FeedbackTypes_typeId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Departments_id, cascadeDelete: true)
                .Index(t => t.FeedbackTypes_typeId)
                .Index(t => t.Departments_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "typeId", "dbo.NotificationTypes");
            DropForeignKey("dbo.Feedbacks", "typeId", "dbo.FeedbackTypes");
            DropForeignKey("dbo.Donations", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Donations", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.FeedbackTypesDepartments", "Departments_id", "dbo.Departments");
            DropForeignKey("dbo.FeedbackTypesDepartments", "FeedbackTypes_typeId", "dbo.FeedbackTypes");
            DropIndex("dbo.FeedbackTypesDepartments", new[] { "Departments_id" });
            DropIndex("dbo.FeedbackTypesDepartments", new[] { "FeedbackTypes_typeId" });
            DropIndex("dbo.Notifications", new[] { "typeId" });
            DropIndex("dbo.Feedbacks", new[] { "typeId" });
            DropIndex("dbo.Donations", new[] { "ProvinceId" });
            DropIndex("dbo.Donations", new[] { "DesignationId" });
            DropTable("dbo.FeedbackTypesDepartments");
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.Notifications");
            DropTable("dbo.Jobs");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Provinces");
            DropTable("dbo.Donations");
            DropTable("dbo.Designations");
            DropTable("dbo.FeedbackTypes");
            DropTable("dbo.Departments");
            DropTable("dbo.Blogs");
        }
    }
}
