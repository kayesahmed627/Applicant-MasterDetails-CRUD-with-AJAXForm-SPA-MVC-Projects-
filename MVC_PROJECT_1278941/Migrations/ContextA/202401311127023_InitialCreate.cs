namespace MVC_PROJECT_1278941.Migrations.ContextA
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        ApplicantName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                        Picture = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        Institute = c.String(nullable: false, maxLength: 50),
                        PassingYear = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QualificationId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId);



            ///////////////////////////////////////// 

            CreateStoredProcedure("InsertApplicant", c => new {
                ApplicantName = c.String(maxLength: 50),
                Phone = c.String(maxLength: 20),
                BirthDate = c.DateTime(),
                PayRate = c.Decimal(),
                Picture = c.String(maxLength: 50),
                DepartmentId = c.Int(),

            }, @"INSERT INTO Applicants (ApplicantName, Phone, BirthDate, PayRate, Picture, DepartmentId)
                VALUES (@ApplicantName, @Phone, @BirthDate,@PayRate, @Picture, @DepartmentId)
                SELECT SCOPE_IDENTITY() AS ApplicantId
            RETURN ");
            CreateStoredProcedure("UpdateApplicant", c => new
            {
                ApplicantId = c.Int(),
                ApplicantName = c.String(maxLength: 50),
                Phone = c.String(maxLength: 20),
                BirthDate = c.DateTime(),
                PayRate = c.Decimal(),
                Picture = c.String(maxLength: 50),
                DepartmentId = c.Int(),

            }, @"UPDATE Applicants SET ApplicantName = @ApplicantName, Phone=@Phone, BirthDate=@BirthDate,PayRate=@PayRate, Picture=@Picture,DepartmentId=@DepartmentId
            WHERE  ApplicantId = @ApplicantId
        RETURN");
            CreateStoredProcedure("DeleteApplicant", c => new
            {
                ApplicantId = c.Int()

            }, @"DELETE FROM Applicants
        WHERE ApplicantId = @ApplicantId
    RETURN");

            CreateStoredProcedure("DeleteQualificationByApplicantId", c => new
            {
                ApplicantId = c.Int()

            }, @"DELETE FROM Qualifications
        WHERE ApplicantId = @ApplicantId
    RETURN");
            CreateStoredProcedure("InsertQualification", c => new
            {

                Institute = c.String(maxLength: 50),
                PassingYear = c.Int(),
                Degree = c.Int(),
                ApplicantId = c.Int()
            }, @"INSERT INTO Qualifications (Institute, PassingYear, Degree, ApplicantId)
        VALUES (@Institute, @PassingYear, @Degree, @ApplicantId)
        SELECT SCOPE_IDENTITY() as QualificationId
    RETURN");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Qualifications", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Qualifications", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "DepartmentId" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.Departments");
            DropTable("dbo.Applicants");

            ////////////////////////////////

            DropStoredProcedure("InsertApplicant");
            DropStoredProcedure("UpdateApplicant");
            DropStoredProcedure("DeleteApplicant");
            DropStoredProcedure("DeleteQualificationByApplicantId");
            DropStoredProcedure("InsertQualification");

        }
    }
}
