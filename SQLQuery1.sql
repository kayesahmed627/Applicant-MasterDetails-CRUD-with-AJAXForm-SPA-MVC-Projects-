            CreateStoredProcedure("InsertApplicant", c => new {
                ApplicantName = c.String(maxLength: 50),
                Phone = c.String(maxLength: 20),
                BirthDate = c.DateTime(),
                Picture = c.String(maxLength: 50),
                DepartmentId = c.Int(),

            }, @"INSERT INTO Applicants (ApplicantName, Phone, BirthDate, Picture, DepartmentId)
                VALUES (@ApplicantName, @Phone, @BirthDate, @Picture, @DepartmentId)
                SELECT SCOPE_IDENTITY() AS ApplicantId
            RETURN ");
            CreateStoredProcedure("UpdateApplicant", c => new
            {
                ApplicantId = c.Int(),
                ApplicantName = c.String(maxLength: 50),
                Phone = c.String(maxLength: 20),
                BirthDate = c.DateTime(),
                Picture = c.String(maxLength: 50),
                DepartmentId = c.Int(),

            }, @"UPDATE Applicants SET ApplicantName = @ApplicantName, Phone=@Phone, BirthDate=@BirthDate, Picture=@Picture,DepartmentId=@DepartmentId
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
	


	