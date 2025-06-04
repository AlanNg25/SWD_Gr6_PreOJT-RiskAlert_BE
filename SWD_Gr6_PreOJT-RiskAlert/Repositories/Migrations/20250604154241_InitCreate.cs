using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.ClassID);
                });

            migrationBuilder.CreateTable(
                name: "majors",
                columns: table => new
                {
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_majors", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "programs",
                columns: table => new
                {
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programs", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_programs_majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_users_majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_subjects_programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MajorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentStatus = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_enrollments_classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    NotificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_notifications_users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "predictions",
                columns: table => new
                {
                    PredictionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Probability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_predictions", x => x.PredictionID);
                    table.ForeignKey(
                        name: "FK_predictions_users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "curriculums",
                columns: table => new
                {
                    CurriculumID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculums", x => x.CurriculumID);
                    table.ForeignKey(
                        name: "FK_curriculums_programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_curriculums_subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_grades_subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grades_users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subjectInClasses",
                columns: table => new
                {
                    SubjectInClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjectInClasses", x => x.SubjectInClassID);
                    table.ForeignKey(
                        name: "FK_subjectInClasses_classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subjectInClasses_subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_subjectInClasses_users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "syllabus",
                columns: table => new
                {
                    SyllabusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SyllabusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrerequisitesSubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentTasks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScoringCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PrerequisiteSubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syllabus", x => x.SyllabusID);
                    table.ForeignKey(
                        name: "FK_syllabus_subjects_PrerequisiteSubjectID",
                        column: x => x.PrerequisiteSubjectID,
                        principalTable: "subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syllabus_subjects_PrerequisitesSubjectID",
                        column: x => x.PrerequisitesSubjectID,
                        principalTable: "subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "riskAnalysis",
                columns: table => new
                {
                    RiskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riskAnalysis", x => x.RiskID);
                    table.ForeignKey(
                        name: "FK_riskAnalysis_enrollments_EnrollmentID",
                        column: x => x.EnrollmentID,
                        principalTable: "enrollments",
                        principalColumn: "EnrollmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gradeDetails",
                columns: table => new
                {
                    GradeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScoreWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinScr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gradeDetails", x => x.GradeDetailID);
                    table.ForeignKey(
                        name: "FK_gradeDetails_grades_GradeID",
                        column: x => x.GradeID,
                        principalTable: "grades",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassSubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendNumber = table.Column<int>(type: "int", nullable: false),
                    SessionNumber = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_attendances_enrollments_EnrollmentID",
                        column: x => x.EnrollmentID,
                        principalTable: "enrollments",
                        principalColumn: "EnrollmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_attendances_subjectInClasses_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "subjectInClasses",
                        principalColumn: "SubjectInClassID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "actions",
                columns: table => new
                {
                    ActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdvisorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK_actions_riskAnalysis_RiskID",
                        column: x => x.RiskID,
                        principalTable: "riskAnalysis",
                        principalColumn: "RiskID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actions_users_AdvisorID",
                        column: x => x.AdvisorID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actions_AdvisorID",
                table: "actions",
                column: "AdvisorID");

            migrationBuilder.CreateIndex(
                name: "IX_actions_RiskID",
                table: "actions",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_ClassSubjectID",
                table: "attendances",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_EnrollmentID",
                table: "attendances",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_curriculums_ProgramID",
                table: "curriculums",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_curriculums_SubjectID",
                table: "curriculums",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_ClassID",
                table: "enrollments",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_MajorID",
                table: "enrollments",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_StudentID",
                table: "enrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_gradeDetails_GradeID",
                table: "gradeDetails",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_StudentID",
                table: "grades",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_grades_SubjectID",
                table: "grades",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_ReceiverID",
                table: "notifications",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_predictions_StudentID",
                table: "predictions",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_programs_MajorID",
                table: "programs",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_riskAnalysis_EnrollmentID",
                table: "riskAnalysis",
                column: "EnrollmentID");

            migrationBuilder.CreateIndex(
                name: "IX_subjectInClasses_ClassID",
                table: "subjectInClasses",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_subjectInClasses_SubjectID",
                table: "subjectInClasses",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_subjectInClasses_TeacherID",
                table: "subjectInClasses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_ProgramID",
                table: "subjects",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_PrerequisitesSubjectID",
                table: "syllabus",
                column: "PrerequisitesSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_syllabus_PrerequisiteSubjectID",
                table: "syllabus",
                column: "PrerequisiteSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_users_MajorID",
                table: "users",
                column: "MajorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actions");

            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "curriculums");

            migrationBuilder.DropTable(
                name: "gradeDetails");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "predictions");

            migrationBuilder.DropTable(
                name: "syllabus");

            migrationBuilder.DropTable(
                name: "riskAnalysis");

            migrationBuilder.DropTable(
                name: "subjectInClasses");

            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "enrollments");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "classes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "programs");

            migrationBuilder.DropTable(
                name: "majors");
        }
    }
}
